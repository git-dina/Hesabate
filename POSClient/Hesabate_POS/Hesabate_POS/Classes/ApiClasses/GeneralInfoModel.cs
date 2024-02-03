using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class GeneralInfoModel
    {
        public MainOpModel MainOp { get; set; }
        public List<Floor> tables { get; set; }
        public int[] red_tab { get; set; }
        public int[] yellow_tab { get; set; }
        public int[] reserved { get; set; }
        public int BILL_ID { get; set; }
        public List<CategoryModel> buttons_cat { get; set; } // for items
        //public Dictionary<string,string> units { get; set; }

        public string[] campage_item_only { get; set; }
        public string[] campage_all_only { get; set; }
    }
}
