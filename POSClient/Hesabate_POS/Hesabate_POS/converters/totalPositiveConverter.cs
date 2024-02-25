using Hesabate_POS.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hesabate_POS.converters
{
   public class totalPositiveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value != null)
                {
                    decimal num = decimal.Parse(value.ToString());
                    if (num < 0)
                        num *= -1;
                    string s = HelpClass.DecTostring(num);
                    return decimal.Parse(s);
                }
                else return 0;

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
