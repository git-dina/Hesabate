using Hesabate_POS.Classes.ApiClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes
{
    public class AuthService
    {
        private  HttpClient client = AppSettings.httpClient;

        public  async Task<dynamic> Login(string userName,string password,string lang)
        {
            dynamic res = "";
            //using (var client = new HttpClient())
            {
                //client.Timeout = System.TimeSpan.FromSeconds(3600);

                var request = new HttpRequestMessage(HttpMethod.Post,AppSettings.APIUri+ "/POS/p5api2.php");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(userName), "U");
                content.Add(new StringContent(password), "P");
                content.Add(new StringContent(""), "D");
                content.Add(new StringContent(lang), "lang");
                request.Content = content;
                try
                {
                    var response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<dynamic>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                        if (res["result"] != null)
                            return res["code"];
                        else
                        {
                            setAppSettings(jsonString);
                            return "";
                        }
                    }
                }
                catch 
                {
                    res = "error";
                }
            }
            return res;
        } 
        public  async Task<dynamic> Login(string idCard)
        {
            dynamic res = "";
            using (var client = new HttpClient())
            {
                client.Timeout = System.TimeSpan.FromSeconds(3600);

                var request = new HttpRequestMessage(HttpMethod.Post,AppSettings.APIUri+ "/POS/p5api2.php");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(idCard), "K");
                request.Content = content;
                try
                {
                    var response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        res = JsonConvert.DeserializeObject<dynamic>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                        if (res["result"] != null)
                            return res["code"];
                        else
                        {
                            setAppSettings(jsonString);
                            return "";
                        }

                    }
                }
                catch 
                {
                    //Languages = new List<LanguageModel>();
                }
                return res;
            }
        }

        private void setAppSettings(string jsonString)
        {
           var  res = JsonConvert.DeserializeObject<dynamic>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            
            AppSettings.handhold_out = res["handhold_out"];
            AppSettings.handhold_in = res["handhold_in"];
            AppSettings.handhold = res["handhold"];
            AppSettings.items_page = res["items_page"];
            AppSettings.reservation = res["reservation"];
            AppSettings.customer_report = res["customer_report"];
            AppSettings.convert = res["convert"];
            AppSettings.database_id = res["database_id"];
            AppSettings.userId = res["userid"];
            AppSettings.userName = res["name"];
            AppSettings.cashBoxId = res["cash_box"];
            AppSettings.showPx = res["showPx"];
            AppSettings.MainCurrency = res["MainCurrency"];
            AppSettings.token = res["token"];

            var currencyLst = JObject.Parse(jsonString)["currency"].ToList();
            GeneralInfoService.currencyList = currencyLst.Select(d => new CurrencyModel
            {
                id = (int) d["id"],
                name = (string) d["name"],
               parts = (int) d["parts"],
               price = (decimal) d["price"],
               symbol = (string) d["symbol"],
            }).ToList();
            //accuracy
            AppSettings.accuracy = GeneralInfoService.currencyList.Where(x => x.id == AppSettings.MainCurrency).Select(x => x.parts).FirstOrDefault().ToString();
            
            
            var boxesLst = JObject.Parse(jsonString)["cash_boxes"].ToList();

            GeneralInfoService.cashBoxes = boxesLst.Select(d => new CashBoxModel
            {
                Name = (string)d["name"],
                BoxId = (string)d["id"] 
            }).ToList();

            
        }
    }
}
