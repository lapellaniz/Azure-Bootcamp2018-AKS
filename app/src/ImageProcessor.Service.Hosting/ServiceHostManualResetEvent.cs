using System.Threading;

namespace Lra.ImageProcessor.Service.Hosting
{
    /// <inheritdoc />
    /// <summary>
    /// Manages the thread for the application (service) and program (host).
    /// </summary>
    /// <seealso cref="IServiceHostManualResetEvent" />
    public class ServiceHostManualResetEvent : IServiceHostManualResetEvent
    {
        private readonly ManualResetEventSlim _hostTerminationEvent = new ManualResetEventSlim();
        private readonly ManualResetEventSlim _applicationEvent = new ManualResetEventSlim();
        private readonly CancellationTokenSource _cancelSource = new CancellationTokenSource();

        /// <inheritdoc />
        public CancellationToken Token => _cancelSource.Token;

        /// <inheritdoc />
        public void Start()
        {
            _applicationEvent.Wait();
        }

        /// <inheritdoc />
        public void Stop()
        {
            _cancelSource.Cancel();
            _applicationEvent.Set();
        }

        /// <inheritdoc />
        public void Unload()
        {
            _hostTerminationEvent.Wait();
        }

        /// <inheritdoc />
        public void End()
        {
            // Sleep for 5 seconds. This gives the unload method enough time to end gracefully
            Thread.Sleep(5000);
            _hostTerminationEvent.Set();
        }
    }
}
