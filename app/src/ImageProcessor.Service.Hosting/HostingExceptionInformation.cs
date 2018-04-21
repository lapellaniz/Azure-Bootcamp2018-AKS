using System;
using Lra.ImageProcessor.Service.Hosting.Abstractions;

namespace Lra.ImageProcessor.Service.Hosting
{
    public class HostingExceptionInformation : IHostingExceptionInformation
    {
        public Exception Error { get; set; }
    }
}
