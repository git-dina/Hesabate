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
        public string is_do { get;set; }//0 : save invoice , 1: Update invoice , 2 : Hold Invoice , 3 : Update Hold Invoice
        public decimal total_after_discount { get;set; }//invoice total
        public string UNo { get;set; }//customer id
        public string over_discount { get;set; }//discount amount
        public decimal vat { get;set; }//vat percent
        public string vat_included { get;set; }//is vat included ??from where
        public decimal vat_amount { get;set; }//calculateed vat amount
        public string bill_status { get;set; }//invoice status
        public string cname { get;set; }//customer name
        public string paid { get;set; }//payment type(0 not paid, 1 paid)
        public string note { get;set; }
        public string note2 { get;set; }
        public string over_discount_percentage { get;set; }//discount percentage
        public string external { get;set; }//is invoice external
        public decimal service { get;set; }//service amount ??value or percentage
        public string emp { get;set; } //employer id
        public string for_use { get;set; }//is this invoice for internal use not sale
        public string takeaway { get;set; }//is this takeaway invoice
        public string on_table { get;set; }//on table seletced ( like external and takeaway)
        public int room_reservation_id { get;set; }//room reservation id if used
        public decimal round_dis { get;set; }//discount calculated through round mathmatics
        public decimal auto_discount { get;set; }//discount from campages and other options
        public decimal request { get;set; }//id of holded invoice changed from


    }
}
