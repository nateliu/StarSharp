using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace StarSharp.Core.Utility
{
    public class XmlUtils
    {
        public static void ThrowXmlError(XmlNode node)
        {
            throw new Exception("xml error!" + node.InnerXml);
        }

        public static string TryGetXmlAttribute(XmlNode node, string attrName)
        {
            if (node is XmlElement)
                return ((XmlElement)node).GetAttribute(attrName);
            return null;
        }

        public static string GetXmlAttribute(XmlNode node, string attrName)
        {
            if (node is XmlElement)
                return ((XmlElement)node).GetAttribute(attrName);
            return String.Empty;
        }

        public static void SetXmlAttribute(XmlNode node, string attrName, string value)
        {
            if (node is XmlElement)
            {
                string setXmlAttribute = "<" + attrName + " value=\"" + value + "\">" + value + "</" + attrName + ">";
                ((XmlElement)node).InnerXml = setXmlAttribute + ((XmlElement)node).InnerXml;
            }
        }

        public static bool GetXmlBooleanAttribute(XmlNode node, string attrName)
        {
            if (node is XmlElement)
            {
                return ((XmlElement)node).GetAttribute(attrName) == "1"
                    || ((XmlElement)node).GetAttribute(attrName).ToLower() == "true";
            }
            return false;
        }

        public static int GetXmlIntegerAttribute(XmlNode node, string attrName)
        {
            int result;
            if ((node is XmlElement) && (Int32.TryParse(((XmlElement)node).GetAttribute(attrName).Replace("px", ""), out result)))
                return result;
            return -1;
        }

        public static bool TestXpath(XmlNode node, string xpath)
        {
            if (xpath == null || xpath.Length == 0) return true;
            return (node.SelectSingleNode(xpath) != null);
        }   

    }
}
