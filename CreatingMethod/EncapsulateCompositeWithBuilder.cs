using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;


namespace PatternOrientedRefactoring
{
    public class TagBuilder
    {
        private XmlDocument doc;
        XmlNode current;
        public TagBuilder(String rootTagName)
        {
            doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement(rootTagName));
            current = doc.FirstChild;
        }

        public String toXml()
        {
            return doc.InnerXml;
        }

        public void addChild(string childTagName)
        {
            /// Create a new node.
            XmlElement element = doc.CreateElement(childTagName);
 //           element.InnerText = "";
            
            /// Add child
            current.AppendChild(element);
            current = current.LastChild;
        }

        public void addSibling(string childTagName)
        {
            /// Create a new node.
            XmlElement element = doc.CreateElement(childTagName);
            //           element.InnerText = "";

            /// Add child
            current.ParentNode.AppendChild(element);
        }

        public void addToParent(string childTagName)
        {
            /// Create a new node.
            XmlElement element = doc.CreateElement(childTagName);
            //           element.InnerText = "";

            /// Add child
            doc.FirstChild.AppendChild(element);
            current = doc.FirstChild.LastChild;
        }
    }
}
