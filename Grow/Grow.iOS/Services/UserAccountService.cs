using Firebase.Auth;
using Foundation;
using Google.SignIn;
using Grow.Core.Authentication;
using Grow.iOS.Services;
using Grow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(UserAccountService))]
namespace Grow.iOS.Services
{
    class UserAccountService : NSObject,IAuthenticationService,ISignInDelegate
    {
        private TaskCompletionSource<(bool IsCodeSent, string message)> _PhoneAuthenticationTCS;
        private string _VerificationID;
        Action<bool> _GoogleSigInCallBack;
        public UserAccountService()
        {
            SignIn.SharedInstance.PresentingViewController = GetTopViewController();
            SignIn.SharedInstance.Delegate = this;
        }

        public Task<bool> UserLoginWithEmailAndPasswordAsync(string Email, string Password)
        {
            var taskCompletionVar = new TaskCompletionSource<bool>();

            Auth.DefaultInstance.SignInWithPasswordAsync(Email, Password)
                .ContinueWith((task) => OnAuthCompleted(task, taskCompletionVar));

            return taskCompletionVar.Task;
        }

        public Task<(bool IsSuccess, string Message)> UserLoginWithPhoneNumberAsync(string Code)
        {
            var tcs = new TaskCompletionSource<(bool IsSuccess, string Message)>();

            var credential = PhoneAuthProvider.DefaultInstance.GetCredential(
                _VerificationID, Code);

            Auth.DefaultInstance.SignInWithCredentialAsync(credential)
                .ContinueWith((task) => { 
                    if(task.IsCanceled)
                    {
                        tcs.TrySetResult((false, "Something went wrong"));
                    }
                    else if(task.IsFaulted)
                    {
                        tcs.TrySetResult((false, task.Exception.Message));
                    }
                    else
                    {
                        tcs.TrySetResult((true, ""));
                    }
                
                });

            return tcs.Task;
        }

        public bool IsUserLoggedIn()
        {
            return Auth.DefaultInstance.CurrentUser != null;
        }
        public Task<bool> UserLogoutAsync()
        {
            var taskCompletionVar = new TaskCompletionSource<bool>();

            Auth.DefaultInstance.SignOut(out NSError error);

            taskCompletionVar.TrySetResult(true);

            return taskCompletionVar.Task;
        }

        public  Task<(bool IsSuccess, string Message)> CreateEmailUser(string Email, string Password)
        {
          
                var tcs = new TaskCompletionSource<(bool IsSuccess, string Message)>();
                var authResult =  Auth.DefaultInstance.CreateUserAsync(Email, Password).ContinueWith((task) => {
                    if (task.IsCanceled)
                    {
                        tcs.TrySetResult((false, "Something went wrong"));
                    }
                    else if (task.IsFaulted)
                    {
                        tcs.TrySetResult((false, task.Exception.Message));
                    }
                    else
                    {
                        if (task.Result != null)
                        {
                            task.Result.User.SendEmailVerificationAsync()
                            .ContinueWith((_task) =>
                            {
                                if (task.IsCanceled)
                                {
                                    tcs.TrySetResult((false, "Something went wrong"));
                                }
                                else if (task.IsFaulted)
                                {
                                    tcs.TrySetResult((false, task.Exception.Message));
                                }
                                else
                                {
                                    tcs.TrySetResult((false, "Something went wrong"));
                                }
                            });

                        }
                        else
                        {
                            tcs.TrySetResult((false, "Something went wrong"));
                        }
                    }
                }); ;

                return tcs.Task;
            
        }

        public Task<(bool IsCodeSent, string message)> SendOtpCodeAsync(string PhoneNumber)
        {
            _PhoneAuthenticationTCS = new TaskCompletionSource<(bool IsCodeSent, string message)>();
            
            PhoneAuthProvider.DefaultInstance.VerifyPhoneNumber(
                PhoneNumber,
                null,
                new VerificationResultHandler(OnVerificationResult));

            return _PhoneAuthenticationTCS.Task;
        }

        private void OnVerificationResult(string verificationId, NSError error)
        {
            if (error != null)
            {
                // something went wrong
                _PhoneAuthenticationTCS?.TrySetResult((false,error.AccessibilityTextualContext));
                return;
            }
            _VerificationID = verificationId;
            _PhoneAuthenticationTCS?.TrySetResult((true, ""));
        }

        private void OnAuthCompleted(Task task, TaskCompletionSource<bool> taskCompletionVar)
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                taskCompletionVar.SetResult(false);
            }
            else
            {
                taskCompletionVar.SetResult(true);
            }
        }

        public Task<(bool IsEmailVerified, bool Success, string Message)> UserLoginWithEmailAndPasswordWithEmailVerificationAsync(string email, string password)
        {
            var tcs = new TaskCompletionSource<(bool IsEmailVerified, bool Success, string Message)>();

            var user = Auth.DefaultInstance.SignInWithPasswordAsync(email, password).ContinueWith((task) => {
                if (task.IsCanceled)
                {

                    tcs.TrySetResult((false, false, "Somthing went wrong"));
                }
                else if (task.IsFaulted)
                {

                    tcs.TrySetResult((false, false, task.Exception.Message));
                }
                else
                {
                    if (task.Result != null && task.Result.User != null)
                    {
                        tcs.TrySetResult((task.Result.User.IsEmailVerified, true, ""));
                    }
                    else
                    {
                        tcs.TrySetResult((false, false, "Somthing went wrong"));
                    }

                }

            });

            return tcs.Task;
        }

        
        public bool UserLoginWithGoogle(Action<bool> GoogleSigInCallBack)
        {
            if (SignIn.SharedInstance.HasPreviousSignIn)
            {
                SignIn.SharedInstance.RestorePreviousSignIn();
                var user = SignIn.SharedInstance.CurrentUser;
                GoogleSigInCallBack(true);
            }
            else
            {
                _GoogleSigInCallBack = GoogleSigInCallBack;
               
                SignIn.SharedInstance.SignInUser();
            }
            return true;
        }


        public static UIViewController GetTopViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;

            if (vc is UINavigationController navController)
                vc = navController.ViewControllers.Last();

            return vc;
        }
        public Task<string> GetCurrentUserUUID()
        {
            throw new NotImplementedException();
        }

        string IAuthenticationService.GetCurrentUserUUID()
        {
            throw new NotImplementedException();
        }

        public void DidSignIn(SignIn signIn, GoogleUser user, NSError error)
        {
            if(user!=null)
            {
                string idToken = user.Authentication?.IdToken;
                string accessToke = user.Authentication?.AccessToken;
                if (!string.IsNullOrEmpty(idToken)&& string.IsNullOrEmpty(accessToke))
                {
                    var googleCredentials = GoogleAuthProvider.GetCredential(idToken, accessToke);
                    Auth.DefaultInstance.SignInWithCredentialAsync(googleCredentials).ContinueWith((task) => {
                        if (task.IsCanceled)
                        {

                            _GoogleSigInCallBack(false);
                        }
                        else if (task.IsFaulted)
                        {

                            _GoogleSigInCallBack(false);
                        }
                        else
                        {
                            if (task.Result != null && task.Result.User != null)
                            {
                                _GoogleSigInCallBack(true);
                            }
                            else
                            {
                                _GoogleSigInCallBack(false);
                            }

                        }

                    }); ;
                }
                else
                {
                    _GoogleSigInCallBack(false);
                }
            }
            else
            {
                _GoogleSigInCallBack(false);
            }
        }
    }
}