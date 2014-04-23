﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringCommodity
{
    public class Loan
    {
        public double getUnusedPercentage()
        {
            return 1.5;
        }

        public double getCommitment()
        {
            return 2.5;
        }

        public double outstandingRiskAmount()
        {
            return 3.5;
        }

        public double unusedRiskAmount()
        {
            return 4.5;
        }
    }

    public abstract class CapitalStrategy
    {
        public double capital(Loan loan)
        {
            return riskAmountFor(loan) * duration(loan) * riskFactorFor(loan);
        }
        public abstract double riskAmountFor(Loan loan);
        private double riskFactorFor(Loan loan)
        {
            return 2.0;
        }

        private double duration(Loan loan)
        {
            return 1.0;
        }
    }

    public class CapitalStrategyAdvisedLine : CapitalStrategy
    {
        public override double riskAmountFor(Loan loan)
        {
            return loan.getCommitment() * loan.getUnusedPercentage();
        }       
    }

    public class CapitalStrategyTermLoan : CapitalStrategy
    {
        public override double riskAmountFor(Loan loan)
        {
            return loan.getCommitment();
        }
    }
}
