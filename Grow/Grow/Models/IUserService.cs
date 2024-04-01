using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grow.Models.Liabilities;

namespace Grow.Models
{
    public interface IUserService
    {
        Task<User> GetUser();
        Task<bool> AddUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> IsUserExists();
        Task<bool> AddUpdateUser(User user);

        Task<Transactions> GetTransactions();
        Task<bool> AddTransactions(Transactions InTransactions);
        Task<bool> UpdateTransactions(Transactions InTransactions);
        Task<bool> DoesTransactionExist();
        Task<bool> AddUpdateTransaction(Transactions InTransactions);

        Task<Investments> GetInvesments();
        Task<bool> AddInvestments(Investments InInvestments);
        Task<bool> UpdateInvestments(Investments InInvestments);
        Task<bool> DoesInvestmentExist();
        Task<bool> AddUpdateInvestment(Investments InInvestments);

        Task<LiabilitiesModel> GetLiabilities();
        Task<bool> AddLiabilities(LiabilitiesModel InLiabilities);
        Task<bool> UpdateLiabilities(LiabilitiesModel InLiabilities);
        Task<bool> DoesLiabilityExist();
        Task<bool> AddUpdateLiabilities(LiabilitiesModel InLiabilities);


    }
}
