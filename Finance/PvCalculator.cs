using System;
using System.Linq;

namespace Finance
{
    public interface IPvCalculator
    {
        /// <summary>
        /// Calculates the Present Value of a given Future Value (cash flow).
        /// </summary>
        /// <param name="futureValue">Future cash flows to be discounted</param>
        /// <param name="dicountPeriods">Number of compounding periods to discount over</param>
        /// <param name="interestRate">Interest rate for one compounding period, in %</param>
        /// <returns>Present Value</returns>
        decimal CalculatePv(decimal futureValue, int dicountPeriods, decimal interestRate);

        /// <summary>
        /// Calculates the Net Present Value of given Future Values (cash flows).
        /// </summary>
        /// <param name="futureCashFlows">Future cash flows to be discounted</param>
        /// <param name="interestRate">Interest rate (discount rate) for the period</param>
        /// <returns>Net Present Value of the future cash flows</returns>
        decimal CalculateNpv(decimal[] futureCashFlows, decimal interestRate);
    }

    public class PvCalculator : IPvCalculator
    {
        // PV = C / (1 + i) ^ n
        /// <summary>
        /// Calculates the Present Value of a given Future Value (cash flow).
        /// </summary>
        /// <param name="futureValue">Future cash flows to be discounted</param>
        /// <param name="dicountPeriods">Number of compounding periods to discount over</param>
        /// <param name="interestRate">Interest rate for one compounding period</param>
        /// <returns>Present Value</returns>
        public decimal CalculatePv(decimal futureValue, int dicountPeriods, decimal interestRate)
        {
            if (dicountPeriods < 0)
                throw new ArgumentOutOfRangeException("dicountPeriods", dicountPeriods, "Cannot PV using negative number of discount periods");

            if (dicountPeriods == 0)
                return futureValue;

            if (futureValue == 0)
                return 0;

            decimal discounter = (decimal) Math.Pow((double) (1 + interestRate), (uint)dicountPeriods);
            return futureValue/discounter;
        }

        // NPV = Sum (t = 1 -> n) of Cn / (1 + r) ^ t
        /// <summary>
        /// Calculates the Net Present Value of given Future Values (cash flows).
        /// </summary>
        /// <param name="futureCashFlows">Future cash flows to be discounted</param>
        /// <param name="interestRate">Interest rate (discount rate) for the period, in %</param>
        /// <returns>Net Present Value of the future cash flows</returns>
        public decimal CalculateNpv(decimal[] futureCashFlows, decimal interestRate)
        {
            if (futureCashFlows == null)
                throw new ArgumentNullException("futureCashFlows", "Must not be null");

            if (futureCashFlows.Length < 1)
                throw new ArgumentException("Must contain at least one future cash flow", "futureCashFlows");

            return futureCashFlows.Select((futureValue, i) => CalculatePv(futureValue, i + 1, interestRate)).Sum();
        }

       
    }
}
