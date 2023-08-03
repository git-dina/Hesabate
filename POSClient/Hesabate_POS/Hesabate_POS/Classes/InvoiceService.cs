using Hesabate_POS.Classes.ApiClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes
{
    public class InvoiceService
    {
        private HttpClient client = AppSettings.httpClient;
        public async Task<InvoiceModel> SaveInvoice(List<ItemModel> invoiceItems,string doStr,string tableId,string totalAfterDiscount,
           string UNo ,string overDiscount, string vat,string vatIncluded,string vatAmount,string billStatus,
           string cname,string paid,string note,string note2,string overDiscountPercentage, string external,
           string service,string emp, string forUse,string takeaway, string onTable, string roomReservationId,
           string roundDis, string autoDiscount, string requestP)
        {

            InvoiceModel invoice = new InvoiceModel();
            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + "/POS/p5api2.php");
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(AppSettings.token), "token");
                content.Add(new StringContent("2"), "op");

                var myContent = JsonConvert.SerializeObject(invoiceItems);
                content.Add(new StringContent(myContent), "items");
                content.Add(new StringContent(doStr), "Do");
                content.Add(new StringContent(tableId), "table_id");
                content.Add(new StringContent(totalAfterDiscount), "total_after_discount");
                content.Add(new StringContent(UNo), "UNo");
                content.Add(new StringContent(overDiscount), "over_discount");
                content.Add(new StringContent(vat), "vat");
                content.Add(new StringContent(vatIncluded), "vat_included");
                content.Add(new StringContent(vatAmount), "vat_amount");
                content.Add(new StringContent(billStatus), "bill_status");
                content.Add(new StringContent(cname), "cname");//مبيعات نقدية
                content.Add(new StringContent(paid), "paid");//0,1
                content.Add(new StringContent(note), "note");
                content.Add(new StringContent(note2), "note2");
                content.Add(new StringContent(overDiscountPercentage), "over_discount_percentage");
                content.Add(new StringContent(external), "external");//0,1
                content.Add(new StringContent(service), "service");
                content.Add(new StringContent(emp), "emp");//empId
                content.Add(new StringContent(forUse), "for_use");//0,1
                content.Add(new StringContent(takeaway), "takeaway");//0,1
                content.Add(new StringContent(onTable), "on_table");
                content.Add(new StringContent(roomReservationId), "room_reservation_id");
                content.Add(new StringContent(roundDis), "round_dis");
                content.Add(new StringContent(autoDiscount), "auto_discount");
                content.Add(new StringContent(requestP), "request");//0

                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                  invoice = JsonConvert.DeserializeObject<InvoiceModel>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                }
            }
            catch (Exception ex)
            {
               // return new List<ItemModel>();
            }
            return invoice;

        }
    }
}
