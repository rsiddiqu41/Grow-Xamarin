using Acklann.Plaid;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grow.Core.Plaid
{
    public interface IPlaidService
    {
        /// <summary>
        /// Methods for performing token exchanges and getting lifetime Access Token
        /// </summary>
        /// <param name="InPlaidClient"></param>
        /// <param name="InClientUserId"></param>
        /// <returns></returns>
        Task<Acklann.Plaid.Management.CreateLinkTokenResponse> CreateLinkTokenAsync(PlaidClient InPlaidClient, string InClientUserId);
        Task<Acklann.Plaid.Management.ExchangeTokenResponse> ExecuteTokenExchangeAsync(PlaidClient InPlaidClient, string InPublicToken);

        /// <summary>
        /// Methods for retrieving most recent data from linked financial accounts
        /// </summary>
        /// <param name="InPlaidClient"></param>
        /// <param name="inAccessToken"></param>
        /// <returns></returns>
        Task<Acklann.Plaid.Accounts.GetAccountResponse> GetAccountsAsync(PlaidClient InPlaidClient, string inAccessToken);
        Task<Acklann.Plaid.Investments.GetInvestmentTransactionsResponse> GetInvestmentTransactionsAsync(PlaidClient InPlaidClient, string InAccessToken);
        Task<Acklann.Plaid.Investments.GetInvestmentHoldingsResponse> GetInvestmentHoldingsAsync(PlaidClient InPlaidClient, string InAccessToken);
        Task<Acklann.Plaid.Liabilities.GetLiabilitiesResponse> GetLiabilitiesAsync(PlaidClient InPlaidClient, string InAccessToken);
        Task<Acklann.Plaid.Transactions.GetTransactionsResponse> GetTransactionsAsync(PlaidClient InPlaidClient, string InAccessToken);
    }
}
