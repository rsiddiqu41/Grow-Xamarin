using Grow.Core.Authentication;
using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Login;
using Grow.ElementViewModels;
using Grow.Models;
using Grow.Views;
using Grow.Views.Registration;
using Plugin.CloudFirestore;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace Grow.ViewModels
{
    public class LoginEmailViewModel : ViewModelBase, ILoginEmailViewModel
    {
        string _IncorrectLoginPrompt = "";

        string _Email = "";
        string _Password = "";

        string _PasswordCharacterValidationPromptColor;
        string _PasswordLengthValidationPromptColor;

        ICommand _SignInCommand;
        IAuthenticationService _AuthenticationService;
        IUserService _userService;

        public LoginEmailViewModel(IAuthenticationService InAuthService, IUserService InUserService)
        {
            _AuthenticationService = InAuthService;
            _userService = InUserService;

            EmailEntryViewModel = new LoginEntryViewModel("Email", false);
            PasswordEntryViewModel = new LoginEntryViewModel("Password", true);

            SignInCommand = new Command(UserLoginAction);
        }

        private async void UserLoginAction(object obj)
        {
            ErrorPrompt = "";

            if (!IsValidLoginFormat())
            {
                ErrorPrompt = "Our records don't have that Email/Password combination. Please check again.";
                return;
            }

            var LoginAttempt = await _AuthenticationService.UserLoginWithEmailAndPasswordWithEmailVerificationAsync(Email, Password);

            if (!string.IsNullOrEmpty(LoginAttempt.Message))
            {
                ErrorPrompt = LoginAttempt.Message;
            }
            else
            {
                if (LoginAttempt.Success && LoginAttempt.IsEmailVerified)
                {
                    //_NavService.CreatePage<FinanceOverviewViewModel>();
                    if (await _userService.IsUserExists())
                    {
                        await Shell.Current.GoToAsync($"///{nameof(FinanceOverviewPage)}");
                    }
                    else
                    {
                        await Shell.Current.GoToAsync($"{nameof(RegisterationPageAfterLogin)}");
                    }
                }
                else
                {
                    if (!LoginAttempt.IsEmailVerified)
                    {
                        ErrorPrompt = "Please verify your email and then login";
                    }
                    else
                    {
                        ErrorPrompt = "Invalid Username/Password Combination. Please try again.";
                    }
                }
            }
        }

        private bool IsValidLoginFormat()
        {
            bool isValidFormat = true;

            if(string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) || !Email.Contains("@") || !Email.Contains("."))
            {
                isValidFormat = false;
            }

            return isValidFormat;
        }

        public async Task CheckValidLogin()
        {
            var document = await CrossCloudFirestore.Current.Instance.Collection("users").Document("MZjL5DAT9ZrvXsp4Ns3Y").GetAsync();
        }

        public void UpdatePasswordValidationLengthPrompt(bool isValid)
        {
            
        }

        public void UpdatePasswordValidationCharacterPrompt(bool isValid)
        {
            
        }

        #region Fields
        public LoginEntryViewModel EmailEntryViewModel { get; set; }
        public LoginEntryViewModel PasswordEntryViewModel { get; set; }

        public ICommand SignInCommand
        {
            get => _SignInCommand;
            set => SetProperty(ref _SignInCommand, value);
        }

        public string PasswordLengthValidationPromptColor
        {
            get => _PasswordLengthValidationPromptColor;
            set => SetProperty(ref _PasswordLengthValidationPromptColor, value);
        }

        public string PasswordCharacterValidationPromptColor
        {
            get => _PasswordCharacterValidationPromptColor;
            set => SetProperty(ref _PasswordCharacterValidationPromptColor, value);
        }

        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }
        }

        public string ErrorPrompt
        {
            get
            {
                return _IncorrectLoginPrompt;
            }
            set
            {
                _IncorrectLoginPrompt = value;
                OnPropertyChanged("ErrorPrompt");
            }
        }

        #endregion
    }
}
