using Grow.Core.Authentication;
using Grow.Core.Base;
using Grow.Models;
using Grow.Views;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace Grow.ViewModels.RegistrationVM
{
    public interface IRegisterationPageAfterLoginViewModel
    {        void UpdateUserLocation(double InLattitude, double InLongitude);
    }

    public class RegisterationPageAfterLoginViewModel : ViewModelBase, IRegisterationPageAfterLoginViewModel
    {
        IAuthenticationService _AuthenticationService;
        IUserService _UserService;

        ICommand _SignUpCommand;

        string[] StateList;

        string _City = "";
        string _State = "";
        string _Country = "";
        string _FirstName = "";
        string _LastName = "";
        string _Location = "";

        int Balance = 0;
        int NetWorth = 0;
        int Loans = 0;
        int CreditCards = 0;

        DateTime _DOB;

        public RegisterationPageAfterLoginViewModel(IAuthenticationService InAuthService, IUserService InUserService)
        {
            _AuthenticationService = InAuthService;
            _UserService = InUserService;

            SignUpCommand = new Command(async (a) => { await UserRegisterAction(a); });

            StateList = new string[] { "AL", "AK", };
        }

        private async Task UserRegisterAction(object obj)
        {
            // Register user on a separate thread
            bool validFields = CheckValidRegisterValues();

            if (validFields)
            {
                var user = new User() {
                    IsFinanceAccountConnected = false,
                    City = City,
                    State = State,
                    Country = Country,
                    FirstName = FirstName,
                    LastName = LastName,
                    DOB = DOB.ToString("yyyy-MM-dd"),
                    Investments = 0,
                    Cash = 0,
                    NetWorth = 0,
                    Loans = 0,
                    CreditCards = 0,
                    FinanceAccountTypes = new HashSet<string>(),
                    AccessTokenDict = new Dictionary<string, HashSet<string>>(),
                    FinanceAccountsDict = new Dictionary<string, Acklann.Plaid.Entity.Account>()
                };

                if (await _UserService.AddUpdateUser(user))
                {
                    City = "";
                    State = "";
                    Country = "";
                    FirstName = "";
                    LastName = "";
                    Balance = 0;
                    NetWorth = 0;
                    Loans = 0;
                    CreditCards = 0;
                    DOB = new DateTime(year: 1900,month:1,day:1);

                    await Shell.Current.GoToAsync($"///{nameof(FinanceOverviewPage)}");
                }                
            }
        }

        public bool CheckValidRegisterValues()
        {
            // Check to see if Name and Password are valid (i.e non empty)
            if (string.IsNullOrWhiteSpace(City) || string.IsNullOrWhiteSpace(State) || string.IsNullOrWhiteSpace(Country) || string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) || DOB == null )
            {
                return false;
            }

            return true;
        }

        public async void UpdateUserLocation(double InLattitude, double InLongitude)
        {
            var address = await Geocoding.GetPlacemarksAsync(InLattitude, InLongitude);

            Placemark placemark = address?.FirstOrDefault();

            if (placemark != null)
            {
                City = placemark.Locality;
                State = placemark.AdminArea;
                Country = placemark.CountryCode;
            }
        }

        public ICommand SignUpCommand
        {
            get => _SignUpCommand;
            set => SetProperty(ref _SignUpCommand, value);
        }
        public string City
        {
            get => _City;
            set => SetProperty(ref _City, value);
        }

        public string State
        {
            get => _State;
            set => SetProperty(ref _State, value);
        }

        public string Country
        {
            get => _Country;
            set => SetProperty(ref _Country, value);
        }

        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public DateTime DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
                OnPropertyChanged("DOB");
            }
        }
    }
}
