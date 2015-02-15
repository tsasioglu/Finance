using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace Finance.Data
{
    public class SixYieldCurveProvider : IYieldCurveProvider
    {
        private readonly ISixYieldCurveHtmlProvider _htmlProvider;

        public SixYieldCurveProvider(ISixYieldCurveHtmlProvider htmlProvider)
        {
            _htmlProvider = htmlProvider;
        }

        public YieldPoint[] GetCurve(string currency)
        {
            string html;
            try
            {
                html = _htmlProvider.ProvideHtml(currency);
            }
            catch (WebException)
            {
                return new YieldPoint[0];
            }
            
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            var dataRows = document.DocumentNode.SelectNodes("//tr[@class='row-odd'] | //tr[@class='row-even']");
            List<YieldPoint> yieldPoints = new List<YieldPoint>(dataRows.Count);

            foreach (HtmlNode dataRow in dataRows)
            {
                var rates = dataRow.ChildNodes.Skip(1)
                    .Select(td => td.InnerText)
                    .Where(it => it != "&nbsp;")
                    .Select(Decimal.Parse)
                    .ToArray();

                if (rates.Any())
                    yieldPoints.Add(new YieldPoint(new Tenor(dataRow.FirstChild.InnerText), rates.Average()));
            }

            return yieldPoints.ToArray();
        }
    }

    public interface ISixYieldCurveHtmlProvider
    {
        string ProvideHtml(string currency);
    }

    public class SixYieldCurveHtmlProvider : ISixYieldCurveHtmlProvider
    {
        private const string BaseUrl = 
                    @"http://www.six-swiss-exchange.com/yield_curves_data_en.html?currentChartType=YC&countryCode1=CH&curve1Type=B&countryCode2=EU&curve2Type=B&countryCode3={0}&qs=&benchmarksOnOff=on&iRSOnOff=on&dROnOff=on&name=null&yield=-1&ttm=-1&benchmarks=-1&irs=-1&dr=-1&xcoord=0&ycoord=0";           

        public string ProvideHtml(string currency)
        {
            using (WebClient client = new WebClient())
            {
                var usdUrl = String.Format(BaseUrl, IsoCurrencyToSix(currency));
                return client.DownloadString(usdUrl);
            }
        }

        private static string IsoCurrencyToSix(string isoCurrency)
        {
            return isoCurrency.Substring(0, 2);
        }
    }
}
