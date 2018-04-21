using System;
using System.Threading.Tasks;
using Lra.ImageProcessor.Service.Hosting;
using Lra.ImageProcessor.Service.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace lra.ImageProcessor.Service
{
    internal class Startup : IStartup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHostedService, ImageProcessorService>();
            return services.BuildServiceProvider();
        }

        public Task ConfigureAsync(IService app)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
            var hostSettingsAccessor = app.ApplicationServices.GetRequiredService<IOptions<ServiceHostSettings>>();
            var hostSettingsMonitor = app.ApplicationServices.GetRequiredService<IOptionsMonitor<ServiceHostSettings>>();
            var hostSettings = hostSettingsAccessor.Value;
            logger.LogInformation($"Environment: {hostSettings.Environment}");

            hostSettingsMonitor.OnChange(vals => {
                logger.LogTrace($"Config changed: {string.Join(", ", vals)}");
            });

            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
