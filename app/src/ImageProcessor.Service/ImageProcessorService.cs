using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lra.ImageProcessor.Service.Hosting;
using Lra.ImageProcessor.Service.Hosting.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace lra.ImageProcessor.Service
{
    /// <summary>
    /// Custom implementation.
    /// </summary>
    /// <seealso cref="IHostedService" />
    public class ImageProcessorService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly ServiceHostSettings _serviceHostOptions;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="logger">Logger</param>
        public ImageProcessorService(ILogger<ImageProcessorService> logger, IOptions<ServiceHostSettings> serviceHostOptionsAccessor)
        {
            _serviceHostOptions = serviceHostOptionsAccessor.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (_serviceHostOptions.Environment == "Error")
            {
                throw new ArgumentException("Environment is not supported");
            }

            string hostName = Dns.GetHostName();
            _logger.LogInformation($"HostName: {hostName}");
            string fqdn = (await Dns.GetHostEntryAsync(hostName)).HostName;
            _logger.LogInformation($"FullyQualifiedDomainName: {fqdn}");
            _logger.LogInformation("Application start called.");
        }

        /// <inheritdoc />
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Application stop called.");
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task HandleExceptionAsync(IHostingExceptionInformation exceptionInformation, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Application exception handler called.");
            if(exceptionInformation?.Error != null)
            {
                _logger.LogError(exceptionInformation.Error, "A critical error has occurred.");
            }
            return Task.CompletedTask;
        }
    }
}
