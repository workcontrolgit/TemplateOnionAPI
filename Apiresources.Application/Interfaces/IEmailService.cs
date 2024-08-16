namespace $safeprojectname$.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}