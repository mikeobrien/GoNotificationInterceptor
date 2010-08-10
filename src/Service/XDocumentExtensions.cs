using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace GoNotificationInterceptor
{
    public static class XDocumentExtensions
    {
        public static string Transform(this XDocument document, string path, params KeyValuePair<string, string>[] parameters)
        {
            using (XmlReader stylesheet = XmlReader.Create(path))
            {
                XslCompiledTransform xslTransformer = new XslCompiledTransform();
                xslTransformer.Load(stylesheet);

                XsltArgumentList arguments = null;
                if (parameters.Length > 0)
                {
                    arguments = new XsltArgumentList();
                    foreach (KeyValuePair<string, string> parameter in parameters)
                        arguments.AddParam(parameter.Key, string.Empty, parameter.Value);
                }

                MemoryStream documentStream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(new StreamWriter(documentStream));

                xslTransformer.Transform(document.CreateReader(), arguments, writer);

                documentStream.Position = 0;
                return new StreamReader(documentStream).ReadToEnd();
            }
        }

        public static bool IsHtmlStylesheet(this XDocument document)
        {
            var manager = new XmlNamespaceManager(new NameTable());
            manager.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");

            var element = document.XPathSelectElement("xsl:stylesheet/xsl:output", manager);

            return element != null && element.Attribute("method") != null ? 
                       element.Attribute("method").Value.Equals("html", System.StringComparison.OrdinalIgnoreCase) : false;
        }
    }
}
