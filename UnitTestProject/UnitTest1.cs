using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SampleCode;
using PatternOrientedRefactoring;

using Moq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestSampleCode
    {
        public TestContext TestContext { set; get; }

        [TestMethod]
        public void TestMethodHelloWorld()
        {
            HelloWorld obj = new HelloWorld();
            obj.writeHelloWorld();
        }
    }

    [TestClass]
    public class UnitTestCreate
    {
        public TestContext TestContext { set; get; }

        [TestMethod]
        public void TestTagBuilerFirst()
        {
            String expectedXml = "<flavors />";
            String actualXml = new TagBuilder("flavors").toXml();
            Assert.AreEqual(expectedXml, actualXml);
        }

        [TestMethod]
        public void TestTagBuilerOneChild()
        {
            String expectedXml = "<flavors>" + "<flavor />" + "</flavors>";

            TagBuilder builder = new TagBuilder("flavors");
            builder.addChild("flavor");
            String actualXml = builder.toXml();
            Assert.AreEqual(expectedXml, actualXml);
        }
    }
}
