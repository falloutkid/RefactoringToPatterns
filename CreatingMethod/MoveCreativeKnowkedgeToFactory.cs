using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    class Parser
    {
        public Boolean ShouldDecode { set; get; }
        public Boolean ShouldRemoveEscapeCharactor { set; get; }
    }

    public interface Node { }

    class NodeFactory
    {
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
            parser.ShouldDecode = shouldDecode;
            return nodeFactory.createStringNode(textBuffer, textBegin, textEnd, parser.ShouldDecode);
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
