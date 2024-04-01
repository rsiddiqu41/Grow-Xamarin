using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models.Liabilities
{
    public class LiabilitiesMortgageModel
    {
        public int PastDueAmount { get; set; }

        public int OriginationPrincipalAmount { get; set; }

        public string OriginationDate { get; set; }

        public string NextPaymentDueDate { get; set; }

        public double NextMonthlyPayment { get; set; }

        public string MaturityDate { get; set; }

        public string LoanTypeDescription { get; set; }

        public double YtdInterestPaid { get; set; }

        public string LoanTerm { get; set; }

        public double LastPaymentAmount { get; set; }

        public double InterestRatePercentage { get; set; }

        public string InterestRateType { get; set; }

        public bool HasPrepaymentPenalty { get; set; }

        public bool HasPmi { get; set; }

        public double EscrowBalance { get; set; }

        public int CurrentLateFee { get; set; }

        public long AccountNumber { get; set; }

        public string AccountId { get; set; }

        public string LastPaymentDate { get; set; }

        public double YtdPrincipalPaid { get; set; }

        public string PropertyAddressCity { get; set; }

        public string PropertyAddressCountry { get; set; }

        public int PropertyAddressPostalCode { get; set; }

        public string PropertyAddressRegion { get; set; }

        public string PropertyAddressStreet { get; set; }
    }
}
