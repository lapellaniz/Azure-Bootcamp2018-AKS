using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lra.ImageProcessor.Service.Hosting.Abstractions
{
    public interface IHostedService
    {
        /// <summary>
        /// Triggered when the application host has fully started.
        /// </summary>
        Task StartAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        Task StopAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Triggered when the application host is trying tohandle unobserved exceptions.
        /// </summary>
        /// <param name="exceptionInformation">The exception information.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task HandleExceptionAsync(IHostingExceptionInformation exceptionInformation, CancellationToken cancellationToken);
    }
}
