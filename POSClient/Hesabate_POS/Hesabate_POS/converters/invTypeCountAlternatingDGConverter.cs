using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Hesabate_POS.converters
{
     public class invTypeCountAlternatingDGConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    string text = value.ToString();
                    if (text == "0")
                        return int.Parse("2");
                    else
                        return int.Parse("999999");
                }
                else return int.Parse("2");
            }
            catch
            {
                return int.Parse("2");
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
