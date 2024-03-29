﻿using System;
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

    class XMLBuilder : OutputBuilder
    {
        string tag_name_;
        public XMLBuilder(string tagName)
        {
            tag_name_ = tagName;
        }
        public void addBelow(string tagName) { }
        public void addAbove(string tagName) { }
    }

    public abstract class AbstractBuilderTest
    {
        protected OutputBuilder builder;

        protected abstract OutputBuilder createBuilder(string rootName);

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

    public class DOMBuilderTest:AbstractBuilderTest
    {
        protected override OutputBuilder createBuilder(string rootName)
        {
            return new DOMBuilder(rootName);
        }
    }

    public class XMLBuilderTest:AbstractBuilderTest
    {
        protected override OutputBuilder createBuilder(string rootName)
        {
            return new XMLBuilder(rootName);
        }
    }
}
