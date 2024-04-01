using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Login;
using Grow.ViewModels.LoginVM;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginGooglePage : ContentPage
    {
        ILoginGoogleViewModel _vM;
        public LoginGooglePage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<LoginGoogleViewModel>();

            BindingContext = _vM;
        }
    }
}