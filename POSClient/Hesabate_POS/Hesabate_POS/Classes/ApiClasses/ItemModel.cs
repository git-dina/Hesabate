﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class ItemModel
    {
        public int id { get; set; }
        public int id2 { get; set; }
        public int but_mast_id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public List<ItemModel> level2 { get; set; }
        public List<ItemModel> items { get; set; }
        public decimal discount { get; set; }
        public decimal price { get; set; }
        public string no_w { get; set; }
        public string unit { get; set; }
        public string x_vat { get; set; }
        public string alias { get; set; }
        public string is_special { get; set; }
        public string discount_per { get; set; }
        public int read_it { get; set; }
        public string plimit { get; set; }
        public int addons { get; set; }
        public string img { get; set; }
        public string tax_class { get; set; }
        public string measure_id { get; set; }
        public string unit_parts { get; set; }

    }
}
