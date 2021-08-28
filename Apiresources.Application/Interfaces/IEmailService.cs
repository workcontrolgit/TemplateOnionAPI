using $safeprojectname$.DTOs.Email;
using System.Threading.Tasks;

namespace $safeprojectname$.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}