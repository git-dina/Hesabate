using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes.ApiClasses
{
    public class Floor
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public List<TableModel> SubTable { get; set; }
    }

    public class TableModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
