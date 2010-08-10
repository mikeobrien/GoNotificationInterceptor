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
            LoadValues(values, message.Subject, subjectRegex);
            LoadValues(values, message.Body, bodyRegex);
            document = new XDocument(new XElement(rootElementName, 
                                     from value in values
                                     select new XElement(value.Key, new XCData(value.Value))));
            return values.Count > 0;
        }

        private static void LoadValues(Dictionary<string, string> values, string text, string regexPattern)
        {
            var regex = new Regex(regexPattern, RegexOptions.Multiline);
            var match = regex.Match(text);
            int index = 0;
            if (match != null)
                foreach (var group in match.Groups.OfType<Group>())
                    if (index++ > 0) values.Add(regex.GroupNameFromNumber(index - 1), group.Value);
        }
    }
}
