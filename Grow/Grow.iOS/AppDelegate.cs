using System;
using System.Collections.Generic;
using System.Linq;
using CarouselView.FormsPlugin.iOS;

using Foundation;
using UIKit;

using Lottie.Forms.Platforms.Ios;
using Grow.Core.Base;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Grow.Core.Authentication;
using Grow.iOS.Services;

using Google.SignIn;
using Firebase.Analytics;

namespace Grow.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Rg.Plugins.Popup.Popup.Init();
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init();
            CarouselViewRenderer.Init();
            Startup.Init(ConfigureServices);
            var googleServiceDictionary = NSDictionary.FromFile("GoogleService-Info.plist");
            SignIn.SharedInstance.ClientId = googleServiceDictionary["CLIENT_ID"].ToString();
            LoadApplication(new App());

            Firebase.Core.App.Configure();
            Analytics.SetAnalyticsCollectionEnabled(true);

            return base.FinishedLaunching(app, options);
        }

        private void SharedInstance_SignedIn(object sender, SignInDelegateEventArgs e)
        {
            //string a = e.User.Authentication.IdToken;
            var ab = SignIn.SharedInstance.CurrentUser;
        }

        // For iOS 9 or newer
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            var openUrlOptions = new UIApplicationOpenUrlOptions(options);
            return SignIn.SharedInstance.HandleUrl(url);
        }

        // For iOS 8 and older
        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return SignIn.SharedInstance.HandleUrl(url);
        }
        private void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationService, UserAccountService>();
        }
    }
}
