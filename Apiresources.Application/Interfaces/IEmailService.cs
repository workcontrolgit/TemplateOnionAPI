namespace $safeprojectname$.Interfaces
{
    // Define an interface for sending email messages.
    public interface IEmailService
    {
        // Asynchronously send an email message using the provided request object.
        Task SendAsync(EmailRequest request);
    }
}