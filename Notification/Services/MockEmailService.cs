using Notification.Services.Abstractions;

namespace Notification.Services;

public class MockEmailService : IEmailService
{
    private readonly ILogger<MockEmailService> _logger;
    public List<EmailMessage> SentEmails { get; } = new();

    public record EmailMessage(string To, string Subject, string Body, DateTime SentAt);

    public MockEmailService(ILogger<MockEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendEmailAsync(string to, string subject, string body)
    {
        var message = new EmailMessage(to, subject, body, DateTime.UtcNow);
        SentEmails.Add(message);
        
        _logger.LogInformation(
            "Mock email sent to {To} with subject {Subject} at {SentAt}", 
            to, subject, message.SentAt);

        return Task.CompletedTask;
    }
}