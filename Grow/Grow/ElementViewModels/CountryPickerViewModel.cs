using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

using Grow.Core.Base;
using Grow.Core.UIServices;
using Grow.Models;
using Grow.ElementViews.Entries;

namespace Grow.ElementViewModels
{
    class CountryPickerViewModel : ViewModelBase
    {
        #region Fields

        private CountryModel _selectedCountry;

        #endregion Fields


        #region Constructors

        public CountryPickerViewModel()
        {
            Title = "About";
            SelectedCountry = CountryUtils.GetCountryModelByName("United States");
            ShowPopupCommand = new Command(async _ => await ExecuteShowPopupCommand());
            CountrySelectedCommand = new Command(country => ExecuteCountrySelectedCommand(country as CountryModel));
        }

        #endregion Constructors

        #region Properties

        public CountryModel SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }

        #endregion Properties

        #region Commands

        public ICommand ShowPopupCommand { get; }
        public ICommand CountrySelectedCommand { get; }

        #endregion Commands


        #region Private Methods

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

        #endregion Private Methods
    }
}
