using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringCommodity
{
    class Document
    {
        public Element createElement(string child)
        {
            throw new NotImplementedException();
        }
        internal Element createTextNode(string value)
        {
            throw new NotImplementedException();
        }
    }
    class Element
    {
        public void setAttribute(string name, string value)
        {
            throw new NotImplementedException();
        }

        internal void appendChild(Element childNode)
        {
            throw new NotImplementedException();
        }
    }
    interface AbstractBuilder
    {
        void addValue(String value);
        void addAttribute(String name, String value);
    }
    public class DOMBuilder : AbstractBuilder
    {
        private Document document;
        private Element root;
        private Element parent;
        private Element current;
        private Stack<Element> history = new Stack<Element>();
        private string CANNOT_ADD_BESIDE_ROOT = "Cannnot add beside root";
        public void addAttribute(String name, String value)
        {
            current.setAttribute(name, value);
        }
        public void addBelow(String child)
        {
            Element childNode = document.createElement(child);
            current.appendChild(childNode);
            parent = current;
            current = childNode;
            history.Push(current);
        }
        public void addBeside(String sibling)
        {
            if (current == root)
                throw new Exception(CANNOT_ADD_BESIDE_ROOT);
            Element siblingNode = document.createElement(sibling);
            parent.appendChild(siblingNode);
            current = siblingNode;
            history.Pop();
            history.Push(current);
        }
        public void addValue(String value)
        {
            current.appendChild(document.createTextNode(value));
        }
    }

    class TagNode
    {
        public string TagName { set; get; }

        public TagNode(string tagName)
        {
            this.TagName = tagName;
        }
        public void addValue(string value)
        {
            throw new NotImplementedException();
        }

        public TagNode getParent()
        {
            throw new NotImplementedException();
        }

        public void addAttribute(string name, string value)
        {
            throw new NotImplementedException();
        }

        public void add(TagNode currentNode)
        {
            throw new NotImplementedException();
        }
    }
    public class XMLBuilder : AbstractBuilder
    {
        private TagNode rootNode;
        private TagNode currentNode;
        public void addChild(String childTagName)
        {
            addTo(currentNode, childTagName);
        }
        public void addSibling(String siblingTagName)
        {
            addTo(currentNode.getParent(), siblingTagName);
        }
        private void addTo(TagNode parentNode, String tagName)
        {
            currentNode = new TagNode(tagName);
            parentNode.add(currentNode);
        }
        public void addAttribute(String name, String value)
        {
            currentNode.addAttribute(name, value);
        }
        public void addValue(String value)
        {
            currentNode.addValue(value);
        }
    }
}
