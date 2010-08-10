using System.Xml.Linq;

namespace GoNotificationInterceptor
{
    public static class StringExtensions
    {
        public static XDocument OpenPathAsXDocument(this string path)
        {
            return XDocument.Load(path);
        }
    }
}
