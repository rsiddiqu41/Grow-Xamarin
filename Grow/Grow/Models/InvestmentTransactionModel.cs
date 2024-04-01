using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class InvestmentTransactionModel
    {
        //
        // Summary:
        //     The ID of the Investment transaction, unique across all Plaid transactions.
        public string InvestmentTransactionId { get; set; }
        //
        // Summary:
        //     The ID of the account against which this transaction posted.
        public string AccountId { get; set; }
        //
        // Summary:
        //     The ID of the security to which this transaction is related.
        public string SecurityId { get; set; }
        //
        // Summary:
        //     The ISO-8601 posting date for the transaction, or transacted date for pending
        //     transactions.
        public DateTime Date { get; set; }
        //
        // Summary:
        //     The Institution’s description of the transaction.
        public string Name { get; set; }
        //
        // Summary:
        //     The Amount of the security involved in this transaction.
        public decimal Quantity { get; set; }
        //
        // Summary:
        //     The complete value of the transaction.Positive values when cash is debited, e.g.purchases
        //     of stock; negative values when cash is credited, e.g.sales of stock.Treatment
        //     remains the same for cash-only movements unassociated with securities.
        public decimal Amount { get; set; }
        //
        // Summary:
        //     The price of the security at which this transaction occurred.
        public decimal Price { get; set; }
        //
        // Summary:
        //     The combined value of all fees applied to this transaction.
        public decimal? Fees { get; set; }
        //
        // Summary:
        //     Transaction Type
        public string TransactionType { get; set; }
        //
        // Summary:
        //     Transaction Sub-Type
        public string TransactionSubType { get; set; }
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
