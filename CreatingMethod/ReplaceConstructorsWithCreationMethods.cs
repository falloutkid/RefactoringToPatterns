using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoring
{
    public class Loan
    {
        CaptalStrategyIf captalStrategy_;
        double commitment_;
        double outstanding_;
        int riskRating_;
        DateTime maturity_;
        DateTime expiry_;

        public static Loan createTermLoan(double commitment, int riskRating, DateTime maturity)
        {
            return new Loan(null, commitment, 0.0, riskRating, maturity, new DateTime(DateTime.MinValue.Ticks));
        }

        public static Loan createTermLoan(CaptalStrategyIf captalStrategy, double commitment, double outstanding, int riskRating, DateTime maturity)
        {
            return new Loan(captalStrategy, commitment, outstanding, riskRating, maturity, new DateTime(DateTime.MinValue.Ticks));
        }

        public static Loan createRevolver(double commitment, double outstanding, int riskRating, DateTime expiry)
        {
            return new Loan(null, commitment, outstanding, riskRating, new DateTime(DateTime.MinValue.Ticks), expiry);
        }

        public static Loan createRevolver(CaptalStrategyIf captalStrategy, double commitment, double outstanding, int riskRating, DateTime expiry)
        {
            return new Loan(captalStrategy, commitment, outstanding, riskRating, new DateTime(DateTime.MinValue.Ticks), expiry);
        }

        public static Loan createRCTL(double commitment, double outstanding, int riskRating, DateTime maturity, DateTime expiry)
        {
            return new Loan(null, commitment, outstanding, riskRating, maturity, expiry);
        }

        public static Loan createRCTL(CaptalStrategyIf captalStrategy, double commitment, double outstanding, int riskRating, DateTime maturity, DateTime expiry)
        {
            return new Loan(captalStrategy, commitment, outstanding, riskRating, maturity, expiry);
        }

        private Loan(CaptalStrategyIf captalStrategy, double commitment, double outstanding, int riskRating, DateTime maturity, DateTime expiry)
        {
            commitment_ = commitment;
            outstanding_ = outstanding;
            riskRating_ = riskRating;
            maturity_ = maturity;
            expiry_ = expiry;
            if (captalStrategy == null)
            {
                if (expiry_.Equals(new DateTime(DateTime.MinValue.Ticks)))
                    captalStrategy_ = new CaptalStrategyTermLoan();
                else if (maturity_.Equals(new DateTime(DateTime.MinValue.Ticks)))
                    captalStrategy_ = new CaptalStrategyRevolver();
                else
                    captalStrategy_ = new CaptalStrategyRCTL();
            }
            else
            {
                captalStrategy_ = captalStrategy;
            }
        }
    }

    public interface CaptalStrategyIf{

    }

    public class CaptalStrategyTermLoan : CaptalStrategyIf { }
    public class CaptalStrategyRevolver : CaptalStrategyIf { }
    public class CaptalStrategyRCTL : CaptalStrategyIf { }
}
