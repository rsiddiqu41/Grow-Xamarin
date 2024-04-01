using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Login;
using Grow.Core.UIServices;
using Grow.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

namespace Grow.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        ILoginViewModel _vM;

        public LoginPage()
        {             
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<LoginViewModel>();

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