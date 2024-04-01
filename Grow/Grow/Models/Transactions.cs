using System;
using System.Collections.Generic;
using System.Text;
using static Acklann.Plaid.Entity.Transaction;

namespace Grow.Models
{
    // Summary:
    //     Represents a banking transaction.
    //
    // Remarks:
    //     Transaction data is standardized across financial institutions, and in many cases
    //     transactions are linked to a clean name, entity type, location, and category.
    //     Similarly, account data is standardized and returned with a clean name, number,
    //     balance, and other meta information where available.
    public class Transactions
    {
        /// <summary>
        /// 
        /// Stores a dictionary of all the transactions associated with a user in key-value pairs for fast lookup times.
        /// Maps the account to all the transactions associated with the account using the account id
        /// 
        /// Key: AccountId - AccountId for each transaction
        /// 
        /// Value: The transaction associated with the Id
        /// 
        /// </summary>
        public Dictionary<string, List<TransactionModel>> TransactionsDict { get; set; }
    }
}