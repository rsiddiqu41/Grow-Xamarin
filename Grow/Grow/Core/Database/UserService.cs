using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using Grow.Core.Authentication;
using Grow.Models;
using Grow.Models.Liabilities;
using Plugin.CloudFirestore;

namespace Grow.Core.Database
{
    public class UserService : IUserService
    {
        readonly IAuthenticationService _authenticationService;
        public UserService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #region User
        public async Task<User> GetUser()
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("users")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .GetAsync();

            return document.ToObject<User>();
        }

        public async Task<bool> AddUser(User user)
        {

            await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("users")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .SetAsync(user);
            return true;
        }

        public async Task<bool> UpdateUser(User user)
        {

            await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("users")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .UpdateAsync(user);
            return true;
        }

        public async Task<bool> IsUserExists()
        {
            var user = await GetUser();
            if (user == null)
                return false;
            else
                return true;
        }

        public async Task<bool> AddUpdateUser(User InUser)
        {
            if(InUser == null)
            {
                return false;
            }

            var user = await GetUser();

            if (user == null)
                await AddUser(InUser);
            else
                await UpdateUser(InUser);

            return true;
        }
        #endregion

        #region Transactions        
        public async Task<Transactions> GetTransactions()
        {
            var document = await CrossCloudFirestore.Current
                            .Instance
                            .Collection("transactions")
                            .Document(_authenticationService.GetCurrentUserUUID())
                            .GetAsync();

            
            return document.ToObject<Transactions>();
        }

        public async Task<bool> AddTransactions(Transactions InTransactions)
        {

            await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("transactions")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .SetAsync(InTransactions);
            return true;
        }

        public async Task<bool> UpdateTransactions(Transactions InTransactions)
        {            

            await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("transactions")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .UpdateAsync(InTransactions);
            return true;
        }

        public async Task<bool> DoesTransactionExist()
        {
            var transactions = await GetTransactions();
            if (transactions == null)
                return false;
            else
                return true;
        }

        public async Task<bool> AddUpdateTransaction(Transactions InTransactions)
        {
            if (InTransactions == null)
            {
                return false;
            }

            var transactions = await GetTransactions();

            //if(transactions.Data.Count <= 0 || transactions.Data == null || transactions == null)
            if(transactions == null)
            {
                await AddTransactions(InTransactions);
            }
            else
            {
                await UpdateTransactions(InTransactions);
            }

            return true;
        }

        #endregion

        #region Investments
        public async Task<Investments> GetInvesments()
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("investments")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .GetAsync();

            return document.ToObject<Investments>();
        }

        public async Task<bool> AddInvestments(Investments InInvestments)
        {

            await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("investments")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .SetAsync(InInvestments);
            return true;
        }

        public async Task<bool> UpdateInvestments(Investments InInvestments)
        {

            await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("investments")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .UpdateAsync(InInvestments);
            return true;
        }

        public async Task<bool> DoesInvestmentExist()
        {
            var _Invesments = await GetInvesments();
            if (_Invesments == null)
                return false;
            else
                return true;
        }

        public async Task<bool> AddUpdateInvestment(Investments InInvestments)
        {
            if (InInvestments == null)
            {
                return false;
            }

            var investments = await GetInvesments();

            if (investments == null)
            {
                await AddInvestments(InInvestments);
            }
            else
            {
                await UpdateInvestments(InInvestments);
            }

            return true;
        }
        #endregion

        #region Liabilities
        public async Task<LiabilitiesModel> GetLiabilities()
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("liabilities")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .GetAsync();

            return document.ToObject<LiabilitiesModel>();
        }

        public async Task<bool> AddLiabilities(LiabilitiesModel InLiabilities)
        {

            await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("liabilities")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .SetAsync(InLiabilities);
            return true;
        }

        public async Task<bool> UpdateLiabilities(LiabilitiesModel InLiabilities)
        {

            await CrossCloudFirestore.Current
                                        .Instance
                                        .Collection("liabilities")
                                        .Document(_authenticationService.GetCurrentUserUUID())
                                        .UpdateAsync(InLiabilities);
            return true;
        }

        public async Task<bool> DoesLiabilityExist()
        {
            var liabilities = await GetLiabilities();
            if (liabilities == null)
                return false;
            else
                return true;
        }

        public async Task<bool> AddUpdateLiabilities(LiabilitiesModel InLiabilities)
        {
            if (InLiabilities == null){
                return false;
            }

            var liabilities = await GetLiabilities();

            if (liabilities == null)
            {
                await AddLiabilities(InLiabilities);
            }
            else
            {
                await UpdateLiabilities(InLiabilities);
            }

            return true;
        }
        #endregion
    }
}
