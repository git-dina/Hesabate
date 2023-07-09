using Hesabate_POS.Classes.ApiClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes
{
    public class ItemService
    {

        public List<ItemModel> getCatItems(int catId)
        {
            if (catId == 0)
                return GeneralInfoService.GeneralInfo.buttons_cat;
            else
            {
                var cat = GeneralInfoService.GeneralInfo.buttons_cat.Where(x => x.id == catId).FirstOrDefault();
                if (cat.level2 != null)
                    return cat.level2;
                else if (cat.items != null)
                    return cat.items;
                else
                    return new List<ItemModel>();
            }
        }
    }
}
