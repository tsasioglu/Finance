using System;
using System.Collections.Generic;
using System.Linq;
using Finance.Calculators;
using NUnit.Framework;

namespace Finance.Tests.Calculators
{
    public class StatisticsCalculatorTests
    {
        private StatisticsCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new StatisticsCalculator();
        }

        #region CalculateVariance Tests

        [Test]
        public void CalculateVariance_ThrowsException_WhenNullInputs()
        {
            IEnumerable<decimal> inputs = null;

            Assert.Throws<ArgumentNullException>(() => _calculator.CalculateVariance(inputs));
        }

        [Test]
        public void CalculateVariance_ThrowsException_WhenEmptyInputs()
        {
            IEnumerable<decimal> inputs = Enumerable.Empty<decimal>();

            Assert.Throws<ArgumentException>(() => _calculator.CalculateVariance(inputs));
        }

        [Test]
        public void CalculateVariance_ReturnsZero_WhenSingleInput()
        {
            IEnumerable<decimal> inputs = new[] { 4m };

            decimal variance = _calculator.CalculateVariance(inputs);

            Assert.That(variance, Is.EqualTo(0));
        }

        [Test]
        public void CalculateVariance_ReturnsCorrect_WhenSimple()
        {
            IEnumerable<decimal> inputs = new[] { 83m, 87m, 82m, 89m, 88m };

            decimal variance = _calculator.CalculateVariance(inputs);

            Assert.That(variance, Is.EqualTo(7.76m));
        }

        [Test]
        public void CalculateVariance_ReturnsCorrect_WhenSimple_Samples()
        {
            IEnumerable<decimal> inputs = new[] { 83m, 87m, 82m, 89m, 88m };
            bool sample = true;

            decimal variance = _calculator.CalculateVariance(inputs, sample);

            Assert.That(variance, Is.EqualTo(9.7m));
        }

        #endregion CalculateVariance Tests


        #region CalculateStandardDeviation Tests

        [Test]
        public void CalculateStandardDeviation_ThrowsException_WhenNullInputs()
        {
            IEnumerable<decimal> inputs = null;

            Assert.Throws<ArgumentNullException>(() => _calculator.CalculateStandardDeviation(inputs));
        }

        [Test]
        public void CalculateStandardDeviation_ThrowsException_WhenEmptyInputs()
        {
            IEnumerable<decimal> inputs = Enumerable.Empty<decimal>();

            Assert.Throws<ArgumentException>(() => _calculator.CalculateStandardDeviation(inputs));
        }

        [Test]
        public void CalculateStandardDeviation_ReturnsZero_WhenSingleInput()
        {
            IEnumerable<decimal> inputs = new[] { 4m };

            decimal variance = _calculator.CalculateStandardDeviation(inputs);

            Assert.That(variance, Is.EqualTo(0));
        }

        [Test]
        public void CalculateStandardDeviation_ReturnsCorrect_WhenSimple()
        {
            IEnumerable<decimal> inputs = new[] { 83m, 87m, 82m, 89m, 88m };

            decimal variance = _calculator.CalculateStandardDeviation(inputs);

            Assert.That(variance, Is.EqualTo(2.78567765543682m));
        }

        [Test]
        public void CalculateStandardDeviation_ReturnsCorrect_WhenSimple_Samples()
        {
            IEnumerable<decimal> inputs = new[] { 83m, 87m, 82m, 89m, 88m };
            bool sample = true;

            decimal variance = _calculator.CalculateStandardDeviation(inputs, sample);

            Assert.That(variance, Is.EqualTo(3.11448230047949m));
        }

        #endregion CalculateStandardDeviation Tests
    }
}
