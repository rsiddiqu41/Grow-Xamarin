using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class InvestmentSecuritiesModel
    {
        //
        // Summary:
        //     The ISO-4217 currency code of the holding. Always null if unofficial_currency_code
        //     is non-null.

        public string IsoCurrencyCode { get; set; }
        //
        // Summary:
        //     Date for which close_price is accurate. Always null if close_price_as_of is null.

        public DateTime? ClosePriceAsOf { get; set; }
        //
        // Summary:
        //     Price of the security at the close of the previous trading session. null for
        //     non-public securities.

        public decimal? ClosePrice { get; set; }
        //
        // Summary:
        //     The security type of the holding.

        public SecurityType Type { get; set; }
        //
        // Summary:
        //     Indicates that a security is a highly liquid asset, and can be treated as cash.

        public bool IsCashEquivalent { get; set; }
        //
        // Summary:
        //     A descriptive name for the security, suitable for display.

        public string Name { get; set; }
        //
        // Summary:
        //     The unofficial currency of the holding. Always null if iso_currency_code is non-null.
        //     This is present if the price is denominated in an unrecognized currency e.g.
        //     Bitcoin, rewards points.

        public string UnofficialCurrencyCode { get; set; }
        //
        // Summary:
        //     The security’s trading symbol for publicly traded securities, and otherwise a
        //     short identifier if available.

        public string TickerSymbol { get; set; }
        //
        // Summary:
        //     If institution_security_id is present, this field indicates the Plaid institution_id
        //     of the institution referenced.

        public string InstitutionId { get; set; }
        //
        // Summary:
        //     An identifier that is meaningful within the institution’s own schema.

        public string InstitutionSecurityId { get; set; }
        //
        // Summary:
        //     9-character CUSIP, an identifier assigned to North American securities.

        public string Cusip { get; set; }
        //
        // Summary:
        //     7-character SEDOL, an identifier assigned to securities in the UK.

        public string Sedol { get; set; }
        //
        // Summary:
        //     12-character ISIN, a globally unique securities identifier. Will be present when
        //     either CUSIP or SEDOL are present.

        public string Isin { get; set; }
        //
        // Summary:
        //     A unique Plaid identifier for the holding.

        public string SecurityId { get; set; }
        //
        // Summary:
        //     In certain cases, Plaid will provide the ID of another security whose performance
        //     resembles this security—typically when the original security has low volume,
        //     or when a private security can be modeled with a publicly traded security.

        public string ProxySecurityId { get; set; }
        //
        // Summary:
        //     Gets the currency code from either IsoCurrencyCode or UnofficialCurrencyCode.
        //     If non-null, IsoCurrencyCode is returned, else if non-null, UnofficialCurrencyCode,
        //     else null is returned.
        //
        // Value:
        //     Either available currency code.
        public string CurrencyCode { get; set; }
    }

    public enum SecurityType
    {
        //
        // Summary:
        //     Cash, currency, and money market funds.
        Cash = 0,
        //
        // Summary:
        //     Options, warrants, and other derivative instruments.
        Derivative = 1,
        //
        // Summary:
        //     Domestic and foreign equities.
        Equity = 2,
        //
        // Summary:
        //     Multi-asset investment funds traded on exchanges.
        Etf = 3,
        //
        // Summary:
        //     Bonds and CDs.
        FixedIncome = 4,
        //
        // Summary:
        //     Loans and loan receivables.
        Loan = 5,
        //
        // Summary:
        //     Open- and closed-end vehicles pooling funds of multiple investors.
        MutualFund = 6,
        //
        // Summary:
        //     Any unknown or unclassified investment vehicle.
        Other = 7
    }
}
