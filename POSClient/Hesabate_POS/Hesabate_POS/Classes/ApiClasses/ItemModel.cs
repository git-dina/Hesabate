using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class ItemModel : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string name { get; set; }
        public string serial { get; set; } //هل المعلومات هي سيريال أو لا
        public string unit { get; set; } // unit id
        public string no_w { get; set; }// هل نظام السيريال فعال أو لا
        public string dangure { get; set; }//هل الصنف مصنف على انه صنف خطير حتى يظهر رسالة على النظام
        public decimal tax_class { get; set; } //نسبة الضريبة على الصنف
        public string discount_per { get; set; }//نسبة الخصم المثبت في بطاقة الصنف
        //public decimal price { get; set; } // item price 
        public string measure_id { get; set; }
        public decimal min_p { get; set; } // min price 
        public decimal max_p { get; set; } // max price
        public decimal discount { get; set; } // الخصم الصافي على الصنف
        public string serial_text { get; set; } // السيريال الذي تم بيعه في هذه العملية
        public string detail { get; set; }//note


        public string x_vat { get; set; } //ضريبة العنصر
        public string is_special { get; set; }//غير موجود في اجراء getitems
        public string x_discount { get; set; }//غير موجود في اجراء getitems
        public string bonus { get; set; }//غير موجود في اجراء getitems
        public string is_ext { get; set; }//غير موجود في اجراء getitems


        //extra
        public bool isUrgent { get; set; }

        public string unitName { get; set; }
        /// <summary>
        ///  invoice details
        /// </summary>
        //public int index { get; set; }
        private int _index;
        public int index
        {
            get => _index;
            set
            {
                if (_index == value) return;

                _index = value;
                OnPropertyChanged();
            }
        }

        public string nameUnit
        {
            get
            {
                return (string.IsNullOrWhiteSpace(unit) ? $"{name}" : $"{name} - {unit}");
            }
            set
            {
                nameUnit = value;
            }
        }
        private int _amount;
        public int amount
        {
            get => _amount;
            set
            {
                if (_amount == value) return;

                _amount = value;
                OnPropertyChanged();
            }
        }

        private decimal _price;
        public decimal price
        {
            get => _price;
            set
            {
                if (_price == value) return;

                _price = value;
                OnPropertyChanged();
            }
        }
        private decimal _total;
        public decimal total
        {
            get => _total;
            set
            {
                if (_total == value) return;

                _total = value;
                OnPropertyChanged();
            }
        }
        public List<CategoryModel> extraItems = new List<CategoryModel>();
        //public List<ItemModel> deleteItems = new List<ItemModel>();
        private string _notes;
        public string notes
        {
            get => _notes;
            set
            {
                if (_notes == value) return;

                _notes = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }


    }
}
