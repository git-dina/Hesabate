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
       public static GeneralInfoModel GeneralInfo = new GeneralInfoModel();
      public static  List<LanguageModel> Languages = new List<LanguageModel>();

        public static async Task GetLanguages()
        {

            using (var client = new HttpClient())
            {
                client.Timeout = System.TimeSpan.FromSeconds(3600);
                //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                var request = new HttpRequestMessage(HttpMethod.Post, "http://s.hesabate.com/POS/p5api2.php");

                try
                {
                    var response = await client.SendAsync(request);
                 
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        Languages = JsonConvert.DeserializeObject<List<LanguageModel>>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                    }
                }
                catch (Exception ex) {
                    Languages = new List<LanguageModel>();
                }
            }
        }
        
        public static async Task GetMainInfo()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = System.TimeSpan.FromSeconds(3600);

                var request = new HttpRequestMessage(HttpMethod.Post, "http://s.hesabate.com/POS/p5api2.php");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("KCvWV8bgsJKjTBsNDjJWCtZurF7bBJPOraSDtOJ27PYkPTToqkzDsVRVXilEwUkPKTkWGSxYIodapKKQWS5Hnx5puLgNhfH33hOOOSEZAlzsfyS2alxoMsbbP19LscsnbNMLFBgDNt2+xMrUQXDlhJYHB/+vSCHyDub89k/I6s+wCoG4YhV3vzoMmyW9mhsMo2IGSJzb4kyFWm8KvzWhrpdomkMNWL3ybqICjPq1RBYTpufI2ACasIMaAMwRbvxf"), "token");
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
        }
     
    }
}
