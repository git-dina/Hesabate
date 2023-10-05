using Hesabate_POS.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Hesabate_POS.converters
{
    public class urgentColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                bool _value = (bool)value;
                if (_value)
                {
                    return Application.Current.Resources["mediumRed"] as SolidColorBrush;
                }
                else
                {
                    return Application.Current.Resources["MainColor"] as SolidColorBrush;
                }
            }
            catch
            {
                return "";
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
