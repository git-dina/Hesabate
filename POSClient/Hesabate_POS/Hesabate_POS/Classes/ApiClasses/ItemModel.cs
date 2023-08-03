using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class ItemModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string serial { get; set; } //هل المعلومات هي سيريال أو لا
        public string unit { get; set; } // unit id
        public string no_w { get; set; }// هل نظام السيريال فعال أو لا
        public string dangure { get; set; }//هل الصنف مصنف على انه صنف خطير حتى يظهر رسالة على النظام
        public string tax_class { get; set; } //نسبة الضريبة على الصنف
        public string discount_per { get; set; }//نسبة الخصم المثبت في بطاقة الصنف
        public decimal price { get; set; } // item price 
        public decimal min_p { get; set; } // min price 
        public decimal max_p { get; set; } // max price
        public decimal discount { get; set; } // الخصم الصافي على الصنف
        public string usedSerialTxt { get; set; } // السيريال الذي تم بيعه في هذه العملية


        private int _index;
        public int count { get; set; }


    }
}
