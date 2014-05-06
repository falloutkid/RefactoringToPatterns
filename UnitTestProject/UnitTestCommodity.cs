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
            CompositeSpec specs = new CompositeSpec();
            specs.Add(new ColorSpec(Color.red));
            specs.Add(new SizeSpec(ProductSize.SMALL));
            specs.Add(new BelowPriceSpec((float)10.00));
            List<Product> foundProducts = repository.selectBy(specs);
            Assert.AreEqual(0, foundProducts.Count, "small red products below .00");
        }
    }

    [TestClass]
    public class ReplaceHardCodedNotificationsWithObserverTest
    {
        [TestMethod]
        public void run()
        {
            ConcreteCompany Company = new ConcreteCompany("アニマルランド");

            //社長就任
            Company.PresidentName = "ライオンキング";

            //社員採用
            Company.AddObserver(new ConcreteObserverWorker(Company, "パンダ"));
            Company.AddObserver(new ConcreteObserverWorker(Company, "うさぎ"));
            Company.AddObserver(new ConcreteObserverWorker(Company, "コアラ"));
            Company.AddObserver(new ConcreteObserverWorker(Company, "シマウマ"));

            //社員に通知
            Company.SetChanged();

            //社長交代（オブジェクトに変化あり）
            Company.PresidentName = "空飛ぶダンボ";

            //社員に通知
            Company.SetChanged();
        }
    }

    [TestClass]
    public class ExtractAdapterTest
    {
        public TestContext TestContext { set; get; }

        AbstractQuery query;
        [TestMethod]
        [TestCase("database1", "hoge", "fuga", null, false)]
        [TestCase("database1", "hoge", "fuga", "fugafuga", true)]
        public void TestLoginToDatabase()
        {
            TestContext.Run((string database, string id, string password, string config_file, bool isUsingSDVersion52) =>
                {
                    try
                    {
                        if (isUsingSDVersion52)
                        {
                            query = new QuerySD52(config_file);
                        }
                        else
                        {
                            query = new QuerySD51();
                        }
                        query.login(database, id, password);
                    }
                    catch (QueryException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                });
        }
    }
}
