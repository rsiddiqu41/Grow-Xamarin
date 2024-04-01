using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models.Liabilities
{
    public class LiabilitiesStudentModel
    {
        public string ServicerAddressCity { get; set; }

        public string ServicerAddressCountry { get; set; }

        public string ServicerAddressPostalCode { get; set; }

        public string ServicerAddressRegion { get; set; }

        public string ServicerAddressStreet { get; set; }

        public string SequenceNumber { get; set; }

        public string PslfStatusEstimatedEligibilityDate { get; set; }

        public int PslfStatusPaymentsMade { get; set; }

        public int PslfStatusPaymentsRemaining { get; set; }

        public string PaymentReferenceNumber { get; set; }

        public double OutstandingInterestAmount { get; set; }

        public int OriginationPrincipalAmount { get; set; }

        public string OriginationDate { get; set; }

        public string RepaymentPlanDescription { get; set; }

        public string RepaymentPlanType { get; set; }

        public string NextPaymentDueDate { get; set; }

        public int MinimumPaymentAmount { get; set; }

        public string LoanStatusEndDate { get; set; }

        public string LoanStatusType { get; set; }

        public string LoanName { get; set; }

        public string LastStatementIssueDate { get; set; }

        public double LastStatementBalance { get; set; }

        public string LastPaymentDate { get; set; }

        public double LastPaymentAmount { get; set; }

        public bool IsOverdue { get; set; }

        public double InterestRatePercentage { get; set; }

        public string Guarantor { get; set; }

        public string ExpectedPayoffDate { get; set; }

        public string[] DisbursementDates { get; set; }

        public string AccountNumber { get; set; }

        public string AccountId { get; set; }

        public double YtdInterestPaid { get; set; }

        public double YtdPrincipalPaid { get; set; }
    }
}
