using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class FinanceAccounts
    {
        /// <summary>
        /// 
        /// Stores a dictionary of all the finance accounts associated with a user in key-value pairs for fast lookup times
        /// 
        /// Key: Id - Unique Account Id
        /// 
        /// Value: The Account object associated with the Id
        /// 
        /// </summary>
        public Dictionary<string, Acklann.Plaid.Entity.Account> FinanceAccountsDict { get; set; }
        /*
        public string Id { get; set; } //Unique Account Id created by Plaid
        public Acklann.Plaid.Entity.Balance Balance { get; set; } //Account balance
        public string InstitutionId { get; set; } //Id of the financial institution
        public string ItemId { get; set; } //Gets or sets the Acklann.Plaid.Entity.Item identifier
        public string Mask { get; set; } //Last 4 digits of account number
        public string Name { get; set; } //Custom name user has for the account
        public string OfficialName { get; set; } //Official account name from institution
        public Acklann.Plaid.Entity.Owner[] Owners { get; set; } //Names, phone numbers, emails, and addresses associated with the account
        public string Type { get; set; } //Type of account. options are Investment, Credit, Depository, Loan, Brokerage, other
        public string SubType { get; set; } //what type of account it is, i.e 401k, debit, etc
        public Dictionary<string, Transactions> TransactionsDictionary { get; set; } // Maps transaction Id to transaction object
        */
    }
}
