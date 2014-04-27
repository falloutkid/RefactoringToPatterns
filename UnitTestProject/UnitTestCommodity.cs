using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using PatternOrientedRefactoringCommodity;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestFormTemplateMethod
    {
        [TestMethod]
        public void TestMethod()
        {
            CapitalStrategy csa = new CapitalStrategyAdvisedLine();
            CapitalStrategy cst = new CapitalStrategyTermLoan();

            Loan loan = new Loan();
            System.Diagnostics.Debug.WriteLine("csa capital : " + csa.capital(loan));
            System.Diagnostics.Debug.WriteLine("cst capital : " + cst.capital(loan));
        }
    }

    [TestClass]
    public class ProductRepositoryTest
    {
        private ProductRepository repository;
        Product fireTruck;
        Product barbieClassic;
        Product frisbee;
        Product baseball;
        Product toyConvertible;

        [TestInitialize()]
        public void TestInitialize()
        {
            repository = new ProductRepository();
            fireTruck = new Product("f1234", "Fire Truck", Color.red, 8.95f, ProductSize.MEDIUM);
            barbieClassic = new Product("b7654", "Barbie Classic", Color.yellow, 15.95f, ProductSize.SMALL);
            frisbee = new Product("f4321", "Frisbee", Color.pink, 9.99f, ProductSize.LARGE);
            baseball = new Product("b2343", "Baseball", Color.white, 8.95f, ProductSize.NOT_APPLICABLE);
            toyConvertible = new Product("p1112", "Toy Porsche Convertible", Color.red, 230.00f, ProductSize.NOT_APPLICABLE);
            repository.add(fireTruck);
            repository.add(barbieClassic);
            repository.add(frisbee);
            repository.add(baseball);
            repository.add(toyConvertible);
        }

        [TestMethod()]
        public void testFindByColor()
        {
            List<Product> foundProducts = repository.selectBy(new ColorSpec(Color.red));
            Assert.AreEqual(2, foundProducts.Count, "found 2 red products");
            Assert.IsTrue(foundProducts.Contains(fireTruck), "found fireTruck");
            Assert.IsTrue(foundProducts.Contains(toyConvertible), "found Toy Porsche Convertible");
        }

        [TestMethod]
        public void testFindByColorSizeAndBelowPrice()
        {
            List<Spec> specs = new List<Spec>();
            specs.Add(new ColorSpec(Color.red));
            specs.Add(new SizeSpec(ProductSize.SMALL));
            specs.Add(new BelowPriceSpec((float)10.00));
//            List<Product> foundProducts = repository.selectBy(specs);
            List<Product> foundProducts = repository.selectBy(new CompositeSpec(specs));
            Assert.AreEqual(0, foundProducts.Count, "small red products below .00");
        }
    }
}
