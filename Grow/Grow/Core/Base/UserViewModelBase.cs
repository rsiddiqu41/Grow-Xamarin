using Grow.Core.Authentication;
using Grow.Core.Finance;
using Grow.Core.Mediator;
using Grow.Models;
using Grow.Models.Liabilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Grow.Core.Base
{
    public class UserViewModelBase : ViewModelBase
    {
        IUserService _UserService;
        IMediator _Messenger;

        protected User CurrentUser { get; set; }
        protected Transactions CurrentTransactions { get; set; }
        protected Investments CurrentInvestments { get; set; }
        protected LiabilitiesModel CurrentLiabilities { get; set; }


        public UserViewModelBase(IUserService InUserService, IMediator InMessenger)
        {
            _UserService = InUserService;
            _Messenger = InMessenger;
            
            CurrentUser = new User();
            CurrentTransactions = new Transactions();
            CurrentLiabilities = new LiabilitiesModel();
            CurrentInvestments = new Investments();

            InitializeUser();
        }

        private async void InitializeUser()
        {
            CurrentUser = await _UserService.GetUser();

            if (CurrentUser == null)
            {
                Debug.WriteLine("Unable to get user successfully");
            }

            if(CurrentUser.FinanceAccountTypes != null && CurrentUser.FinanceAccountTypes.Count > 0)
            {
                foreach(string accountType in CurrentUser.FinanceAccountTypes)
                {
                    if (accountType == PlaidAccountTypes.CREDIT || accountType == PlaidAccountTypes.DEPOSITORY || accountType == PlaidAccountTypes.LOAN || accountType == PlaidAccountTypes.OTHER)
                    {
                        var tempTransactions = await _UserService.GetTransactions();

                        if(tempTransactions != null && tempTransactions.TransactionsDict != null)
                        {
                            CurrentTransactions = tempTransactions;
                        }                        
                    }

                    if (accountType == PlaidAccountTypes.INVESTMENT || accountType == PlaidAccountTypes.BROKERAGE)
                    {
                        var tempInvestments = await _UserService.GetInvesments();

                        if (tempInvestments != null)
                        {
                            if (tempInvestments.HoldingsDict != null || tempInvestments.InvestmentTransactionsDict != null || tempInvestments.SecuritiesDict != null)
                            {
                                CurrentInvestments = tempInvestments;
                            }
                        }
                    }

                    if (accountType == PlaidAccountTypes.CREDIT || accountType == PlaidAccountTypes.LOAN)
                    {
                        var tempLiabilities = await _UserService.GetLiabilities();

                        if (tempLiabilities != null)
                        {
                            if (tempLiabilities.CreditLiabilitiesDict != null || tempLiabilities.MortgageLiabilitiesDict != null || tempLiabilities.StudentLiabilitiesDict != null)
                            {
                                CurrentLiabilities = tempLiabilities;
                            }
                        }
                    }
                }
            }

            _Messenger.NotifyColleagues("LoadUser");
        }

        private Dictionary<string,object> TransactionsDatabaseHelper(object myObject)
        {
            if (myObject is IEnumerable)
            {
                Dictionary<string,object> tempDict = new Dictionary<string, object>();
                var enumerator = ((IEnumerable)myObject).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var currentObj = enumerator.Current;


                    if (currentObj is IDictionary &&
                            currentObj.GetType().IsGenericType &&
                            currentObj.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>)))
                    {

                    }
                    


                    //tempDict.Add(enumerator.Current);
                }
                return tempDict;
            }
            return null;
        }
    }
}
