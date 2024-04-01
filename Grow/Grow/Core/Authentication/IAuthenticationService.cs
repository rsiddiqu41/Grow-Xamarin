using Grow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grow.Core.Authentication
{
    public interface IAuthenticationService
    {
        Task<(bool IsEmailVerified, bool Success, string Message)> UserLoginWithEmailAndPasswordWithEmailVerificationAsync(string Email, string Password);

        bool UserLoginWithGoogle(Action<bool> GoogleSignInCallBack);

        Task<(bool IsSuccess, string Message)> UserLoginWithPhoneNumberAsync(string Code);

        bool IsUserLoggedIn();

        Task<bool> UserLogoutAsync();

        Task<(bool IsSuccess, string Message)> CreateEmailUser(string Email, string Password);

        Task<(bool IsCodeSent, string message)> SendOtpCodeAsync(string PhoneNumber);

        string GetCurrentUserUUID();
    }
}
