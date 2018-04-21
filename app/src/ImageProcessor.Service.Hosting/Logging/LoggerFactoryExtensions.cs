using System;
using Microsoft.Extensions.Logging;

namespace Lra.ImageProcessor.Service.Hosting.Logging
{
    /// <summary>
    /// Extension methods for <see cref="ILoggerFactory"/>
    /// </summary>
    public static class LoggerFactoryExtensions
    {
        /// <summary>
        /// Creates an instance of the hosting logger.
        /// </summary>
        /// <param name="loggerFactory">The logger factory instance.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">loggerFactory</exception>
        public static ILogger CreateHostingLogger(this ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            return loggerFactory.CreateLogger(LoggingConstants.HostingLoggerName);
        }
    }
}
