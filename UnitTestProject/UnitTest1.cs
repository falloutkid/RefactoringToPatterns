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

        [TestMethod]
        public void TestTagBuilerChildren()
        {
            String expectedXml = "<flavors>" + "<flavor>" + "<requirements>" + "<requirement />" + "</requirements>" + "</flavor>" + "</flavors>";

            TagBuilder builder = new TagBuilder("flavors");
            builder.addChild("flavor");
            builder.addChild("requirements");
            builder.addChild("requirement");

            String actualXml = builder.toXml();
            Assert.AreEqual(expectedXml, actualXml);
        }

        [TestMethod]
        public void testBuildSibling()
        {
            String expectedXml = "<flavors>" + "<flavor1 />" + "<flavor2 />" + "</flavors>";

            TagBuilder builder = new TagBuilder("flavors");
            builder.addChild("flavor1");
            builder.addSibling("flavor2");

            String actualXml = builder.toXml();
            Assert.AreEqual(expectedXml, actualXml);
        }

        [TestMethod]
        public void testRepeatingChildrenAndGrandchildren()
        {
            String expectedXml =
              "<flavors>" +
                "<flavor>" +
                  "<requirements>" +
                    "<requirement />" +
                  "</requirements>" +
                "</flavor>" +
                "<flavor>" +
                  "<requirements>" +
                    "<requirement />" +
                  "</requirements>" +
                "</flavor>" +
              "</flavors>";

            TagBuilder builder = new TagBuilder("flavors");
            for (int i = 0; i < 2; i++)
            {
                builder.addToParent("flavor");
                builder.addChild("requirements");
                builder.addChild("requirement");
            }
            Assert.AreEqual(expectedXml, builder.toXml());
        }

        [TestMethod]
        public void testAttributesAndValues()
        {
            String expectedXml =
              "<flavor name=\"Test-Driven Development\">" +
                "<requirements>" +
                  "<requirement type=\"hardware\">" +
                      "1 computer for every 2 participants" +
                    "</requirement>" +
                  "<requirement type=\"software\">" +
                      "IDE" +
                  "</requirement>" +
                "</requirements>" +
              "</flavor>";

            TagBuilder builder = new TagBuilder("flavor");
            builder.addAttribute("name", "Test-Driven Development");
            builder.addChild("requirements");
            builder.addToParent("requirements", "requirement");
            builder.addAttribute("type", "hardware");
            builder.addValue("1 computer for every 2 participants");
            builder.addToParent("requirements", "requirement");
            builder.addAttribute("type", "software");
            builder.addValue("IDE");

            Assert.AreEqual(expectedXml, builder.toXml());
        }
    }
}
