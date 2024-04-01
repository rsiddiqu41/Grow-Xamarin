using Grow.Core.Base;
using Grow.ViewModels.RegistrationVM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterationPageAfterLogin : ContentPage
    {
        IRegisterationPageAfterLoginViewModel _vM;
        public RegisterationPageAfterLogin()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<RegisterationPageAfterLoginViewModel>();

            BindingContext = _vM;
        }

        private async void LocateMe_Tapped(object sender, EventArgs e)
        {
            try
            {
                double UserLattitude, UserLongitude;

                var UserLocation = await Geolocation.GetLastKnownLocationAsync();

                if (UserLocation == null)
                {
                    UserLocation = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromSeconds(30)));
                }

                if(UserLocation == null)
                {
                    // display to user that location cannot be found
                }
                else
                {
                    UserLattitude = UserLocation.Latitude;

                    UserLongitude = UserLocation.Longitude;

                    _vM.UpdateUserLocation(UserLattitude, UserLongitude);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Location not found. {ex.Message}");
            }
        }
    }
}