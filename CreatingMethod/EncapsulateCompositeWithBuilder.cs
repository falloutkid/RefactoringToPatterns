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
        XmlNode root;
        public TagBuilder(String rootTagName)
        {
            doc = new XmlDocument();
            doc.AppendChild(doc.CreateElement(rootTagName));
            root = doc.FirstChild;
        }

        public String toXml()
        {
            return doc.InnerXml;
        }

        public void addChild(string childTagName)
        {
//            TagNode parentNode = currentNode;
//            currentNode = new TagNode(childTagName);
//            parentNode.add(currentNode);
        }
    }
}
