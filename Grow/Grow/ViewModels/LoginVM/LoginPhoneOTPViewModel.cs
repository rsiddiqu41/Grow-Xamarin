using Grow.Core.Authentication;
using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Login;
using Grow.Models;
using Grow.Views;
using Grow.Views.Registration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Grow.ViewModels.LoginVM
{
    public class LoginPhoneOTPViewModel : ViewModelBase, ILoginPhoneOTPViewModel
    {
        IAuthenticationService _AuthenticationService;
        IUserService _UserService;

        ICommand _VerifyCommand;
        private ICommand _HomeCommand;

        bool _IsHomeButtonVisible = false;

        string _IncorrectLoginPrompt = "";
        string _CodeOne;
        string _CodeTwo;
        string _CodeThree;
        string _CodeFour;
        string _CodeFive;
        string _CodeSix;
        string _OTPTime = string.Empty;
        string _OTPTimeColor = string.Empty;

        const int OTP_TIMEOUT = 60;

        CancellationTokenSource _TimerCancellationTokenSource;
        CancellationToken _LastCancellationToken;

        public LoginPhoneOTPViewModel(IAuthenticationService InAuthenticationService, IUserService InUserService)
        {
            _AuthenticationService = InAuthenticationService;
            _UserService = InUserService;

            HomeCommand = new Command(OnHomeAction);

            _TimerCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(OTP_TIMEOUT));
            RegisterTimer();
        }

        void StopTimer()
        {
            if (_LastCancellationToken != null)
            {
                if (!_LastCancellationToken.IsCancellationRequested)
                {
                    _TimerCancellationTokenSource.Cancel();
                }
            }
        }

        bool IsTimmerExipired()
        {
            if (_LastCancellationToken != null)
            {
                return _LastCancellationToken.IsCancellationRequested;
            }
            return false;
        }

        public async Task RegisterTimer()
        {
            _TimerCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(60));
            _LastCancellationToken = _TimerCancellationTokenSource.Token;
            int second = OTP_TIMEOUT;
            while (!_LastCancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                second--;
                if(second == 0)
                {
                    OTPTimeColor = "Red";
                }
                else
                {
                    OTPTimeColor = "Black";
                }
                await Device.InvokeOnMainThreadAsync(() => {
                    OTPTime = $"{second} sec";
                });
            }
        }

        public void ValidateCode()
        {
            VerifyOTPCode();
        }

        public async void VerifyOTPCode()
        {
            string Code = string.Empty;

            if(CodeOne == null || CodeTwo == null || CodeThree == null || CodeFour == null || CodeFive == null || CodeSix == null)
            {
                ErrorPrompt = "Please enter a 6 digit code.";
                return;
            }

            Code = CodeOne + CodeTwo + CodeThree + CodeFour + CodeFive + CodeSix;

            if (IsTimmerExipired())
            {
                ErrorPrompt = "Timer expired. Please return to the login page to send another otp. Wait one minute before trying again";
                IsHomeButtonVisible = true;
                return;
            }
            StopTimer();
            // Login to account after verifying OTP code
            var (LoginAttempt, Message) = await _AuthenticationService.UserLoginWithPhoneNumberAsync(Code);

            if (LoginAttempt)
            {
                if (await _UserService.IsUserExists())
                {
                    await Shell.Current.GoToAsync($"///{nameof(FinanceOverviewPage)}");
                }
                else
                {
                    Code = "";
                    ErrorPrompt = "";
                    await Shell.Current.GoToAsync($"{nameof(RegisterationPageAfterLogin)}");
                }

            }
            else
            {
                // Verification Unsuccessful, display popup message and send user back to the start page
                ErrorPrompt = Message;
                IsHomeButtonVisible = true;
            }
        }

        private async void OnHomeAction(object obj)
        {
            await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
        }

        #region Properties

        public ICommand HomeCommand
        {
            get => _HomeCommand;
            set => SetProperty(ref _HomeCommand, value);
        }

        public ICommand VerifyCommand
        {
            get => _VerifyCommand;
            set => SetProperty(ref _VerifyCommand, value);
        }

        public bool IsHomeButtonVisible
        {
            get => _IsHomeButtonVisible;
            set => SetProperty(ref _IsHomeButtonVisible, value);
        }

        public string CodeOne
        {
            get => _CodeOne;
            set => SetProperty(ref _CodeOne, value);
        }

        public string CodeTwo
        {
            get => _CodeTwo;
            set => SetProperty(ref _CodeTwo, value);
        }

        public string CodeThree
        {
            get => _CodeThree;
            set => SetProperty(ref _CodeThree, value);
        }

        public string CodeFour
        {
            get => _CodeFour;
            set => SetProperty(ref _CodeFour, value);
        }

        public string CodeFive
        {
            get => _CodeFive;
            set => SetProperty(ref _CodeFive, value);
        }

        public string CodeSix
        {
            get => _CodeSix;
            set => SetProperty(ref _CodeSix, value);
        }

        public string OTPTime
        {
            get => _OTPTime;
            set => SetProperty(ref _OTPTime, value);
        }

        public string OTPTimeColor
        {
            get => _OTPTimeColor;
            set => SetProperty(ref _OTPTimeColor, value);
        }

        public string ErrorPrompt
        {
            get => _IncorrectLoginPrompt;
            set => SetProperty(ref _IncorrectLoginPrompt, value);
        }

        #endregion
    }
}
