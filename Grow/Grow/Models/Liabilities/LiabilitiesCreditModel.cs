using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class LiabilitiesCreditModel
    {
        public string AccountId { get; set; }

        public Apr[] Aprs { get; set; }

        public bool IsOverdue { get; set; }

        public double LastPaymentAmount { get; set; }

        public string LastPaymentDate { get; set; }

        public double LastStatementBalance { get; set; }

        public string LastStatementIssueDate { get; set; }

        public int MinimumPaymentAmount { get; set; }

        public string NextPaymentDueDate { get; set; }

        public struct Apr
        {

            public double AprPercentage { get; set; }

            public string AprType { get; set; }

            public double BalanceSubjectToApr { get; set; }

            public double InterestChargeAmount { get; set; }
        }
    }
}
