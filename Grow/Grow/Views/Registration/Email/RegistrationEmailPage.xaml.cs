using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Registration;
using Grow.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

namespace Grow.Views.Registration
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class RegistrationEmailPage : ContentPage
    {
        IRegistrationEmailViewModel _vM;

        public RegistrationEmailPage()
        {
            InitializeComponent();

            SignUpButton.WidthRequest = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density * 0.80;

            _vM = Startup.ServiceProvider.GetService<RegistrationEmailViewModel>();

            BindingContext = _vM;
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            _vM.ValidatePassword();
        }
    }
}