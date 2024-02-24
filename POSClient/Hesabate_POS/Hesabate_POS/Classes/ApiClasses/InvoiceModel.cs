using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class InvoiceModel : INotifyPropertyChanged
    {

        public int customer_id { get; set; }
        //public string BillId { get; set; } = "000000";
        private string _BillId = GeneralInfoService.GeneralInfo.BILL_ID.ToString();
        public string id
        {
            get => _BillId;
            set
            {
                if (_BillId == value) return;

                _BillId = value;
                OnPropertyChanged();
            }
        }



        public string next_billid { get; set; }
        public string return_billid { get; set; }
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
        public string external { get; set; } = "0"; //is invoice external {0: normal, 1: delivery, 2: Pending delivery}
        public decimal service { get; set; }//service amount ??value or percentage
        public string emp { get; set; } = "0"; //employer id
        public string empn { get; set; }  //employer name
        public string for_use { get; set; } = "0";//is this invoice for internal use not sale
        public string takeaway { get; set; } = "0";//is this takeaway invoice {0: normal, 1: takeaway}
        public string on_table { get; set; } = "0";//on table seletced ( like external and takeaway)
        public int room_reservation_id { get; set; } = 0;//room reservation id if used
        public decimal round_dis { get; set; }//discount calculated through round mathmatics
        public decimal auto_discount { get; set; }//discount from campages and other options
        public decimal request { get; set; }//id of holded invoice changed from
        public string pending { get; set; } = "0"; //0: normal, 1: pending

        //extra
        public string discountType { get; set; } = "rate";
        public decimal manualDiscount { get; set; }
        public decimal total { get; set; }
        private string _invType = "0";  //0:sales ,1:return, 2: manual return, 3:replace
        public string invType
        {
            get => _invType;
            set
            {
                if (_invType == value) return;

                _invType = value;
                OnPropertyChanged();
            }
        }

        public List<InvoiceItem> items { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

    }

    public class InvoiceItem
    {
        public int id { get; set; } // item id
        public string detail { get; set; } // item id
        public string unit { get; set; } // unit id
        public string name { get; set; } // item name
        public string discount_per { get; set; }//نسبة الخصم المثبت في بطاقة الصنف
        public string measure_id { get; set; }
        public decimal discount { get; set; } // الخصم الصافي على الصنف
        public string serial_text { get; set; } // السيريال الذي تم بيعه في هذه العملية
        public string x_vat { get; set; } //ضريبة العنصر
        public string is_special { get; set; }
        public decimal x_discount { get; set; }
        public int bonus { get; set; }
        public string is_ext { get; set; }
        public bool isUrgent { get; set; }
        public int index { get; set; }
        public int amount { get; set; }
        public decimal price { get; set; }
        public string notes { get; set; }
        public string no_w { get; set; }
        public string back_val { get; set; }
        public decimal min_p { get; set; }
        public decimal max_p { get; set; }

        public List<ExtraItemModel> extraitems = new List<ExtraItemModel>();
        public List<ExtraItemModel> addsitems = new List<ExtraItemModel>();
        public List<ExtraItemModel> deletesitems = new List<ExtraItemModel>();
        public List<ExtraItemModel> isbasics = new List<ExtraItemModel>();
    }

    public class ExtraItemModel
    {
        public string group_name { get; set; }
        public int group_count { get; set; }

        public List<GroupItemModel> group_items { get; set; } = new List<GroupItemModel>();
    }
}
