using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grow.Droid
{
    //[Activity(Label = "AndroidSplashScreen", Theme ="@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class AndroidSplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your application here
            StartActivity(typeof(MainActivity));
        }

        /*protected override void OnResume()
        {
            base.OnResume();
            Task startupwork = new Task(() => { AnimateStartup(); });
            startupwork.Start();
        }

        async void AnimateStartup()
        {
            await Task.Delay(5000);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }*/
    }
}