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
            List<ItemModel> items = null;
            if (catId == 0)
                return GeneralInfoService.GeneralInfo.buttons_cat;
            else
            {

                foreach (var catRow in GeneralInfoService.GeneralInfo.buttons_cat)
                {
                    if (catRow.id == catId)
                    {
                        if (catRow.level2 != null)
                            return catRow.level2;
                        else if (catRow.items != null)
                            return catRow.items;
                    }
                    if (catRow.level2 != null)
                    {
                        items = SearchCatLevel2(catRow.level2, catId);
                        if (items != null)
                            return items;
                    }
                }
  

                return new List<ItemModel>();
                //var cat = GeneralInfoService.GeneralInfo.buttons_cat.Where(x => x.id == catId).FirstOrDefault();
                //if (cat != null)
                //{
                //    if (cat.level2 != null)
                //        return cat.level2;
                //    else if (cat.items != null)
                //        return cat.items;
                //}
                //else
                //    return new List<ItemModel>();
            }
        }

        private List<ItemModel> SearchCatLevel2(List<ItemModel> level2, int itemId)
        {
            foreach (var level in level2)
            {
                if (level.id == itemId)
                {
                    if (level.level2 != null)
                        return level.level2;
                    else if (level.items != null)
                        return level.items;
                }

                if (level.level2 != null)
                    return SearchCatLevel2(level.level2, itemId);
            }

            return null;
        }

        //private List<ItemModel> searchCatItems(List<ItemModel>  cat,int catId)
        //{
        //    List<ItemModel> item = null;

        //    foreach (var catRow in cat)
        //    {
        //        if (catRow.items != null)
        //        {
        //            item = catRow.items.Where(x => x.id == catId).FirstOrDefault();
        //            if (item != null)
        //                return item;
        //        }
        //        else
        //        {
        //            foreach (var level in catRow.level2)
        //            {
        //                if (level.items != null)
        //                {
        //                    item = level.items.Where(x => x.id == catId).FirstOrDefault();
        //                    if (item != null)
        //                        return item;
        //                }
        //                else
        //                {
        //                    item = searchCatItems(level.level2, itemId);
        //                    if (item != null)
        //                        return item;
        //                }
        //            }
        //        }
        //    }

        //    return null;


        //}

        // cat, item
        public ItemModel getItem(int itemId,string type)
        {
            ItemModel itemModel = null;
            if (GeneralInfoService.GeneralInfo.buttons_cat != null)
            {
                if (type == "cat")
                {
                    foreach (var catRow in GeneralInfoService.GeneralInfo.buttons_cat)
                    {
                        if (catRow.id == itemId)
                            return catRow;

                        itemModel = SearchInLevel2(catRow.level2, itemId);

                        foreach (var item in catRow.items)
                        {
                            if (item.id == itemId)
                                return item;
                        }
                    }
                }
                else
                {
                    return SearchInItems(GeneralInfoService.GeneralInfo.buttons_cat, itemId);

                }
            }
            return null;
        }

        private ItemModel SearchInLevel2(List<ItemModel> level2,int itemId)
        {
            foreach (var level in level2)
            {
                if (level.id == itemId)
                    return level;
                else if(level.level2 != null)
                  return  SearchInLevel2(level.level2, itemId);
            }

            return null;
        } 
        
        private ItemModel SearchInItems(List<ItemModel> cat,int itemId)
        {
            ItemModel item = null;
           
            foreach (var catRow in cat)
            {
                if (catRow.items != null)
                {
                    item =  catRow.items.Where(x => x.id == itemId).FirstOrDefault();
                    if (item != null)
                        return item;
                }
               else
                {
                    foreach (var level in catRow.level2)
                    {
                        if (level.items != null)
                        {
                            item = level.items.Where(x => x.id == itemId).FirstOrDefault();
                            if (item != null)
                                return item;
                        }
                        else
                        {
                            item = SearchInItems(level.level2, itemId);
                            if (item != null)
                                return item;
                        }
                    }
                }
            }

            return null;
        }

        static public bool itemIsLast(ItemModel item)
        {
            // is  Last
            if (item.level2 == null && (item.items == null || item.items.Count == 0))
                return true;
            else
                return false;
        }
    }
}
