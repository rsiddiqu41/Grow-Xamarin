using Grow.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Google.Cloud.Firestore;

using Grow.ViewModels;
using Grow.Core.Base;
using Grow.Models;
using System.Threading.Tasks;
using Grow.Core.Authentication;
using Microsoft.Extensions.DependencyInjection;

[assembly: ExportFont("HindSiliguri-Light.ttf", Alias = "StartPageDescriptionLight")]
[assembly: ExportFont("HindSiliguri-Regular.ttf", Alias = "StartPageDescriptionRegular")]
[assembly: ExportFont("Poppins-SemiBold.otf", Alias = "PoppinsFont")]
[assembly: ExportFont("Rubik-Regular.ttf", Alias = "RubikNormalFont")]
[assembly: ExportFont("Rubik-Bold.ttf", Alias = "RubikBoldFont")]
[assembly: ExportFont("FontAwesome5Brands.otf", Alias = "FontAwesomeBrands")]
[assembly: ExportFont("FontAwesome5Regular.otf", Alias = "FontAwesomeRegular")]
[assembly: ExportFont("FontAwesome5Solid.otf", Alias = "FontAwesomeSolid")]

namespace Grow
{
    public partial class App : Application
    {
        public App()
        {
            //Add experimental flag for gradient brush
            Device.SetFlags(new[] { "Brush_Experimental" });

            Device.SetFlags(new[] { "Shapes_Experimental", "MediaElement_Experimental" });

            InitializeComponent();

            MainPage = new AppShell();
            Application.Current.PageAppearing += Current_PageAppearing;
        }

        private void Current_PageAppearing(object sender, Page e)
        {
            var _authenticationService = (IAuthenticationService)Startup.ServiceProvider.GetRequiredService(typeof(IAuthenticationService));
            if (!_authenticationService.IsUserLoggedIn() && Shell.Current.CurrentPage != null && Shell.Current.CurrentPage.GetType() != typeof(StartPage) && !Shell.Current.CurrentPage.GetType().FullName.Contains("Reg") && !Shell.Current.CurrentPage.GetType().FullName.Contains("Login"))
            {
                Shell.Current.GoToAsync($"///{nameof(StartPage)}").GetAwaiter().GetResult();
            }
        }

        protected /*async*/ override void OnStart()
        {
            //Add tree uploading screen
            //Also add grow your life slogan and send to login page          

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
