using Hesabate_POS.Classes.ApiClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes
{
    public class ItemService
    {
        private List<CategoryModel> categoryPath;
        private HttpClient client = AppSettings.httpClient;

        public List<CategoryModel> getCatItems(int catId)
        {
            List<CategoryModel> items = null;
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
  

                return new List<CategoryModel>();
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

        private List<CategoryModel> SearchCatLevel2(List<CategoryModel> level2, int itemId)
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

        

        // cat, item
        public CategoryModel getItem(int itemId,string type)
        {
            CategoryModel itemModel = null;
            if (GeneralInfoService.GeneralInfo.buttons_cat != null)
            {
                if (type == "cat")
                {
                    foreach (var catRow in GeneralInfoService.GeneralInfo.buttons_cat)
                    {
                        if (catRow.id == itemId)
                            return catRow;

                        if (catRow.level2 != null)
                        {
                            itemModel = SearchInLevel2(catRow.level2, itemId);
                            if (itemModel != null)
                                return itemModel;
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

        private CategoryModel SearchInLevel2(List<CategoryModel> level2,int itemId)
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
        
        private CategoryModel SearchInItems(List<CategoryModel> cat,int itemId)
        {
            CategoryModel item = null;
           
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
                        else if(level.level2 != null)
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

        #region path category
        public List<CategoryModel> getCategoryPath(int itemId)
        {
            categoryPath = new List<CategoryModel>();
            if (GeneralInfoService.GeneralInfo.buttons_cat != null)
            {
                foreach (var catRow in GeneralInfoService.GeneralInfo.buttons_cat)
                {
                    if (catRow.id == itemId)
                    {
                        categoryPath.Insert(0, catRow);
                        return categoryPath;
                    }
                    if (catRow.level2 != null)
                    {
                        var itemsModel = getItemWithUpLevel2(catRow.level2, itemId);
                        if (itemsModel != null)
                        {
                            categoryPath.Insert(0, catRow);
                            return categoryPath;
                        }
                    }

                }               
            }
            return categoryPath;
        }

        private CategoryModel getItemWithUpLevel2(List<CategoryModel> level2, int itemId)
        {
            foreach (var level in level2)
            {
                //var item = level.
                if (level.id == itemId)
                {
                    categoryPath.Insert(0, level);
                    return level;
                }
                else if (level.level2 != null)
                {
                    var item = getItemWithUpLevel2(level.level2, itemId);
                    if (item != null)
                    {
                        categoryPath.Insert(0, level);
                        return item;
                    }
                }
            }

            return null;
        }
       
        #endregion
        static public bool itemIsLast(CategoryModel item)
        {
            // is  Last
            if (item.level2 == null && (item.items == null || item.items.Count == 0) )
                return true;
            if (item.id2 != null)
                return true;
            else
                return false;
        }
        public string GetLocalUri(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string dir = Directory.GetCurrentDirectory();
            string tmpPath = Path.Combine(dir, AppSettings.ItemsImgPath);
            tmpPath = Path.Combine(tmpPath, fileName);

            if (File.Exists(tmpPath))
                return tmpPath;
            else
                return "";
        }
        public void SaveImage(byte[] arrBytes, string fileName)
        {
            fileName = Path.GetFileName(fileName);
            string dir = Directory.GetCurrentDirectory();
            string tmpPath = Path.Combine(dir, AppSettings.ItemsImgPath);
            if (!Directory.Exists(tmpPath))
                Directory.CreateDirectory(tmpPath);

            tmpPath = Path.Combine(tmpPath, fileName);
            if (System.IO.File.Exists(tmpPath))
            {
                System.IO.File.Delete(tmpPath);
            }
            if (arrBytes != null)
            {
                using (FileStream fs = new FileStream(tmpPath, FileMode.Create, FileAccess.ReadWrite))
                {
                    fs.Write(arrBytes, 0, arrBytes.Length);
                }
            }
        }
        public byte[] readLocalImage(string imagePath)
        {
            byte[] data = null;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(imagePath);
            // The byte[] to save the data in
            data = new byte[fileInfo.Length];
            using (var stream = new System.IO.FileStream(imagePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                stream.Read(data, 0, data.Length);
            }
            return data;
        }
        public void ClearSavedImages()
        {
            string dir = Directory.GetCurrentDirectory();
            string tmpPath = Path.Combine(dir, AppSettings.ItemsImgPath);

            if (!Directory.Exists(tmpPath))
                Directory.CreateDirectory(tmpPath);

            var files = Directory.GetFiles(tmpPath);
            foreach (var f in files)
                File.Delete(f);
        }
        //public  async Task<byte[]> DownloadImageAsync(string apiUri,string imagePath)
        //{

        //    string remoteUri = apiUri+"/"+imagePath;
        //    using (var httpClient = new HttpClient())
        //    {
        //            var url = new Uri(remoteUri);
        //            // Get the file extension
        //            var uriWithoutQuery = url.GetLeftPart(UriPartial.Path);
        //        var fileName = "";

        //        var s = url.AbsolutePath.Split('/');
        //        fileName = s[s.Length - 1];
           
    
        //            var fileExtension = Path.GetExtension(uriWithoutQuery);

        //            // Create file path and ensure directory exists
        //            //var fileName = "dd";
        //            string directoryPath = Directory.GetCurrentDirectory();
        //            string path = Path.Combine(directoryPath,AppSettings.ItemsImgPath);
        //            if (!Directory.Exists(path))
        //                Directory.CreateDirectory(path);
        //            path = Path.Combine(path, fileName);
        //            // var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
        //            //Directory.CreateDirectory(directoryPath);

        //            // Download the image and write to the file
        //           // var imageBytes = await httpClient.GetByteArrayAsync(remoteUri);
        //            var imageBytes1 = await client.GetByteArrayAsync(remoteUri);

        //        return imageBytes1;
        //       //return new  MemoryStream(imageBytes);
        //       //if (imageBytes != null)
        //       //{
        //       //    using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
        //       //    {
        //       //        fs.Write(imageBytes, 0, imageBytes.Length);
        //       //    }
        //       //}
        //       //File.WriteAllBytes(path, imageBytes);
        //       //    return path;

        //    }
        //}

        public async Task< List<ItemModel> >GetItems()
        {

            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + "/POS/p5api2.php");
           // try
           // {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(AppSettings.token), "token");
                content.Add(new StringContent("13"), "op");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    GeneralInfoService.items = JsonConvert.DeserializeObject<List<ItemModel>>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                }
           // }
            //catch (Exception ex)
            //{
            //    return new List<ItemModel>();
            //}
            return GeneralInfoService.items;
        } 
        
        public async Task< ItemModel>GetItemInfo(string searchText,string zType,int customerId,string priceId,string sTr="",string unitid="0")
        {
            ItemModel item = null;


                var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + "/POS/p5api2.php");
                try
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(AppSettings.token), "token");
                    content.Add(new StringContent("4"), "op");
                    content.Add(new StringContent(searchText), "id");// search in id
                    content.Add(new StringContent(zType), "ztype");// 1 search according to text search only, 0 to apply search on all parameters
                    content.Add(new StringContent(customerId.ToString()), "cid"); // customer id in invoice
                    content.Add(new StringContent(priceId), "priceid"); // from main info
                    content.Add(new StringContent(sTr), "sTr");//barcode 
                    content.Add(new StringContent(unitid), "unitid");//search in other item details
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                       item = JsonConvert.DeserializeObject<ItemModel>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                return item;
        }
    }
}
