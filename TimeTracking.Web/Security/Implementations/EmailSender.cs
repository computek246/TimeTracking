using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using TimeTracking.Web.Data;

namespace TimeTracking.Web.Security.Implementations
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _options;

        public EmailSender(IOptions<EmailSettings> options)
        {
            _options = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await Task.FromResult(true);
        }
    }
}