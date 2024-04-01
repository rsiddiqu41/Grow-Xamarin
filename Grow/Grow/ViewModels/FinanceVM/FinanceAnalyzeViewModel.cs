using Grow.Core.Authentication;
using Grow.Core.Base;
using Grow.Core.Mediator;
using Grow.ElementViews.Finance;
using Grow.Models;
using Grow.Views;
using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Grow.ViewModels.FinanceVM
{
    class FinanceAnalyzeViewModel : ViewModelBase
    {
        IAuthenticationService _AuthenticationService;
        IUserService _UserService;
        //IMediator _Messenger;

        private ICommand _PlaidCommand;
        private ICommand _RefreshCommand;
        private ICommand _FlyoutCommand;
        private ICommand _KnowledgeCommand;

        private LayoutState _mainState;

        bool _DisplayPlaidLink;// = false;
        string _userName;
        string _PlaidLinkUri;

        Microcharts.Forms.ChartView _IncomeOutcomeChart;
        Microcharts.Forms.ChartView _CategoricalSpendingChart;

        public FinanceAnalyzeViewModel(IAuthenticationService InAuthenticationService, IUserService InUserService)//, IMediator InMessenger)//InUserService, InMessenger)//, IPlaidHelper InPlaidService)
        {
            _AuthenticationService = InAuthenticationService;
            _UserService = InUserService;
            //_Messenger = InMessenger;

            _IncomeOutcomeChart.Chart = new PieChart();
            _CategoricalSpendingChart.Chart = new BarChart();

            PlaidCommand = new Command(OnPlaidAction);
            RefreshCommand = new Command(OnRefreshAsync);
            FlyoutCommand = new Command(OnFlyoutButtonPressed);
            KnowledgeCommand = new Command(GoToKnowledgeAsync);
            // Get user data
            

            //Check to see if bank is connected. if it is, populate entries variable to update microchart on home page. If not, bring up box to connect bank account stuff
        }

        private async void OnRefreshAsync()
        {
            MainState = LayoutState.Loading;
            IsLoading = true;
            // Update latest 5 transactions and current balance

            await Task.Delay(3000);
            IsLoading = false;
            MainState = LayoutState.None;
        }

        private async void OnPlaidAction(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(PlaidUI)}");
        }

        private async void GoToKnowledgeAsync(object obj)
        {
            await Shell.Current.GoToAsync($"//{nameof(KnowledgeHome)}");
        }

        private void OnFlyoutButtonPressed(object obj)
        {
            Shell.Current.FlyoutIsPresented = true;
        }


        #region Fields
        public ICommand PlaidCommand
        {
            get => _PlaidCommand;
            set => SetProperty(ref _PlaidCommand, value);
        }

        public ICommand RefreshCommand
        {
            get => _RefreshCommand;
            set => SetProperty(ref _RefreshCommand, value);
        }

        public ICommand FlyoutCommand
        {
            get => _FlyoutCommand;
            set => SetProperty(ref _FlyoutCommand, value);
        }

        public ICommand KnowledgeCommand
        {
            get => _KnowledgeCommand;
            set => SetProperty(ref _KnowledgeCommand, value);
        }

        public bool DisplayPlaidLink
        {
            get => _DisplayPlaidLink;
            set => SetProperty(ref _DisplayPlaidLink, value);
        }

        public string PlaidLinkUri
        {
            get => _PlaidLinkUri;
            set => SetProperty(ref _PlaidLinkUri, value);
        }

        public string Name
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        public LayoutState MainState
        {
            get => _mainState;
            set => SetProperty(ref _mainState, value);
        }

        public Microcharts.Forms.ChartView IncomeOutcomeChart
        {
            get => _IncomeOutcomeChart;
            set => SetProperty(ref _IncomeOutcomeChart, value);
        }

        public Microcharts.Forms.ChartView CategoricalSpendingChart
        {
            get => _CategoricalSpendingChart;
            set => SetProperty(ref _CategoricalSpendingChart, value);
        }
        #endregion
    }
}
