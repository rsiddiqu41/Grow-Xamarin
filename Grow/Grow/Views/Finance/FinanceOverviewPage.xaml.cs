using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grow.ViewModels;
using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Finance;

using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Grow.ElementViews.Finance;
using Microcharts;
using SkiaSharp;

namespace Grow.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class FinanceOverviewPage : ContentPage
    {
        IFinanceOverviewViewModel _vM;

        public FinanceOverviewPage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<FinanceOverviewViewModel>();

            BindingContext = _vM;

            double temp = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height / Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density * 0.50;

            MainLayout.Margin = new Thickness(0,temp,0,0);
        }

        private void NetWorth_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FinanceSummaryPopup("NetWorth"));
        }

        private void Balance_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FinanceSummaryPopup("Balance"));
        }

        private void Investments_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FinanceSummaryPopup("Investments"));
        }

        private void Loans_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FinanceSummaryPopup("Loans"));
        }

        private void CreditCards_Tapped(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new FinanceSummaryPopup("CreditCards"));
        }
    }
}