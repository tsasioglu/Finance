using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml.Serialization;

namespace Finance.Data
{
    public interface ISpotProvider
    {
        ExchangeRate[] GetAllSpotRate();
        ExchangeRate   GetSpotRate(string currencyPair);
    }

    public class YahooSpotProvider : ISpotProvider
    {
        private readonly IStreamProvider _yahooStreamProvider;
        
        public YahooSpotProvider(IStreamProvider yahooStreamProvider)
        {
            _yahooStreamProvider = yahooStreamProvider;
        }
        
        public ExchangeRate[] GetAllSpotRate()
        {
            throw new NotImplementedException();
        }

        public ExchangeRate GetSpotRate(string currencyPair)
        {
            if(currencyPair == null)
                throw new ArgumentNullException("currencyPair", "Must not be null");

            if(currencyPair.Trim().Length != 6)
                throw new ArgumentException("Must be of the format e.g. 'USDEUR'", "currencyPair");

            Stream stream = _yahooStreamProvider.GetStream(currencyPair.Trim());
            return Deserialise(stream).Results.Single();
        }

        public Query Deserialise(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Query));
            Query query;
            try
            {
                query = (Query)serializer.Deserialize(stream);
            }
            catch (InvalidOperationException e)
            {
                throw new Exception(String.Format("Unable to parse XML response.{0}{0}{1}", Environment.NewLine, e));
            }

            return query;
        }
    }



    public interface IStreamProvider
    {
        Stream GetStream(string currencyPair);
    }

    public class YahooStreamProvider : IStreamProvider
    {
        private const string YahooAllCurrencyQuery =
            @"https://query.yahooapis.com/v1/public/yql?q=select%20%2a%20from%20yahoo.finance.xchange%20where%20pair%20in%20%28%22USDEUR%22,%20%22USDJPY%22,%20%22USDBGN%22,%20%22USDCZK%22,%20%22USDDKK%22,%20%22USDGBP%22,%20%22USDHUF%22,%20%22USDLTL%22,%20%22USDLVL%22,%20%22USDPLN%22,%20%22USDRON%22,%20%22USDSEK%22,%20%22USDCHF%22,%20%22USDNOK%22,%20%22USDHRK%22,%20%22USDRUB%22,%20%22USDTRY%22,%20%22USDAUD%22,%20%22USDBRL%22,%20%22USDCAD%22,%20%22USDCNY%22,%20%22USDHKD%22,%20%22USDIDR%22,%20%22USDILS%22,%20%22USDINR%22,%20%22USDKRW%22,%20%22USDMXN%22,%20%22USDMYR%22,%20%22USDNZD%22,%20%22USDPHP%22,%20%22USDSGD%22,%20%22USDTHB%22,%20%22USDZAR%22,%20%22USDISK%22%29&env=store://datatables.org/alltableswithkeys";

        private const string YahooBaseCurrencyQuery =
            @"https://query.yahooapis.com/v1/public/yql?q=select%20%2a%20from%20yahoo.finance.xchange%20where%20pair%20in%20%28%22{0}%22%29&env=store://datatables.org/alltableswithkeys";

        private static string BuildCurrencyQuery(string currencyPair)
        {
            return String.Format(YahooBaseCurrencyQuery, currencyPair);
        }

        public Stream GetStream(string currencyPair)
        {
            Stream stream;
            using (WebClient webClient = new WebClient())
            {
                string queryUrl = BuildCurrencyQuery(currencyPair);
                stream = webClient.OpenRead(queryUrl);

                if (stream == null)
                {
                    throw new Exception(String.Format("No stream found at '{0}'", queryUrl));
                }
            }

            return stream;
        }

    }
}
