using Grow.Core.Authentication;
using Grow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

//[assembly:Dependency(typeof(AuthenticationService))]
namespace Grow.Core.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task<(bool IsSuccess, string Message)> CreateEmailUser(string Email, string Password)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsCodeSent, string message)> SendOtpCodeAsync(string PhoneNumber)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, string Message)> UserLoginWithPhoneNumberAsync(string Code)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserLogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsEmailVerified, bool Success, string Message)> UserLoginWithEmailAndPasswordWithEmailVerificationAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public string GetCurrentUserUUID()
        {
            throw new NotImplementedException();
        }

        public bool UserLoginWithGoogle(Action<bool> GoogleSignInCallBack)
        {
            throw new NotImplementedException();
        }

        public bool IsUserLoggedIn()
        {
            throw new NotImplementedException();
        }
    }
}
