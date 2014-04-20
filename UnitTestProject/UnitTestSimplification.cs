﻿using System;
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

            Parser parser = Parser.createParser(ENCODED_WORKSHOP_TITLE, true);
            parser.setNodeDecoding(true);  // tell parser_ to decode StringNodes

            String decodedContent = parser.toPlainTextString();

            Assert.AreEqual(DECODED_WORKSHOP_TITLE, decodedContent.ToString());
        }
    }
    [TestClass]
    public class UnitTestReplaceState
    {
        private SystemPermission permission;
        private SystemProfile profile;
        private SystemUser user;
        private SystemAdmin admin;

        [TestInitialize]
        public void setUp()
        {
            permission = new SystemPermission(user, new SystemProfile());
            admin = new SystemAdmin();
            permission.Admin = admin;
        }

        [TestMethod]
        public void testGrantedBy()
        {
            permission.grantedBy(admin);
            Assert.AreEqual(PermissionState.REQUESTED, permission.PermissionState, "requested");
            Assert.AreEqual(false, permission.IsGranted, "not granted");
            permission.claimedBy(admin);
            permission.grantedBy(admin);
            Assert.AreEqual(PermissionState.GRANTED, permission.PermissionState, "granted");
            Assert.AreEqual(true, permission.IsGranted, "granted");
        }
    }
    [TestClass]
    public class TagTests
    {
        private static String SAMPLE_PRICE = "8.95";

        [TestMethod]
        public void testSimpleTagWithOneAttributeAndValue()
        {
            TagNode priceTag = new TagNode("price");
            priceTag.addAttribute("currency", "USD");
            priceTag.addValue(SAMPLE_PRICE);
            String expected =
              "<price currency=" +
              "'" +
              "USD" +
              "'>" +
              SAMPLE_PRICE +
              "</price>";
            Assert.AreEqual(expected, priceTag.toString(), "price XML");
        }
    }
}
