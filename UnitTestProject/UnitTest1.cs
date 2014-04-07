using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using SampleCode;

using Moq;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { set; get; }

        [TestMethod]
        public void TestMethodHelloWorld()
        {
            HelloWorld obj = new HelloWorld();
            obj.writeHelloWorld();
        }
    }
}
