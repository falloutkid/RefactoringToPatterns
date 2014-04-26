using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringCommodity
{
    public class Tag
    {
        int tag_begin_;
        int tag_end_;
        string tag_contents_;
        string tag_line_;
        public Tag(int tag_begin, int tag_end, string tag_contents, string tag_line)
        {
            tag_begin_ = tag_begin;
            tag_end_ = tag_end;
            tag_contents_ = tag_contents;
            tag_line_ = tag_line;
        }
    }
    public class Node
    {
        public string PlainTextString { set; get; }
    }

    public abstract class CompositeTag : Tag
    {
        public CompositeTag(int tag_begin, int tag_end, string tag_contents, string tag_line)
            : base(tag_begin, tag_end, tag_contents, tag_line) { }

        protected List<Node> children;
        public String toPlainTextString()
        {
            StringBuilder string_builder = new StringBuilder();
            foreach (Node node in children)
            {
                string_builder.Append(node.PlainTextString);
            }
            return string_builder.ToString();
        }
    }

    public class LinkTag : CompositeTag
    {
        public LinkTag(int tag_begin, int tag_end, string tag_contents, string tag_line)
            : base(tag_begin, tag_end, tag_contents, tag_line) { }

    }

    public class FormTag : CompositeTag
    {
        public FormTag(int tag_begin, int tag_end, string tag_contents, string tag_line)
            : base(tag_begin, tag_end, tag_contents, tag_line) { }
    }
}
