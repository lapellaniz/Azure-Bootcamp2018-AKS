using System;
using System.Threading;
using Lra.ImageProcessor.Service.Hosting.Abstractions;

namespace Lra.ImageProcessor.Service.Hosting
{
    /// <summary>
    /// Represents an instance of a hosted service.
    /// </summary>
    /// <seealso cref="IService" />
    public class Service : IService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Service" /> class.
        /// </summary>
        /// <param name="applicationServices">The application services.</param>
        /// <param name="cancelToken">The cancellation token used to cancel service operations.</param>
        /// <exception cref="ArgumentNullException">applicationServices</exception>
        public Service(IServiceProvider applicationServices, CancellationToken cancelToken = default(CancellationToken))
        {
            if (cancelToken == null)
            {
                throw new ArgumentNullException(nameof(cancelToken));
            }

            ApplicationServices = applicationServices ?? throw new ArgumentNullException(nameof(applicationServices));
            CancellationToken = cancelToken;
        }

        /// <inheritdoc />
        public IServiceProvider ApplicationServices { get; }

        /// <inheritdoc />
        public CancellationToken CancellationToken { get; }
    }
}
