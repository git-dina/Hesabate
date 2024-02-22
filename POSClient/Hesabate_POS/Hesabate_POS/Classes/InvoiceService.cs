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
                List<ExtraItemModel> basics = new List<ExtraItemModel>();
                if(item.extraItems != null )
                {
                    foreach (var row in item.basicItems)
                    {
                        basics.Add(new ExtraItemModel()
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
                                if (deletes.Count.Equals(0))
                                    deletes.Add(new ExtraItemModel() { 
                                        group_name = row.group_name,
                                        group_count = row.group_count,
                                    });

                                deletes[0].group_items.Add(add);
                            }

                }
                items.Add(new InvoiceItem() {
                    id =  item.product_id,
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
                    x_discount =  item.x_discount,
                    x_vat = item.x_vat,
                    extraitems = extras,
                    addsitems = adds,
                    deletesitems = deletes,
                    isbasics = basics
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
                if(item.deletesItems != null )
                {
                    foreach (var row in item.deletesItems)
                        foreach (var add in row.group_items)
                            if (add.basicAmount != 0)
                            {
                                if (deletes.Count.Equals(0))
                                    deletes.Add(new ExtraItemModel() { 
                                        group_name = row.group_name,
                                        group_count = row.group_count,
                                    });

                                deletes[0].group_items.Add(add);
                            }

                }
                items.Add(new InvoiceItem() {
                    id = item.product_id,
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
                    extraitems = extras,
                    addsitems = adds,
                    deletesitems = deletes,
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
                content.Add(new StringContent(invoice.external), "external");//0,1,2
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

        public async Task<InvoiceModel> GetInvoiceInfo(string type,string invoiceId)
        {
            InvoiceModel invoice = new InvoiceModel();
            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + "/POS/p5api2.php");

            var content = new MultipartFormDataContent();
            content.Add(new StringContent(AppSettings.token), "token");
            content.Add(new StringContent("14"), "op");
            content.Add(new StringContent(type), "type"); //0: next invoice, 1: previouse invoice, 2:current invoice
            content.Add(new StringContent(invoiceId), "id");
            content.Add(new StringContent("0"), "table_id");
            content.Add(new StringContent("0"), "request");
            content.Add(new StringContent("1"), "priceid");
            request.Content = content;
            var response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                //var res = JsonConvert.DeserializeObject<dynamic>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                //invoice.items = JsonConvert.DeserializeObject<List<InvoiceItem>>(res["items"], new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
                //jsonString = "{\"id\":6,\"year_id\":1,\"bill_date\":\"1900-1-1\",\"is_converted\":\"1\",\"customer_id\":\"0\",\"bill_status\":\"1\",\"is_printed\":0,\"round_dis\":\"0\",\"auto_discount\":\"0\",\"allow\":1,\"total\":\"100\",\"dumy_name\":\"\",\"paid\":\"0\",\"external\":0,\"for_use\":0,\"takeaway\":0,\"on_table\":0,\"over_discount\":\"0\",\"vat\":\"0\",\"vat_included\":\"0\",\"vat_amount\":\"0\",\"dis_per\":\"0\",\"note2\":\"\",\"emp\":\"0\",\"empn\":\"\",\"note\":\"\",\"table_id\":\"0\",\"items\":[{\"id\":6915,\"detail\":\"\",\"amount\":2,\"bonus\":0,\"price\":50,\"name\":\"بروست 6 قطع\",\"no_w\":\"0\",\"x_vat\":\"0\",\"unit\":\"1\",\"x_discount\":0,\"measure_id\":\"0\",\"serial_text\":\"\",\"discount_per\":\"0\",\"discount\":0,\"is_special\":\"-1\",\"back_val\":\"\",\"is_ext\":\"0\",\"min_p\":0,\"max_p\":0,\"extraitems\":\"[{\\\"group_name\\\":\\\"\\\\u0645\\\\u062c\\\\u0645\\\\u0648\\\\u0639\\\\u0629 2\\\",\\\"group_count\\\":1,\\\"group_items\\\":[{\\\"id\\\":6912,\\\"name\\\":\\\"\\\\u0643\\\\u0648\\\\u0644\\\\u0627\\\",\\\"unit\\\":\\\"1\\\",\\\"unit_name\\\":\\\"\\\\u062d\\\\u0628\\\\u0629\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":1,\\\"add_price\\\":10,\\\"add_price_amount\\\":0,\\\"sub_price\\\":0,\\\"allow_add\\\":0,\\\"allow_sub\\\":0,\\\"groupName\\\":\\\"\\\\u0645\\\\u062c\\\\u0645\\\\u0648\\\\u0639\\\\u0629 2\\\",\\\"basicAmount\\\":1},{\\\"id\\\":6909,\\\"name\\\":\\\"toti\\\",\\\"unit\\\":\\\"1\\\",\\\"unit_name\\\":\\\"\\\\u062d\\\\u0628\\\\u0629\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":0,\\\"add_price\\\":10,\\\"add_price_amount\\\":0,\\\"sub_price\\\":0,\\\"allow_add\\\":0,\\\"allow_sub\\\":0,\\\"groupName\\\":\\\"\\\\u0645\\\\u062c\\\\u0645\\\\u0648\\\\u0639\\\\u0629 2\\\",\\\"basicAmount\\\":0}]},{\\\"group_name\\\":\\\"\\\\u0645\\\\u062c\\\\u0645\\\\u0648\\\\u0639\\\\u0629 1\\\",\\\"group_count\\\":1,\\\"group_items\\\":[{\\\"id\\\":6910,\\\"name\\\":\\\"Rizo\\\",\\\"unit\\\":\\\"1\\\",\\\"unit_name\\\":\\\"\\\\u062d\\\\u0628\\\\u0629\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":1,\\\"add_price\\\":0,\\\"add_price_amount\\\":0,\\\"sub_price\\\":0,\\\"allow_add\\\":0,\\\"allow_sub\\\":0,\\\"groupName\\\":\\\"\\\\u0645\\\\u062c\\\\u0645\\\\u0648\\\\u0639\\\\u0629 1\\\",\\\"basicAmount\\\":0},{\\\"id\\\":6911,\\\"name\\\":\\\"\\\\u0633\\\\u0637\\\\u0644\\\\u0629\\\",\\\"unit\\\":\\\"1\\\",\\\"unit_name\\\":\\\"\\\\u062d\\\\u0628\\\\u0629\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":0,\\\"add_price\\\":0,\\\"add_price_amount\\\":0,\\\"sub_price\\\":0,\\\"allow_add\\\":0,\\\"allow_sub\\\":0,\\\"groupName\\\":\\\"\\\\u0645\\\\u062c\\\\u0645\\\\u0648\\\\u0639\\\\u0629 1\\\",\\\"basicAmount\\\":1}]}]\",\"addsitems\":\"[{\\\"group_name\\\":\\\"\\\\u0627\\\\u0636\\\\u0627\\\\u0641\\\\u0627\\\\u062a\\\",\\\"group_count\\\":0,\\\"group_items\\\":[{\\\"id\\\":6921,\\\"name\\\":\\\"\\\\u0645\\\\u0644\\\\u062d \\\\u0627\\\\u0636\\\\u0627\\\\u0641\\\\u0627\\\\u062a \\\\u0628\\\\u062f\\\\u0648\\\\u0646 \\\\u0633\\\\u0639\\\\u0631\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":0,\\\"add_price\\\":3,\\\"add_price_amount\\\":5,\\\"sub_price\\\":0,\\\"allow_add\\\":1,\\\"allow_sub\\\":0,\\\"groupName\\\":null,\\\"basicAmount\\\":1},{\\\"id\\\":6922,\\\"name\\\":\\\"\\\\u0645\\\\u0644\\\\u062d \\\\u0627\\\\u0636\\\\u0627\\\\u0641\\\\u0627\\\\u062a \\\\u0645\\\\u0639 \\\\u0633\\\\u0639\\\\u0631\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":0,\\\"add_price\\\":5,\\\"add_price_amount\\\":5,\\\"sub_price\\\":0,\\\"allow_add\\\":1,\\\"allow_sub\\\":0,\\\"groupName\\\":null,\\\"basicAmount\\\":2},{\\\"id\\\":6925,\\\"name\\\":\\\"\\\\u0635\\\\u0646\\\\u0641 \\\\u0645\\\\u062d\\\\u0630\\\\u0648\\\\u0641\\\\u0627\\\\u062a+\\\\u0627\\\\u0636\\\\u0627\\\\u0641\\\\u0627\\\\u062a\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":0,\\\"add_price\\\":10,\\\"add_price_amount\\\":5,\\\"sub_price\\\":10,\\\"allow_add\\\":1,\\\"allow_sub\\\":1,\\\"groupName\\\":null,\\\"basicAmount\\\":3},{\\\"id\\\":6923,\\\"name\\\":\\\"\\\\u0633\\\\u0644\\\\u0637\\\\u0629 \\\\u0645\\\\u062d\\\\u0630\\\\u0648\\\\u0641\\\\u0627\\\\u062a \\\\u0628\\\\u062f\\\\u0648\\\\u0646 \\\\u0633\\\\u0639\\\\u0631\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":0,\\\"add_price\\\":0,\\\"add_price_amount\\\":5,\\\"sub_price\\\":4,\\\"allow_add\\\":0,\\\"allow_sub\\\":1,\\\"groupName\\\":null,\\\"basicAmount\\\":1},{\\\"id\\\":6924,\\\"name\\\":\\\"\\\\u0633\\\\u0644\\\\u0637\\\\u0629 \\\\u0645\\\\u062d\\\\u0630\\\\u0648\\\\u0641\\\\u0627\\\\u062a \\\\u0645\\\\u0639 \\\\u0633\\\\u0639\\\\u0631\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":0,\\\"add_price\\\":0,\\\"add_price_amount\\\":5,\\\"sub_price\\\":3,\\\"allow_add\\\":0,\\\"allow_sub\\\":1,\\\"groupName\\\":null,\\\"basicAmount\\\":2},{\\\"id\\\":6925,\\\"name\\\":\\\"\\\\u0635\\\\u0646\\\\u0641 \\\\u0645\\\\u062d\\\\u0630\\\\u0648\\\\u0641\\\\u0627\\\\u062a+\\\\u0627\\\\u0636\\\\u0627\\\\u0641\\\\u0627\\\\u062a\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":0,\\\"add_price\\\":10,\\\"add_price_amount\\\":5,\\\"sub_price\\\":10,\\\"allow_add\\\":1,\\\"allow_sub\\\":1,\\\"groupName\\\":null,\\\"basicAmount\\\":3}]}]\",\"deletesitems\":\"\",\"isbasics\":\"[{\\\"group_name\\\":\\\"\\\\u0627\\\\u0633\\\\u0627\\\\u0633\\\\u064a\\\",\\\"group_count\\\":6,\\\"group_items\\\":[{\\\"id\\\":6916,\\\"name\\\":\\\"\\\\u0648\\\\u0631\\\\u0643\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":2,\\\"add_price\\\":8,\\\"add_price_amount\\\":0,\\\"sub_price\\\":0,\\\"allow_add\\\":0,\\\"allow_sub\\\":0,\\\"groupName\\\":\\\"\\\\u0627\\\\u0633\\\\u0627\\\\u0633\\\\u064a\\\",\\\"basicAmount\\\":2},{\\\"id\\\":6914,\\\"name\\\":\\\"\\\\u0635\\\\u062f\\\\u0648\\\\u0631\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":2,\\\"add_price\\\":8,\\\"add_price_amount\\\":0,\\\"sub_price\\\":0,\\\"allow_add\\\":0,\\\"allow_sub\\\":0,\\\"groupName\\\":\\\"\\\\u0627\\\\u0633\\\\u0627\\\\u0633\\\\u064a\\\",\\\"basicAmount\\\":2},{\\\"id\\\":6913,\\\"name\\\":\\\"\\\\u0641\\\\u062e\\\\u0627\\\\u062f\\\",\\\"unit\\\":\\\"0\\\",\\\"unit_name\\\":\\\"\\\",\\\"no_w\\\":\\\"0\\\",\\\"tax_class\\\":\\\"0\\\",\\\"measure_id\\\":\\\"0\\\",\\\"discount_per\\\":\\\"0\\\",\\\"start_amount\\\":2,\\\"add_price\\\":10,\\\"add_price_amount\\\":0,\\\"sub_price\\\":0,\\\"allow_add\\\":0,\\\"allow_sub\\\":0,\\\"groupName\\\":\\\"\\\\u0627\\\\u0633\\\\u0627\\\\u0633\\\\u064a\\\",\\\"basicAmount\\\":2}]}]\",\"isurgent\":true}],\"request\":0,\"room_reservation_id\":\"0\",\"kiosk\":\"0\",\"last_move_id\":\"49\"}";
                invoice = JsonConvert.DeserializeObject<InvoiceModel>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

            }

            return invoice;
        }
    }
}
