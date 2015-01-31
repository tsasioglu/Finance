using System;
using NUnit.Framework;

namespace Finance.Tests
{
    [TestFixture]
    public class PvCalculatorTests
    {
        private PvCalculator _pvCalculator;

        [SetUp]
        public void SetUp()
        {
            _pvCalculator = new PvCalculator();
        }

        #region CalculatePv

        [Test]
        public void CalculatePv_ThrowsException_WhenDiscountPerdiodsNegative()
        {
            const decimal futureValue     = 0;
            const int     discountPeriods = -1;
            const decimal interestRate    = 0.02m;

            Assert.Throws<ArgumentOutOfRangeException>(() => _pvCalculator.CalculatePv(futureValue, discountPeriods, interestRate));
        }
        
        [Test]
        public void CalculatePv_Calculates_WhenFutureValue_Zero()
        {
            const decimal futureValue     = 0;
            const int     discountPeriods = 2;
            const decimal interestRate    = 0.02m;

            decimal pv = _pvCalculator.CalculatePv(futureValue, discountPeriods, interestRate);

            Assert.That(pv, Is.EqualTo(0m));
        }

        [Test]
        public void CalculatePv_Calculates_WhenDiscountPeriods_Zero()
        {
            const decimal futureValue     = 100;
            const int     discountPeriods = 0;
            const decimal interestRate    = 0.02m;

            decimal pv = _pvCalculator.CalculatePv(futureValue, discountPeriods, interestRate);

            Assert.That(pv, Is.EqualTo(100m));
        }

        [Test]
        public void CalculatePv_Calculates_WhenInterestRate_Zero()
        {
            const decimal futureValue     = 100;
            const int     discountPeriods = 2;
            const decimal interestRate    = 0;

            decimal pv = _pvCalculator.CalculatePv(futureValue, discountPeriods, interestRate);

            Assert.That(pv, Is.EqualTo(100m));
        }

        [Test]
        public void CalculatePv_Calculates_WhenSimple()
        {
            const decimal futureValue     = 1000;
            const int     discountPeriods = 5;
            const decimal interestRate    = 0.1m;

            decimal pv = _pvCalculator.CalculatePv(futureValue, discountPeriods, interestRate);

            Assert.That(pv, Is.EqualTo(620.92132305915517444784571347m));
        }

        [Test]
        public void CalculatePv_Calculates_WhenComplex()
        {
            const decimal futureValue     = 42000000;
            const int     discountPeriods = 104;
            const decimal interestRate    = 0.035677m;

            decimal pv = _pvCalculator.CalculatePv(futureValue, discountPeriods, interestRate);

            Assert.That(pv, Is.EqualTo(1096273.1975538904662465661194m));
        }

        #endregion CalculatePv

        #region CalculateNPv

        [Test]
        public void CalculateNPv_ThrowsException_WhenNullFutureValues()
        {
            decimal[]     futureValues     = null;
            const decimal interestRate     = 0.035677m;
            
            Assert.Throws<ArgumentNullException>(() => _pvCalculator.CalculateNpv(futureValues, interestRate));
        }

        [Test]
        public void CalculateNPv_ThrowsException_WhenNoFutureValuesGiven()
        {
            decimal[]     futureValues     = new decimal[0];
            const decimal interestRate     = 0.035677m;

            Assert.Throws<ArgumentException>(() => _pvCalculator.CalculateNpv(futureValues, interestRate));
        }

        [Test]
        public void CalculateNPv_Calculates_WhenSimple()
        {
            decimal[]     futureValues = { 110m, 121m };
            const decimal interestRate = 0.1m;

            decimal npv = _pvCalculator.CalculateNpv(futureValues, interestRate);

            Assert.That(npv, Is.EqualTo(200));
        }

        [Test]
        public void CalculateNPv_Calculates_WhenComplex()
        {
            decimal[]     futureValues = { -100000m, 10000m, 10000m, 10000m, -45456m, 10000m, 10000m, 17863m, -23789m, 10000m, 10000m, 10000m, 10000m };
            const decimal interestRate = 0.1m;

            decimal npv = _pvCalculator.CalculateNpv(futureValues, interestRate);

            Assert.That(npv, Is.EqualTo(-74061.937761836070005194626887M));
        }
        
        #endregion CalculateNPv
    }
}
