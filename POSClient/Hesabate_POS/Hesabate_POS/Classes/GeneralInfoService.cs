using Hesabate_POS.Classes.ApiClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes
{
    public static class GeneralInfoService
    {

        private static HttpClient client = AppSettings.httpClient;

        public static GeneralInfoModel GeneralInfo;
        public static List<LanguageModel> Languages;
        public static List<LanguageTermModel> LanguageTerms ;
        public static List<ItemModel> items ;
        public static List<CashBoxModel> cashBoxes ;

        public static async Task<List<LanguageModel>> GetLanguages()
        {
            
            //using (var client = new HttpClient())
            {
               // client.Timeout = System.TimeSpan.FromSeconds(3600);
                //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + "/POS/p5api2.php");

                try
                {
                    var response = await client.SendAsync(request);
                 
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        Languages = JsonConvert.DeserializeObject<List<LanguageModel>>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                    }
                }
                catch  {
                    Languages = new List<LanguageModel>();
                }
            }

            return Languages;
        }
        
        public static async Task GetMainInfo()
        {
            var request = new HttpRequestMessage(HttpMethod.Post,AppSettings.APIUri+ "/POS/p5api2.php");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(AppSettings.token), "token");
            content.Add(new StringContent("1"), "op");
            request.Content = content;
            try
            {
                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    GeneralInfo = JsonConvert.DeserializeObject<GeneralInfoModel>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });


                }
            }
            catch(Exception ex)
            {
                GeneralInfo = new GeneralInfoModel();
            }
             
        }

        public static async Task GetLanguagesTerms(int languageId)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + "/POS/p5api2.php");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(languageId.ToString()), "lang");
            request.Content = content;
            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    LanguageTerms = JsonConvert.DeserializeObject<List<LanguageTermModel>>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                }
            }
            catch (Exception ex)
            {
                LanguageTerms = new List<LanguageTermModel>();
            }
        }

    }
}
