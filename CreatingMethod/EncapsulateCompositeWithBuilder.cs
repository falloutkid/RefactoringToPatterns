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

        public void addAttribute(string p1, string p2)
        {
            XmlAttribute attr = doc.CreateAttribute(p1);
            attr.Value = p2;
            current.Attributes.Append(attr);
        }

        public void addToParent(string parent, string child)
        {
            XmlNode search_node = findParent(parent);
            if (search_node == null)
                return;
            current = search_node;
            addChild(child);
        }

        private XmlNode findParent(string parent_prefix)
        {
            XmlNode search_node = current;
            while(search_node != null)
            {
                if (parent_prefix == search_node.Name)
                    return search_node;
                search_node = search_node.ParentNode;
            }
            return null;
        }

        public void addValue(string p)
        {
            current.AppendChild(doc.CreateTextNode(p));
        }
    }
}
