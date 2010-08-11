using System.Net.Mail;

namespace GoNotificationInterceptor
{
    public interface IEmailService
    {
        void Send(MailMessage message);
    }
}
