using System;
using System.IO;
using System.Net.Mail;
using GoNotificationInterceptor.Configuration;

namespace GoNotificationInterceptor
{
    public class EmailService : IEmailService
    {
        private readonly string _server;
        private readonly int _port;

        public EmailService(string server)
        {
            _server = server;
            _port = 25;
        }

        public EmailService(string server, int port)
        {
            _server = server;
            _port = port;
        }

        public void Send(MailMessage message)
        {
            if (Manager.Current.Application.DebugMode)
                File.WriteAllText(
                    string.Format("Out.{0:yyyyMMddhhmmssfffffff}.msg", DateTime.Now),
                    string.Format("From: {0}\r\nTo: {1}\r\nSubject: {2}\r\n\r\n{3}", 
                                  message.From, message.To, message.Subject, message.Body));

            var smtpClient = new SmtpClient(_server, _port);
            smtpClient.Send(message);
        }
    }
}
