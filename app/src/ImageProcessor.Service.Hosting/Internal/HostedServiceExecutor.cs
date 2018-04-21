using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lra.ImageProcessor.Service.Hosting.Abstractions;
using Microsoft.Extensions.Logging;

namespace Lra.ImageProcessor.Service.Hosting.Internal
{
    public class HostedServiceExecutor
    {
        private readonly IEnumerable<IHostedService> _services;
        private readonly ILogger<HostedServiceExecutor> _logger;

        public HostedServiceExecutor(ILogger<HostedServiceExecutor> logger, IEnumerable<IHostedService> services)
        {
            _logger = logger;
            _services = services ?? Enumerable.Empty<IHostedService>();
        }

        public async Task StartAsync(CancellationToken token)
        {
            await ExecuteAsync(service => service.StartAsync(token));
        }

        public async Task StopAsync(CancellationToken token)
        {
            try
            {
                await ExecuteAsync(service => service.StopAsync(token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred handling global exceptions in the application");
            }            
        }

        public async Task HandleExceptionAsync(IHostingExceptionInformation exceptionInformation,
            CancellationToken token)
        {
            try
            {
                await ExecuteAsync(service => service.HandleExceptionAsync(exceptionInformation, token));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred handling global exceptions in the application");
                throw;
            }
        }

        private async Task ExecuteAsync(Func<IHostedService, Task> callback)
        {
            List<Exception> exceptions = null;

            foreach (var service in _services)
            {
                try
                {
                    await callback(service);
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                    {
                        exceptions = new List<Exception>();
                    }

                    exceptions.Add(ex);
                }
            }

            // Throw an aggregate exception if there were any exceptions
            if (exceptions != null)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
