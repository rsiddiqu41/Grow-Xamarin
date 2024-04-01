using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Finance;
using Grow.ElementViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.ElementViews.Finance
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class PlaidUI : ContentPage
    {
        IPlaidUIViewModel _vM;

        public PlaidUI()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<PlaidUIViewModel>();

            BindingContext = _vM;
        }

        private async void WebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.ToLower().Contains("plaidlink:"))
            {
                e.Cancel = true;
            }
            else
            {
                e.Cancel = false;
            }
            var linkScheme = "plaidlink";
            var scheme = new Uri(e.Url);
            var param = HttpUtility.ParseQueryString(e.Url);

            var actionScheme = scheme.Scheme;
            var actionType = scheme.Host;

            if (actionScheme == linkScheme)
            {
                switch (actionType)
                {
                    case "connected":
                        Debug.WriteLine("Successfully Connected");

                        await _vM.OnSuccessCallback(scheme);

                        Thread.Sleep(500);

                        _vM.ReturnToOverviewPage();
                        break;

                    case "exit":
                        Debug.WriteLine("Exit");
                        _vM.ReturnToOverviewPage();
                        break;

                    case "event":
                        Debug.WriteLine($"Event name: {param["event_name"]}");
                        break;

                    default:
                        Debug.WriteLine($"Link action detected: {actionType}");
                        break;
                }
            }
        }
    }
}