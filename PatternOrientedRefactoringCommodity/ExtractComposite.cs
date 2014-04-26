using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringCommodity
{
    public class Tag { }
    public class Node
    {
        public string PlainTextString { set; get; }
    }
    public class LinkTag : Tag
    {
        private List<Node> linkData;
        public String toPlainTextString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node node in linkData)
            {
                sb.Append(node.PlainTextString);
            }
            return sb.ToString();
        }
    }

    public class FormTag : Tag
    {
        protected List<Node> allNodesList;
        public String toPlainTextString()
        {
            StringBuilder stringRepresentation = new StringBuilder();
            foreach (Node node in allNodesList)
            {
                stringRepresentation.Append(node.PlainTextString);
            }
            return stringRepresentation.ToString();
        }
    }
}
