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
            
            /// Add element
            current.AppendChild(element);
            current = current.LastChild;

            incrementBufferSizeByTagLength(childTagName);
        }

        public void addSibling(string childTagName)
        {
            /// Create a new node.
            XmlElement element = doc.CreateElement(childTagName);
            //           element.InnerText = "";

            /// Add element
            current.ParentNode.AppendChild(element);
        }

        public void addToParent(string childTagName)
        {
            /// Create a new node.
            XmlElement element = doc.CreateElement(childTagName);
            //           element.InnerText = "";

            /// Add element
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

        public Dictionary<string, string> makeTreeSchema(string[] schema)
        {
            Dictionary<string, string> schema_tree = new Dictionary<string, string>();
            schema_tree.Add(doc.FirstChild.Name, null);
            string[] parent = new string[10];
            parent[0] = doc.FirstChild.Name;

            foreach (string data in schema)
            {
                if (doc.FirstChild.Name != data)
                {
                    int level = data.Length - data.Replace("\t", "").Length;
                    if (parent[level - 1] == null)
                        continue;
                    else
                    {
                        schema_tree.Add(data.Replace("\t", ""), parent[level - 1].ToString());
                        parent[level] = data.Replace("\t", "");
                    }
                }
            }
            return schema_tree;
        }
    }
}
