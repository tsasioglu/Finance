using System;
using NUnit.Framework;

namespace Finance.Tests
{
    [TestFixture]
    public class TenorTestscs
    {
        [Test]
        public void Constructor_ThrowsException_WhenNullName()
        {
            string name = null;

            Assert.Throws<ArgumentNullException>(() => new Tenor(name));
        }

        [Test]
        public void Constructor_ThrowsException_WhenEmptyName()
        {
            string name = String.Empty;

            Assert.Throws<ArgumentException>(() => new Tenor(name));
        }

        [Test]
        public void Constructor_ThrowsException_WhenWhiteSpaceName()
        {
            const string name = "\t\n";

            Assert.Throws<ArgumentException>(() => new Tenor(name));
        }

        [Test]
        public void Constructor_ThrowsException_WhenDoubleUnits()
        {
            const string name = "5DD";

            Assert.Throws<ArgumentException>(() => new Tenor(name));
        }

        [Test]
        public void Constructor_ThrowsException_WhenNegative()
        {
            const string name = "-10W";

            Assert.Throws<ArgumentException>(() => new Tenor(name));
        }
        
        [Test]
        public void Constructor_ThrowsException_WhenNonsense()
        {
            const string name = "banana";

            Assert.Throws<ArgumentException>(() => new Tenor(name));
        }

        [Test]
        public void Constructor_ThrowsException_WhenExcplicitUnits()
        {
            const string name = "5Days";

            Assert.Throws<ArgumentException>(() => new Tenor(name));
        }

        [Test]
        public void Constructor_Returns_WhenValidDays()
        {
            const string name = "5D";

            var tenor = new Tenor(name);

            Assert.That(tenor.Unit,  Is.EqualTo(TenorUnit.Days));
            Assert.That(tenor.Value, Is.EqualTo(5));
            Assert.That(tenor.Name,  Is.EqualTo(name));
        }

        [Test]
        public void Constructor_Returns_WhenValidWeeks()
        {
            const string name = "100W";

            var tenor = new Tenor(name);

            Assert.That(tenor.Unit,  Is.EqualTo(TenorUnit.Weeks));
            Assert.That(tenor.Value, Is.EqualTo(100));
            Assert.That(tenor.Name,  Is.EqualTo(name));
        }
        
        [Test]
        public void Constructor_Returns_WhenValidMonths()
        {
            const string name = "41M";

            var tenor = new Tenor(name);

            Assert.That(tenor.Unit,  Is.EqualTo(TenorUnit.Months));
            Assert.That(tenor.Value, Is.EqualTo(41));
            Assert.That(tenor.Name,  Is.EqualTo(name));
        }

        [Test]
        public void Constructor_Returns_WhenValidYears()
        {
            const string name = "6Y";

            var tenor = new Tenor(name);

            Assert.That(tenor.Unit,  Is.EqualTo(TenorUnit.Years));
            Assert.That(tenor.Value, Is.EqualTo(6));
            Assert.That(tenor.Name,  Is.EqualTo(name));
        }
    }
}
