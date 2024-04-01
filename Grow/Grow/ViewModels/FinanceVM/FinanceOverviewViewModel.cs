using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using Grow.Core.Base;
using Grow.Core.Authentication;
using Grow.Core.ExternalInterfaces.Finance;

using System.Threading.Tasks;
using Grow.Models;
//using Grow.Core.Api.PlaidApi;
using System.Windows.Input;
using Grow.ElementViews;
using Grow.ElementViews.Finance;
using Xamarin.CommunityToolkit.UI.Views;
using Grow.Views;
using Grow.Core.Mediator;
using Microcharts;
using Grow.Core.Finance;
using System.Collections.ObjectModel;
using Grow.Core.SystemConfiguration;
using System.ComponentModel;

namespace Grow.ViewModels
{
    class FinanceOverviewViewModel : UserViewModelBase, IFinanceOverviewViewModel
    {
        IAuthenticationService _AuthenticationService;
        IUserService _UserService;
        IMediator _Messenger;
        IFinancialSummaryService _SummaryService;
        ISystemLayoutModel _LayoutModel;

        private ICommand _PlaidCommand;
        private ICommand _RefreshCommand;
        private ICommand _FlyoutCommand;
        private ICommand _KnowledgeCommand;

        private LayoutState _mainState;

        bool _IsFinanceAccountConnected;
        bool _DisplayPlaidLink;// = false;
        string _userName = "";
        string _PlaidLinkUri = "";

        int _NetWorth = 0;
        int _Cash = 0;
        int _Investments = 0;
        int _CreditCards = 0;
        int _Loans = 0;

        ObservableCollection<LineChartModel> _AccountData;
        //Chart _BalanceLiabilityChart;

        string _ChartColor = "#2edeaa";

        private BackgroundWorker _StartupWorker = new BackgroundWorker();

        public FinanceOverviewViewModel(IAuthenticationService InAuthenticationService, IUserService InUserService, IMediator InMessenger, IFinancialSummaryService InSummaryService, ISystemLayoutModel InLayoutModel) : base(InUserService, InMessenger)//, IPlaidHelper InPlaidService)
        {
            _AuthenticationService = InAuthenticationService;
            _UserService = InUserService;
            _LayoutModel = InLayoutModel;
            _Messenger = InMessenger;
            _SummaryService = InSummaryService;

            PlaidCommand = new Command(OnPlaidAction);
            RefreshCommand = new Command(OnRefreshAsync);
            FlyoutCommand = new Command(OnFlyoutButtonPressed);
            KnowledgeCommand = new Command(GoToKnowledgeAsync);

            MainState = LayoutState.Loading;
            IsLoading = true;

            _StartupWorker.DoWork += (o, e) =>
            {
                LoadItemsForStart();
            };

            _StartupWorker.RunWorkerAsync();

            if (_LayoutModel.FinanceQuoteLayoutScreen.FinanceQuotesList.Count < 1)
            {
                // Shutdown the application if it is not startup properly
                Application.Current.Quit();
            }

            _Messenger.Register("LoadUser", InitializePage);

            AccountData = new ObservableCollection<LineChartModel>();
        }



        public void InitializePage()
        {
            Name = CurrentUser.FirstName;

            // Retrieve latest data from database
            OnRefreshAsync();

            /*if (!CurrentUser.IsFinanceAccountConnected)
            {
                DisplayPlaidLink = true;
            }
            else
            {
                DisplayPlaidLink = false;

                //Get finance account info from "Accounts" collection in database 
            }*/
        }

        private async void OnRefreshAsync()
        {
            if(MainState != LayoutState.Loading || !IsLoading)
            {
                MainState = LayoutState.Loading;
                IsLoading = true;
            }

            UpdateFinanceSummaryData();

            // Update latest 5 transactions and current balance
            //Cash = CurrentUser.Cash;
            //Loans = CurrentUser.Loans;
            //NetWorth = CurrentUser.NetWorth;
            //CreditCards = CurrentUser.CreditCards;

            await Task.Delay(1000);
            IsLoading = false;
            MainState = LayoutState.None;
        }

        private void UpdateFinanceSummaryData()
        {
            //Update Net Worth
            _SummaryService.UpdateSummaryItems(CurrentUser.FinanceAccountsDict, out _Cash, out _Loans, out _CreditCards, out _Investments, out _NetWorth);

            Cash = _Cash;
            Loans = _Loans;
        }

        public List<FinanceQuotesModel> LoadItemsForStart()
        {
            _LayoutModel.ReadConfigFileForFinancialQuotesView();

            return _LayoutModel.FinanceQuoteLayoutScreen.FinanceQuotesList;
        }

        public List<FinanceQuotesModel> FinanceQuotesItems { get => LoadItemsForStart(); }

        private async void OnPlaidAction(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(PlaidUI)}");
        }

        private async void GoToKnowledgeAsync(object obj)
        {
            await Shell.Current.GoToAsync($"///{nameof(KnowledgeHome)}");
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

        public int NetWorth 
        {
            get => _NetWorth;
            set => SetProperty(ref _NetWorth, value);
        }

        public int Cash
        {
            get => _Cash;
            set => SetProperty(ref _Cash, value);
        }

        public int Investments
        {
            get => _Investments;
            set => SetProperty(ref _Investments, value);
        }

        public int Loans
        {
            get => _Loans;
            set => SetProperty(ref _Loans, value);
        }

        public int CreditCards
        {
            get => _CreditCards;
            set => SetProperty(ref _CreditCards, value);
        }

        public bool IsFinanceAccountConnected 
        { 
            get => _IsFinanceAccountConnected;
            set => SetProperty(ref _IsFinanceAccountConnected, value);
        }

        public ObservableCollection<LineChartModel> AccountData
        {
            get => _AccountData;
            set => SetProperty(ref _AccountData, value);
        }

        public string Author
        {
            get
            {
                return Author;
            }
            set
            {
                Author = value;
                OnPropertyChanged("Author");
            }
        }

        public string Quote
        {
            get
            {
                return Quote;
            }
            set
            {
                Quote = value;
                OnPropertyChanged("Quote");
            }
        }

        /*public Chart BalanceLiabilityChart
        {
            get => _BalanceLiabilityChart;
            set => SetProperty(ref _BalanceLiabilityChart, value);
        }*/

        #endregion
    }
}
