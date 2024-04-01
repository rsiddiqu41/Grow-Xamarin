using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using Grow.Core.Base;
using Grow.Core.ExternalInterfaces;
using Grow.Core.SystemConfiguration;
using Grow.Models;
using Grow.Views;
using Grow.ViewModels;
using System.ComponentModel;

//[assembly:Dependency(typeof(StartViewModel))]
namespace Grow.ViewModels
{
    class StartViewModel : ViewModelBase, IStartViewModel
    {
        protected ISystemLayoutModel _LayoutModel;

        private ICommand _loginCommand;
        private ICommand _registerCommand;

        private BackgroundWorker _StartupWorker = new BackgroundWorker();

        public StartViewModel(ISystemLayoutModel InSystemLayoutModel)
        {
            _LayoutModel = InSystemLayoutModel;

            LoginCommand = new Command(OnLoginAction);
            RegisterCommand = new Command(OnRegisterAction);

            _StartupWorker.DoWork += (o, e) =>
            {
                LoadItemsForStart();
            };

            _StartupWorker.RunWorkerAsync();

            //LoadItemsForStart();

            if (_LayoutModel.SystemStartLayoutScreen.StartScreenTypes.Count < 1)
            {
                // Shutdown the application if it is not startup properly
                Application.Current.Quit();
            }
        }

        private async void OnLoginAction(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}", true);
        }

        private async void OnRegisterAction(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}", true);
        }

        public List<SystemStartLayoutRecords> LoadItemsForStart()
        {
            _LayoutModel.ReadConfigFileForStartupScreen();

            return _LayoutModel.SystemStartLayoutScreen.StartScreenTypes;
        }

        public List<SystemStartLayoutRecords> StartScreenItems { get => LoadItemsForStart(); }

        public ICommand LoginCommand
        {
            get => _loginCommand;
            set => SetProperty(ref _loginCommand, value);
        }

        public ICommand RegisterCommand
        {
            get => _registerCommand;
            set => SetProperty(ref _registerCommand, value);
        }

        public string SlideTitle
        {
            get
            {
                return SlideTitle;
            }
            set
            {
                SlideTitle = value;
                OnPropertyChanged("SlideTitle");
            }
        }

        public string Details
        {
            get
            {
                return Details;
            }
            set
            {
                Details = value;
                OnPropertyChanged("Details");
            }
        }

        public string ImageUrl
        {
            get
            {
                return ImageUrl;
            }
            set
            {
                ImageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }
    }
}
