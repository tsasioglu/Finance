using System;
using Finance.Calculators;
using NUnit.Framework;

namespace Finance.Tests.Calculators
{
    [TestFixture]
    public class PolationCalculatorTests
    {
        private PolationCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new PolationCalculator();
        }

        #region StraightLineIntepolate

        [Test]
        public void StraightLineIntepolate_Calculates_WhenSameDays()
        {
            const decimal firstDays  = 30m;
            const decimal firstRate  = 0.08m;
            const decimal secondDays = 30m;
            const decimal secondRate = 0.085m;
            const decimal targetDays = 39m;

            Assert.Throws<ArgumentException>(() => _calculator.StraightLineIntepolate(firstDays, firstRate, secondDays, secondRate, targetDays));
        }

        [Test]
        public void StraightLineIntepolate_Calculates_WhenSameRate()
        {
            const decimal firstDays  = 30m;
            const decimal firstRate  = 0.08m;
            const decimal secondDays = 61m;
            const decimal secondRate = 0.08m;
            const decimal targetDays = 39m;

            decimal targetRate = _calculator.StraightLineIntepolate(firstDays, firstRate, secondDays, secondRate, targetDays);

            Assert.That(targetRate, Is.EqualTo(0.08m));
        }

        [Test]
        public void StraightLineIntepolate_Calculates_WhenSimple()
        {
            const decimal firstDays  = 30m;
            const decimal firstRate  = 0.08m;
            const decimal secondDays = 61m;
            const decimal secondRate = 0.085m;
            const decimal targetDays = 39m;

            decimal targetRate = _calculator.StraightLineIntepolate(firstDays, firstRate, secondDays, secondRate, targetDays);

            Assert.That(targetRate, Is.EqualTo(0.0814516129032258064516129034m));
        }

        #endregion StraightLineIntepolate

        #region LogarithmicIntepolate

        [Test]
        public void LogarithmicIntepolate_Calculates_WhenSameDays()
        {
            const decimal firstDays  = 30m;
            const decimal firstRate  = 0.08m;
            const decimal secondDays = 30m;
            const decimal secondRate = 0.085m;
            const decimal targetDays = 39m;

            Assert.Throws<ArgumentException>(() => _calculator.LogarithmicIntepolate(firstDays, firstRate, secondDays, secondRate, targetDays));
        }

        [Test]
        public void LogarithmicIntepolate_Calculates_WhenSameRate()
        {
            const decimal firstDays  = 30m;
            const decimal firstRate  = 0.08m;
            const decimal secondDays = 61m;
            const decimal secondRate = 0.08m;
            const decimal targetDays = 39m;

            decimal targetRate = _calculator.LogarithmicIntepolate(firstDays, firstRate, secondDays, secondRate, targetDays);

            Assert.That(targetRate, Is.EqualTo(0.08m));
        }

        [Test]
        public void LogarithmicIntepolate_Calculates_WhenSimple()
        {
            const decimal firstDays  = 61m;
            const decimal firstRate  = 0.075m;
            const decimal secondDays = 92m;
            const decimal secondRate = 0.076m;
            const decimal targetDays = 73m;

            decimal targetRate = _calculator.LogarithmicIntepolate(firstDays, firstRate, secondDays, secondRate, targetDays);

            Assert.That(targetRate, Is.EqualTo(7.53855263288879m));
        }

        #endregion LogarithmicIntepolate
    }
}
