using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.Finance
{
    public class FinancialSummaryService : IFinancialSummaryService
    {
        /*public int CalculateNetWorth(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict)
        {
            int netWorth = 0;

            return netWorth;
        }

        public int CalculateCashBalance(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict)
        {
            decimal balance = 0;

            foreach (KeyValuePair<string, Acklann.Plaid.Entity.Account> financeAccount in InFinanceAccountDict)
            {
                if (financeAccount.Value.Type == PlaidAccountTypes.DEPOSITORY)
                {
                    balance += financeAccount.Value.Balance.Current;
                }
            }

            int returnBalance = Convert.ToInt32(balance);

            return returnBalance;
        }

        public int CalculateInvestments(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict)
        {
            decimal investments = 0;

            foreach (KeyValuePair<string, Acklann.Plaid.Entity.Account> financeAccount in InFinanceAccountDict)
            {
                if (financeAccount.Value.Type == PlaidAccountTypes.INVESTMENT)
                {
                    investments += financeAccount.Value.Balance.Current;
                }
            }

            int returnInvestments = Convert.ToInt32(investments);

            return returnInvestments;
        }

        public int CalculateCreditDebt(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict)
        {
            decimal balance = 0;

            foreach (KeyValuePair<string, Acklann.Plaid.Entity.Account> financeAccount in InFinanceAccountDict)
            {
                if (financeAccount.Value.Type == PlaidAccountTypes.CREDIT)
                {
                    balance += financeAccount.Value.Balance.Current;
                }
            }

            int returnBalance = Convert.ToInt32(balance);

            return returnBalance;
        }

        public int CalculateLoans(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict)
        {
            decimal balance = 0;

            foreach (KeyValuePair<string, Acklann.Plaid.Entity.Account> financeAccount in InFinanceAccountDict)
            {
                if (financeAccount.Value.Type == PlaidAccountTypes.LOAN)
                {
                    balance += financeAccount.Value.Balance.Current;
                }
            }

            int returnBalance = Convert.ToInt32(balance);

            return returnBalance;
        }*/

        public void UpdateSummaryItems(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict, out int OutBalance, out int OutLoans, out int OutCreditCards, out int OutInvestments, out int OutNetWorth)
        {
            OutBalance = 0;
            OutLoans = 0;
            OutCreditCards = 0;
            OutInvestments = 0;

            foreach (KeyValuePair<string, Acklann.Plaid.Entity.Account> financeAccount in InFinanceAccountDict)
            {
                if (financeAccount.Value.Type == PlaidAccountTypes.DEPOSITORY)
                {
                    OutBalance += Convert.ToInt32(financeAccount.Value.Balance.Current);
                }
                else if (financeAccount.Value.Type == PlaidAccountTypes.CREDIT)
                {
                    OutCreditCards += Convert.ToInt32(financeAccount.Value.Balance.Current);
                }
                else if (financeAccount.Value.Type == PlaidAccountTypes.LOAN)
                {
                    OutLoans += Convert.ToInt32(financeAccount.Value.Balance.Current);
                }
                else if (financeAccount.Value.Type == PlaidAccountTypes.INVESTMENT || financeAccount.Value.Type == PlaidAccountTypes.BROKERAGE)
                {
                    OutInvestments += Convert.ToInt32(financeAccount.Value.Balance.Current);
                }
            }

            OutNetWorth = OutBalance + OutInvestments - OutLoans - OutCreditCards;
        }
    }
}
