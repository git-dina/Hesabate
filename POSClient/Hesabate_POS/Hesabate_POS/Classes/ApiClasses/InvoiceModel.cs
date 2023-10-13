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
        public string BillId { get; set; }
        public string next_billid { get; set; }
        public string table_id { get; set; } = "0";
        public string is_do { get; set; } = "0";//0 : save invoice , 1: Update invoice , 2 : Hold Invoice , 3 : Update Hold Invoice
        public decimal total_after_discount { get; set; }//invoice total
        public int UNo { get; set; } = 0;//customer id
        public decimal over_discount { get; set; }//discount amount
        public decimal over_discount_percentage { get; set; }//discount percentage
        public decimal vat { get; set; }//vat percent
        public string vat_included { get; set; } = "0"; //is vat included ??from where
        public decimal vat_amount { get; set; }//calculateed vat amount
        public string bill_status { get; set; } = "1";//invoice status
        public string cname { get; set; } = "";//customer name
        public string paid { get; set; } = "0";//payment type(0 not paid, 1 paid)
        public string note { get; set; }
        public string note2 { get; set; }
        public string external { get; set; } = "0"; //is invoice external
        public decimal service { get; set; }//service amount ??value or percentage
        public string emp { get; set; } = "0"; //employer id
        public string for_use { get; set; } = "0";//is this invoice for internal use not sale
        public string takeaway { get; set; } = "0";//is this takeaway invoice
        public string on_table { get; set; } = "0";//on table seletced ( like external and takeaway)
        public int room_reservation_id { get; set; } = 0;//room reservation id if used
        public decimal round_dis { get; set; }//discount calculated through round mathmatics
        public decimal auto_discount { get; set; }//discount from campages and other options
        public decimal request { get; set; }//id of holded invoice changed from


        //extra
        public string discountType { get; set; } = "rate";
        public decimal manualDiscount { get; set; } 
    }

    public class InvoiceItem
    {
        public string product_id { get; set; } // item id
        public string unit { get; set; } // unit id
        public string discount_per { get; set; }//نسبة الخصم المثبت في بطاقة الصنف
        public string measure_id { get; set; }
        public decimal discount { get; set; } // الخصم الصافي على الصنف
        public string serial_text { get; set; } // السيريال الذي تم بيعه في هذه العملية
        public string x_vat { get; set; } //ضريبة العنصر
        public string is_special { get; set; }
        public string x_discount { get; set; }
        public int bonus { get; set; }
        public string is_ext { get; set; }
        public bool isUrgent { get; set; }
        public int index { get; set; }
        public int amount { get; set; }
        public decimal price { get; set; }
        public string notes { get; set; }

        public List<ExtraItemModel> extraItems = new List<ExtraItemModel>();
        public List<ExtraItemModel> addsItems = new List<ExtraItemModel>();
        public List<ExtraItemModel> deletesItems = new List<ExtraItemModel>();
    }

    public class ExtraItemModel
    {
        public string group_name { get; set; }
        public int group_count { get; set; }

        public List<GroupItemModel> group_items { get; set; } = new List<GroupItemModel>();
    }
}
