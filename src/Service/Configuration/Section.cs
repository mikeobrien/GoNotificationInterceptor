using System.Configuration;

namespace GoNotificationInterceptor.Configuration
{
    public class Section : ConfigurationSection
    {
        private const string ListenPortAttribute = "listenPort";
        private const string SmtpServerAttribute = "smtpServer";
        private const string SmtpPortAttribute = "smtpPort";
        private const string RootNodeNameAttribute = "rootNodeName";
        private const string SubjectRegexAttribute = "subjectRegex";
        private const string BodyRegexAttribute = "bodyRegex";
        private const string SubjectStylesheetPathAttribute = "subjectStylesheetPath";
        private const string BodyStylesheetPathAttribute = "bodyStylesheetPath";

        [ConfigurationProperty(ListenPortAttribute)]
        public int ListenPort
        {
            get { return (int)this[ListenPortAttribute]; }
        }

        [ConfigurationProperty(SmtpServerAttribute)]
        public string SmtpServer
        {
            get { return (string)this[SmtpServerAttribute]; }
        }

        [ConfigurationProperty(SmtpPortAttribute)]
        public int SmtpPort
        {
            get { return (int)this[SmtpPortAttribute]; }
        }

        [ConfigurationProperty(RootNodeNameAttribute)]
        public string RootNodeName
        {
            get { return (string)this[RootNodeNameAttribute]; }
        }

        [ConfigurationProperty(SubjectRegexAttribute)]
        public string SubjectRegex
        {
            get { return (string)this[SubjectRegexAttribute]; }
        }

        [ConfigurationProperty(BodyRegexAttribute)]
        public string BodyRegex
        {
            get { return (string)this[BodyRegexAttribute]; }
        }

        [ConfigurationProperty(SubjectStylesheetPathAttribute)]
        public string SubjectStylesheetPath
        {
            get { return (string)this[SubjectStylesheetPathAttribute]; }
        }

        [ConfigurationProperty(BodyStylesheetPathAttribute)]
        public string BodyStylesheetPath
        {
            get { return (string)this[BodyStylesheetPathAttribute]; }
        }
    }
}


