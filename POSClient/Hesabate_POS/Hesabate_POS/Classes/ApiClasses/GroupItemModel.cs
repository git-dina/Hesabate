using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class GroupItemModel
    {
        public int id { get; set; }
        public decimal price { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public string no_w { get; set; }
        public string tax_class { get; set; }
        public string measure_id { get; set; }
        public string discount_per { get; set; }
        public int start_amount { get; set; }
        public decimal add_price { get; set; }
        public int add_price_amount { get; set; }

        public decimal sub_price { get; set; }
        public string allow_add { get; set; }
        public string allow_sub { get; set; }
    }
}
