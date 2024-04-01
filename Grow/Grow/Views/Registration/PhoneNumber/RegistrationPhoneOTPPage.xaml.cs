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

namespace Grow.Views.Registration
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPhoneOTPPage : ContentPage
    {
        IRegistrationPhoneOTPViewModel _vM;
        public List<Entry> myEntryList { get; set; }

        int CODE_LENGTH = 0;

        public RegistrationPhoneOTPPage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<RegistrationPhoneOTPViewModel>();

            BindingContext = _vM;

            myEntryList = new List<Entry>();
            myEntryList.Add(this.Code1);
            myEntryList.Add(this.Code2);
            myEntryList.Add(this.Code3);
            myEntryList.Add(this.Code4);
            myEntryList.Add(this.Code5);
            myEntryList.Add(this.Code6);
        
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(async () => { await Task.Delay(250); Code1.Focus(); });

            /*await Task.Run(() =>
            {
                Task.Delay(300);

                Device.BeginInvokeOnMainThread(async () =>
                {
                    Code1.Focus();
                });
            });*/
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry; // .. and check for null

            int entryLength = entry.Text.Length;

            if (entryLength >= 1)
            {
                var index = myEntryList.IndexOf(entry); // 
                var nextIndex = (index + 1) >= myEntryList.Count ? 0 : index + 1; //first or next element?
                var next = myEntryList.ElementAt(nextIndex);

                if (CODE_LENGTH == 5)
                {
                    entry.Unfocus();
                    Task.Delay(300);
                    _vM.ValidateCode();
                    return;
                }
                next?.Focus();
                CODE_LENGTH++;
            }
        }
    }
}