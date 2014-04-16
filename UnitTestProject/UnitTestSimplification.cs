using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PatternOrientedRefactoringSimplification;


namespace UnitTestProject
{
    [TestClass]
    public class UnitTestMoveEmbellishmentToDecorator
    {
        public TestContext TestContext { set; get; }

        [TestMethod]
        public void testDecodingAmpersand()
        {
            String ENCODED_WORKSHOP_TITLE =
            "The Testing &amp; Refactoring Workshop";

            String DECODED_WORKSHOP_TITLE =
            "The Testing & Refactoring Workshop";

            Parser parser = Parser.createParser(ENCODED_WORKSHOP_TITLE);
            parser.setNodeDecoding(true);  // tell parser_ to decode StringNodes

            String decodedContent = parser.toPlainTextString();

            Assert.AreEqual(DECODED_WORKSHOP_TITLE, decodedContent.ToString());
        }
    }
}
