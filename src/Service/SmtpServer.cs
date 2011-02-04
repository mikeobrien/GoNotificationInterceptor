using System;
using System.IO;
using System.Net.Mail;
using System.Threading;
using EricDaugherty.CSES.Net;
using EricDaugherty.CSES.SmtpServer;
using GoNotificationInterceptor.Configuration;

namespace GoNotificationInterceptor
{
    public class SmtpServer
    {
        private readonly SimpleServer _server;
        private readonly SMTPProcessor _processor;

        public SmtpServer(int port, Action<MailMessage> messageHandler)
        {
            _processor = new SMTPProcessor(
                            Environment.MachineName, 
                            new AllRecipientFilter(), 
                            new MessageSpoolProxy(messageHandler));
            _server = new SimpleServer(port, _processor.ProcessConnection);
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }

        private class MessageSpoolProxy : IMessageSpool
        {
            private readonly Action<MailMessage> _handleMessage;

            public MessageSpoolProxy(Action<MailMessage> messageHandler)
            {
                _handleMessage = messageHandler;   
            }

            public bool SpoolMessage(SMTPMessage message)
            {
                if (Manager.Current.Application.DebugMode)
                    File.WriteAllText(
                        string.Format("In.{0:yyyyMMddhhmmssfffffff}.msg", DateTime.Now),
                        message.Data);

                ThreadPool.QueueUserWorkItem(x =>
                        {
                            var netMessage = new MailMessage
                                                {
                                                    From =
                                                        new MailAddress(
                                                        message.FromAddress.Address)
                                                };

                            foreach (var address in message.ToAddresses)
                                netMessage.To.Add(new MailAddress(address.Address));

                            //foreach (var key in message.Headers.Keys.Cast<string>())
                            //    netMessage.Headers[key] = message.Headers[key].ToString();

                            if (message.Headers.ContainsKey("Subject"))
                                netMessage.Subject = message.Headers["Subject"].ToString();
                            if (message.Headers.ContainsKey("Sender"))
                                netMessage.Sender =
                                    new MailAddress(message.Headers["Sender"].ToString());
                            if (message.Headers.ContainsKey("Reply-To"))
                                netMessage.ReplyToList.Add(new MailAddress(message.Headers["Reply-To"].ToString()));

                            var data = message.Data.Split(new[] {"\r\n\r\n"}, 2,
                                                        StringSplitOptions.None);
                            if (data.Length == 2) netMessage.Body = data[1];
                            if (netMessage.Body.EndsWith("\r\n\r\n"))
                                netMessage.Body = netMessage.Body.Substring(0,
                                                                            netMessage.Body.
                                                                                Length - 4);

                            _handleMessage(netMessage);
                        });
                return true;
            }
        }

        private class AllRecipientFilter : IRecipientFilter
        {
            public bool AcceptRecipient(SMTPContext context, 
                                        EricDaugherty.CSES.Common.EmailAddress recipient)
            {
                return true;
            }
        }
    }
}
