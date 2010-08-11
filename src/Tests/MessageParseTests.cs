using System.Net.Mail;
using System.Reflection;
using System.Xml.Linq;
using GoNotificationInterceptor;
using GoNotificationInterceptor.Configuration;
using NUnit.Framework;
using Tests.Extensions;

namespace Tests
{
    [TestFixture]
    public class MessageParseTests
    {
        private string Subject = Assembly.GetExecutingAssembly().GetResourceString("Subject.txt");
        private string Body = Assembly.GetExecutingAssembly().GetResourceString("Body.txt");

        private string SubjectStylesheetPath = Assembly.GetExecutingAssembly().SaveResourceAsTempFile("Subject.xslt");
        private string BodyStylesheetPath = Assembly.GetExecutingAssembly().SaveResourceAsTempFile("Body.xslt");
        private Section _application = Manager.Current.Application;

        [Test]
        public void SubjectParseTest()
        {
            var subject = Assembly.GetExecutingAssembly().GetResourceString("Subject.txt");
            var application = Manager.Current.Application;
            var values = subject.ExtractGroups(application.SubjectRegex);

            Assert.IsTrue(values.ContainsKey("pipeline"));
            Assert.AreEqual(values["pipeline"], "ProjectEuler.NET");

            Assert.IsTrue(values.ContainsKey("label"));
            Assert.AreEqual(values["label"], "21");

            Assert.IsTrue(values.ContainsKey("stage"));
            Assert.AreEqual(values["stage"], "CI");

            Assert.IsTrue(values.ContainsKey("job"));
            Assert.AreEqual(values["job"], "1");

            Assert.IsTrue(values.ContainsKey("status"));
            Assert.AreEqual(values["status"], "passed");
        }

        [Test]
        public void BodyParseTest()
        {
            var subject = Assembly.GetExecutingAssembly().GetResourceString("Body.txt");
            var application = Manager.Current.Application;
            var values = subject.ExtractGroups(application.BodyRegex);

            Assert.IsTrue(values.ContainsKey("detailsUrl"));
            Assert.AreEqual(values["detailsUrl"], "http://192.168.1.101:8153/cruise/pipelines/ProjectEuler.NET/21/CI/1");

            Assert.IsTrue(values.ContainsKey("vcs"));
            Assert.AreEqual(values["vcs"], @"Git: git://github.com/mikeobrien/ProjectEuler.NET.git
revision: af55e5b718374be78320019c7cc62d08ec3f51b7, modified by Mike O'Brien <mob@mikeobrien.net> on 2010-08-09 19:46:26.0
Test
modified src/ProjectEuler.UI/Web.config");
        }

        [Test]
        public void MessageToXmlTest()
        {
            XDocument document;
            var message = new MailMessage()
            {
                Subject = Subject,
                Body = Body
            };

            Assert.IsTrue(message.TryParseToXml(
                _application.RootNodeName,
                _application.SubjectRegex,
                _application.BodyRegex,
                out document));

            Assert.AreEqual(@"<go>
  <pipeline><![CDATA[ProjectEuler.NET]]></pipeline>
  <label><![CDATA[21]]></label>
  <stage><![CDATA[CI]]></stage>
  <job><![CDATA[1]]></job>
  <status><![CDATA[passed]]></status>
  <detailsUrl><![CDATA[http://192.168.1.101:8153/cruise/pipelines/ProjectEuler.NET/21/CI/1]]></detailsUrl>
  <vcs><![CDATA[Git: git://github.com/mikeobrien/ProjectEuler.NET.git
revision: af55e5b718374be78320019c7cc62d08ec3f51b7, modified by Mike O'Brien <mob@mikeobrien.net> on 2010-08-09 19:46:26.0
Test
modified src/ProjectEuler.UI/Web.config]]></vcs>
</go>", document.ToString());
        }
    }
}
