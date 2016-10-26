using System.Collections.Generic;
using System.Xml;

namespace Comm.Tools.Utility
{
    public static class XmlHelper
    {
        public static XmlDocument ToXmlDocument(this string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }

        public static T GetNodeText<T>(this XmlDocument doc, string name, T defaultValue)
        {
            try
            {
                return doc.GetElementsByTagName(name)[0].InnerText.Trim().ConvertTo(defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }


        public static Dictionary<string, string> ToDictionary(this XmlDocument xml, string name)
        {
            try
            {
                var result = new Dictionary<string, string>();
                var xnls = xml.GetElementsByTagName(name)[0];
                foreach (XmlNode xn in xnls)
                {
                    result[xn.Name] = xn.InnerText;
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static Dictionary<string, string> ToDictionary(this XmlNode node)
        {
            try
            {
                var result = new Dictionary<string, string>();
                foreach (XmlNode xn in node)
                {
                    result[xn.Name] = xn.InnerText;
                }
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}