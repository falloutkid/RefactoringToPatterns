using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringSimplification
{
    public class Parser
    {
        public NodeFactory nodeFactory { set; get; }
        public Boolean ShouldRemoveEscapeCharactor { set; get; }

        static List<Node> nodes;
        static Parser parser_;
        static StringParser string_parser;
        static bool shouldDecode;
        public bool shouldDecodeNode
        {
            set
            {
                shouldDecode = value;
            }
            get
            {
                return shouldDecode;
            }
        }

        public Parser()
        {
            nodes = new List<Node>();
            string_parser = new StringParser();
        }

        public static Parser createParser(string root_node_text)
        {
            if (parser_ == null)
                parser_ = new Parser();

            StringBuilder input = new StringBuilder();
            input.Append(root_node_text);

            Node root_node = string_parser.find(input, 0, root_node_text.Length, shouldDecode);

            nodes.Add(root_node);

            return parser_;
        }

        public string toPlainTextString()
        {
            StringBuilder result = new StringBuilder();

            foreach (Node node in nodes)
                result.Append(node.toPlainTextString());

            return result.ToString();
        }

        public void setNodeDecoding(bool p)
        {
            shouldDecode = p;
        }

        public static Parser createParser(string root_node_text, bool p)
        {
            if (parser_ == null)
                parser_ = new Parser();
            shouldDecode = p;
            StringBuilder input = new StringBuilder();
            input.Append(root_node_text);

            Node root_node = string_parser.find(input, 0, root_node_text.Length, shouldDecode);

            nodes.Add(root_node);

            return parser_;
        }
    }

    public interface Node
    {
        string toPlainTextString();
        String NodeText { set; get; }
    }

    public class NodeFactory
    {
        public Boolean decodeStringNodes { set; get; }

        public Node createStringNode(StringBuilder textBuffer, int textBegin, int textEnd)
        {
            if (decodeStringNodes)
                return new DecodingStringNode(textBuffer, textBegin, textEnd);
            return StringNode.createStringNode(textBuffer, textBegin, textEnd);
        }

        public Node createStringNode(StringBuilder textBuffer, int textBegin, int textEnd, Boolean shouldDecode)
        {
            if (shouldDecode)
                return new DecodingStringNode(textBuffer, textBegin, textEnd);
            return StringNode.createStringNode(textBuffer, textBegin, textEnd);
        }
    }

    public class StringParser
    {
        public Node find(StringBuilder textBuffer, int textBegin, int textEnd, bool shouldDecode)
        {
            Parser parser = new Parser();
            NodeFactory nodeFactory = new NodeFactory();
            nodeFactory.decodeStringNodes = shouldDecode;

            parser.nodeFactory = nodeFactory;

            return parser.nodeFactory.createStringNode(textBuffer, textBegin, textEnd, parser.shouldDecodeNode);
        }
    }
    class DecodingStringNode : StringNode
    {
        Dictionary<string, string> replace_list = new Dictionary<string, string>(){
        {"&amp;","&"},
        {"&divide;","÷"},
        {"&lt;","<"},
        {"&rt;",">"},
        };

        public DecodingStringNode(StringBuilder textBuffer, int textBegin, int textEnd):base(textBuffer,  textBegin,  textEnd) {  }
        public override string toPlainTextString()
        {
            String result = string_builder.ToString();
            StringBuilder temporary = string_builder;
            foreach (string key in replace_list.Keys)
                temporary.Replace(key, replace_list[key]);
            result = temporary.ToString();

            return result;
        }
    }
    class StringNode : Node
    {
        protected StringBuilder string_builder;
        public bool ShouldRemoveEscapeCharactor { set; get; }
        public String NodeText { set; get; }

        protected StringNode(StringBuilder textBuffer, int textBegin, int textEnd)
        {
            string_builder = textBuffer;
        }

        public static StringNode createStringNode(StringBuilder textBuffer, int textBegin, int textEnd)
        {
            return new StringNode(textBuffer, textBegin, textEnd);
        }

        public virtual string toPlainTextString()
        {
            String result = string_builder.ToString();

            if(ShouldRemoveEscapeCharactor)
            {
                StringBuilder temporary = string_builder;
                temporary.Replace("\r", "");
                temporary.Replace("\n", "");
                result = temporary.ToString();
            }
            return result;
        }
    }
}
