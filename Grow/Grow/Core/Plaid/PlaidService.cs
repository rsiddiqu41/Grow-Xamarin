using Acklann.Plaid;
using Grow.Core.Finance;
using Grow.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Grow.Core.Plaid
{
    public class PlaidService : IPlaidService
    {
        /// <summary>
        /// Create a plaid link token from a plaid client
        /// This link token allows us to access Plaid in the webview
        /// </summary>
        public async Task<Acklann.Plaid.Management.CreateLinkTokenResponse> CreateLinkTokenAsync(PlaidClient InPlaidClient, string InClientUserId)
        {
            Acklann.Plaid.Management.CreateLinkTokenRequest.UserInfo plaidUser = new Acklann.Plaid.Management.CreateLinkTokenRequest.UserInfo
            {
                ClientUserId = InClientUserId
            };

            Acklann.Plaid.Management.CreateLinkTokenRequest createLinkTokenRequest = new Acklann.Plaid.Management.CreateLinkTokenRequest
            {
                ClientId = PlaidAPIKeys.CLIENT_ID,
                ClientName = PlaidMembers.CLIENT_NAME, //app name displayed in plaid link ui
                CountryCodes = new string[] { "us" },
                Language = PlaidMembers.LANUAGE,
                Products = new string[] { "transactions", "investments", "liabilities" },//Acklann.Plaid.Management.Product.Investments, Acklann.Plaid.Management.Product.Liabilities, Acklann.Plaid.Management.Product.Transaction },//  "transactions", "balance", "liabilities", "investments" };
                User = plaidUser
            };

            var linkTokenResponse = await InPlaidClient.CreateLinkToken(createLinkTokenRequest);

            return linkTokenResponse;
        }

        /// <summary>
        /// Perform the Token Exhange
        /// Swaps the public token for an access token
        /// </summary>
        public async Task<Acklann.Plaid.Management.ExchangeTokenResponse> ExecuteTokenExchangeAsync(PlaidClient InPlaidClient, string InPublicToken)
        {
            Acklann.Plaid.Management.ExchangeTokenRequest exchangeTokenRequest = new Acklann.Plaid.Management.ExchangeTokenRequest
            {
                PublicToken = InPublicToken
            };

            // Swap public token for access
            var exchangeTokenResponse = await InPlaidClient.ExchangeTokenAsync(exchangeTokenRequest);

            return exchangeTokenResponse;
        }

        /// <summary>
        /// Get all the accounts associated with the current access token
        /// </summary>
        /// <returns></returns>
        public async Task<Acklann.Plaid.Accounts.GetAccountResponse> GetAccountsAsync(PlaidClient InPlaidClient, string inAccessToken)
        {
            Acklann.Plaid.Accounts.GetAccountRequest requestAccounts = new Acklann.Plaid.Accounts.GetAccountRequest
            {
                AccessToken = inAccessToken,
                ClientId = PlaidAPIKeys.CLIENT_ID,
                Secret = PlaidAPIKeys.SANDBOX_SECRET
            };

            Acklann.Plaid.Accounts.GetAccountResponse accountsResponse = await InPlaidClient.FetchAccountAsync(requestAccounts);

            if (accountsResponse == null)
            {
                Debug.WriteLine("No accounts found with that access token");
                return null;
            }

            return accountsResponse;
        }

        public async Task<Acklann.Plaid.Transactions.GetTransactionsResponse> GetTransactionsAsync(PlaidClient InPlaidClient, string InAccessToken)
        {
            DateTime TodayDateTime = DateTime.Today;

            DateTime StartDate = TodayDateTime.AddMonths(-12);
            DateTime EndDate = TodayDateTime;

            Acklann.Plaid.Transactions.GetTransactionsRequest.PaginationOptions options = new Acklann.Plaid.Transactions.GetTransactionsRequest.PaginationOptions
            {
                Total = 250,
                Offset = 0
            };

            Acklann.Plaid.Transactions.GetTransactionsRequest transactionRequestObject = new Acklann.Plaid.Transactions.GetTransactionsRequest
            {
                AccessToken = InAccessToken,
                StartDate = StartDate,
                EndDate = EndDate,
                Options = options
            };

            Acklann.Plaid.Transactions.GetTransactionsResponse transactionsResponse = await InPlaidClient.FetchTransactionsAsync(transactionRequestObject);

            if (transactionsResponse == null)
            {
                Debug.WriteLine("No transactions found with that access token");
                return null;
            }

            return transactionsResponse;
        }

        public async Task<Acklann.Plaid.Investments.GetInvestmentTransactionsResponse> GetInvestmentTransactionsAsync(PlaidClient InPlaidClient, string InAccessToken)
        {
            DateTime TodayDateTime = DateTime.Today;

            DateTime StartDate = TodayDateTime.AddMonths(-12);
            DateTime EndDate = TodayDateTime;


            Acklann.Plaid.Investments.GetInvestmentTransactionsRequest.PaginationOptions paginationOptions = new Acklann.Plaid.Investments.GetInvestmentTransactionsRequest.PaginationOptions
            {
                Total = 100,
                Offset = 0
            };

            Acklann.Plaid.Investments.GetInvestmentTransactionsRequest investmentTransactionsRequest = new Acklann.Plaid.Investments.GetInvestmentTransactionsRequest
            {
                AccessToken = InAccessToken,
                StartDate = StartDate,
                EndDate = EndDate,
                Options = paginationOptions
            };

            Acklann.Plaid.Investments.GetInvestmentTransactionsResponse investmentTransactionsResponse = await InPlaidClient.FetchInvestmentTransactionsAsync(investmentTransactionsRequest);

            if (investmentTransactionsResponse == null)
            {
                Debug.WriteLine("Unable to get investment transactions with that access token");
                return null;
            }

            return investmentTransactionsResponse;
        }

        public async Task<Acklann.Plaid.Investments.GetInvestmentHoldingsResponse> GetInvestmentHoldingsAsync(PlaidClient InPlaidClient, string InAccessToken)
        {
            Acklann.Plaid.Investments.GetInvestmentHoldingsRequest investmentHoldingsRequest = new Acklann.Plaid.Investments.GetInvestmentHoldingsRequest
            {
                AccessToken = InAccessToken
            };

            Acklann.Plaid.Investments.GetInvestmentHoldingsResponse investmentHoldingsResponse = await InPlaidClient.FetchInvestmentHoldingsAsync(investmentHoldingsRequest);

            if (investmentHoldingsResponse == null)
            {
                Debug.WriteLine("Unable to get investment holdings with that access token");
                return null;
            }

            return investmentHoldingsResponse;
        }

        public async Task<Acklann.Plaid.Liabilities.GetLiabilitiesResponse> GetLiabilitiesAsync(PlaidClient InPlaidClient, string InAccessToken)
        {            
            Acklann.Plaid.Liabilities.GetLiabilitiesRequest liabilitiesRequest = new Acklann.Plaid.Liabilities.GetLiabilitiesRequest
            {
                AccessToken = InAccessToken
            };

            Acklann.Plaid.Liabilities.GetLiabilitiesResponse liabilitiesResponse = await InPlaidClient.FetchLiabilitiesAsync(liabilitiesRequest);

            if (!liabilitiesResponse.IsSuccessStatusCode)
            {
                Debug.WriteLine("Unable to get liabilities with current access token");
                return null;
            }

            return liabilitiesResponse;
        }
    }
}
