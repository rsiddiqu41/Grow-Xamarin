using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Registration;
using Grow.ViewModels;

using Microsoft.Extensions.DependencyInjection;
using Grow.ViewModels.RegistrationVM;

namespace Grow.Views.Registration.Email
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class EmailVerificationLinkSentPage : ContentPage
    {
        IEmailVerificationLinkSentViewModel _vM;
        public EmailVerificationLinkSentPage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<EmailVerificationSentViewModel>();

            BindingContext = _vM;
        }
    }
}