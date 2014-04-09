using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    class Parser
    {
        public NodeFactory nodeFactory { set; get; }
        public Boolean ShouldRemoveEscapeCharactor { set; get; }
    }

    public interface Node { }

    class NodeFactory
    {
         public Boolean decodeStringNodes { set; get; }

         public Node createStringNode(StringBuilder textBuffer, int textBegin, int textEnd)
         {
             if(decodeStringNodes)
                 return new DecodingStringNode(new StringNode(textBuffer, textBegin, textEnd));
             return new StringNode(textBuffer, textBegin, textEnd); 
         }

         public Node createStringNode(StringBuilder textBuffer, int textBegin, int textEnd, Boolean shouldDecode)
         {
             if (shouldDecode)
                 return new DecodingStringNode(new StringNode(textBuffer, textBegin, textEnd));
             return new StringNode(textBuffer, textBegin, textEnd);
         }
    }

    class StringParser
    {
        public Node find(StringBuilder textBuffer, int textBegin, int textEnd, bool shouldDecode)
        {
            Parser parser = new Parser();
            NodeFactory nodeFactory = new NodeFactory();
            nodeFactory.decodeStringNodes = shouldDecode;

            parser.nodeFactory = nodeFactory;

            return parser.nodeFactory.createStringNode(textBuffer, textBegin, textEnd);
        }
    }
    class DecodingStringNode : Node
    {
        public DecodingStringNode(StringNode string_node) { }
    }
    class StringNode : Node
    {
        public StringNode(StringBuilder textBuffer, int textBegin, int textEnd)
        {
        }
    }
}
