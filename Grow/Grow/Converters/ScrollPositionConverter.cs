using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Grow.Converters
{
    public class ScrollPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fmt = new NumberFormatInfo();
            fmt.NegativeSign = "-";

            var position = (double)value;

            var allParams = ((string)parameter).Split((';'));
            var factor = Double.Parse(allParams[0], fmt);
            var min = Double.Parse(allParams[1]);
            var max = Double.Parse(allParams[2]);
            var reverse = bool.Parse(allParams[3]);
            var delayUntilPosition = Double.Parse(allParams[4]);

            if (position == 0) return min;

            position = position * factor;

            if (delayUntilPosition > 0 && position < delayUntilPosition) return min;

            return Math.Max(0, min - position);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
