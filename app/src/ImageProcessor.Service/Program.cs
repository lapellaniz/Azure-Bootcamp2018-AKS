using System;
using System.Threading.Tasks;
using Lra.ImageProcessor.Service.Hosting;

namespace lra.ImageProcessor.Service
{
    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            var host = await new ServiceHostBuilder()
                .UseConfiguration(args)
                .UseStartup<Startup>().BuildAsync();
            await host.RunAsync();
        }
    }
}
