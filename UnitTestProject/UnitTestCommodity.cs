using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
}
