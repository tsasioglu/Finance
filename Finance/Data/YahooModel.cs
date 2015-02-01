using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Finance.Data
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "query")]
    public class Query
    {
        [XmlArray("results")]
        [XmlArrayItem("rate", IsNullable = false)]
        public ExchangeRate[] Results { get; set; }

        [XmlAttribute(AttributeName = "count", Form = XmlSchemaForm.Qualified, Namespace = "http://www.yahooapis.com/v1/base.rng")]
        public byte Count { get; set; }

        [XmlAttribute(AttributeName = "created", Form = XmlSchemaForm.Qualified, Namespace = "http://www.yahooapis.com/v1/base.rng")]
        public DateTime Created { get; set; }

        [XmlAttribute(AttributeName = "lang", Form = XmlSchemaForm.Qualified, Namespace = "http://www.yahooapis.com/v1/base.rng")]
        public string Lang { get; set; }
    }

    [XmlType(AnonymousType = true, TypeName = "rate")]
    public class ExchangeRate
    {
        [XmlAttribute(AttributeName = "id")]
        public string  Id { get; set; }
        
        public string  Name { get; set; }

        public decimal Rate { get; set; }

        public string  Date { get; set; }

        public string  Time { get; set; }

        public decimal Ask { get; set; }

        public decimal Bid { get; set; }
    }
}
