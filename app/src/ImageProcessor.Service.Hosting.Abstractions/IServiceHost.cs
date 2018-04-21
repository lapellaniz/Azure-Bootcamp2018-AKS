using System.Threading.Tasks;

namespace Lra.ImageProcessor.Service.Hosting.Abstractions
{
    /// <summary>
    /// Defines operations against a hosted service.
    /// </summary>
    public interface IServiceHost
    {
        /// <summary>
        /// Runs the application and blocks the thread.
        /// </summary>
        Task RunAsync();
    }
}
