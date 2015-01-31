using System;

namespace Finance
{
    public interface IFxForwardCalculator
    {
        /// <summary>
        /// Calculates the forward FX rate for the given currencies
        /// </summary>
        /// <param name="spot">Spot rate</param>
        /// <param name="days">Length of contract</param>
        /// <param name="baseInterestRate">Base currency interest rate</param>
        /// <param name="baseDayCountConvention">Base currency day count convention</param>
        /// <param name="quotedInterestRate">Quoted currency interest rate</param>
        /// <param name="quotedDayCountConvention">Quoted currency day count convention</param>
        /// <returns>Forward FX Rate</returns>
        decimal CalculateForwardRate(decimal spot, int days, decimal baseInterestRate, DayCountConvention baseDayCountConvention, decimal quotedInterestRate, DayCountConvention quotedDayCountConvention);

        /// <summary>
        /// Calculates the forward FX basis points for the given currencies
        /// </summary>
        /// <param name="spot">Spot rate</param>
        /// <param name="days">Length of contract</param>
        /// <param name="baseInterestRate">Base currency interest rate</param>
        /// <param name="baseDayCountConvention">Base currency day count convention</param>
        /// <param name="quotedInterestRate">Quoted currency interest rate</param>
        /// <param name="quotedDayCountConvention">Quoted currency day count convention</param>
        /// <returns>Forward Basis Points</returns>
        decimal CalculateForwardPoints(decimal spot, int days, decimal baseInterestRate, DayCountConvention baseDayCountConvention, decimal quotedInterestRate, DayCountConvention quotedDayCountConvention);
    }

    public class FxForwardCalculator : IFxForwardCalculator
    {
        /// <summary>
        /// Calculates the forward FX rate for the given currencies
        /// </summary>
        /// <param name="spot">Spot rate</param>
        /// <param name="days">Length of contract</param>
        /// <param name="baseInterestRate">Base currency interest rate</param>
        /// <param name="baseDayCountConvention">Base currency day count convention</param>
        /// <param name="quotedInterestRate">Quoted currency interest rate</param>
        /// <param name="quotedDayCountConvention">Quoted currency day count convention</param>
        /// <returns>Forward FX Rate</returns>
        public decimal CalculateForwardRate(decimal spot, int days, decimal baseInterestRate, DayCountConvention baseDayCountConvention, decimal quotedInterestRate, DayCountConvention quotedDayCountConvention)
        {
            if (spot <= 0) throw new ArgumentOutOfRangeException("spot", "Must not be null");
            if (days < 0)  throw new ArgumentOutOfRangeException("days", "Must not be null");

            decimal quotedEffectiveRate = quotedInterestRate * days / (int)quotedDayCountConvention;
            decimal baseEffectiveRate   = baseInterestRate   * days / (int)baseDayCountConvention;

            return spot * (1 + quotedEffectiveRate)/(1 + baseEffectiveRate);
        }

        /// <summary>
        /// Calculates the forward FX basis points for the given currencies
        /// </summary>
        /// <param name="spot">Spot rate</param>
        /// <param name="days">Length of contract</param>
        /// <param name="baseInterestRate">Base currency interest rate</param>
        /// <param name="baseDayCountConvention">Base currency day count convention</param>
        /// <param name="quotedInterestRate">Quoted currency interest rate</param>
        /// <param name="quotedDayCountConvention">Quoted currency day count convention</param>
        /// <returns>Forward Basis Points</returns>
        public decimal CalculateForwardPoints(decimal spot, int days, decimal baseInterestRate, DayCountConvention baseDayCountConvention, decimal quotedInterestRate, DayCountConvention quotedDayCountConvention)
        {
            decimal forwardRate = CalculateForwardRate(spot, days, baseInterestRate, baseDayCountConvention,quotedInterestRate, quotedDayCountConvention);
            return (forwardRate - spot) / 1000;
        }
    }

    public enum DayCountConvention
    {
        _360 = 360,
        _365 = 365,
    }
}
