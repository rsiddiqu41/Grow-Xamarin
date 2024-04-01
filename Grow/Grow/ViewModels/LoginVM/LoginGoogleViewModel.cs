using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Grow.Core.Authentication;
using Grow.Core.Base;
using Grow.Core.Database;
using Grow.Core.ExternalInterfaces.Login;
using Grow.Views;
using Xamarin.Forms;

namespace Grow.ViewModels.LoginVM
{
    class LoginGoogleViewModel : ViewModelBase, ILoginGoogleViewModel
    {
        IAuthenticationService _AuthenticationService;

        public LoginGoogleViewModel(IAuthenticationService InAuthService)
        {
            _AuthenticationService = InAuthService;
            SignUpCommand = new Command(SignUpCommandAction);
        }

        private void SignUpCommandAction(object obj)
        {
            /*_AuthenticationService.UserLoginWithGoogle(async (IsSuccess) =>
            {
                if(IsSuccess)
                {
                    await UserService.AddUpdateUser(new Models.User() { FirstName = "test", LastName = "test", DOB = "12/12/12", City = "us", State = "wa" }, _AuthenticationService.GetCurrentUserUUID());
                    await Shell.Current.GoToAsync($"//{nameof(FinanceOverviewPage)}");
                }
            });*/
        }
        #region Fields

        ICommand _SignUpCommand;
        public ICommand SignUpCommand
        {
            get => _SignUpCommand;
            set => SetProperty(ref _SignUpCommand, value);
        }
        string _Email = "";
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

        #endregion
    }
}
