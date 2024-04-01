using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Registration;
using Grow.Views;
using Xamarin.Forms;

namespace Grow.ViewModels.RegistrationVM
{
    class RegistrationGoogleViewModel : ViewModelBase, IRegistrationGoogleViewModel
    {
        private ICommand _StartCommand;

        public RegistrationGoogleViewModel()
        {
            _StartCommand = new Command(GoToHomePageAsync);
        }

        private async void GoToHomePageAsync(object obj)
        {
            await Shell.Current.GoToAsync($"///{nameof(StartPage)}");
        }

        public ICommand StartCommand
        {
            get => _StartCommand;
            set => SetProperty(ref _StartCommand, value);
        }
    }
}
