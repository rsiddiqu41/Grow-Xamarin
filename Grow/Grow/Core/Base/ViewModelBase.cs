using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading.Tasks;

using Xamarin.Forms;
using Grow.Models;

namespace Grow.Core.Base
{
    public class ViewModelBase : ExtendedBindableObject, INotifyPropertyChanged
    {
        string _title;
        /// <summary>
        /// Title of the Page, settable in the PageModel
        /// </summary>
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        bool _isLoading;
        /// <summary>
        /// Flag to notify the Page on network activity
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }


        /// <summary>
        /// Performs View Model initialization Asynchronously
        /// </summary>
        /// <param name="navigationData"></param>
        /// <returns></returns>
        public virtual Task InitializeAsync(object NavigationData)
        {
            return Task.CompletedTask;
        }

        /*public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/

    }
}
