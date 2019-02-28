
using System.Threading.Tasks;

namespace D1.Model.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
