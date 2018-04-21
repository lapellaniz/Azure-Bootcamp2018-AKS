using System;
using System.IO;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Lra.ImageProcessor.Service.Hosting.Abstractions;
using Lra.ImageProcessor.Service.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lra.ImageProcessor.Service.Hosting
{
    /// <summary>
    /// Represent a host for a reliable capability service.
    /// </summary>
    /// <seealso cref="IServiceHost" />
    internal class ServiceHost : IServiceHost
    {
        private readonly IServiceProvider _hostingServiceProvider;
        private readonly IServiceCollection _appServicesCollection;
        private readonly IServiceHostManualResetEvent _manualResetEvent;
        private readonly ILogger _logger;
        private IServiceProvider _applicationServices;
        private HostedServiceExecutor _hostedServiceExecutor;
        private IStartup _startup;
        private IService _app;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHost" /> class.
        /// </summary>
        /// <param name="manualResetEvent">Manages the application and program thread.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="appServices">The application services.</param>
        /// <param name="hostingServiceProvider">The hosting service provider.</param>
        /// <exception cref="ArgumentNullException">logger
        /// or
        /// manualResetEvent</exception>
        public ServiceHost(
            IServiceHostManualResetEvent manualResetEvent,
            ILogger logger,
            IServiceCollection appServices,
            IServiceProvider hostingServiceProvider)
        {
            _hostingServiceProvider = hostingServiceProvider ?? throw new ArgumentNullException(nameof(hostingServiceProvider));
            _appServicesCollection = appServices ?? throw new ArgumentNullException(nameof(appServices));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _manualResetEvent = manualResetEvent ?? throw new ArgumentNullException(nameof(manualResetEvent));
        }

        public async Task InitializeAsync()
        {
            using (_logger.BeginScope("Startup"))
            {
                _logger.LogTrace("Initializing application startup...");

                EnsureStartup();

                // init application  services
                _applicationServices = _startup.ConfigureServices(_appServicesCollection);

                // configure the application (pipeline)
                _app = new Service(_applicationServices, _manualResetEvent.Token);
                _logger.LogTrace("Configuring application...");
                await _startup.ConfigureAsync(_app).ConfigureAwait(false);

                _logger.LogTrace("Finished application startup...");
            }
        }

        /// <inheritdoc />
        public async Task RunAsync()
        {
            bool shouldCallServiceStopOnException = false;
            try
            {
                using (_logger.BeginScope("HostRun"))
                {
                    _hostedServiceExecutor = _applicationServices.GetRequiredService<HostedServiceExecutor>();
                    UseCancelKeyPress();
                    UseSafeUnloading();
                    UseExceptionHandler();

                    shouldCallServiceStopOnException = true;
                    await _hostedServiceExecutor.StartAsync(CancellationToken.None);

                    var path = Path.Combine(Environment.CurrentDirectory, "Status.txt");
                    _logger.LogTrace($"Status log path is ${path}.");
                    File.WriteAllText(path, "OK");

                    // This blocks the thread

                    _logger.LogInformation("Service is running. Press ctrl + C to stop process...");
                    _manualResetEvent.Start();

                    await _hostedServiceExecutor.StopAsync(CancellationToken.None);
                    shouldCallServiceStopOnException = false;

                    // TEST (REMOVE)
                    if (_app != null)
                    {
                        // Show config changed but only in scope.
                        using (var scope = _app.ApplicationServices.CreateScope())
                        {
                            var settings = scope.ServiceProvider.GetService<ServiceHostSettings>();
                            _logger.LogTrace($"Host environment setting during exit is: {settings.Environment}");
                        }
                    }

                    // This allows the thread to unload gracefully
                    _logger.LogInformation("Received signal gracefully shutting down...");
                    _manualResetEvent.End();
                }
            }
            catch (Exception ex)
            {
                if (shouldCallServiceStopOnException)
                {
                    await _hostedServiceExecutor.StopAsync(CancellationToken.None);
                }
                Console.Error.WriteLine($"Critical error occurred running service host. Details: ${ex}");                
                _logger.LogCritical(ex, "Critical error occurred running service host.");
                await Console.Error.FlushAsync();
            }
            finally
            {
                UnRegisterCancelKeyPress();
                UnRegisterSafeUnloading();
                UnRegisterExceptionHandler();
            }
        }

        private void EnsureStartup()
        {
            if (_startup != null)
            {
                return;
            }

            _startup = _hostingServiceProvider.GetService<IStartup>();

            if (_startup == null)
            {
                throw new InvalidOperationException($"No startup configured. Please specify startup via ServiceHostBuilder.UseStartup.");
            }
        }

        /// <summary>
        /// Register the cancel key event
        /// </summary>
        private void UseCancelKeyPress()
        {
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;
        }

        /// <summary>
        /// Cleanup cancel key press event handler.
        /// </summary>
        private void UnRegisterCancelKeyPress()
        {
            Console.CancelKeyPress -= ConsoleOnCancelKeyPress;
        }

        private void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs eventArgs)
        {
            _logger.LogTrace("Cancel key has been signaled. Exiting the application.");
            _manualResetEvent.Stop();

            // Don't terminate the process immediately, wait for the Main thread to exit gracefully.
            eventArgs.Cancel = true;
        }

        /// <summary>
        /// Handles the assembly unload event
        /// </summary>
        private void UseSafeUnloading()
        {
            AssemblyLoadContext.Default.Unloading += OnUnloading;
        }

        private void OnUnloading(AssemblyLoadContext ctx)
        {
            _logger.LogTrace("Assembly is being unloaded.");
            _manualResetEvent.Stop();

            _logger.LogTrace("Waiting for the application to unload before terminating.");
            _manualResetEvent.Unload();
        }

        private void UnRegisterSafeUnloading()
        {
            AssemblyLoadContext.Default.Unloading -= OnUnloading;
        }

        /// <summary>
        /// Handles unhandled exceptions
        /// </summary>
        private void UseExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs eventArgs)
        {
            Exception ex = eventArgs.Exception;
            _hostedServiceExecutor.HandleExceptionAsync(new HostingExceptionInformation { Error = ex }, _manualResetEvent.Token).GetAwaiter().GetResult();
            _logger.LogError(ex, $"Unhandled exception occurred. {eventArgs.Exception.Message}");
            eventArgs.SetObserved();
            _manualResetEvent.Stop();
        }

        private void OnUnhandledException(object o, UnhandledExceptionEventArgs eventArgs)
        {
            Exception ex = (Exception)eventArgs.ExceptionObject;
            _hostedServiceExecutor.HandleExceptionAsync(new HostingExceptionInformation { Error = ex }, _manualResetEvent.Token).GetAwaiter().GetResult();
            _logger.LogError(ex, "Unhandled exception occurred.");
            _manualResetEvent.Stop();
        }

        private void UnRegisterExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException -= OnUnhandledException;
            TaskScheduler.UnobservedTaskException -= OnUnobservedTaskException;
        }
    }
}
