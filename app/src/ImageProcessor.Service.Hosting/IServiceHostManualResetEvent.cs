using System.Threading;

namespace Lra.ImageProcessor.Service.Hosting
{
    /// <summary>
    /// Defines a synchronization pattern for managing the application thread.
    /// </summary>
    public interface IServiceHostManualResetEvent
    {
        /// <summary>
        /// Gets the token used to propagate notification that operations should be canceled.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        CancellationToken Token { get; }

        /// <summary>
        /// Blocks the thread indicating the application has started.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the application thread.
        /// </summary>
        void Stop();

        /// <summary>
        /// Starts the unload process.
        /// </summary>
        void Unload();

        /// <summary>
        /// Exits the application.
        /// </summary>
        void End();
    }
}
