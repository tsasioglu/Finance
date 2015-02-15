using System;

namespace Finance.Calculators
{
    public class PolationCalculator
    {
        /// <summary>
        /// Uses straight line interpolation to calculate the estimated value between two points.
        /// </summary>
        /// <param name="x1">First known value in the first dimension</param>
        /// <param name="y1">First known value in the second dimention</param>
        /// <param name="x2">Second known value in the first dimension</param>
        /// <param name="y2">Second known value in the second dimension</param>
        /// <param name="xTarget">Value between x1 and x2 in the first dimension for which we wish to find the corresponding value in the second dimension</param>
        /// <returns>Value between y1 and y2 in the second dimension which corresponds to xTarget</returns>
        public decimal StraightLineIntepolate(decimal x1, decimal y1, decimal x2, decimal y2, decimal xTarget)
        {
            if (x1 == x2)
                throw new ArgumentException(String.Format("Distinct reference points required. Only '{0}' provided. ", x1));
            if (y1 == y2)
                return y1;

            decimal gradient   = (y2 - y1)/(x2 - x1);
            decimal additional = (xTarget - x1);

            return y1 + gradient * additional;
        }

        /// <summary>
        /// Uses logarithmic (i.e. geometric) interpolation to calculate the estimated value between two points.
        /// </summary>
        /// <param name="x1">First known value in the first dimension</param>
        /// <param name="y1">First known value in the second dimention</param>
        /// <param name="x2">Second known value in the first dimension</param>
        /// <param name="y2">Second known value in the second dimension</param>
        /// <param name="xTarget">Value between x1 and x2 in the first dimension for which we wish to find the corresponding value in the second dimension</param>
        /// <returns>Value between y1 and y2 in the second dimension which corresponds to xTarget</returns>
        public decimal LogarithmicIntepolate(decimal x1, decimal y1, decimal x2, decimal y2, decimal xTarget)
        {
            if (x1 == x2)
                throw new ArgumentException(String.Format("Distinct reference points required. Only '{0}' provided. ", x1));
            if (y1 == y2)
                return y1;

            decimal gradient  = (xTarget - x1) / (x2 - x1);
            double additional = (Math.Log((double) y2 *100) - Math.Log((double) y1 * 100));
            var power         = (Math.Log((double) y1 * 100) + (double) (gradient * (decimal) additional));

            return (decimal) Math.Pow(Math.E, power); 
        }
    }
}
