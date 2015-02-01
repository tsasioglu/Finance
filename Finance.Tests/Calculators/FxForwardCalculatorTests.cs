using System;
using Finance.Calculators;
using NUnit.Framework;

namespace Finance.Tests.Calculators
{
    [TestFixture]
    public class FxForwardCalculatorTests
    {
        private IFxForwardCalculator _forwardCalculator;

        [SetUp]
        public void SetUp()
        {
            _forwardCalculator = new FxForwardCalculator();
        }

        #region CalculateForwardRate

        [Test]
        public void CalculateForwardRate_ThrowsException_WhenSpotZero()
        {
            const decimal spot = 0m;
            const int     days = 184;
            const decimal baseInterestRate   = 0.06m;
            const decimal quotedInterestRate = 0.02m;
            const DayCountConvention baseDayCount   = DayCountConvention._360;
            const DayCountConvention quotedDayCount = DayCountConvention._360;

            Assert.Throws<ArgumentOutOfRangeException>(() => _forwardCalculator.CalculateForwardRate(spot, days, baseInterestRate, baseDayCount, quotedInterestRate, quotedDayCount));
        }

        [Test]
        public void CalculateForwardRate_ThrowsException_WhenSpotNegative()
        {
            const decimal spot = -1m;
            const int     days = 184;
            const decimal baseInterestRate   = 0.06m;
            const decimal quotedInterestRate = 0.02m;
            const DayCountConvention baseDayCount   = DayCountConvention._360;
            const DayCountConvention quotedDayCount = DayCountConvention._360;

            Assert.Throws<ArgumentOutOfRangeException>(() => _forwardCalculator.CalculateForwardRate(spot, days, baseInterestRate, baseDayCount, quotedInterestRate, quotedDayCount));
        }

        [Test]
        public void CalculateForwardRate_ThrowsException_WhenDaysNegative()
        {
            const decimal spot = 1.5m;
            const int     days = -1;
            const decimal baseInterestRate   = 0.06m;
            const decimal quotedInterestRate = 0.02m;
            const DayCountConvention baseDayCount   = DayCountConvention._360;
            const DayCountConvention quotedDayCount = DayCountConvention._360;

            Assert.Throws<ArgumentOutOfRangeException>(() => _forwardCalculator.CalculateForwardRate(spot, days, baseInterestRate, baseDayCount, quotedInterestRate, quotedDayCount));
        }

        [Test]
        public void CalculateForwardRate_Calculates_WhenSimple()
        {
            const decimal spot = 1.5m;
            const int     days = 184;
            const decimal baseInterestRate   = 0.06m;
            const decimal quotedInterestRate = 0.02m;
            const DayCountConvention baseDayCount   = DayCountConvention._360;
            const DayCountConvention quotedDayCount = DayCountConvention._360;

            decimal fowardRate = _forwardCalculator.CalculateForwardRate(spot, days, baseInterestRate, baseDayCount, quotedInterestRate, quotedDayCount);

            Assert.That(fowardRate, Is.EqualTo(1.4702457956015523932729624837m));
        }

        #endregion CalculateForwardRate

        #region CalculateForwardPoints

        [Test]
        public void CalculateForwardPoints_ThrowsException_WhenSpotZero()
        {
            const decimal spot = 0m;
            const int     days = 184;
            const decimal baseInterestRate   = 0.06m;
            const decimal quotedInterestRate = 0.02m;
            const DayCountConvention baseDayCount  = DayCountConvention._360;
            const DayCountConvention quotedDayCount = DayCountConvention._360;

            Assert.Throws<ArgumentOutOfRangeException>(() => _forwardCalculator.CalculateForwardPoints(spot, days, baseInterestRate, baseDayCount, quotedInterestRate, quotedDayCount));
        }

        [Test]
        public void CalculateForwardPoints_ThrowsException_WhenSpotNegative()
        {
            const decimal spot = -1m;
            const int     days = 184;
            const decimal baseInterestRate   = 0.06m;
            const decimal quotedInterestRate = 0.02m;
            const DayCountConvention baseDayCount   = DayCountConvention._360;
            const DayCountConvention quotedDayCount = DayCountConvention._360;

            Assert.Throws<ArgumentOutOfRangeException>(() => _forwardCalculator.CalculateForwardPoints(spot, days, baseInterestRate, baseDayCount, quotedInterestRate, quotedDayCount));
        }

        [Test]
        public void CalculateForwardPoints_ThrowsException_WhenDaysNegative()
        {
            const decimal spot = 1.5m;
            const int     days = -1;
            const decimal baseInterestRate   = 0.06m;
            const decimal quotedInterestRate = 0.02m;
            const DayCountConvention baseDayCount   = DayCountConvention._360;
            const DayCountConvention quotedDayCount = DayCountConvention._360;

            Assert.Throws<ArgumentOutOfRangeException>(() => _forwardCalculator.CalculateForwardPoints(spot, days, baseInterestRate, baseDayCount, quotedInterestRate, quotedDayCount));
        }

        [Test]
        public void CalculateForwardPoints_Calculates_WhenSimple()
        {
            const decimal spot = 1.5m;
            const int     days = 184;
            const decimal baseInterestRate   = 0.06m;
            const decimal quotedInterestRate = 0.02m;
            const DayCountConvention baseDayCount   = DayCountConvention._360;
            const DayCountConvention quotedDayCount = DayCountConvention._360;

            decimal fowardRate = _forwardCalculator.CalculateForwardPoints(spot, days, baseInterestRate, baseDayCount, quotedInterestRate, quotedDayCount);

            Assert.That(fowardRate, Is.EqualTo(-0.0000297542043984476067270375M));
        }

        #endregion CalculateForwardRate

    }
}
