using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringSimplification
{
    #region パラメータクラス
    public class Payment
    {
        public double amount { set; get; }
        public DateTime Date { set; get; }
    }

    public class LoanRiskFactor
    {
        public double RiskFactor { set; get; }
        public double UnRiskFactor { set; get; }
    }

    public class LoanPaymentFactor
    {
        public LoanRiskFactor RiskFactor { set; get; }
    }
    #endregion

    public abstract class CapitalStrategy
    {
        readonly DateTime initDateTime = new DateTime(DateTime.MinValue.Ticks);
        const double MILLIS_PER_DAY = 86400000.0;
        const double DAYS_PER_YEAR = 365.0;

        public abstract double capital(LoanSimplification loan);
        
        public double unusedRiskFactor(LoanSimplification loan)
        {
            return loan.loanPaymentFactor.RiskFactor.UnRiskFactor;
        }

        public double riskFactor(LoanSimplification loan)
        {
            return loan.loanPaymentFactor.RiskFactor.RiskFactor;
        }

        public double duration(LoanSimplification loan)
        {
            return yearTo(loan);
        }

        protected double yearTo(LoanSimplification loan)
        {
            return (getTime(loan.EndDate) - getTime(loan.StartTime)) / MILLIS_PER_DAY / DAYS_PER_YEAR;
        }

        private double getTime(DateTime time)
        {
            var ts = time - new DateTime(1970, 1, 1);
            return ts.TotalMilliseconds;
        }
    }

    public class CapitalStrategyTermLoan:CapitalStrategy
    {
        public override double capital(LoanSimplification loan)
        {
            return loan.Commitment * duration(loan) * riskFactor(loan);
        }

        public new double duration(LoanSimplification loan)
        {
            return weightedAdverageDeration(loan);
        }

        private double weightedAdverageDeration(LoanSimplification loan)
        {
            double duration = 0.0;
            double weightedAdverage = 0.0;
            double sumOfPayments = 0.0;

            foreach (Payment payment in loan.Payments)
            {
                sumOfPayments += payment.amount;
                weightedAdverage += yearTo(loan) * payment.amount;
            }

            if (loan.Commitment != 0.0)
                duration = weightedAdverage / sumOfPayments;

            return duration;
        }
    }

    public class CapitalStrategyRevolver : CapitalStrategy
    {
        public override double capital(LoanSimplification loan)
        {
            return loan.Commitment * loan.getUnusedPercentage() * duration(loan) * riskFactor(loan);
        }
    }
    public class CapitalStrategyAdvisedLine : CapitalStrategy
    {
        public override double capital(LoanSimplification loan)
        {

            return (loan.outstandingRiskAmount() * duration(loan) * riskFactor(loan))
                + (loan.unusedRiskAmount() * duration(loan) * unusedRiskFactor(loan));
        }
    }

    public class LoanSimplification
    {
        DateTime expiry_;
        DateTime maturity_;
        DateTime start_;
        DateTime endDate_;
        double commitment_;
        double outstanding_;
        int riskRating_;
        List<Payment> payments_;

        public LoanPaymentFactor loanPaymentFactor{get;set;}

        public DateTime Expiry { get { return expiry_; } }
        public DateTime Maturity { get { return maturity_; } }
        public DateTime StartTime { get {return start_; } }
        public DateTime EndDate { get { return endDate_; } }
        public double Commitment { get { return commitment_; } }
        public List<Payment> Payments { get { return payments_; } }

        private CapitalStrategy capitalstrategy_;

        public double capital()
        {
            return capitalstrategy_.capital(this);
        }

        public double duration()
        {
            return capitalstrategy_.duration(this);
        }

        private LoanSimplification(CapitalStrategy capitalstrategy)
        {
            expiry_ = new DateTime(DateTime.MinValue.Ticks);
            maturity_ = new DateTime(DateTime.MinValue.Ticks);
            commitment_ = 0.0;
            outstanding_ = 0.0;
            riskRating_ = 0;
            payments_ = new List<Payment>();
            start_ = DateTime.Today;

            capitalstrategy_ = capitalstrategy;
        }

        public static LoanSimplification newRevolver()
        {
            return new LoanSimplification(new CapitalStrategyRevolver());
        }

        public static LoanSimplification newAdvicedLine()
        {
            return new LoanSimplification(new CapitalStrategyAdvisedLine());
        }

        public static LoanSimplification newTermLoan()
        {
            return new LoanSimplification(new CapitalStrategyTermLoan());
        }

        public double unusedRiskAmount()
        {
            return (commitment_ - outstanding_);
        }

        public double outstandingRiskAmount()
        {
            return outstanding_;
        }

        public double getUnusedPercentage()
        {
            throw new NotImplementedException();
        }  
    }
}
