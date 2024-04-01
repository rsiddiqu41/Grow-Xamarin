using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class TransactionModel
    {
        //
        // Summary:
        //     Gets or sets the name of the account owner. This property is not typically populated
        //     and only relevant when dealing with sub-accounts.
        //
        // Value:
        //     The account owner.
        public string AccountOwner { get; set; }

        //
        // Summary:
        //     Gets or sets the id of a posted transaction's associated pending transaction.
        //
        // Value:
        //     The pending transaction identifier.
        public string PendingTransactionId { get; set; }

        //
        // Summary:
        //     Gets or sets a value indicating whether this Acklann.Plaid.Entity.Transaction
        //     is pending or unsettled. Pending transaction details (name, Acklann.Plaid.Entity.Transaction.TransactionType,
        //     Acklann.Plaid.Entity.Transaction.Amount) may change before they are settled.
        //
        // Value:
        //     true if pending; otherwise, false.
        public bool Pending { get; set; }

        //
        // Summary:
        //     Gets or sets the reference number.
        //
        // Value:
        //     The reference number.
        public string ReferenceNumber { get; set; }
        //
        // Summary:
        //     Gets or sets the PPD identifier.
        //
        // Value:
        //     The PPD identifier.
        public string PPDId { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the payee.
        //
        // Value:
        //     The name of the payee.
        public string PayeeName { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the payer.
        //
        // Value:
        //     The name of the payer.
        public string Payer { get; set; }
        //
        // Summary:
        //     Gets or sets the payment processor.
        //
        // Value:
        //     The payment processor.
        public string PaymentProcessor { get; set; }
        //
        // Summary:
        //     Gets or sets the reason. The payer-supplied description of the transfer.
        //
        // Value:
        //     The reason.
        public string Reason { get; set; }
        //
        // Summary:
        //     Gets or sets the by order of. The party initiating a wire transfer. Will be null if the transaction is not a wire transfer.
        //
        // Value:
        //     The by order of.
        public string ByOrderOf { get; set; }

        //
        // Summary:
        //     Gets or sets the address of the merchant's location. Typically null.
        //
        // Value:
        //     The location details.
        public string Address { get; set; }
        //
        // Summary:
        //     Gets or sets the city.
        //
        // Value:
        //     The city.
        public string City { get; set; }
        //
        // Summary:
        //     Gets or sets the state.
        //
        // Value:
        //     The state.
        public string State { get; set; }
        //
        // Summary:
        //     Gets or sets the zip.
        //
        // Value:
        //     The zip.
        public string Zip { get; set; }
        //
        // Summary:
        //     Gets or sets the latitude (x-coordinate).
        //
        // Value:
        //     The latitude.
        public double? Latitude { get; set; }
        //
        // Summary:
        //     Gets or sets the longitude (y-coordinate).
        //
        // Value:
        //     The longitude.
        public double? Longitude { get; set; }
        //
        // Summary:
        //     Gets or sets the alpha-2 country code where the transaction occurred.
        //
        // Value:
        //     Country code
        public string Country { get; set; }
        //
        // Summary:
        //     Gets or sets the store number.
        //
        // Value:
        //     The store number.
        public string StoreNumber { get; set; }

        //
        // Summary:
        //     Gets or sets the date that the transaction was authorized.
        //
        // Value:
        //     The transaction authorized date. Optional
        public DateTime? AuthorizedDate { get; set; }

        //
        // Summary:
        //     Gets or sets the date of the transaction.
        //
        // Value:
        //     The date.
        //
        // Remarks:
        //     For pending transactions, Plaid returns the date the transaction occurred; for
        //     posted transactions, Plaid returns the date the transaction posts.
        public DateTime Date { get; set; }

        //
        // Summary:
        //     Gets the currency code from either IsoCurrencyCode or UnofficialCurrencyCode.
        //     If non-null, IsoCurrencyCode is returned, else if non-null, UnofficialCurrencyCode,
        //     else null is returned.
        //
        // Value:
        //     Either available currency code.
        public string CurrencyCode { get; set; }

        //
        // Summary:
        //     The unofficial currency code associated with the transaction. Always null if
        //     iso_currency_code is non-null.
        //
        // Value:
        //     The unofficial currency code.
        public string UnofficialCurrencyCode { get; set; }

        //
        // Summary:
        //     The ISO currency code of the transaction, either USD or CAD. Always null if unofficial_currency_code
        //     is non-null.
        //
        // Value:
        //     The ISO currency code.
        public string IsoCurrencyCode { get; set; }

        //
        // Summary:
        //     Gets or sets the settled dollar value. Positive values when money moves out of
        //     the account; negative values when money moves in. For example, purchases are
        //     positive; credit card payments, direct deposits, refunds are negative.
        //
        // Value:
        //     The amount.
        public decimal Amount { get; set; }

        //
        // Summary:
        //     The channel used to make a payment. Possible values are: online, in store, other.
        //     This field will replace the transaction_type field
        public string PaymentChannel { get; set; }

        //
        // Summary:
        //     Gets or sets the id of the category to which this transaction belongs. See https://plaid.com/docs/api/#categories.
        //
        // Value:
        //     The category identifier.
        public string CategoryId { get; set; }
        //
        // Summary:
        //     Gets or sets the hierarchical array of the categories to which this transaction.
        //
        // Value:
        //     The categories.
        public string[] Categories { get; set; }
        //
        // Summary:
        //     Gets or sets the id of the account in which this transaction occurred.
        //
        // Value:
        //     The account identifier.
        public string AccountId { get; set; }
        //
        // Summary:
        //     Gets or sets the unique id of the transaction.
        //
        // Value:
        //     The transaction identifier.
        public string TransactionId { get; set; }
        //
        // Summary:
        //     Gets or sets the name of the store/service.
        //
        // Value:
        //     The name.
        public string Name { get; set; }
    }
}
