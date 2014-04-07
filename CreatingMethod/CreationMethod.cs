using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreationMethod
{
    public class Loan
    {
        CaptalStrategy captalStrategy_;
        double commitment_;
        double outstanding_;
        int riskRating_;
        DateTime maturity_;
        DateTime expiry_;

        Loan(double commitment, int riskRating, DateTime maturity)
            : this(null, commitment, 0.0, riskRating, maturity, new DateTime(DateTime.MinValue.Ticks))
        {
        }
        public static Loan createTermLoan(double commitment, int riskRating, DateTime maturity)
        {
            return new Loan(commitment, riskRating, maturity);
        }

        public Loan(double commitment, int riskRating, DateTime maturity, DateTime expiry)
            : this(null, commitment, 0.0, riskRating, maturity, expiry)
        {
        }
        public Loan(double commitment, double outstanding, int riskRating, DateTime maturity, DateTime expiry)
            : this(null, commitment, outstanding, riskRating, maturity, expiry)
        {
        }
        public Loan(CaptalStrategy captalStrategy, double commitment, int riskRating, DateTime maturity, DateTime expiry)
            : this(captalStrategy, commitment, 0.0, riskRating, maturity, expiry)
        {
        }

        public Loan(CaptalStrategy captalStrategy, double commitment, double outstanding, int riskRating, DateTime maturity, DateTime expiry)
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

    public interface CaptalStrategy{

    }

    public class CaptalStrategyTermLoan : CaptalStrategy { }
    public class CaptalStrategyRevolver : CaptalStrategy { }
    public class CaptalStrategyRCTL : CaptalStrategy { }
}
