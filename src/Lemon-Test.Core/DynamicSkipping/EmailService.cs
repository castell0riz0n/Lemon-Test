namespace Lemon_Test.Core.DynamicSkipping;

/// <summary>
/// Email service that demonstrates environment-dependent functionality
/// </summary>
public class EmailService
{
    public bool SendEmail(string to, string subject, string body)
    {
        // In real implementation, this would send actual emails
        // For demo purposes, we'll simulate success
        Console.WriteLine($"Sending email to {to}: {subject}");
        return true;
    }

    public bool IsSmtpConfigured()
    {
        // Check if SMTP configuration is available
        var smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST");
        var smtpPort = Environment.GetEnvironmentVariable("SMTP_PORT");
        
        return !string.IsNullOrEmpty(smtpHost) && !string.IsNullOrEmpty(smtpPort);
    }
}

/// <summary>
/// External API client that may not always be available
/// </summary>
public class ExternalApiClient
{
    public async Task<string> GetDataAsync()
    {
        // Simulate API call
        await Task.Delay(100);
        return "API Response Data";
    }

    public static bool IsApiAvailable()
    {
        // Check if external API is reachable
        var apiKey = Environment.GetEnvironmentVariable("EXTERNAL_API_KEY");
        return !string.IsNullOrEmpty(apiKey);
    }
}

/// <summary>
/// Service that requires premium features
/// </summary>
public class PremiumService
{
    public string ExecutePremiumFeature()
    {
        return "Premium feature executed successfully";
    }

    public static bool HasPremiumLicense()
    {
        var licenseKey = Environment.GetEnvironmentVariable("PREMIUM_LICENSE");
        return !string.IsNullOrEmpty(licenseKey);
    }
}
