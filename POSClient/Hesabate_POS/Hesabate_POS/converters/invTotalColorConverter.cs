using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace Hesabate_POS.converters
{
    public class invTotalColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    decimal num =decimal.Parse( value.ToString());
                    if (num >= 0)
                        return Application.Current.Resources["MainColor"] as SolidColorBrush;
                    else
                        return Application.Current.Resources["mediumRed"] as SolidColorBrush;
                }
                else return Application.Current.Resources["MainColor"] as SolidColorBrush;
            }
            catch
            {
                return Application.Current.Resources["MainColor"] as SolidColorBrush;
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
