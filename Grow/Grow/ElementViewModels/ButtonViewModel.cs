using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Grow.Core.Base;
using Xamarin.Forms;

namespace Grow.ElementViewModels
{
    public class ButtonViewModel : ExtendedBindableObject
    {
        string _Text;
        bool _IsVisible;
        bool _IsEnabled;
        ICommand _command;

        public ButtonViewModel(string title, ICommand command, bool isVisible = true, bool isEnabled = true)
        {
            Text = title;
            IsVisible = isVisible;
            IsEnabled = isEnabled;
            Command = command;
        }

        public ButtonViewModel(string title, Action action, bool isVisible = true, bool isEnabled = true)
        {
            Text = title;
            IsVisible = isVisible;
            IsEnabled = isEnabled;
            Command = new Command(action);
        }

        #region Fields
        public string Text
        {
            get => _Text;
            set => SetProperty(ref _Text, value);
        }

        public bool IsVisible
        {
            get => _IsVisible;
            set => SetProperty(ref _IsVisible, value);
        }
        public bool IsEnabled
        {
            get => _IsEnabled;
            set => SetProperty(ref _IsEnabled, value);
        }
        public ICommand Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        #endregion
    }
}
