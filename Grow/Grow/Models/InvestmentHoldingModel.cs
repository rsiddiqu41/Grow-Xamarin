using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class InvestmentHoldingModel
    {
        //
        // Summary:
        //     The Plaid Account ID associated with the holding.
        public string AccountId { get; set; }
        //
        // Summary:
        //     The Plaid Security ID of the security associated with the holding.
        public string SecurityId { get; set; }
        //
        // Summary:
        //     The last price given by the institution for this security.
        public decimal InstitutionPrice { get; set; }
        //
        // Summary:
        //     The date at which institution_price was current.
        public DateTime? InstitutionPriceAsOf { get; set; }
        //
        // Summary:
        //     The value of the holding, as stated by the institution.
        public decimal InstitutionValue { get; set; }
        //
        // Summary:
        //     The total cost of acquiring the holding.
        public decimal? CostBasis { get; set; }
        //
        // Summary:
        //     The total quantity of the asset held, as reported by the financial institution.
        public decimal Quantity { get; set; }
        //
        // Summary:
        //     The ISO-4217 currency code of the holding. Always null if unofficial_currency_code
        //     is non-null.
        public string IsoCurrencyCode { get; set; }
        //
        // Summary:
        //     The unofficial currency of the holding. Always null if iso_currency_code is non-null.
        //     This is present if the price is denominated in an unrecognized currency e.g.
        //     Bitcoin, rewards points.
        public string UnofficialCurrencyCode { get; set; }
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
}
