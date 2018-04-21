using System;

namespace Lra.ImageProcessor.Service.Hosting.Abstractions
{
    public interface IHostingExceptionInformation
    {
        Exception Error { get; }
    }
}