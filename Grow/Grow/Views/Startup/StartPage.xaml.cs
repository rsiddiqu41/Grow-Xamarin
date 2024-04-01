using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using Grow.ViewModels;
using Grow.Core.Mediator;

using Xamarin.Forms;
//using Xamarin.Forms.Xaml;
using Grow.Core.ExternalInterfaces;
using Grow.Models;
using Grow.Core.Base;

namespace Grow.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        IStartViewModel _vM;

        public StartPage()
        {
            InitializeComponent();

            _vM = Startup.ServiceProvider.GetService<StartViewModel>();

            BindingContext = _vM;
        }
    }
}