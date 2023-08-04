using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class InvoiceModel
    {

        public int CustomerId { get; set; }
        public string BillId { get;set; }
        public string next_billid { get;set; }
        public string table_id { get;set; }
        public string is_do { get;set; }
        public decimal total_after_discount { get;set; }
        public string UNo { get;set; }
        public string over_discount { get;set; }
        public decimal vat { get;set; }
        public string vat_included { get;set; }
        public decimal vat_amount { get;set; }
        public string bill_status { get;set; }
        public string cname { get;set; }
        public string paid { get;set; }
        public string note { get;set; }
        public string note2 { get;set; }
        public string over_discount_percentage { get;set; }
        public string external { get;set; }
        public decimal service { get;set; }
        public string emp { get;set; }
        public string for_use { get;set; }
        public string takeaway { get;set; }
        public string on_table { get;set; }
        public int room_reservation_id { get;set; }
        public decimal round_dis { get;set; }
        public decimal auto_discount { get;set; }
        public decimal request { get;set; }


    }
}
