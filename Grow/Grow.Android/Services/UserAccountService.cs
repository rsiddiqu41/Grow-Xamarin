using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grow.Droid.Services;

using Xamarin.Forms;
using Xamarin.Essentials;

using Firebase.Auth;
using Firebase.Firestore;

using Grow.Core.Authentication;
using Java.Util.Concurrent;
using Firebase;
using Grow.Models;
using Grow.Droid.ServiceListener;

using Java.Interop;

[assembly: Dependency(typeof(UserAccountService))]
namespace Grow.Droid.Services
{
    public class UserAccountService : PhoneAuthProvider.OnVerificationStateChangedCallbacks, IAuthenticationService
    {
        //Java.Lang.Long OTP_TIMEOUT = (Java.Lang.Long)60; // in seconds
        private TaskCompletionSource<(bool IsCodeSent, string message)> _PhoneAuthenticationTCS;
        private string _VerificationID;
        private PhoneAuthProvider.ForceResendingToken _ResendToken;
        private PhoneAuthProvider.OnVerificationStateChangedCallbacks _Callbacks;
        private PhoneAuthCredential _Credential;

        private FirebaseAuth _Auth;

        public UserAccountService()
        {

        }

        public Task<(bool IsEmailVerified, bool Success, string Message)> UserLoginWithEmailAndPasswordWithEmailVerificationAsync(string email, string password)
        {
            var tcs = new TaskCompletionSource<(bool IsEmailVerified, bool Success, string Message)>();

            var user = FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith((task) => {

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

        public Task<(bool IsSuccess, string Message)> UserLoginWithPhoneNumberAsync(string InCode)
        {
            var tcs = new TaskCompletionSource<(bool IsSuccess, string Message)>();
            if (!string.IsNullOrWhiteSpace(_VerificationID))
            {

                var credential = PhoneAuthProvider.GetCredential(_VerificationID, InCode);

                var user = FirebaseAuth.Instance.SignInWithCredentialAsync(credential).ContinueWith((task) => {
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
                        tcs.TrySetResult((true, ""));
                    }

                }); 
            }
            return tcs.Task;
        }

        public bool IsUserLoggedIn()
        {
            return FirebaseAuth.Instance.CurrentUser != null;
        }

        public Task<bool> UserLogoutAsync()
        {
            var taskCompletionVar = new TaskCompletionSource<bool>();

            FirebaseAuth.Instance.SignOut();

            taskCompletionVar.TrySetResult(true);

            return taskCompletionVar.Task;
        }

        public Task<(bool IsSuccess, string Message)> CreateEmailUser(string Email, string Password)
        {
            var tcs = new TaskCompletionSource<(bool IsSuccess, string Message)>();
            FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(Email, Password).ContinueWith((task) => {
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
                        task.Result.User.SendEmailVerification().AddOnCompleteListener(new SignInWithCredentialsOnComplete());
                        tcs.TrySetResult((true, ""));
                    }
                    else
                    {
                        tcs.TrySetResult((false, "Something went wrong"));
                    }
                }

            });

            return tcs.Task;
        }

        private void OnValidAuth(TaskCompletionSource<bool> taskCompletionVar, Task task)
        {
            if (task.IsFaulted || task.IsCanceled)
            {
                taskCompletionVar.SetResult(false);
                return;
            }
            else
            {
                taskCompletionVar.SetResult(true);

                // Reset verification id as we have successfully authenticated user
                _VerificationID = null;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////
        //                              NEEDS TO BE DONE                                //
        //////////////////////////////////////////////////////////////////////////////////
        public Task<(bool IsCodeSent, string message)> SendOtpCodeAsync(string PhoneNumber)
        {
            _PhoneAuthenticationTCS = new TaskCompletionSource<(bool IsCodeSent, string message)>();

            Java.Lang.Long OtpTimeout = (Java.Lang.Long)(long)60;

            PhoneAuthOptions newOptions = PhoneAuthOptions.
                NewBuilder()
                .SetPhoneNumber(PhoneNumber)
                .SetTimeout(OtpTimeout, TimeUnit.Seconds)
                .SetActivity(Platform.CurrentActivity)
                .SetCallbacks(this).Build();
            PhoneAuthProvider.VerifyPhoneNumber(newOptions);

            return _PhoneAuthenticationTCS.Task;
        }

        public override void OnVerificationCompleted(PhoneAuthCredential p0)
        {
            System.Diagnostics.Debug.WriteLine("PhoneAuthCredential created automatically");
            signInWithPhoneAuthCredential(p0);
        }

        public override void OnVerificationFailed(FirebaseException p0)
        {
            System.Diagnostics.Debug.WriteLine("Verification Failed: " + p0.Message);
            _PhoneAuthenticationTCS?.TrySetResult((false, p0.Message));
        }

        public override void OnCodeSent(string InVerificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
        {
            base.OnCodeSent(InVerificationId, forceResendingToken);
            _VerificationID = InVerificationId;
            _ResendToken = forceResendingToken;
            _PhoneAuthenticationTCS?.TrySetResult((true, ""));
        }

        public string GetCurrentUserUUID()
        {
            return FirebaseAuth.Instance.CurrentUser.Uid;
        }

        private void signInWithPhoneAuthCredential(PhoneAuthCredential credential)
        {
            FirebaseAuth.Instance.SignInWithCredential(credential).AddOnCompleteListener(new SignInWithCredentialsOnComplete());
        }

        public bool UserLoginWithGoogle(Action<bool> GoogleSignInCallBack)
        {
            MainActivity.ThisActivity.GoogleSigInCallBack = GoogleSignInCallBack;
            MainActivity.ThisActivity.StartActivityForResult(MainActivity._googleSignInClient.SignInIntent, MainActivity.RC_SIGN_IN);
            return true;
        }
    }

    public class SignInWithCredentialsOnComplete : Java.Lang.Object, Android.Gms.Tasks.IOnCompleteListener
    {
        public SignInWithCredentialsOnComplete()
        {

        }
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                // Sign in success, update UI with the signed-in user's information
                //Log.d(TAG, "signInWithCredential:success");

                var user = task.Result;
                // Update UI
            }
            else
            {
                // Sign in failed, display a message and update the UI
                //Log.w(TAG, "signInWithCredential:failure", task.getException());
                if (task.Exception is FirebaseAuthInvalidCredentialsException)
                {
                    // The verification code entered was invalid
                }
            }
        }


    }
}