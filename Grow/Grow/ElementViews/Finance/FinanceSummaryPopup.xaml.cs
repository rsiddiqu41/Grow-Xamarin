using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.ElementViews.Finance
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinanceSummaryPopup : PopupPage
    {
        public FinanceSummaryPopup(string InType)
        {
            InitializeComponent();

            switch (InType)
            {
                case "NetWorth":
                    {
                        Header.Text = "What is Net Worth?";
                        Body.Text = "Net worth is a value that represents all of the assets you own minus the total of all liabilities. Basically, what you own minus what you owe";
                        break;
                    }

                case "Balance":
                    {
                        Header.Text = "What is Balance?";
                        Body.Text = "Your balance represents all the current cash you have in all your linked financial accounts";
                        break;
                    }

                case "Investments":
                    {
                        Header.Text = "What are Investments?";
                        Body.Text = "Your investments represents all the current investments you have in all your linked financial accounts. This can include stocks, real estate, crypto, etc";
                        break;
                    }

                case "Loans":
                    {
                        Header.Text = "What are loans?";
                        Body.Text = "The total amount of current outstanding loans in all your linked financial accounts";
                        break;
                    }

                case "CreditCards":
                    {
                        Header.Text = "What does this mean?";
                        Body.Text = "The total of all the currently linked credit cards balances";
                        break;
                    }
                default:
                    {
                        break;
                    }

            }
        }
    }
}