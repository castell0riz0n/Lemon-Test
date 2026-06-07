namespace Lemon_Test.Core.Services;

/// <summary>
/// Mock implementation for demos
/// </summary>
public class MockEmailService : IEmailService
{
    public List<EmailMessage> SentEmails { get; } = new();

    public Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        SentEmails.Add(new EmailMessage(to, subject, body, DateTime.UtcNow));
        return Task.FromResult(true);
    }
}

public record EmailMessage(string To, string Subject, string Body, DateTime SentAt);
