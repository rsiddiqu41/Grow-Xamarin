using Grow.ViewModels;
using Grow.Views;
using System;
using System.IO;
using System.Collections.Generic;
using Xamarin.Forms;
using Grow.Views.Registration;
using Grow.Views.Registration.Email;
using Grow.Views.Registration.PhoneNumber;
using Grow.Views.Login;
using Grow.Core.Authentication;
using Grow.Core.Base;
using Microsoft.Extensions.DependencyInjection;
using Grow.ElementViews;
using Grow.ElementViews.Finance;

namespace Grow
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        IAuthenticationService _AuthenticationService;
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(RegistrationEmailPage), typeof(RegistrationEmailPage));
            Routing.RegisterRoute(nameof(RegistrationPhonePage), typeof(RegistrationPhonePage));
            Routing.RegisterRoute(nameof(RegistrationGooglePage), typeof(RegistrationGooglePage));
            Routing.RegisterRoute(nameof(RegistrationPhoneOTPPage), typeof(RegistrationPhoneOTPPage));
            Routing.RegisterRoute(nameof(RegistrationPhoneExistsPage), typeof(RegistrationPhoneExistsPage));
            Routing.RegisterRoute(nameof(RegisterationPageAfterLogin), typeof(RegisterationPageAfterLogin));
            Routing.RegisterRoute(nameof(EmailVerificationLinkSentPage), typeof(EmailVerificationLinkSentPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(LoginEmailPage), typeof(LoginEmailPage));
            Routing.RegisterRoute(nameof(LoginPhonePage), typeof(LoginPhonePage));
            Routing.RegisterRoute(nameof(LoginGooglePage), typeof(LoginGooglePage));
            Routing.RegisterRoute(nameof(LoginPhoneOTPPage), typeof(LoginPhoneOTPPage));
            Routing.RegisterRoute(nameof(PlaidUI), typeof(PlaidUI));

            Routing.RegisterRoute(nameof(KnowledgeHome), typeof(KnowledgeHome));

            _AuthenticationService = (IAuthenticationService)Startup.ServiceProvider.GetRequiredService(typeof(IAuthenticationService));
            //PlaidApiHelper.InitializeClient();
        }

        private async void MenuLogout_Clicked(object sender, EventArgs e)
        {
            // Logout through firebase first then route back to startpage
            await _AuthenticationService.UserLogoutAsync();
            Application.Current.MainPage = new AppShell();
        }

    }
}
