using System;
using System.Collections.Generic;
using System.Linq;

namespace Finance.Calculators
{
    public class StatisticsCalculator
    {
        /// <summary>
        ///  Calculates the Variance of the set of numbers given
        /// </summary>
        /// <param name="values">Set of values to calculate the variance of</param>
        /// <param name="sample">True if values is a sample, False if values is the entire population</param>
        /// <returns>Variance</returns>
        public decimal CalculateVariance(IEnumerable<decimal> values, bool sample = false)
        {
            if (values == null) throw new ArgumentNullException("values", "Must not be null. ");

            decimal[] inputs = values.ToArray();

            if (!inputs.Any()) throw new ArgumentException("Must not be empty. ", "values");


            decimal mean = inputs.Average();
            double sumOfDifferences = inputs.Sum(x => Math.Pow((double) (mean - x), 2));
            int numberOfValuesDivisor = sample ? inputs.Length - 1 : inputs.Length;
            return (decimal) (sumOfDifferences / numberOfValuesDivisor);
        }

        /// <summary>
        ///  Calculates the Standard Deviation (sqrt. Variance) of the set of numbers given
        /// </summary>
        /// <param name="values">Set of values to calculate the variance of</param>
        /// <param name="sample">True if values is a sample, False if values is the entire population</param>
        /// <returns>Standard Deviation (sqrt. Variance)</returns>
        public decimal CalculateStandardDeviation(IEnumerable<decimal> values, bool sample = false)
        {
            return (decimal) Math.Sqrt((double) CalculateVariance(values, sample));
        }

        /// <summary>
        /// Calculates the annualised historic volatility of the values given
        /// </summary>
        /// <param name="values">List of observed values</param>
        /// <param name="annualFrequency">Number of given values which span a year</param>
        /// <param name="sample">True if values is a sample. False if values is the entire population</param>
        /// <returns>Annualised historic volatility</returns>
        public decimal CalculateHistoricVolatility(IList<decimal> values, decimal annualFrequency)
        {
            if (values == null) throw new ArgumentNullException("values", "Must not be null. ");

            decimal[] inputs = values.ToArray();

            if (!inputs.Any()) throw new ArgumentException("Must not be empty. ", "values");
            
            IEnumerable<decimal> logDifferences = inputs.Zip(inputs.Skip(1), (start, end) => (decimal) Math.Log((double) (end / start)));
            decimal sd = CalculateStandardDeviation(logDifferences, true);
            return sd * (decimal) Math.Sqrt((double) annualFrequency);
        }
    }
}
