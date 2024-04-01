using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Login;
using Grow.Views;
using Grow.Views.Login;

using Xamarin.Forms;
using Xamarin.Essentials;
using Grow.Core.Authentication;
using Grow.Models;
using Grow.Views.Registration;

namespace Grow.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        LoginRegistrationOptionViewModel _LoginEmailViewModel;
        LoginRegistrationOptionViewModel _LoginPhoneViewModel;
        LoginRegistrationOptionViewModel _LoginGoogleViewModel;

        ICommand _BackCommand;

        string _Icon;

        IAuthenticationService _AuthenticationService;
        readonly IUserService _userService;
        public LoginViewModel(IAuthenticationService InAuthService,IUserService userService)
        {
            _AuthenticationService = InAuthService;
            _userService = userService;

            BackCommand = new Command(GoBackAsync);

            LoginPhoneViewModel = new LoginRegistrationOptionViewModel(
                "Sign in with Phone",
                20,
                GoToPhoneLoginAsync,
                Color.Black,
                Color.White,
                "Phone.png"
                );

            LoginEmailViewModel = new LoginRegistrationOptionViewModel(
                "Sign in with Email",
                20,
                GoToEmailLoginAsync,
                Color.FromHex("#00B687"),
                Color.White,
                "Mail.png"
                );

            LoginGoogleViewModel = new LoginRegistrationOptionViewModel(
                "Sign in with Google",
                20,
                GoToGoogleLoginAsync,
                Color.FromHex("#9e9e9e"),
                Color.White,
                "GoogleLogo.png"
                );
        }

        private async void GoToPhoneLoginAsync()
        {
            await Shell.Current.GoToAsync(nameof(LoginPhonePage), true);
        }

        private async void GoToEmailLoginAsync()
        {
            await Shell.Current.GoToAsync(nameof(LoginEmailPage));
        }

        private async void GoToGoogleLoginAsync()
        {
           
            _AuthenticationService.UserLoginWithGoogle(async (IsSuccess) =>
            {
                if (IsSuccess)
                {
                    if (await _userService.IsUserExists())
                    {
                        await Shell.Current.GoToAsync($"///{nameof(FinanceOverviewPage)}");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync($"{nameof(RegisterationPageAfterLogin)}");
                        //await _userService.AddUpdateUser(new Models.User() { FirstName = "test", LastName = "test", DOB = "12/12/12", City = "us", State = "wa" }, _AuthenticationService.GetCurrentUserUUID());
                    }                    
                }
            });
        }

        private async void GoBackAsync()
        {
            await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
        }

        #region Fields
        public string Icon
        {
            get => _Icon;
            set => SetProperty(ref _Icon, value);
        }

        public ICommand BackCommand
        {
            get => _BackCommand;
            set => SetProperty(ref _BackCommand, value);
        }

        public LoginRegistrationOptionViewModel LoginPhoneViewModel
        {
            get => _LoginPhoneViewModel;
            set => SetProperty(ref _LoginPhoneViewModel, value);
        }

        public LoginRegistrationOptionViewModel LoginEmailViewModel
        {
            get => _LoginEmailViewModel;
            set => SetProperty(ref _LoginEmailViewModel, value);
        }

        public LoginRegistrationOptionViewModel LoginGoogleViewModel
        {
            get => _LoginGoogleViewModel;
            set => SetProperty(ref _LoginGoogleViewModel, value);
        }
        #endregion
    }
}
