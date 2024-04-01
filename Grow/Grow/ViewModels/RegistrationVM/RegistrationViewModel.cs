using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Grow.Models;
using Grow.Core.Base;
using Grow.Views;
using Grow.Views.Registration;
using Grow.Core.ExternalInterfaces.Registration;
using System.Windows.Input;

namespace Grow.ViewModels
{
    public class RegistrationViewModel : ViewModelBase, IRegistrationViewModel
    {
        LoginRegistrationOptionViewModel _RegistrationEmailViewModel;
        LoginRegistrationOptionViewModel _RegistrationPhoneViewModel;
        LoginRegistrationOptionViewModel _RegistrationGoogleViewModel;

        ICommand _BackCommand;

        string _Icon;

        public RegistrationViewModel()
        {
            BackCommand = new Command(GoBackAsync);

            RegistrationPhoneViewModel = new LoginRegistrationOptionViewModel(
                "Sign up with Phone",
                20,
                GoToPhoneRegistrationAsync,
                Color.Black,
                Color.White,
                "Phone.png"
                );

            RegistrationEmailViewModel = new LoginRegistrationOptionViewModel(
                "Sign up with Email",
                20,
                GoToEmailRegistrationAsync,
                Color.FromHex("#00B687"),
                Color.White,
                "Mail.png"
                );

            RegistrationGoogleViewModel = new LoginRegistrationOptionViewModel(
                "Sign up with Google",
                20,
                GoToGoogleLoginAsync,
                Color.FromHex("#9e9e9e"),
                Color.White,
                "GoogleLogo.png"
                );
        }

        private async void GoToPhoneRegistrationAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationPhonePage));
        }

        private async void GoToEmailRegistrationAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationEmailPage));
        }

        private async void GoToGoogleLoginAsync()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationGooglePage));
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

        public LoginRegistrationOptionViewModel RegistrationPhoneViewModel
        {
            get => _RegistrationPhoneViewModel;
            set => SetProperty(ref _RegistrationPhoneViewModel, value);
        }

        public LoginRegistrationOptionViewModel RegistrationEmailViewModel
        {
            get => _RegistrationEmailViewModel;
            set => SetProperty(ref _RegistrationEmailViewModel, value);
        }

        public LoginRegistrationOptionViewModel RegistrationGoogleViewModel
        {
            get => _RegistrationGoogleViewModel;
            set => SetProperty(ref _RegistrationGoogleViewModel, value);
        }
        #endregion
    }
}
