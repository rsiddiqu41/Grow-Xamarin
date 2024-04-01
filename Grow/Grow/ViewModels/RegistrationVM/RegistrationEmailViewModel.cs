using Grow.Core.Authentication;
using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Registration;
using Grow.Views.Registration.Email;

using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Grow.ViewModels
{
    public class RegistrationEmailViewModel : ViewModelBase, IRegistrationEmailViewModel
    {
        ICommand _SignUpCommand;
        IAuthenticationService _AuthenticationService;

        bool _IsPasswordLengthValid = false;
        bool _IsPasswordLowercaseValid = false;
        bool _IsPasswordUppercaseValid = false;
        bool _IsPasswordSymbolValid = false;
        bool _IsPasswordNumericValid = false;

        private string _PasswordLengthValidationPromptColor = "Black";
        private string _PasswordLowerCaseValidationPromptColor = "Black";
        private string _PasswordUpperCaseValidationPromptColor = "Black";
        private string _PasswordCharacterSymbolValidationPromptColor = "Black";
        private string _PasswordCharacterNumericValidationPromptColor = "Black";

        char[] _ValidSymbols;

        private string _Email = "";
        private string _Password = "";
        private string _IncorrectLoginPrompt = "";

        public RegistrationEmailViewModel(IAuthenticationService InAuthService)
        {
            _AuthenticationService = InAuthService;

            _ValidSymbols = new char[] { '!', '@', '#', '$', '^', '*', '(', ')', '-', '_', '[', ']', '{', '}' };

            SignUpCommand = new Command(async (a) => { await UserRegisterAction(a); });
        }

        private async Task UserRegisterAction(object obj)
        {
            // Create an email user on a separate thread

            bool validEmailPasswordFormat = CheckValidRegisterValues();

            if (validEmailPasswordFormat)
            {
                (bool IsSuccess, string Message) = await _AuthenticationService.CreateEmailUser(Email, Password);
                {
                    if (IsSuccess)
                    {
                        await Shell.Current.GoToAsync($"{nameof(EmailVerificationLinkSentPage)}");
                    }
                    else
                    {
                        ErrorPrompt = Message;
                    }
                }
            }
            else
            {
                ErrorPrompt = "Registration Failed. Double check to ensure that your are entering a valid email and password.";
            }            
        }

        public bool CheckValidRegisterValues()
        {
            // Check to see if Name and Password are valid (i.e non empty)
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            //Check to see if email is valid format

            //Check to see if password is valid format
            
            bool isValidPassword = ValidatePassword();

            if (!isValidPassword)
            {
                return false;
            }

            return true;
        }

        public bool ValidatePassword()        
        {           
            int lowercaseCount = 0;
            int uppercaseCount = 0;
            int numericCount = 0;

            foreach (char c in Password)
            {
                if (c >= 'a' && c <= 'z')
                {
                    lowercaseCount++;
                }

                if (c >= 'A' && c <= 'Z')
                {
                    uppercaseCount++;
                }

                if (c >= '0' && c <= '9')
                {
                    numericCount++;
                }                
            }

            #region Password Lowercase Check

            if (lowercaseCount > 0)
            {
                _IsPasswordLowercaseValid = true;
                PasswordLowerCaseValidationPromptColor = "Green";
            }

            else
            {
                _IsPasswordLowercaseValid = false;
                PasswordLowerCaseValidationPromptColor = "Red";
            }

            #endregion

            #region Password Uppercase Check

            if(uppercaseCount > 0)
            {
                _IsPasswordUppercaseValid = true;
                PasswordUpperCaseValidationPromptColor = "Green";
            }

            else
            {
                _IsPasswordUppercaseValid = false;
                PasswordUpperCaseValidationPromptColor = "Red";
            }

            #endregion

            #region Password Numeric Character Check            

            if (numericCount > 0)
            {
                _IsPasswordNumericValid = true;
                PasswordCharacterNumericValidationPromptColor = "Green";
            }

            else
            {
                _IsPasswordNumericValid = false;
                PasswordCharacterNumericValidationPromptColor = "Red";
            }

            #endregion

            #region Password Symbol Character Check

            if (Password.IndexOfAny(_ValidSymbols) == -1)
            {
                _IsPasswordSymbolValid = false;
                PasswordCharacterSymbolValidationPromptColor = "Red";
            }
            else
            {
                _IsPasswordSymbolValid = true;
                PasswordCharacterSymbolValidationPromptColor = "Green";
            }

            #endregion

            #region Password Length Check

            _IsPasswordLengthValid = Password.Length >= 8 ? true : false;

            if (_IsPasswordLengthValid)
            {
                PasswordLengthValidationPromptColor = "Green";
            }
            else
            {
                PasswordLengthValidationPromptColor = "Red";
            }

            #endregion

            return _IsPasswordLowercaseValid && _IsPasswordUppercaseValid && _IsPasswordNumericValid && _IsPasswordSymbolValid && _IsPasswordLengthValid;
        }

        #region Properties

        public ICommand SignUpCommand
        {
            get => _SignUpCommand;
            set => SetProperty(ref _SignUpCommand, value);
        }

        public string PasswordLengthValidationPromptColor
        {
            get => _PasswordLengthValidationPromptColor;
            set => SetProperty(ref _PasswordLengthValidationPromptColor, value);
        }

        public string PasswordLowerCaseValidationPromptColor
        {
            get => _PasswordLowerCaseValidationPromptColor;
            set => SetProperty(ref _PasswordLowerCaseValidationPromptColor, value);
        }

        public string PasswordUpperCaseValidationPromptColor
        {
            get => _PasswordUpperCaseValidationPromptColor;
            set => SetProperty(ref _PasswordUpperCaseValidationPromptColor, value);
        }

        public string PasswordCharacterSymbolValidationPromptColor
        {
            get => _PasswordCharacterSymbolValidationPromptColor;
            set => SetProperty(ref _PasswordCharacterSymbolValidationPromptColor, value);
        }

        public string PasswordCharacterNumericValidationPromptColor
        {
            get => _PasswordCharacterNumericValidationPromptColor;
            set => SetProperty(ref _PasswordCharacterNumericValidationPromptColor, value);
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
