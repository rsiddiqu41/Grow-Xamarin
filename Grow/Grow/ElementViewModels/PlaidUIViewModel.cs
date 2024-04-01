using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web;

using Grow.Core.Authentication;
using Grow.Core.Base;
using Grow.Core.Finance;
using Grow.Core.ExternalInterfaces.Finance;
using Grow.Models;

using Acklann.Plaid;
using Grow.Core.Mediator;
using Grow.Core.Plaid;
using System.Threading;
using Grow.Core.Adapter;
using Grow.Models.Liabilities;
using Grow.Views;
using Xamarin.Forms;

namespace Grow.ElementViewModels
{
    public class PlaidUIViewModel : UserViewModelBase, IPlaidUIViewModel
    {
        Uri OnSuccessUri = null;

        string _PlaidUri = "";
        string _AccessToken = "";
        string _LinkToken = "";

        PlaidClient _PlaidClient;

        IUserService _UserService;
        IAuthenticationService _AuthenticationService;
        IMediator _Messenger;
        IPlaidService _PlaidService;
        IPlaidAdapter _PlaidAdapter;

        public PlaidUIViewModel(IUserService InUserService, IAuthenticationService InAuthenticationService, IPlaidService InPlaidService, IPlaidAdapter InPlaidAdapter, IMediator InMessenger) : base(InUserService, InMessenger)
        {
            _UserService = InUserService;
            _AuthenticationService = InAuthenticationService;
            _PlaidService = InPlaidService;
            _PlaidAdapter = InPlaidAdapter;
            _Messenger = InMessenger;

            InitializePlaidAsync();
        }

        public async Task InitializePlaidAsync()
        {
            //First get current user
            //var user = await _UserService.Get();

            if(CurrentUser == null)
            {
                Debug.WriteLine("Unable to get current user from database");
                return;
            }

            //Create new plaid client each time plaid is opened up
            _PlaidClient = new PlaidClient(PlaidAPIKeys.CLIENT_ID, PlaidAPIKeys.SANDBOX_SECRET, "", Acklann.Plaid.Environment.Sandbox);

            if (_PlaidClient == null)
            {
                Debug.WriteLine("Plaid Client is null");
                return;
            }

            string clientUserId = _AuthenticationService.GetCurrentUserUUID();

            // create link token and create plaid uri for webview
            Acklann.Plaid.Management.CreateLinkTokenResponse linkTokenResponse = await _PlaidService.CreateLinkTokenAsync(_PlaidClient, clientUserId);

            if(linkTokenResponse == null)
            {
                Debug.WriteLine("Link Token Response is null");
                return;
            }

            LinkToken = linkTokenResponse.LinkToken;

            PlaidUri = string.Format("https://cdn.plaid.com/link/v2/stable/link.html?isWebview=true&token={0}", LinkToken);
        }

        public async Task<bool> OnSuccessCallback(Uri InUri)
        {
            OnSuccessUri = InUri;

            // Get Public Token
            string PublicToken = ParsePublicToken(OnSuccessUri);

            if (PublicToken == String.Empty || String.IsNullOrWhiteSpace(PublicToken))
            {
                Debug.WriteLine("ERROR: Public token is empty or null");
                return false;
            }

            // Exchange public token for access token
            var accessTokenTask = await _PlaidService.ExecuteTokenExchangeAsync(_PlaidClient, PublicToken);

            if (accessTokenTask == null)
            {
                Debug.WriteLine("Token exchange failed");
                return false;
            }

            _AccessToken = accessTokenTask.AccessToken;

            // Use access token to get account transactions and update current user accounts model
            var accountsResponse = await _PlaidService.GetAccountsAsync(_PlaidClient, _AccessToken);
            Thread.Sleep(200);
            UpdateAccountsModel(accountsResponse);
            await _UserService.AddUpdateUser(CurrentUser);

            if (CurrentUser.FinanceAccountTypes.Contains(PlaidAccountTypes.CREDIT) || CurrentUser.FinanceAccountTypes.Contains(PlaidAccountTypes.DEPOSITORY) || CurrentUser.FinanceAccountTypes.Contains(PlaidAccountTypes.LOAN) || CurrentUser.FinanceAccountTypes.Contains(PlaidAccountTypes.OTHER))
            {
                var transactions = await _PlaidService.GetTransactionsAsync(_PlaidClient, _AccessToken);
                Thread.Sleep(500);
                UpdateTransactionsModel(transactions);
                await _UserService.AddUpdateTransaction(CurrentTransactions);
            }

            if (CurrentUser.FinanceAccountTypes.Contains(PlaidAccountTypes.INVESTMENT) || CurrentUser.FinanceAccountTypes.Contains(PlaidAccountTypes.BROKERAGE))
            {
                var investmentTransactions = await _PlaidService.GetInvestmentTransactionsAsync(_PlaidClient, _AccessToken);
                Thread.Sleep(500);
                var investmentHoldings = await _PlaidService.GetInvestmentHoldingsAsync(_PlaidClient, _AccessToken);
                Thread.Sleep(500);

                if(investmentTransactions != null)
                {
                    UpdateInvestmentTransactionsModel(investmentTransactions);
                }

                if(investmentHoldings != null)
                {
                    UpdateInvestmentHoldingsModel(investmentHoldings);
                }

                await _UserService.AddUpdateInvestment(CurrentInvestments);
            }

            if (CurrentUser.FinanceAccountTypes.Contains(PlaidAccountTypes.CREDIT) || CurrentUser.FinanceAccountTypes.Contains(PlaidAccountTypes.LOAN))
            {
                var liabilities = await _PlaidService.GetLiabilitiesAsync(_PlaidClient, _AccessToken);
                Thread.Sleep(500);
                UpdateLiabilitiesModel(liabilities);
                await _UserService.AddUpdateLiabilities(CurrentLiabilities);
            }

            return true;
        }

        private void UpdateAccountsModel(Acklann.Plaid.Accounts.GetAccountResponse InAccountsResponse)
        {
            // Update AccessToken Dictionary so we don't have to get the access token each time using the token exchange
            // This will link the financial account forever UNTIL THE USER DELETES IT <---- NEED TO DO

            Dictionary<string, HashSet<string>> tempAccessTokenDict = CurrentUser.AccessTokenDict;
            HashSet<string> tempFinanceAccountTypesSet = CurrentUser.FinanceAccountTypes;
            Dictionary<string, Acklann.Plaid.Entity.Account> tempFinanceAccountsDict = new Dictionary<string, Acklann.Plaid.Entity.Account>();

            foreach (Acklann.Plaid.Entity.Account account in InAccountsResponse.Accounts)
            {
                string accountID = account.Id;

                // If the user has already used this access token before and if there are new accounts associated with this access token, add them
                if (tempAccessTokenDict.ContainsKey(_AccessToken))
                {
                    if (!tempAccessTokenDict[_AccessToken].Contains(accountID))
                    {
                        tempAccessTokenDict[_AccessToken].Add(accountID);
                    }
                }

                // Otherwise add the access token along with a new hashset containing the current account id
                else
                {
                    HashSet<string> newAccountIdSet = new HashSet<string> { accountID };
                    tempAccessTokenDict.Add(_AccessToken, newAccountIdSet);
                }

                // Update the account types hashset with any new account types (i.e mortgage, investment, etc...)
                if (!tempFinanceAccountTypesSet.Contains(account.Type))
                {
                    tempFinanceAccountTypesSet.Add(account.Type);
                }

                if (tempFinanceAccountsDict.ContainsKey(accountID))
                {
                    tempFinanceAccountsDict[accountID] = account;
                }
                else
                {
                    tempFinanceAccountsDict.Add(accountID, account);
                }
            }

            CurrentUser.AccessTokenDict = tempAccessTokenDict;
            CurrentUser.FinanceAccountTypes = tempFinanceAccountTypesSet;
            CurrentUser.FinanceAccountsDict = tempFinanceAccountsDict;
            CurrentUser.IsFinanceAccountConnected = true;
        }

        /// <summary>
        /// We will get the past 100 transactions used for each account the user has registered
        /// Data retrieved is from Plaid's '/transactions/get' API endpoint
        /// </summary>
        /// <returns></returns>
        public void UpdateTransactionsModel(Acklann.Plaid.Transactions.GetTransactionsResponse InTransactionsResponse)
        {
            Dictionary<string, List<TransactionModel>> tempDict = new Dictionary<string, List<TransactionModel>>();

            foreach(Acklann.Plaid.Entity.Transaction PlaidTransaction in InTransactionsResponse.Transactions)
            {
                string accountID = PlaidTransaction.AccountId;

                TransactionModel UpdatedTransaction = _PlaidAdapter.ConvertTransactionsResponseToModel(PlaidTransaction);

                if (tempDict.ContainsKey(accountID))
                {
                    tempDict[accountID].Add(UpdatedTransaction);
                }
                else
                {
                    List<TransactionModel> tempTransactionList = new List<TransactionModel> { UpdatedTransaction };
                    tempDict.Add(accountID, tempTransactionList);
                }
            }

            if(tempDict.Count > 0)
            {
                CurrentTransactions.TransactionsDict = tempDict;
            }

            // Update (write to) database with modified user model data
            //await _UserService.AddUpdateUser(CurrentUser);
        }

        private void UpdateLiabilitiesModel(Acklann.Plaid.Liabilities.GetLiabilitiesResponse InLiabilitiesResponse)
        {
            bool hasCreditLiabilities = false;
            bool hasStudentLiabilities = false;
            bool hasMortgageLiabilities = false;

            if (InLiabilitiesResponse.Liabilities.Credit != null)
            {
                hasCreditLiabilities = true;
            }

            if (InLiabilitiesResponse.Liabilities.Mortgage != null)
            {
                hasMortgageLiabilities = true;
            }

            if (InLiabilitiesResponse.Liabilities.Student != null)
            {
                hasStudentLiabilities = true;
            }

            if (hasCreditLiabilities)
            {
                CurrentLiabilities.CreditLiabilitiesDict.Clear();

                Dictionary<string, List<LiabilitiesCreditModel>> creditDict = new Dictionary<string, List<LiabilitiesCreditModel>>();

                foreach (Acklann.Plaid.Entity.Credit creditLiabilities in InLiabilitiesResponse.Liabilities.Credit)
                {
                    //Convert acklann plaid credit model to modifed credit model
                    LiabilitiesCreditModel UpdatedCreditModel = _PlaidAdapter.ConvertLiabilitiesCreditResponseToModel(creditLiabilities);

                    string accountID = creditLiabilities.AccountId;

                    if (creditDict.ContainsKey(accountID))
                    {
                        creditDict[accountID].Add(UpdatedCreditModel);
                    }
                    else
                    {
                        List<LiabilitiesCreditModel> tempCreditList = new List<LiabilitiesCreditModel> { UpdatedCreditModel };
                        creditDict.Add(accountID, tempCreditList);
                    }
                }

                if (creditDict.Count > 0)
                {
                    CurrentLiabilities.CreditLiabilitiesDict = creditDict;
                }
            }

            if (hasMortgageLiabilities)
            {
                CurrentLiabilities.MortgageLiabilitiesDict.Clear();

                Dictionary<string, List<LiabilitiesMortgageModel>> mortgageDict = new Dictionary<string, List<LiabilitiesMortgageModel>>();

                foreach (Acklann.Plaid.Entity.Mortgage mortgageLiabilities in InLiabilitiesResponse.Liabilities.Mortgage)
                {
                    LiabilitiesMortgageModel UpdatedMortgageModel = _PlaidAdapter.ConvertLiabilitiesMortgageResponseToModel(mortgageLiabilities);

                    string accountID = mortgageLiabilities.AccountId;

                    if (mortgageDict.ContainsKey(accountID))
                    {
                        mortgageDict[accountID].Add(UpdatedMortgageModel);
                    }
                    else
                    {
                        List<LiabilitiesMortgageModel> tempMortgageList = new List<LiabilitiesMortgageModel> { UpdatedMortgageModel };
                        mortgageDict.Add(accountID, tempMortgageList);
                    }
                }

                if (mortgageDict.Count > 0)
                {
                    CurrentLiabilities.MortgageLiabilitiesDict = mortgageDict;
                }
            }

            if (hasStudentLiabilities)
            {
                CurrentLiabilities.StudentLiabilitiesDict.Clear();

                Dictionary<string, List<LiabilitiesStudentModel>> studentDict = new Dictionary<string, List<LiabilitiesStudentModel>>();

                foreach (Acklann.Plaid.Entity.Student studentLiabilities in InLiabilitiesResponse.Liabilities.Student)
                {
                    string accountID = studentLiabilities.AccountId;

                    LiabilitiesStudentModel updatedStudentModel = _PlaidAdapter.ConvertLiabilitiesStudentResponseToModel(studentLiabilities);

                    if (studentDict.ContainsKey(studentLiabilities.AccountId))
                    {
                        studentDict[studentLiabilities.AccountId].Add(updatedStudentModel);
                    }
                    else
                    {
                        List<LiabilitiesStudentModel> tempStudentList = new List<LiabilitiesStudentModel> { updatedStudentModel };
                        studentDict.Add(studentLiabilities.AccountId, tempStudentList);
                    }
                }

                if (studentDict.Count > 0)
                {
                    CurrentLiabilities.StudentLiabilitiesDict = studentDict;
                }
            }
        }

        private void UpdateInvestmentTransactionsModel(Acklann.Plaid.Investments.GetInvestmentTransactionsResponse InInvestmentTransactionsResponse)
        {
            CurrentInvestments.InvestmentTransactionsDict.Clear();

            Dictionary<string, List<InvestmentTransactionModel>> tempDict = new Dictionary<string, List<InvestmentTransactionModel>>();

            foreach (Acklann.Plaid.Entity.InvestmentTransaction SingleTransaction in InInvestmentTransactionsResponse.InvestmentTransactions)
            {
                string accountID = SingleTransaction.AccountId;

                InvestmentTransactionModel currentTransactionModel = _PlaidAdapter.ConvertInvestmentTransactionsResponseToModel(SingleTransaction);

                if (tempDict.ContainsKey(accountID))
                {
                    tempDict[accountID].Add(currentTransactionModel);
                }
                else
                {
                    List<InvestmentTransactionModel> tempTransactionList = new List<InvestmentTransactionModel> { currentTransactionModel };
                    tempDict.Add(accountID, tempTransactionList);
                }
            }

            if (tempDict.Count > 0)
            {
                CurrentInvestments.InvestmentTransactionsDict = tempDict;
            }


            // Get the most recent transaction date
            /*DateTime latestTransactionDate = investmentTransactionsResponse.InvestmentTransactions[0].Date;

            //Reverse transaction array so earliest transactions appear first in array. This makes it easy to append newest transactions to the end
            Acklann.Plaid.Entity.InvestmentTransaction[] investmentTransactionsArray = investmentTransactionsResponse.InvestmentTransactions;

            Array.Reverse(investmentTransactionsArray);

            foreach(Acklann.Plaid.Entity.InvestmentTransaction transaction in investmentTransactionsArray)
            {
                // Eliminate duplicate data based on dates
                if(transaction.Date <= latestTransactionDate)
                {
                    continue;
                }

                string accountID = transaction.AccountId;

                // If the user has already has an account with these transactions, add them to the list
                if (CurrentInvestments.TransactionsDict.ContainsKey(accountID))
                {
                    CurrentInvestments.TransactionsDict[accountID].Add(transaction);
                }

                // Otherwise add the access token along with a new hashset containing the current account id
                else
                {
                    List<Acklann.Plaid.Entity.InvestmentTransaction> tempTrans = new List<Acklann.Plaid.Entity.InvestmentTransaction> { transaction };

                    CurrentInvestments.TransactionsDict.Add(accountID, tempTrans);
                }
            }*/

        }

        private void UpdateInvestmentHoldingsModel(Acklann.Plaid.Investments.GetInvestmentHoldingsResponse InInvestmentHoldingsResponse)
        {
            // Clear the dictionary and fetch updated data
            CurrentInvestments.HoldingsDict.Clear();

            Dictionary<string, List<InvestmentHoldingModel>> tempDict = new Dictionary<string, List<InvestmentHoldingModel>>();

            foreach (Acklann.Plaid.Entity.Holding holding in InInvestmentHoldingsResponse.Holdings)
            {
                string accountID = holding.AccountId;

                InvestmentHoldingModel currentHoldingModel = _PlaidAdapter.ConvertInvestmentHoldingsResponseToModel(holding);

                // If the user has already used this access token before and if there are new accounts associated with this access token, add them
                if (tempDict.ContainsKey(accountID))
                {
                    tempDict[accountID].Add(currentHoldingModel);
                }
                else
                {
                    List<InvestmentHoldingModel> tempHoldings = new List<InvestmentHoldingModel> { currentHoldingModel };

                    tempDict.Add(accountID, tempHoldings);
                }
            }

            if (tempDict.Count > 0)
            {
                CurrentInvestments.HoldingsDict = tempDict;
            }
        }

        /// <summary>
        /// Helper method used for parsing plaid uri queried string to obtain public token
        /// Public token is later swapped for access token
        /// </summary>
        private string ParsePublicToken(Uri InUri)
        {
            string PublicToken = String.Empty;
            try
            {
                PublicToken = HttpUtility.ParseQueryString(InUri.Query).Get("public_token");
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return PublicToken;
        }

        public async void ReturnToOverviewPage()
        {
            await Shell.Current.GoToAsync($"///{nameof(FinanceOverviewPage)}");
        }

        #region Fields
        public string PlaidUri
        {
            get => _PlaidUri;
            set => SetProperty(ref _PlaidUri, value);
        }

        public string LinkToken
        {
            get => _LinkToken;
            set => SetProperty(ref _LinkToken, value);
        }
        #endregion
    }
}
