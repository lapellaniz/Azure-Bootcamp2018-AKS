using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Lra.ImageProcessor.Service.Hosting.Abstractions
{
    /// <summary>
    /// Contract for running application startup.
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The collection of configured services.</param>
        /// <returns>Returns <see cref="IServiceProvider"/>.</returns>
        IServiceProvider ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Use this method to configure service.
        /// </summary>
        /// <param name="app">The application instance.</param>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        Task ConfigureAsync(IService app);

        /// <summary>
        /// Run application unload logic.
        /// </summary>
        /// <returns>Returns a <see cref="Task"/>.</returns>
        Task StopAsync();
    }
}
