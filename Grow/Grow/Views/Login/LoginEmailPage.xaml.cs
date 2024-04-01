using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Login;
using Grow.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

namespace Grow.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class LoginEmailPage : ContentPage
    {
        ILoginEmailViewModel _vM;

        public LoginEmailPage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<LoginEmailViewModel>();

            BindingContext = _vM;
        }

        private async void ResetPassword_Tapped(object sender, EventArgs e)
        {
            EmailEntry.Unfocus();
            PasswordEntry.Unfocus();
            await Navigation.PushAsync(new ForgotPasswordView
            {
                BindingContext = new ForgotPasswordView()
            });
        }

        private void SignIn_Clicked(object sender, EventArgs e)
        {
            EmailEntry.Unfocus();
            PasswordEntry.Unfocus();
        }
    }
}