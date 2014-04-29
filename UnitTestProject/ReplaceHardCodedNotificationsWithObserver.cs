using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass]
    public class ReplaceHardCodedNotificationsWithObserver
    {
        public class Test { }
        public class Throwable { }
        public class TestResult
        {
            protected void addFalure(Test test) { }
        }
        public class UITestResult : TestResult
        {
            private TestRunner fRunner;
            public UITestResult(TestRunner runner)
            {
                fRunner = runner;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            public void addFailure(Test test, Throwable t)
            {
                addFailure(test, t);
                fRunner.addFailure(this, test, t); // notification to TestRunner
            }
        }


        public class Frame { }
        public class TestRunner : Frame
        {
            // TestRunner for AWT
            public TestResult fTestResult;
            public TestContext TestContext { set; get; }
            public TestResult createTestResult()
            {
                return new UITestResult(this); // hard-coded to UITestResult
            }

            [TestMethod]
            [TestCase(1)]
            public void runSuite()
            {
                TestContext.Run((int i) => { fTestResult = createTestResult(); });
            }
            public void addFailure(TestResult result, Test test, Throwable t)
            {
                // display the failure in a graphical AWT window
            }
        }
    }
}
