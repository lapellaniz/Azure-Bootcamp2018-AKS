using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Lra.ImageProcessor.Service.Hosting.Abstractions;
using Lra.ImageProcessor.Service.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lra.ImageProcessor.Service.Hosting
{
    /// <summary>
    /// Builder used to create an instance of <see cref="IServiceHost"/>.
    /// </summary>
    public class ServiceHostBuilder : IServiceHostBuilder
    {
        private readonly IServiceCollection _services = new ServiceCollection();
        private readonly IServiceHostManualResetEvent _manualResetEvent = new ServiceHostManualResetEvent();
        private IConfiguration _configuration;
        private ILoggerFactory _loggerFactor;

        /// <inheritdoc />
        public IServiceHostBuilder UseLoggerFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactor = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            return this;
        }

        /// <inheritdoc />
        public IServiceHostBuilder UseConfiguration(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }
            string baseConfigPath = Directory.GetCurrentDirectory();
            string applicationName = Assembly.GetEntryAssembly().GetName().Name;

            _configuration = new ConfigurationBuilder()
                .SetBasePath(baseConfigPath)
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"/etc/{applicationName}/local/appsettings.json", true, true)
                .AddJsonFile($"/etc/{applicationName}/localsecrets/appsettings.secrets.json", true, true)
                .AddJsonFile($"/etc/{applicationName}/appsettings.json", true, true)
                .Build();
            _services.AddSingleton(_configuration);
            return this;
        }

        /// <inheritdoc />
        public IServiceHostBuilder UseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _services.AddSingleton(configuration);
            return this;
        }

        /// <inheritdoc />
        public IServiceHostBuilder UseStartup<TStartup>()
            where TStartup : class, IStartup
        {
            // TODO : Consider adding convention based startup defined by environment variable (dev/prod/stage)
            _services.AddSingleton<IStartup, TStartup>();
            return this;
        }

        /// <inheritdoc />
        public async Task<IServiceHost> BuildAsync()
        {
            AddCommonServices();
            IServiceProvider hostServices = _services.BuildServiceProvider();

            if (_configuration == null)
            {
                _configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
            }

            if (_loggerFactor == null)
            {
                _loggerFactor = hostServices.GetRequiredService<ILoggerFactory>();
            }

            var logger = _loggerFactor.CreateLogger<ServiceHost>();
            using (logger.BeginScope($"HostStartup > CorrelationId: ${Guid.NewGuid()}"))
            {
                var serviceHost = new ServiceHost(_manualResetEvent, logger, _services.Clone(), hostServices);

                try
                {
                    await serviceHost.InitializeAsync();
                    return serviceHost;
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "Failed to initialize service host.");
                    throw;
                }
            }
        }

        private void AddCommonServices()
        {
            var serviceHostConfig = _configuration.GetSection("ServiceHost");
            var serviceHostSettings = serviceHostConfig.Get<ServiceHostSettings>();
            if (serviceHostSettings == null)
            {
                throw new ArgumentException("Service host settings cannot be null.");
            }
                      
            _services.AddOptions()
                .AddLogging(builder =>
                {
                    builder.AddConfiguration(_configuration.GetSection("Logging"));
                    builder.AddConsole(options => { options.IncludeScopes = true; });
                })
                .Configure<ServiceHostSettings>(serviceHostConfig)
                .AddScoped(sp => sp.GetService<IOptionsSnapshot<ServiceHostSettings>>().Value)
                .AddSingleton<HostedServiceExecutor>();
        }
    }
}
