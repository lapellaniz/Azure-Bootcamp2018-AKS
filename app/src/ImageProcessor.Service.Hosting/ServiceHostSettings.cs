namespace Lra.ImageProcessor.Service.Hosting
{
    /// <summary>
    /// Represents settings for the service host.
    /// </summary>
    public class ServiceHostSettings
    {
        /// <summary>
        /// Gets or set the environment name.
        /// </summary>
        /// <returns></returns>
        public string Environment { get; set; }

        /// <summary>
        /// Gets or sets the application name.
        /// </summary>
        /// <returns></returns>
        public string ApplicationName { get; set; }
    }
}
