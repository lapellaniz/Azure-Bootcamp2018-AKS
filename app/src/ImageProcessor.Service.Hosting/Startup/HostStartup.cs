using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lra.ImageProcessor.Service.Hosting.Abstractions;
using Lra.ImageProcessor.Service.Hosting.Logging;
using Microsoft.Extensions.Logging;

namespace Lra.ImageProcessor.Service.Hosting.Startup
{
    /// <summary>
    /// Represents the startup process for the host.
    /// Abstracts hosting specific logic from the service such as handling signals.
    /// </summary>
    public class HostStartup
    {
        /// <summary>
        /// Runs the application.
        /// </summary>
        /// <typeparam name="TStartup">The type of the startup.</typeparam>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task RunAsync<TStartup>(string[] args)
            where TStartup : class, IStartup
        {
            ILogger logger = null;
            try
            {
                ILoggerFactory loggerFactory = new LoggerFactory();
                loggerFactory.AddConsole(LogLevel.Trace, true);

                // TODO : this logger should use the service name. that data is in configuration and hasn't been loaded yet...
                logger = loggerFactory.CreateHostingLogger();
                var hostingCorrelationId = Guid.NewGuid().ToString();
                using (logger.BeginScope($"Hosting Context: {hostingCorrelationId}"))
                {
                    if (args.Any())
                    {
                        // TODO : hide secrets from log.
                        var argMessage = args.Aggregate(
                            new StringBuilder(),
                            (builder, current) =>
                        {
                            builder.Append($"{current} ");
                            return builder;
                        }, builder => builder.ToString());
                        logger.LogTrace($"Using the following command line arguments for configuration: [{argMessage}]...");
                    }

                    var host = await new ServiceHostBuilder()
                        .UseLoggerFactory(loggerFactory)
                        .UseConfiguration(args)
                        .UseStartup<TStartup>()
                        .BuildAsync()
                        .ConfigureAwait(false);
                    await host.RunAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (logger != null)
                    {
                        logger.LogCritical(ex, "A critical error occurred starting the host.");
                    }
                    else
                    {
                        Console.WriteLine($"A critical error occurred starting the host. {ex}.");
                    }
                }
                catch (Exception innerEx)
                {
                    Console.WriteLine($"A critical error occurred logging host exceptions. Main exception: {ex}. Inner exception: {innerEx}");
                }

                // TODO : review shutdown delay
                // this is added here in order to let the logger catchup. THe MS console logger internally uses a queue and logs can get missed if the console shuts down too early.
                await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);

                // TODO : should this be bubbled up and let the console do another try/catch and set exit code? Consider returning ExitCode from Run method?
                Environment.ExitCode = 1;
            }
        }
    }
}
