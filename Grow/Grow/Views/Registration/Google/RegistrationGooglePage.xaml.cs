using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Registration;
using Grow.ViewModels.RegistrationVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationGooglePage : ContentPage
    {
        IRegistrationGoogleViewModel _vM;
        public RegistrationGooglePage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<RegistrationGoogleViewModel>();

            BindingContext = _vM;
        }
    }
}