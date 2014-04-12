using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    public interface OutputBuilder
    {
        void addBelow(string tagName);
        void addAbove(string tagName);
    }

    class DOMBuilder : OutputBuilder
    {
        string tag_name_;
        public DOMBuilder(string tagName)
        {
            tag_name_ = tagName;
        }
        public void addBelow(string tagName) { }
        public void addAbove(string tagName) { }

    }

    public class DOMBuilderTest
    {
        private OutputBuilder builder;

        private OutputBuilder createBuilder(string rootName)
        {
            return new DOMBuilder(rootName);
        }

        public void testAddAboveRoot()
        {
            String invalidResult =
            "<orders>" +
              "<order>" +
              "</order>" +
            "</orders>" +
            "<customer>" +
            "</customer>";
            builder = createBuilder("orders");
            builder.addBelow("order");
            try
            {
                builder.addAbove("customer");
                System.Diagnostics.Debug.Assert(false, "expecting Exception");
            }
            catch (Exception ignored)
            {
                System.Diagnostics.Debug.WriteLine(ignored.Message.ToString());
            }
        }
    }
}
