using Interface.Service_Interfaces;
using System.Threading.Tasks;

namespace Services.IdentityServices
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
