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
        public async Task<InvoiceModel> SaveInvoice(List<ItemModel> invoiceItems,InvoiceModel invoice)
        {
            #region invoice item object
            List<InvoiceItem> items = new List<InvoiceItem>();
            foreach(var item in invoiceItems)
            {
                List<ExtraItemModel> extras = new List<ExtraItemModel>();
                if(item.extraItems != null )
                {
                    foreach (var row in item.extraItems)
                    {
                        extras.Add(new ExtraItemModel()
                        {
                            group_name = row.group_name,
                            group_count = row.group_count,
                            group_items = row.group_items,
                        });
                        
                    }
                } 
                 List<ExtraItemModel> adds = new List<ExtraItemModel>();
                if(item.addsItems != null )
                {
                    foreach (var row in item.addsItems)
                        foreach (var add in row.group_items)
                            if (add.basicAmount != 0)
                            {
                                if (adds.Count.Equals(0))
                                    adds.Add(new ExtraItemModel() { 
                                        group_name = row.group_name,
                                        group_count = row.group_count,
                                    });

                                adds[0].group_items.Add(add);
                            }

                } 
                
                List<ExtraItemModel> deletes = new List<ExtraItemModel>();
                if(item.addsItems != null )
                {
                    foreach (var row in item.deletesItems)
                        foreach (var add in row.group_items)
                            if (add.basicAmount != 0)
                            {
                                if (adds.Count.Equals(0))
                                    adds.Add(new ExtraItemModel() { 
                                        group_name = row.group_name,
                                        group_count = row.group_count,
                                    });

                                adds[0].group_items.Add(add);
                            }

                }
                items.Add(new InvoiceItem() {
                    product_id = item.product_id,
                    index = item.index,
                    amount = item.amount,
                    bonus = item.bonus,
                    discount  = item.discount,
                    discount_per = item.discount_per,
                    isUrgent = item.isUrgent,
                    is_ext = item.is_ext,
                    is_special = item.is_special,
                    measure_id = item.measure_id,
                    notes = item.notes,
                    price = item.price,
                    serial_text = item.serial_text,
                    unit = item.unit,
                    x_discount = item.x_discount,
                    x_vat = item.x_vat,
                    extraItems = extras,
                    addsItems = adds,
                    deletesItems = deletes,
                }
                );
            }
            #endregion
            InvoiceModel invoiceRes = new InvoiceModel();
            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + "/POS/p5api2.php");
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(AppSettings.token), "token");
                content.Add(new StringContent("2"), "op");

                var myContent = JsonConvert.SerializeObject(items);
                content.Add(new StringContent(myContent), "items");
                content.Add(new StringContent(invoice.is_do), "Do");
                content.Add(new StringContent(invoice.table_id), "table_id");
                content.Add(new StringContent(invoice.total_after_discount.ToString()), "total_after_discount");
                content.Add(new StringContent(invoice.UNo.ToString()), "UNo");
                content.Add(new StringContent(invoice.over_discount.ToString()), "over_discount");
                content.Add(new StringContent(invoice.vat.ToString()), "vat");
                content.Add(new StringContent(invoice.vat_included), "vat_included");
                content.Add(new StringContent(invoice.vat_amount.ToString()), "vat_amount");
                content.Add(new StringContent(invoice.bill_status), "bill_status");
                content.Add(new StringContent(invoice.cname), "cname");//مبيعات نقدية
                content.Add(new StringContent(invoice.paid), "paid");//0,1
                content.Add(new StringContent(invoice.note), "note");
                content.Add(new StringContent(invoice.note2), "note2");
                content.Add(new StringContent(invoice.over_discount_percentage.ToString()), "over_discount_percentage");
                content.Add(new StringContent(invoice.external), "external");//0,1
                content.Add(new StringContent(invoice.service.ToString()), "service");
                content.Add(new StringContent(invoice.emp), "emp");//empId
                content.Add(new StringContent(invoice.for_use), "for_use");//0,1
                content.Add(new StringContent(invoice.takeaway), "takeaway");//0,1
                content.Add(new StringContent(invoice.on_table), "on_table");
                content.Add(new StringContent(invoice.room_reservation_id.ToString()), "room_reservation_id");
                content.Add(new StringContent(invoice.round_dis.ToString()), "round_dis");
                content.Add(new StringContent(invoice.auto_discount.ToString()), "auto_discount");
                content.Add(new StringContent(invoice.request.ToString()), "request");//0

                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                  invoiceRes = JsonConvert.DeserializeObject<InvoiceModel>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                }
            }
            catch (Exception ex)
            {
               // return new List<ItemModel>();
            }
            return invoiceRes;

        } 
        public async Task<InvoiceModel> ArchiveInvoice(List<ItemModel> invoiceItems,InvoiceModel invoice)
        {
            #region invoice item object
            List<InvoiceItem> items = new List<InvoiceItem>();
            foreach(var item in invoiceItems)
            {
                List<ExtraItemModel> extras = new List<ExtraItemModel>();
                if(item.extraItems != null )
                {
                    foreach (var row in item.extraItems)
                    {
                        extras.Add(new ExtraItemModel()
                        {
                            group_name = row.group_name,
                            group_count = row.group_count,
                            group_items = row.group_items,
                        });
                        
                    }
                } 
                 List<ExtraItemModel> adds = new List<ExtraItemModel>();
                if(item.addsItems != null )
                {
                    foreach (var row in item.addsItems)
                        foreach (var add in row.group_items)
                            if (add.basicAmount != 0)
                            {
                                if (adds.Count.Equals(0))
                                    adds.Add(new ExtraItemModel() { 
                                        group_name = row.group_name,
                                        group_count = row.group_count,
                                    });

                                adds[0].group_items.Add(add);
                            }

                } 
                
                List<ExtraItemModel> deletes = new List<ExtraItemModel>();
                if(item.addsItems != null )
                {
                    foreach (var row in item.deletesItems)
                        foreach (var add in row.group_items)
                            if (add.basicAmount != 0)
                            {
                                if (adds.Count.Equals(0))
                                    adds.Add(new ExtraItemModel() { 
                                        group_name = row.group_name,
                                        group_count = row.group_count,
                                    });

                                adds[0].group_items.Add(add);
                            }

                }
                items.Add(new InvoiceItem() {
                    product_id = item.product_id,
                    index = item.index,
                    amount = item.amount,
                    bonus = item.bonus,
                    discount  = item.discount,
                    discount_per = item.discount_per,
                    isUrgent = item.isUrgent,
                    is_ext = item.is_ext,
                    is_special = item.is_special,
                    measure_id = item.measure_id,
                    notes = item.notes,
                    price = item.price,
                    serial_text = item.serial_text,
                    unit = item.unit,
                    x_discount = item.x_discount,
                    x_vat = item.x_vat,
                    extraItems = extras,
                    addsItems = adds,
                    deletesItems = deletes,
                }
                );
            }
            #endregion
            InvoiceModel invoiceRes = new InvoiceModel();
            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + "/POS/p5api2.php");
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(AppSettings.token), "token");
                content.Add(new StringContent("19"), "op");

                var myContent = JsonConvert.SerializeObject(items);
                content.Add(new StringContent(myContent), "items");
                content.Add(new StringContent(invoice.is_do), "Do");
                content.Add(new StringContent(invoice.table_id), "table_id");
                content.Add(new StringContent(invoice.total_after_discount.ToString()), "total_after_discount");
                content.Add(new StringContent(invoice.UNo.ToString()), "UNo");
                content.Add(new StringContent(invoice.over_discount.ToString()), "over_discount");
                content.Add(new StringContent(invoice.vat.ToString()), "vat");
                content.Add(new StringContent(invoice.vat_included), "vat_included");
                content.Add(new StringContent(invoice.vat_amount.ToString()), "vat_amount");
                content.Add(new StringContent(invoice.bill_status), "bill_status");
                content.Add(new StringContent(invoice.cname), "cname");//مبيعات نقدية
                content.Add(new StringContent(invoice.paid), "paid");//0,1
                content.Add(new StringContent(invoice.note), "note");
                content.Add(new StringContent(invoice.note2), "note2");
                content.Add(new StringContent(invoice.over_discount_percentage.ToString()), "over_discount_percentage");
                content.Add(new StringContent(invoice.external), "external");//0,1
                content.Add(new StringContent(invoice.service.ToString()), "service");
                content.Add(new StringContent(invoice.emp), "emp");//empId
                content.Add(new StringContent(invoice.for_use), "for_use");//0,1
                content.Add(new StringContent(invoice.takeaway), "takeaway");//0,1
                content.Add(new StringContent(invoice.on_table), "on_table");
                content.Add(new StringContent(invoice.room_reservation_id.ToString()), "room_reservation_id");
                content.Add(new StringContent(invoice.round_dis.ToString()), "round_dis");
                content.Add(new StringContent(invoice.auto_discount.ToString()), "auto_discount");
                content.Add(new StringContent(invoice.request.ToString()), "request");//0

                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                  invoiceRes = JsonConvert.DeserializeObject<InvoiceModel>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                }
            }
            catch (Exception ex)
            {
               // return new List<ItemModel>();
            }
            return invoiceRes;

        }
    }
}
