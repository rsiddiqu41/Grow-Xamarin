using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grow.Core.Base;
using Grow.Core.ExternalInterfaces.Login;
using Grow.ViewModels;

using Microsoft.Extensions.DependencyInjection;

using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

namespace Grow.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class LoginPhonePage : ContentPage
    {
        ILoginPhoneViewModel _vM;

        public LoginPhonePage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<LoginPhoneViewModel>();

            BindingContext = _vM;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void PhoneNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((Entry)sender).Text;

            int textLength = text.Length;
            char lastChar = '0';

            bool isValidLength = true;
            bool isValidCharacters = true;

            if(textLength < 14)
            {
                isValidLength = false;
            }

            if(textLength > 0)
            {
                lastChar = text[textLength - 1];
            }

            if (lastChar < '0' || lastChar > '9')
            {
                isValidCharacters = false;
            }

            _vM.UpdatePhoneValidationLengthPrompt(isValidLength);

            _vM.UpdatePhoneValidationCharacterPrompt(isValidCharacters);
        }
    }
}