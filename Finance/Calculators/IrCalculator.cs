using System;
using System.Linq;

namespace Finance.Calculators
{
    public interface IIrCalculator
    {
        /// <summary>
        /// Calculates the Irr of the given Future Values (cash flows).
        /// Uses the secant method, to within 0.0001%.
        /// aka the Money Weighted Rate of Return.
        /// </summary>
        /// <param name="futureCashFlows">Future cash flows</param>
        /// <param name="pv">Today's cash flow (PV)</param>
        /// <returns>The interest rate at which makes the future cash flows equal in value to the pv</returns>
        decimal CalculateIrr(decimal[] futureCashFlows, decimal pv);

        /// <summary>
        /// Calculates the Time Weighted Rate of Return 
        /// i.e. looks at returns independently of amount
        /// </summary>
        /// <param name="startOfPeriodAmounts">Amounts at the start of each period</param>
        /// <param name="endOfPeriodAmounts">Amounts at the end of each period</param>
        /// <returns>Time Weighted Rate of Return </returns>
        decimal CalculateTimeWeightedReturn(decimal[] startOfPeriodAmounts, decimal[] endOfPeriodAmounts);
    }

    public class IrCalculator : IIrCalculator
    {
        private readonly IPvCalculator _pvCalculator;

        public IrCalculator(IPvCalculator pvCalculator)
        {
            _pvCalculator = pvCalculator;
        }

        /// <summary>
        /// Calculates the Internal Rate of Return of the given Future Values (cash flows).
        /// Uses the secant method, to within 0.0001%.
        /// aka the Money Weighted Rate of Return.
        /// </summary>
        /// <param name="futureCashFlows">Future cash flows</param>
        /// <param name="pv">Today's cash flow (PV)</param>
        /// <returns>The interest rate at which makes the future cash flows equal in value to the pv</returns>
        public decimal CalculateIrr(decimal[] futureCashFlows, decimal pv)
        {
            if (futureCashFlows == null)
                throw new ArgumentNullException("futureCashFlows", "Must not be null");

            if (futureCashFlows.Length < 1)
                throw new ArgumentException("Must contain at least one future cash flow", "futureCashFlows");

            decimal previousIr = 0.05m;
            decimal currentIr  = 0.1m;
            decimal previousNpv = _pvCalculator.CalculateNpv(futureCashFlows, previousIr) + pv;
            decimal nextIr;

            while (true)
            {
                decimal currentNpv = _pvCalculator.CalculateNpv(futureCashFlows, currentIr) + pv;

                decimal ratio = ((currentIr - previousIr) / (currentNpv - previousNpv));
                nextIr = currentIr - (currentNpv * ratio);

                if (Math.Abs(nextIr - currentIr) < 0.0001m)
                    break;

                previousNpv = currentNpv;
                previousIr = currentIr;
                currentIr = nextIr;
            }

            return nextIr;
        }

        /// <summary>
        /// Calculates the Time Weighted Rate of Return 
        /// i.e. looks at returns independently of amount
        /// </summary>
        /// <param name="startOfPeriodAmounts">Amounts at the start of each period</param>
        /// <param name="endOfPeriodAmounts">Amounts at the end of each period</param>
        /// <returns>Time Weighted Rate of Return </returns>
        public decimal CalculateTimeWeightedReturn(decimal[] startOfPeriodAmounts, decimal[] endOfPeriodAmounts)
        {
            if (startOfPeriodAmounts == null)
                throw new ArgumentNullException("startOfPeriodAmounts", "Must not be null");

            if (endOfPeriodAmounts == null)
                throw new ArgumentNullException("endOfPeriodAmounts", "Must not be null");

            if (startOfPeriodAmounts.Length != endOfPeriodAmounts.Length)
                throw new ArgumentException("Must have equal number of start and end amounts", "startOfPeriodAmounts");

            if (startOfPeriodAmounts.Length < 1)
                throw new ArgumentException("Must contain at least one period", "startOfPeriodAmounts");

            var finalRatio = startOfPeriodAmounts.Zip(endOfPeriodAmounts, (start, end) => end / start)
                                                 .Aggregate((x, y) => x * y);

            return finalRatio - 1;
        }
    }
}
