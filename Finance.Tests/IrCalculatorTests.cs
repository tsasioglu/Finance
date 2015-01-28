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
    }
}
