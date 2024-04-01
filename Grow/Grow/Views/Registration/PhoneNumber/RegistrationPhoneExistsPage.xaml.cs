using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Registration;
using Grow.ViewModels.RegistrationVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.Views.Registration.PhoneNumber
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPhoneExistsPage : ContentPage
    {
        IRegistrationPhoneExistsViewModel _vM;
        public RegistrationPhoneExistsPage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<RegistrationPhoneExistsViewModel>();

            BindingContext = _vM;
        }
    }
}