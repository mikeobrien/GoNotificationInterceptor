using System.Net.Mail;
using GoNotificationInterceptor.Configuration;
using NUnit.Framework;
using GoNotificationInterceptor;
using System.Reflection;
using Rhino.Mocks;
using Tests.Extensions;

namespace Tests
{
    [TestFixture]
    public class NotificationTest
    {
        private const string To = "me@mikeobrien.net";
        private const string From = "build@mikeobrien.net";
        private string Subject = Assembly.GetExecutingAssembly().GetResourceString("Subject.txt");
        private string Body = Assembly.GetExecutingAssembly().GetResourceString("Body.txt");

        private string SubjectStylesheetPath = Assembly.GetExecutingAssembly().SaveResourceAsTempFile("Subject.xslt");
        private string BodyStylesheetPath = Assembly.GetExecutingAssembly().SaveResourceAsTempFile("Body.xslt");
        private Section _application = Manager.Current.Application;

        [Test]
        public void SendMessage()
        {
            var emailService = MockRepository.GenerateStub<IEmailService>();
            emailService.Stub(x => x.Send(null));

            var handler = new MessageHandler(
                emailService,
                _application.RootNodeName,
                _application.SubjectRegex,
                _application.BodyRegex,
                SubjectStylesheetPath,
                BodyStylesheetPath);

            var message = new MailMessage(From, To, Subject, Body);
            var originalSubject = message.Subject;
            var originalBody = message.Body;

            handler.HandleMessage(message);

            emailService.AssertWasCalled(x => x.Send(message));

            Assert.NotNull(message.Subject);
            Assert.NotNull(message.Body);

            Assert.Greater(message.Subject.Length, 0);
            Assert.Greater(message.Body.Length, 0);

            Assert.AreNotEqual(originalSubject, message.Subject);
            Assert.AreNotEqual(originalBody, message.Body);
        }
    }
}
