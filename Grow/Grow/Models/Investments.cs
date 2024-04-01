using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Models
{
    public class Investments
    {
        /// <summary>
        /// 
        /// Stores a dictionary of all the investment transactions associated with a user in key-value pairs for fast lookup times
        /// 
        /// Key: AccountId - Account id indicating which account used these investments
        /// 
        /// Value: The investment transaction object associated with the Id
        /// 
        /// </summary>
        public Dictionary<string, List<InvestmentTransactionModel>> InvestmentTransactionsDict { get; set; }
        public Dictionary<string, List<InvestmentHoldingModel>> HoldingsDict { get; set; }
        public Dictionary<string, List<InvestmentSecuritiesModel>> SecuritiesDict { get; set; }


        public Investments()
        {
            InvestmentTransactionsDict = new Dictionary<string, List<InvestmentTransactionModel>>();
            HoldingsDict = new Dictionary<string, List<InvestmentHoldingModel>>();
            SecuritiesDict = new Dictionary<string, List<InvestmentSecuritiesModel>>();
        }
    }
}
