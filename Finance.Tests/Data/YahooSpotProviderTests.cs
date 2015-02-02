using System;
using System.IO;
using Finance.Data;
using Moq;
using NUnit.Framework;

namespace Finance.Tests.Data
{
    [TestFixture]
    public class YahooSpotProviderTests
    {
        private YahooSpotProvider _yahooSpotProvider;

        private Mock<IStreamProvider> _yahooStreamProvider;

        [SetUp]
        public void SetUp()
        {
            _yahooStreamProvider = new Mock<IStreamProvider>();

            _yahooStreamProvider.Setup(sp => sp.GetStream(It.IsAny<string>()))
                                .Returns(() => new StreamReader(@"../../YahooSingleCurrencyTestData.xml").BaseStream);

            _yahooSpotProvider = new YahooSpotProvider(_yahooStreamProvider.Object);
        }

        [Test]
        public void GetSpotRate_ThrowsException_WhenNullCurrencyPair()
        {
            string currencyPair = null;

            Assert.Throws<ArgumentNullException>(() => _yahooSpotProvider.GetSpotRate(currencyPair));
        }

        [Test]
        public void GetSpotRate_ThrowsException_WhenEmptyCurrencyPair()
        {
            string currencyPair = String.Empty;

            Assert.Throws<ArgumentException>(() => _yahooSpotProvider.GetSpotRate(currencyPair));
        }

        [Test]
        public void GetSpotRate_ThrowsException_WhenIncorrectCurrencyPair()
        {
            const string currencyPair = "EUR";

            Assert.Throws<ArgumentException>(() => _yahooSpotProvider.GetSpotRate(currencyPair));
        }

        [Test]
        public void GetSpotRate_ReturnsNonNull_WhenValid()
        {
            ExchangeRate exchangeRate = _yahooSpotProvider.GetSpotRate("USDEUR");

            Assert.That(exchangeRate, Is.Not.Null);
        }

        [Test]
        public void GetSpotRate_ReturnsAllPropertiesValies_WhenValid()
        {
            ExchangeRate exchangeRate = _yahooSpotProvider.GetSpotRate("USDEUR");

            Assert.That(exchangeRate.Id,   Is.EqualTo("USDEUR"));
            Assert.That(exchangeRate.Name, Is.EqualTo("USD to EUR"));
            Assert.That(exchangeRate.Ask,  Is.EqualTo(0.8867m));
            Assert.That(exchangeRate.Rate, Is.EqualTo(0.8861m));
            Assert.That(exchangeRate.Bid,  Is.EqualTo(0.8856m));
            Assert.That(exchangeRate.Date, Is.EqualTo("1/31/2015"));
            Assert.That(exchangeRate.Time, Is.EqualTo("7:55am"));
        }
        
    }
}
