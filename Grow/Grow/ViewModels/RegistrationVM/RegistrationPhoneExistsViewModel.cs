using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Registration;
using Grow.Models;
using Grow.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Grow.ViewModels.RegistrationVM
{
    class RegistrationPhoneExistsViewModel : ViewModelBase, IRegistrationPhoneExistsViewModel
    {
        private ICommand _StartCommand;
        IUserService _UserService;

        string _Header;
        string _Body;

        public RegistrationPhoneExistsViewModel(IUserService InUserService)
        {
            _UserService = InUserService;

            StartCommand = new Command(GoToHomePageAsync);

            UpdateFields();
        }

        private async void UpdateFields()
        {
            if (await _UserService.IsUserExists())
            {
                Header = "Account Exists";
                Body = "A phone number associated with this account already exists. Return to the phone login page to sign in";
            }
            else
            {
                Header = "Account Created Successfully";
                Body = "Your account was created successfully! You can now return to the login page to sign in with the number you registered with";
            }
        }

        private async void GoToHomePageAsync(object obj)
        {
            await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
        }

        public string Header
        {
            get => _Header;
            set => SetProperty(ref _Header, value);
        }

        public string Body
        {
            get => _Body;
            set => SetProperty(ref _Body, value);
        }

        public ICommand StartCommand
        {
            get => _StartCommand;
            set => SetProperty(ref _StartCommand, value);
        }
    }
}
