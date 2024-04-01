using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool IsFinanceAccountConnected { get; set; }
        public HashSet<string> FinanceAccountTypes { get; set; }
        public Dictionary<string, HashSet<string>> AccessTokenDict { get; set; } //Maps access token to all the account ids of the accounts associated with the access token
        public Dictionary<string, Acklann.Plaid.Entity.Account> FinanceAccountsDict { get; set; } //Maps account Id to its associated account object, for quick lookups
        public int Investments { get; set; }
        public int Cash { get; set; }
        public int NetWorth { get; set; }
        public int Loans { get; set; }
        public int CreditCards { get; set; }
    }
}
