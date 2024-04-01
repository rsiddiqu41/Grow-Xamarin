using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.Finance
{
    interface IFinancialSummaryService
    {
        /*int CalculateNetWorth(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict);
        int CalculateCashBalance(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict);
        int CalculateInvestments(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict);
        int CalculateLoans(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict);
        int CalculateCreditDebt(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict);*/
        void UpdateSummaryItems(Dictionary<string, Acklann.Plaid.Entity.Account> InFinanceAccountDict, out int OutBalance, out int OutLoans, out int OutCreditCards, out int OutInvestments, out int OutNetWorth);
    }
}
