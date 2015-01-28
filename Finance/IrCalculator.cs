using System;

namespace Finance
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
    }

    public class IrCalculator : IIrCalculator
    {
        private readonly IPvCalculator _pvCalculator;

        public IrCalculator(IPvCalculator pvCalculator)
        {
            _pvCalculator = pvCalculator;
        }

        /// <summary>
        /// Calculates the Irr of the given Future Values (cash flows).
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

            decimal previousIr = 5;
            decimal currentIr = 10;
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
    }
}
