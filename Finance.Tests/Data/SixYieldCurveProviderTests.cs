using System.IO;
using Finance.Data;
using Moq;
using NUnit.Framework;

namespace Finance.Tests.Data
{
    [TestFixture]
    public class SixYieldCurveProviderTests
    {
        private SixYieldCurveProvider           _curveProvider;
        private Mock<ISixYieldCurveHtmlProvider> _htmlProvider;
        
        [SetUp]
        public void SetUp()
        {
            _htmlProvider = new Mock<ISixYieldCurveHtmlProvider>();
            _htmlProvider.Setup(hp => hp.ProvideHtml(It.IsAny<string>()))
                         .Returns(File.ReadAllText("../../Data/six_usd_test.html"));

            _curveProvider = new SixYieldCurveProvider(_htmlProvider.Object);
        }

        [Test]
        public void GetCurve_ReturnsCorrectNumberOfPoints()
        {
            var curve = _curveProvider.GetCurve("usd");

            Assert.That(curve.Length, Is.EqualTo(33));
        }

        [Test]
        public void GetCurve_ReturnsValidPoints()
        {
            var curve = _curveProvider.GetCurve("usd");

            foreach (YieldPoint point in curve)
            {
                Assert.That(point.Tenor, Is.Not.Null);
                Assert.That(point.Yield, Is.Not.Null);
            }
        }
    }
}
