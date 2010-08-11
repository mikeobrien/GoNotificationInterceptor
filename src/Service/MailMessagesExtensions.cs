using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GoNotificationInterceptor
{
    public static class MailMessagesExtensions
    {
        public static bool TryParseToXml(this MailMessage message, string rootElementName, string subjectRegex, string bodyRegex, out XDocument document)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            message.Subject.ExtractGroups(values, subjectRegex);
            message.Body.ExtractGroups(values, bodyRegex);
            document = new XDocument(new XElement(rootElementName, 
                                     from value in values
                                     select new XElement(value.Key, new XCData(value.Value))));
            return values.Count > 0;
        }
    }
}
