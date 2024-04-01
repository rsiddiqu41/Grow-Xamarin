using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Grow.Core.SystemConfiguration
{
    [Serializable()]
    public class FinanceQuotesModel
    {
        [XmlElement("Author")]
        public string Author { get; set; }

        [XmlElement("Quote")]
        public string Quote { get; set; }

        public FinanceQuotesModel() { }
    }

    [Serializable()]
    [XmlRoot("QuotesCollection")]
    public class FinancialQuote
    {
        [XmlElement("FinanceQuote")]
        public List<FinanceQuotesModel> FinanceQuotesList { get; set; }

        public FinancialQuote()
        {
            FinanceQuotesList = new List<FinanceQuotesModel>();
        }
    }
}
