using System.Runtime.InteropServices;
using Lemon_Test.Core.DynamicSkipping;

namespace Lemon_Test.Core.Tests.DynamicSkipping;

public class EmailServiceTests
{
    private readonly EmailService _emailService = new();

    //[Fact (Skip = "Feature under dev")]
    [Fact]
    public void SendEmail_WithSmtpConfiguration_SendsSuccessfully()
    {
        // Skip this test if SMTP is not configured
        if (!_emailService.IsSmtpConfigured())
        {
            Assert.Skip("SMTP server not configured. Set SMTP_HOST and SMTP_PORT environment variables.");
        }

        var result = _emailService.SendEmail("test@example.com", "Test Subject", "Test Body");

        Assert.True(result);
    }

    [Fact]
    public void SendEmail_OnWindowsOnly_WorksCorrectly()
    {
        // Skip if not running on Windows
        Assert.SkipUnless(RuntimeInformation.IsOSPlatform(OSPlatform.Windows),
            "Only running on Windows environment.");

        var result = _emailService.SendEmail("windows@example.com", "Windows Test", "Body");

        Assert.True(result);
    }

    [Fact]
    public void SendEmail_SkipOnCI_RunsOnlyLocally()
    {
        // Skip if running in CI environment
        var isCI = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"));
        Assert.SkipWhen(isCI, "Email tests are skipped in CI environment.");

        var result = _emailService.SendEmail("local@example.com", "Local Test", "Body");

        Assert.True(result);
    }
}

public class ExternalApiTests
{
    [Fact]
    public async Task GetData_WhenApiAvailable_ReturnsData()
    {
        // Skip if external API is not available

        var client = new ExternalApiClient();
        var result = await client.GetDataAsync();

        Assert.NotNull(result);
        Assert.Contains("API Response", result);
    }

    [Fact]
    public async Task GetData_InDevelopmentEnvironment_UsesTestData()
    {
        // Only run in development environment

        var client = new ExternalApiClient();
        var result = await client.GetDataAsync();

        Assert.NotNull(result);
    }
}

public class PremiumServiceTests
{
    [Fact]
    public void ExecutePremiumFeature_WithLicense_WorksCorrectly()
    {
        // Skip if premium license is not available

        var service = new PremiumService();
        var result = service.ExecutePremiumFeature();

        Assert.Equal("Premium feature executed successfully", result);
    }

    [Fact]
    public void ExecutePremiumFeature_SkipOnLinux_WindowsOnlyFeature()
    {
        // Skip on Linux - this is a Windows-only premium feature

        var service = new PremiumService();
        var result = service.ExecutePremiumFeature();

        Assert.NotNull(result);
    }
}
