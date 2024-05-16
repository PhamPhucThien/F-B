namespace FooDrink.API.Configuration
{
    public class ConnectionStrings
    {
        public string? DefaultConnection { get; set; }
    }

    public class Logging
    {
        public LogLevel? LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string? Default { get; set; }
        public string? MicrosoftAspNetCore { get; set; }
        public string? MicrosoftHostingLifetime { get; set; }
    }

    public class ApppSettingConfig
    {
        public Logging? Logging { get; set; }
        public ConnectionStrings? ConnectionStrings { get; set; }
        public string? Domain { get; set; }
        public string? AllowedHosts { get; set; }
    }


}
