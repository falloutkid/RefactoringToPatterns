using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    class Parser
    {
        public StringNodeParsingOption stringNodeParsingOption { set; get; }
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

     class StringNodeParsingOption
    {
         public Boolean decodeStringNodes { set; get; }
    }

    class StringParser
    {
        public Node find(StringBuilder textBuffer, int textBegin, int textEnd, bool shouldDecode)
        {
            Parser parser = new Parser();
            NodeFactory nodeFactory = new NodeFactory();

            StringNodeParsingOption stringNodeOption = new StringNodeParsingOption();
            stringNodeOption.decodeStringNodes = shouldDecode;
            parser.stringNodeParsingOption = stringNodeOption;

            return nodeFactory.createStringNode(textBuffer, textBegin, textEnd, parser.stringNodeParsingOption.decodeStringNodes);
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
