using Grow.Models;
using Grow.Models.Liabilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grow.Core.Adapter
{
    public interface IPlaidAdapter
    {
        TransactionModel ConvertTransactionsResponseToModel(Acklann.Plaid.Entity.Transaction InPlaidTransaction);
        InvestmentTransactionModel ConvertInvestmentTransactionsResponseToModel(Acklann.Plaid.Entity.InvestmentTransaction InPlaidInvestmentTransaction);
        InvestmentHoldingModel ConvertInvestmentHoldingsResponseToModel(Acklann.Plaid.Entity.Holding InPlaidInvestmentHolding);
        InvestmentSecuritiesModel ConvertInvestmentSecuritiesResponseToModel(Acklann.Plaid.Entity.Security InPlaidInvestmentSecurity);
        LiabilitiesCreditModel ConvertLiabilitiesCreditResponseToModel(Acklann.Plaid.Entity.Credit InPlaidLiabilitiesCredit);
        LiabilitiesMortgageModel ConvertLiabilitiesMortgageResponseToModel(Acklann.Plaid.Entity.Mortgage InPlaidLiabilitiesMortgage);
        LiabilitiesStudentModel ConvertLiabilitiesStudentResponseToModel(Acklann.Plaid.Entity.Student InPlaidLiabilitiesStudent);
    }
}
