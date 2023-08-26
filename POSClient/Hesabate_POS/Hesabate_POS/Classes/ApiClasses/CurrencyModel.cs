using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class CurrencyModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public decimal price { get; set; }
        public int parts { get; set; }
        public decimal first_price { get; set; }
    }
}
