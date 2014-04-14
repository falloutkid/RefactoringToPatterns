﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
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

        readonly DateTime initDateTime = new DateTime(DateTime.MinValue.Ticks);
        const double MILLIS_PER_DAY = 86400000.0;
        const double DAYS_PER_YEAR = 365.0;

        private LoanPaymentFactor loanPaymentFactor;

        public LoanSimplification()
        {
            expiry_ = new DateTime(DateTime.MinValue.Ticks);
            maturity_ = new DateTime(DateTime.MinValue.Ticks);
            commitment_ = 0.0;
            outstanding_ = 0.0;
            riskRating_ = 0;
            payments_ = new List<Payment>();
            start_ = DateTime.Today;
        }

        public double capital()
        {
            if (expiry_ == initDateTime && maturity_ != initDateTime)
                return commitment_ * duration() * riskFactor();
            if (expiry_ != initDateTime && maturity_ == initDateTime)
            {
                if (getUnusedPercentage() != 1.0)
                    return commitment_ * getUnusedPercentage() * duration() * riskFactor();
                else
                    return (outstandingRiskAmount() * duration() * riskFactor())
                        + (unusedRiskAmount() * duration() * unusedRiskFactor());
            }
            return 0.0;
        }

        private double unusedRiskFactor()
        {
            return loanPaymentFactor.RiskFactor.UnRiskFactor;
        }

        private double riskFactor()
        {
            return loanPaymentFactor.RiskFactor.RiskFactor;
        }

        private double unusedRiskAmount()
        {
            return (commitment_ - outstanding_);
        }

        private double outstandingRiskAmount()
        {
            return outstanding_;
        }

        private double getUnusedPercentage()
        {
            throw new NotImplementedException();
        }

        private double duration()
        {
            if (expiry_ == null && maturity_ != null)
                return weightedAdverageDeration();
            else if (expiry_ != null && maturity_ == null)
                return yearTo(expiry_);
            return 0.0;
        }

        private double yearTo(DateTime expiry_)
        {
            return (getTime(endDate_) - getTime(start_)) / MILLIS_PER_DAY / DAYS_PER_YEAR;
        }

        private double getTime(DateTime time)
        {
            var ts = time - new DateTime(1970, 1, 1);
            return ts.TotalMilliseconds;
        }

        private double weightedAdverageDeration()
        {
            double duration = 0.0;
            double weightedAdverage = 0.0;
            double sumOfPayments = 0.0;

            foreach(Payment payment in payments_)
            {
                sumOfPayments += payment.amount;
                weightedAdverage += yearTo(payment.Date) * payment.amount;
            }

            if (commitment_ != 0.0)
                duration = weightedAdverage / sumOfPayments;

            return duration;
        }
    }
}
