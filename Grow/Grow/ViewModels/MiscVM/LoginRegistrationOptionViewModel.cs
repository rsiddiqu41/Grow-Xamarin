using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

using Xamarin.Forms;

using Grow.Core.Base;

namespace Grow.ViewModels
{
    public class LoginRegistrationOptionViewModel : ViewModelBase
    {
        private Color _BackgroundColor;
        private Color _TextColor;
        private string _Text;
        private int _TextSize;
        private string _Icon;
        private ICommand _TapCommand;

        public LoginRegistrationOptionViewModel(string InText, int InTextSize, Action InTapAction, Color InBgColor, Color InTextColor, string InIcon = "")
        {
            Text = InText;
            TextSize = InTextSize;
            TapCommand = new Command(InTapAction);
            BackgroundColor = InBgColor;
            TextColor = InTextColor;
            Icon = InIcon;
        }

        #region Fields
        public Color BackgroundColor
        {
            get => _BackgroundColor;
            set => SetProperty(ref _BackgroundColor, value);
        }

        public Color TextColor
        {
            get => _TextColor;
            set => SetProperty(ref _TextColor, value);
        }

        public string Text
        {
            get => _Text;
            set => SetProperty(ref _Text, value);
        }

        public int TextSize
        {
            get => _TextSize;
            set => SetProperty(ref _TextSize, value);
        }

        public string Icon
        {
            get => _Icon;
            set => SetProperty(ref _Icon, value);
        }

        public ICommand TapCommand
        {
            get => _TapCommand;
            set => SetProperty(ref _TapCommand, value);
        }
        #endregion
    }
}
