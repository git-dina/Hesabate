using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class MainOpModel
    {
        public string note1 { get; set; }
        public string note2 { get; set; }
        public decimal service { get; set; }
        public string options { get; set; }
        public string rooms { get; set; }
        public string menus { get; set; }
        public string pro_count { get; set; }
        public string con_time { get; set; }
        public string reserv_min { get; set; }
        public string button_width { get; set; }
        public string Ssearch { get; set; }
        public string ThisYearId { get; set; }
        public int is_closed { get; set; }
        public string S_sms { get; set; }
        public decimal vat { get; set; }
        public string HornyTag { get; set; }
        public int MustSerial { get; set; }
        public string MySno { get; set; }
        public string USERSID { get; set; }
        public string Max6 { get; set; }
        public string Max9 { get; set; }
         public List<ItemModel> special_items { get; set; } // التأكد لاحقاً
        public string PASSWORD { get; set; }
        public string BoxId { get; set; }
        public string employe { get; set; }
        public string storeID { get; set; }
        public string price_id { get; set; }
        public int card_type { get; set; }
        public string default_name { get; set; }
        public string DataBase_Name { get; set; }
        public int AMain { get; set; }//accuracy

    }
}
