using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.ElementViews.Entries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CountryPicker : ContentView
    {
        public CountryPicker()
        {
            InitializeComponent();
        }
    }
}