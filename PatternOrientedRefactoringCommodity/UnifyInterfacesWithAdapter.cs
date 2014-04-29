using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringCommodity
{
    interface XMLNode
    {
        void add(XMLNode childNode);
        void addValue(String value);
        void addAttribute(String name, String value);
    }

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

    class ElementAdapter
    {
        Element element_;

        public Element getElement { get { return element_; } }

        public ElementAdapter(Element element)
        {
            element_ = element;
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
        private ElementAdapter rootNode;
        private ElementAdapter parentNode;
        private ElementAdapter currentNode;
        private Stack<ElementAdapter> history = new Stack<ElementAdapter>();
        private string CANNOT_ADD_BESIDE_ROOT = "Cannnot add beside root";
        public void addAttribute(String name, String value)
        {
            addAttribute(currentNode, name, value);
        }

        private void addAttribute(ElementAdapter current, String name, String value)
        {
            current.getElement.setAttribute(name, value);
        }

        public void addBelow(String child)
        {
            ElementAdapter childNode = new ElementAdapter(document.createElement(child));
            add(currentNode, childNode);
            parentNode = currentNode;
            currentNode = childNode;
            history.Push(currentNode);
        }
        public void addSibling(String sibling)
        {
            if (currentNode == rootNode)
                throw new Exception(CANNOT_ADD_BESIDE_ROOT);
            ElementAdapter siblingNode = new ElementAdapter(document.createElement(sibling));
            add(parentNode, siblingNode);
            currentNode = siblingNode;
            history.Pop();
            history.Push(currentNode);
        }

        private void add(ElementAdapter parent, ElementAdapter child)
        {
            parent.getElement.appendChild(child.getElement);
        }

        public void addValue(String value)
        {
            addValue(currentNode, value);
        }
        
        private void addValue(ElementAdapter current, string value)
        {
            current.getElement.appendChild(document.createTextNode(value));
        }
    }

    class TagNode:XMLNode
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

        public void add(XMLNode currentNode)
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
