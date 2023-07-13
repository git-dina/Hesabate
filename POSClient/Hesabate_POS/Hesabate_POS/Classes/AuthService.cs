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
        public  async Task Login(string userName,string password)
        {

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
                       // Languages = JsonConvert.DeserializeObject<List<LanguageModel>>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                    }
                }
                catch (Exception ex)
                {
                    //Languages = new List<LanguageModel>();
                }
            }
        } 
        public  async Task Login(string idCard)
        {

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
                       // Languages = JsonConvert.DeserializeObject<List<LanguageModel>>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                    }
                }
                catch (Exception ex)
                {
                    //Languages = new List<LanguageModel>();
                }
            }
        }
    }
}
