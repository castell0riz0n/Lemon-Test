namespace Lemon_Test.Core.Services;

/// <summary>
/// Simple service interface for dependency injection demos
/// </summary>
public interface IEmailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}
