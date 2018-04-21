using System;
using System.Threading;

namespace Lra.ImageProcessor.Service.Hosting.Abstractions
{
    /// <summary>
    /// Defines a service.
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Gets the application services injected into DI.
        /// </summary>
        /// <value>
        /// The application services.
        /// </value>
        IServiceProvider ApplicationServices { get; }

        /// <summary>
        /// Gets the token used to propagate notification that operations should be canceled.
        /// </summary>
        /// <value>
        /// The cancellation token.
        /// </value>
        CancellationToken CancellationToken { get; }
    }
}
