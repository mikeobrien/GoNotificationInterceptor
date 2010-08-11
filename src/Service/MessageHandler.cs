using System.Net.Mail;
using System.Xml.Linq;
using GoNotificationInterceptor.Configuration;

namespace GoNotificationInterceptor
{
    public class MessageHandler
    {
        private IEmailService _emailService;
        private string _rootNodeName;
        private string _subjectRegex;
        private string _bodyRegex;
        private string _subjectStylesheetPath;
        private string _bodyStylesheetPath;

        public MessageHandler(IEmailService emailService,
                              string rootNodeName,
                              string subjectRegex,
                              string bodyRegex,
                              string subjectStylesheetPath,
                              string bodyStylesheetPath)
        {
            _emailService = emailService;
            _rootNodeName = rootNodeName;
            _subjectRegex = subjectRegex;
            _bodyRegex = bodyRegex;
            _subjectStylesheetPath = subjectStylesheetPath;
            _bodyStylesheetPath = bodyStylesheetPath;
        }

        public void HandleMessage(MailMessage message)
        {
            XDocument document;
            if (message.TryParseToXml(_rootNodeName, _subjectRegex, _bodyRegex, out document))
            {
                message.Subject = document.Transform(_subjectStylesheetPath);
                message.Body = document.Transform(_bodyStylesheetPath);
                message.IsBodyHtml = _bodyStylesheetPath.OpenPathAsXDocument().IsHtmlStylesheet();                
            }
            _emailService.Send(message);
        }
    }
}
