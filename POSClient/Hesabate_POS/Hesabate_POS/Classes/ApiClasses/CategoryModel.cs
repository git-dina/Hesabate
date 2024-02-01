using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class CategoryModel
    {
        public int id { get; set; }
        public int? id2 { get; set; } // for extras

        public int but_mast_id { get; set; }//parent category id
        public string name { get; set; }
        public string color { get; set; }
        public List<CategoryModel> level2 { get; set; } // == null there is no sub category
        public List<CategoryModel> items { get; set; } // == null (maybe has sub category if level2 != null or this is last item)
        public List<CategoryModel> addItems { get; set; } // == 
        public List<CategoryModel> deleteItems { get; set; } // == 
        public List<CategoryModel> isbasic { get; set; } // == 
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

        public decimal min_p { get; set; } // min price 
        public decimal max_p { get; set; } // max price
        //for extra
        public string group_name { get; set; }
        public int group_count { get; set; }
        public List<GroupItemModel> group_items { get; set; }
    }
}
