using System;
using CarouselView.FormsPlugin.Droid;
using Lottie.Forms.Platforms.Android;
using Plugin.Fingerprint;
using Firebase;
using Firebase.Auth;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Grow.Core.Base;
using Grow.Core.Authentication;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Grow.Droid.Services;
using Android.Gms.Tasks;

namespace Grow.Droid
{
    [Activity(Label = "Grow", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    [IntentFilter(new[] { "android.intent.action.VIEW" },
        Categories = new[] { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" },
        DataHost = "growtestapp.page.link",
        DataScheme = "https")]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, GoogleApiClient.IOnConnectionFailedListener
    {
        public const int RC_SIGN_IN = 9001;
        public static MainActivity ThisActivity { get; set; }
        public static GoogleSignInClient _googleSignInClient;
        public Action<bool> GoogleSigInCallBack;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            ThisActivity = this;
            base.OnCreate(savedInstanceState);
            LinkAuthentication();
            setUpGoogleApiClient();
            Rg.Plugins.Popup.Popup.Init(this);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossFingerprint.SetCurrentActivityResolver(() => Xamarin.Essentials.Platform.CurrentActivity);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CarouselViewRenderer.Init();
            Startup.Init(ConfigureServices);
            FirebaseApp.InitializeApp(Application.Context);
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            //SetStatusBarColor(Android.Graphics.Color.Transparent);

            LoadApplication(new App());
        }

        private void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            services.AddSingleton<IAuthenticationService, UserAccountService>();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected async override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            //Log.Debug(TAG, "onActivityResult:" + requestCode + ":" + resultCode + ":" + data);

            // Result returned from launching the Intent from GoogleSignInApi.getSignInIntent(...);
            if (requestCode == RC_SIGN_IN)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                await HandleSignInResult(result);
            }
        }

        public async System.Threading.Tasks.Task HandleSignInResult(GoogleSignInResult result)
        {
            //Log.Debug(TAG, "handleSignInResult:" + result.IsSuccess);
            if (result.IsSuccess)
            {
                string idToken = result.SignInAccount.IdToken;
                AuthCredential credential = GoogleAuthProvider.GetCredential(idToken, null);
                IAuthResult authResult = await FirebaseAuth.Instance.SignInWithCredentialAsync(credential);
                if (authResult != null)
                {
                    GoogleSigInCallBack(true);
                }
                else
                {
                    GoogleSigInCallBack(false);
                }
            }
            else
            {
                GoogleSigInCallBack(false);
            }            
        }

        private void LinkAuthentication()
        {
            string link = Intent.Data?.ToString();

            if (!String.IsNullOrEmpty(link) && FirebaseAuth.Instance.IsSignInWithEmailLink(link))
            {
                FirebaseAuth.Instance.SignInWithEmailLink("", link).AddOnCompleteListener(new SignInWithEmailLinksOnComplete());
            }
        }

        void setUpGoogleApiClient()
        {
            // [START configure_signin]
            // Configure sign-in to request the user's ID, email address, and basic
            // profile. ID and basic profile are included in DEFAULT_SIGN_IN.
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                    .RequestIdToken("604898033129-7q6tsercfo1o1gdot4oh10rbpi7rha4k.apps.googleusercontent.com")
                    .RequestEmail()
                    .Build();
            // [END configure_signin]
            _googleSignInClient = GoogleSignIn.GetClient(this, gso);

            // [START build_client]
            // Build a GoogleApiClient with access to the Google Sign-In API and the
            // options specified by gso.
            //var mGoogleApiClient = new GoogleApiClient.Builder(this)
            //        .EnableAutoManage(this /* FragmentActivity */, this /* OnConnectionFailedListener */)
            //        .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
            //        .Build();
            // [END build_client]

        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            // An unresolvable error has occurred and Google APIs (including Sign-In) will not
            // be available.
            // Log.Debug(TAG, "onConnectionFailed:" + result);
        }


    }

    public class SignInWithEmailLinksOnComplete : Java.Lang.Object, Android.Gms.Tasks.IOnCompleteListener
    {
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var user = task.Result;
            }
            else
            {
                /*bool isFileExists = false;

                for(FileObserver f in file)
                {
                    if(f == fileName)
                    {
                        isFileExists = true;
                        // update file with new acc data
                    }
                }

                if (!isFileExists)
                {
                    // create the new file with the new data
                }*/
            }
        }

    }
}