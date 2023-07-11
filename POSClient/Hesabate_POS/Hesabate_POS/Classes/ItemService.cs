using Hesabate_POS.Classes.ApiClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Security.Permissions;
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

        public static async Task<byte[]> DownloadImageAsync(string apiUri,string imagePath)
        {

            //// Get the file extension
            //var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
            //var fileExtension = Path.GetExtension(uriWithoutQuery);

            //// Create file path and ensure directory exists
            //var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
            //Directory.CreateDirectory(directoryPath);

            //// Download the image and write to the file
            //var imageBytes = await _httpClient.GetByteArrayAsync(uri);
            //await File.WriteAllBytesAsync(path, imageBytes);

            //second
            //  string remoteUri = "http://s.hesabate.com/POS/p5api2.php/storage/db_1828/db_1828_b_18.gif";
            //string fileName = "db_1828_b_18.gif", myStringWebResource = null;
            //// Create a new WebClient instance.
            //WebClient myWebClient = new WebClient();
            //// Concatenate the domain with the Web resource filename.
            //myStringWebResource = remoteUri + fileName;
            //Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            //// Download the Web resource and save it into the current filesystem folder.
            //myWebClient.DownloadFile(myStringWebResource, fileName);

            //third
            //using (WebClient client = new WebClient())
            //{

            //    client.DownloadFileAsync(new Uri(remoteUri), "image35.png");
            //}

            string remoteUri = apiUri+"/"+imagePath;
            using (var httpClient = new HttpClient())
            {
                    var url = new Uri(remoteUri);
                    // Get the file extension
                    var uriWithoutQuery = url.GetLeftPart(UriPartial.Path);
                var fileName = "";

                var s = url.AbsolutePath.Split('/');
                fileName = s[s.Length - 1];
           
    
                    var fileExtension = Path.GetExtension(uriWithoutQuery);

                    // Create file path and ensure directory exists
                    //var fileName = "dd";
                    string directoryPath = Directory.GetCurrentDirectory();
                    string path = Path.Combine(directoryPath,AppSettings.ItemsImgPath);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    path = Path.Combine(path, fileName);
                    // var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
                    //Directory.CreateDirectory(directoryPath);

                    // Download the image and write to the file
                    var imageBytes = await httpClient.GetByteArrayAsync(remoteUri);

                return imageBytes;
               //return new  MemoryStream(imageBytes);
               //if (imageBytes != null)
               //{
               //    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
               //    {
               //        fs.Write(imageBytes, 0, imageBytes.Length);
               //    }
               //}
               //File.WriteAllBytes(path, imageBytes);
               //    return path;

            }
        }
    }
}
