using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class ItemModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string unita { get; set; }
        public string no_w { get; set; }
        public string dangure { get; set; }
        public string tax_class { get; set; }
        public string discount_per { get; set; }
        public string min_p { get; set; }
        public string max_p { get; set; }
        public string unit_parts { get; set; }
        //public string ?column? { get; set; }
        
    }
}
