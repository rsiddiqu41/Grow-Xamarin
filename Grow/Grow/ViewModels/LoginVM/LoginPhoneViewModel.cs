using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

using Grow.Core.Authentication;
using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Login;
using Grow.Core.UIServices;
using Grow.ElementViews.Entries;
using Grow.Models;
using Grow.Views;
using Grow.Views.Login;
using Grow.Views.Registration;
using Xamarin.Forms;

namespace Grow.ViewModels
{
    public class LoginPhoneViewModel : ViewModelBase, ILoginPhoneViewModel
    {
        IAuthenticationService _AuthenticationService;

        string _PhoneNumber;
        string _IncorrectLoginPrompt = "";

        string _LengthValidationPromptColor = "Black";
        string _CharacterValidationPromptColor = "Black";

        string[] _PhoneCharToRemove;
        bool _IsPhoneLengthValid = false;
        bool _IsPhoneCharacterValid = false;
        bool _CodeSent;

        CountryModel _SelectedCountry;

        ICommand _NextCommand;

        public LoginPhoneViewModel(IAuthenticationService InAuthenticationService)
        {
            _AuthenticationService = InAuthenticationService;

            _PhoneCharToRemove = new string[] { "(", ")", " ", "-" };

            SelectedCountry = CountryUtils.GetCountryModelByName("United States");
            ShowPopupCommand = new Command(async _ => await ExecuteShowPopupCommand());
            CountrySelectedCommand = new Command(country => ExecuteCountrySelectedCommand(country as CountryModel));

            NextCommand = new Command(OnNextAction);
        }

        private async void OnNextAction(object obj)
        {
            ErrorPrompt = "";

            if (!_IsPhoneLengthValid || !_IsPhoneCharacterValid)
            {
                return;
            }

            if (CodeSent)
            {
                await Shell.Current.GoToAsync($"{nameof(LoginPhoneOTPPage)}");                
            }
            else
            {
                string modifiedPhoneNumber = "+" + SelectedCountry.CountryCode + PhoneNumber;

                foreach (var c in _PhoneCharToRemove)
                {
                    modifiedPhoneNumber = modifiedPhoneNumber.Replace(c, string.Empty);
                }

                var (_CodeSent,message) = await _AuthenticationService.SendOtpCodeAsync(modifiedPhoneNumber);

                if (!_CodeSent)
                {
                    ErrorPrompt = message;
                    //Not successful
                    //Maybe user entered wrong phone number or something like that
                    return;
                }
               
                CodeSent = true;
                await Shell.Current.GoToAsync($"{nameof(LoginPhoneOTPPage)}");
            }
        }


        private Task ExecuteShowPopupCommand()
        {
            var popup = new ChooseCountryPopup(SelectedCountry)
            {
                CountrySelectedCommand = CountrySelectedCommand
            };
            return Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popup);
        }

        private void ExecuteCountrySelectedCommand(CountryModel country)
        {
            SelectedCountry = country;
        }

        public void UpdatePhoneValidationLengthPrompt(bool isValid)
        {
            _IsPhoneLengthValid = isValid;

            LengthValidationPromptColor = _IsPhoneLengthValid ? "Green" : "Red";
        }

        public void UpdatePhoneValidationCharacterPrompt(bool isValid)
        {
            _IsPhoneCharacterValid = isValid;

            CharacterValidationPromptColor = _IsPhoneCharacterValid ? "Green" : "Red";
        }

        #region Fields

        public string LengthValidationPromptColor
        {
            get => _LengthValidationPromptColor;
            set => SetProperty(ref _LengthValidationPromptColor, value);
        }

        public string CharacterValidationPromptColor
        {
            get => _CharacterValidationPromptColor;
            set => SetProperty(ref _CharacterValidationPromptColor, value);
        }

        public string PhoneNumber
        {
            get => _PhoneNumber;
            set => SetProperty(ref _PhoneNumber, value);
        }

        public bool CodeSent
        {
            get => _CodeSent;
            set => SetProperty(ref _CodeSent, value);
        }

        public ICommand NextCommand
        {
            get => _NextCommand;
            set => SetProperty(ref _NextCommand, value);
        }

        public ICommand ShowPopupCommand { get; }
        public ICommand CountrySelectedCommand { get; }

        public CountryModel SelectedCountry
        {
            get => _SelectedCountry;
            set => SetProperty(ref _SelectedCountry, value);
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

        /*private async void OnNextAction(object obj)
        {
            ErrorPrompt = "";

            if (!_IsPhoneLengthValid || !_IsPhoneCharacterValid)
            {
                return;
            }

            if (CodeSent)
            {
                if(IsTimmerExipired())
                {
                    ButtonText = "Send Code";
                    ErrorPrompt = "OTP token expired please send another otp";
                    CodeSent = false;
                    return;
                }
                StopTimmer();
                // Login to account after verifying OTP code
                var (LoginAttempt,Message) = await _AuthenticationService.UserLoginWithPhoneNumberAsync(Code);

                if (LoginAttempt)
                {
                    if (await _userService.IsUserExists())
                    {
                        await Shell.Current.GoToAsync($"///{nameof(FinanceOverviewPage)}");
                    }
                    else
                    {
                        PhoneNumber = "";
                        Code = "";
                        ErrorPrompt = "";
                        ButtonText = "Send Code";
                        CodeSent = false;
                        await Shell.Current.GoToAsync($"{nameof(RegisterationPageAfterLogin)}");
                       
                        //await _userService.AddUpdateUser(new Models.User() { FirstName = "test", LastName = "test", DOB = "12/12/12", City = "us", State = "wa" }, _AuthenticationService.GetCurrentUserUUID());
                    }
                    
                }
                else
                {
                    ErrorPrompt = Message;
                    // Verification Unsuccessful
                }
            }
            else
            {
                StopTimmer();

                string modifiedPhoneNumber = "+" + SelectedCountry.CountryCode + PhoneNumber;

                foreach (var c in _PhoneCharToRemove)
                {
                    modifiedPhoneNumber = modifiedPhoneNumber.Replace(c, string.Empty);
                }

                var (_CodeSent,message) = await _AuthenticationService.SendOtpCodeAsync(modifiedPhoneNumber);

                if (!_CodeSent)
                {
                    ErrorPrompt = message;
                    //Not successful
                    //Maybe user entered wrong phone number or something like that
                    return;
                }
               
                RegisterTimer();
                CodeSent = true;
                ButtonText = "Verify Code";
            }
        }
         */         
    }
}
