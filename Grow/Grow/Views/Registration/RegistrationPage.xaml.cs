using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grow.ViewModels;
using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Registration;

using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;
//using Xamarin.Forms.Xaml.;
using Xamarin.Essentials;

namespace Grow.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        //string privateKey = "test2grow-452ec-firebase-adminsdk-pwz1j-5b7ee99c13.json";

        IRegistrationViewModel _vM;

        public RegistrationPage()
        {            
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<RegistrationViewModel>();

            BindingContext = _vM;
        }

        private void TermsOfService_Tapped(object sender, EventArgs e)
        {

        }

        private void PrivacyPolicy_Tapped(object sender, EventArgs e)
        {

        }
    }
}