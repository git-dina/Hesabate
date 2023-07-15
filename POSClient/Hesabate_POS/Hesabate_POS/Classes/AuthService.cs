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
        public  async Task<dynamic> Login(string userName,string password)
        {
            dynamic res = "";
            using (var client = new HttpClient())
            {
                client.Timeout = System.TimeSpan.FromSeconds(3600);

                var request = new HttpRequestMessage(HttpMethod.Post,AppSettings.APIUri+ "/POS/p5api2.php");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(userName), "U");
                content.Add(new StringContent(password), "P");
                content.Add(new StringContent(""), "D");
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
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Languages = new List<LanguageModel>();
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


                    }
                }
                catch (Exception ex)
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
            AppSettings.token = res["token"];
         
        }
    }
}
