using System.Threading.Tasks;
using Lra.ImageProcessor.Service.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Lra.ImageProcessor.Service.Hosting
{
    /// <summary>
    /// Defines options that can be used to build a service host.
    /// </summary>
    public interface IServiceHostBuilder
    {
        /// <summary>
        /// Specify the logging factory to use.
        /// </summary>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <returns></returns>
        IServiceHostBuilder UseLoggerFactory(ILoggerFactory loggerFactory);

        /// <summary>
        /// Specify the configuration to use based on command line arguments
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        IServiceHostBuilder UseConfiguration(string[] args);

        /// <summary>
        /// Specify the configuration to use.
        /// </summary>
        /// <param name="configuration">The configuration instance.</param>
        /// <returns></returns>
        IServiceHostBuilder UseConfiguration(IConfiguration configuration);

        /// <summary>
        /// Specify the startup type to be used by the application host.
        /// </summary>
        /// <typeparam name="TStartup">The type of the startup.</typeparam>
        /// <returns></returns>
        IServiceHostBuilder UseStartup<TStartup>()
            where TStartup : class, IStartup;

        /// <summary>
        /// Creates an instance of <see cref="IServiceHost"/> given configuration.
        /// </summary>
        /// <returns></returns>
        Task<IServiceHost> BuildAsync();
    }
}
