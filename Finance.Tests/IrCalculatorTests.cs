using System;
using NUnit.Framework;

namespace Finance.Tests
{
    [TestFixture]
    public class IrCalculatorTests
    {
        private IIrCalculator _irCalculator;

        [SetUp]
        public void SetUp()
        {
            _irCalculator = new IrCalculator(new PvCalculator());
        }

        #region CalculateIrr

        [Test]
        public void CalculateIrr_ThrowsException_WhenNullFutureValues()
        {
            decimal[] futureValues = null;
            const decimal pv = 300m;

            Assert.Throws<ArgumentNullException>(() => _irCalculator.CalculateIrr(futureValues, pv));
        }

        [Test]
        public void CalculateIrr_ThrowsException_WhenNoFutureValuesGiven()
        {
            decimal[] futureValues = new decimal[0];
            const decimal pv = 300m;

            Assert.Throws<ArgumentException>(() => _irCalculator.CalculateIrr(futureValues, pv));
        }
        
        [Test]
        public void CalulcationIrr_Calculates_WhenSimple()
        {
            decimal irr = _irCalculator.CalculateIrr(new[] { 1100m, 1210m }, -2000m);

            Assert.That(irr, Is.InRange(10m - 0.00001m, 10m + 0.00001m));
        }

        [Test]
        public void CalulcationIrr_Calculates_WhenComplex()
        {
            decimal irr = _irCalculator.CalculateIrr(new[] { 10000m, -12000m, 13232m, 12323m }, -20450m);

            Assert.That(irr, Is.InRange(4.5975867663677017941645953688m - 0.00001m, 4.5975867663677017941645953688m + 0.00001m));
        }

        #endregion CalculateIrr


        #region CalculateTimeWeightedReturn

        [Test]
        public void CalculateTimeWeightedReturn_ThrowsException_WhenNullStart()
        {
            decimal[] start = null;
            decimal[] end   = { 100m, 150, 200m };

            Assert.Throws<ArgumentNullException>(() => _irCalculator.CalculateTimeWeightedReturn(start, end));
        }

        [Test]
        public void CalculateTimeWeightedReturn_ThrowsException_WhenNullEnd()
        {
            decimal[] start = { 100m, 150, 200m };
            decimal[] end   = null;

            Assert.Throws<ArgumentNullException>(() => _irCalculator.CalculateTimeWeightedReturn(start, end));
        }

        [Test]
        public void CalculateTimeWeightedReturn_ThrowsException_WhenUnequalPeriods()
        {
            decimal[] start = { 100m, 150, 200m };
            decimal[] end   = { 100m, 150 };

            Assert.Throws<ArgumentException>(() => _irCalculator.CalculateTimeWeightedReturn(start, end));
        }

        [Test]
        public void CalculateTimeWeightedReturn_ThrowsException_WhenNoPeriods()
        {
            decimal[] start = new decimal[0];
            decimal[] end   = new decimal[0];

            Assert.Throws<ArgumentException>(() => _irCalculator.CalculateTimeWeightedReturn(start, end));
        }

        [Test]
        public void CalculateTimeWeightedReturn_Calculates_WhenSimple()
        {
            decimal[] start = { 500m,  1500m };
            decimal[] end   = { 1000m, 1125m };

            decimal rate = _irCalculator.CalculateTimeWeightedReturn(start, end);

            Assert.That(rate, Is.EqualTo(50m));
        }

        [Test]
        public void CalculateTimeWeightedReturn_Calculates_WhenComplex()
        {
            decimal[] start = { 1000m, 2400m, 2500m };
            decimal[] end   = { 1200m, 2550m, 2600m };

            decimal rate = _irCalculator.CalculateTimeWeightedReturn(start, end);

            Assert.That(rate, Is.EqualTo(32.6m));
        }

        #endregion CalculateTimeWeightedReturn
    }
}
