namespace Lemon_Test.Core.MatrixTheoryData;

public class Configuration
{
    public bool IsHttpsEnabled { get; set; }
    public string Environment { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; }

    public Configuration(bool isHttpsEnabled, string environment, int timeoutSeconds)
    {
        IsHttpsEnabled = isHttpsEnabled;
        Environment = environment;
        TimeoutSeconds = timeoutSeconds;
    }

    public bool IsValid()
    {
        // Production must use HTTPS
        if (Environment == "Production" && !IsHttpsEnabled)
            return false;

        // Timeout must be positive
        if (TimeoutSeconds <= 0)
            return false;

        return true;
    }
}
