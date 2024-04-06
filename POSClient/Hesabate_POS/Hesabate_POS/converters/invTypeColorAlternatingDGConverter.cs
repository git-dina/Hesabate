using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace Hesabate_POS.converters
{
    public class invTypeColorAlternatingDGConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    string text = value.ToString();
                    if (text == "0")
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#f3f3f3"));
                    else
                        return (SolidColorBrush)(new BrushConverter().ConvertFrom("#00FFFFFF"));
                }
                else return (SolidColorBrush)(new BrushConverter().ConvertFrom("#f3f3f3"));
            }
            catch
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#f3f3f3"));
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch
            {
                return value;
            }
        }
    }
}
