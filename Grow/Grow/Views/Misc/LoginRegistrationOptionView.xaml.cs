using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginRegistrationOptionView : ContentView
    {
        public LoginRegistrationOptionView()
        {
            InitializeComponent();

            // Scale frame to 80% of page width
            LoginRegistrationFrame.WidthRequest = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density * 0.80;
            LoginRegistrationFrame.HeightRequest = 26;
        }
    }
}