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
        private  int outputBufferSize;
        const int TAG_CHARS_SIZE = 5;
        const int ATTRRIBUTE_CHARS_SISE = 4;

        public TagBuilder(String rootTagName)
        {
            doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement(rootTagName));
            current = doc.FirstChild;

            outputBufferSize = 0;
            incrementBufferSizeByTagLength(rootTagName);
        }

        private void incrementBufferSizeByTagLength(String tag_name)
        {
            outputBufferSize += tag_name.Length * 2 + TAG_CHARS_SIZE;
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

            incrementBufferSizeByTagLength(childTagName);
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

        public void addAttribute(string name, string value)
        {
            XmlAttribute attr = doc.CreateAttribute(name);
            attr.Value = value;
            current.Attributes.Append(attr);

            outputBufferSize += name.Length + value.Length + ATTRRIBUTE_CHARS_SISE;
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

        public void addValue(string value)
        {
            current.AppendChild(doc.CreateTextNode(value));

            outputBufferSize += value.Length;
        }

        public int bufferSize()
        {
            return outputBufferSize;
        }
    }
}
