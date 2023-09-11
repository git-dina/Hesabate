using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class GroupItemModel : INotifyPropertyChanged
    {
        public int id { get; set; }
       // public decimal price { get; set; }
        public string name { get; set; }
        public string unit { get; set; }
        public string no_w { get; set; }
        public string tax_class { get; set; }
        public string measure_id { get; set; }
        public string discount_per { get; set; }
        private int _start_amount;
        public int start_amount
    {
            get => _start_amount;
            set
            {
                if (_start_amount == value) return;

            _start_amount = value;
                OnPropertyChanged();
            }
        }


        private decimal _add_price;
        public decimal add_price
        {
            get => _add_price;
            set
            {
                if (_add_price == value) return;

                _add_price = value;
                OnPropertyChanged();
            }
        }


        public int add_price_amount { get; set; }

        public decimal sub_price { get; set; }
        public string allow_add { get; set; }
        public string allow_sub { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }


        public int groupId { get; set; }
    }
}
