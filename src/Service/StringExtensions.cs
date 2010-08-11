using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace GoNotificationInterceptor
{
    public static class StringExtensions
    {
        public static XDocument OpenPathAsXDocument(this string path)
        {
            return XDocument.Load(path);
        }

        public static Dictionary<string, string> ExtractGroups(this string text, string regexPattern)
        {
            var groups = new Dictionary<string, string>();
            ExtractGroups(text, groups, regexPattern);
            return groups;
        }
        
        public static void ExtractGroups(this string text, Dictionary<string, string> groups, string regexPattern)
        {
            var regex = new Regex(regexPattern, RegexOptions.Multiline);
            var match = regex.Match(text);
            var index = 0;
            foreach (var group in match.Groups.OfType<Group>())
                if (index++ > 0) groups.Add(regex.GroupNameFromNumber(index - 1), group.Value);
        }
    }
}
