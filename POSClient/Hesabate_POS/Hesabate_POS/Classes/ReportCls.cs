using Microsoft.Reporting.WebForms;
using Microsoft.Reporting.WinForms;
using Hesabate_POS.Classes.ApiClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Hesabate_POS.Classes
{
  
    class ReportCls
    {
        public string GetLogoImagePath()
        {
            try
            {
                string imageName = AppSettings.companylogoImage;
                string dir = Directory.GetCurrentDirectory();
                string tmpPath = Path.Combine(dir, @"Thumb\setting");
                tmpPath = Path.Combine(tmpPath, imageName);
                if (File.Exists(tmpPath))
                {

                    return tmpPath;
                }
                else
                {
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Thumb\setting\emptylogo.png");
                }

            }
            catch
            {
                return Path.Combine(Directory.GetCurrentDirectory(), @"Thumb\setting\emptylogo.png");
            }
        }
        public string GetIconImagePath(string iconName)
        {
            try
            {
                string imageName = iconName + ".png";
                string dir = Directory.GetCurrentDirectory();
                string tmpPath = Path.Combine(dir, @"pic");
                tmpPath = Path.Combine(tmpPath, imageName);
                if (File.Exists(tmpPath))
                {

                    return tmpPath;
                }
                else
                {
                    return Path.Combine(Directory.GetCurrentDirectory(), @"Thumb\setting\emptylogo.png");
                }



                //string addpath = @"\Thumb\setting\" ;

            }
            catch
            {
                return Path.Combine(Directory.GetCurrentDirectory(), @"Thumb\setting\emptylogo.png");
            }
        }
        public int GetpageHeight(int itemcount, int repheight)
        {
            // int repheight = 457;
            int tableheight = 33 * itemcount;// 33 is cell height


            int totalheight = repheight + tableheight;
            return totalheight;

        }

        public string GetSupplyingOrderRdlcpath( )
        {
            string addpath;
            //rs.width = 224;//224 =5.7cm
            //rs.height = GetpageHeight(itemscount, 500);

            if (AppSettings.lang == "ar")
            {

                //order Ar
                addpath = @"\Reports\ar\supplyingOrder.rdlc";              
                
            }
            else 
            {
                addpath = @"\Reports\en\supplyingOrder.rdlc";
            }
          
            //
            string reppath = PathUp(addpath);
  
            return reppath;
        }

        public List<ReportParameter> fillSupplyingOrderReport(PurchaseInvoice invoice, LocalReport rep, List<ReportParameter> paramarr)
        {
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();

            string discount = "(" + HelpClass.DecTostring(invoice.CoopDiscount) + " %)";

            paramarr.Add(new ReportParameter("companyName", AppSettings.companyName == null ? "-" : AppSettings.companyName));
            paramarr.Add(new ReportParameter("companyNameAr", AppSettings.companyNameAr == null ? "-" : AppSettings.companyNameAr));

            //
            paramarr.Add(new ReportParameter("Fax", AppSettings.companyFax == null ? "-" : AppSettings.companyFax.Replace("--", "")));
            paramarr.Add(new ReportParameter("Tel", AppSettings.companyPhone == null ? "-" : AppSettings.companyPhone.Replace("--", "")));

            paramarr.Add(new ReportParameter("logoImage", "file:\\" + GetLogoImagePath()));
            paramarr.Add(new ReportParameter("com_tel_icon", "file:\\" + GetIconImagePath("phone")));
            paramarr.Add(new ReportParameter("com_fax_icon", "file:\\" + GetIconImagePath("fax")));

            paramarr.Add(new ReportParameter("OrderDate", HelpClass.DateToString(invoice.OrderDate)));
            paramarr.Add(new ReportParameter("invNumber", invoice.InvNumber == null ? "-" : invoice.InvNumber.ToString()));//paramarr[6]
            paramarr.Add(new ReportParameter("LocationName", invoice.LocationName == null ? "-" : invoice.LocationName.ToString()));
            paramarr.Add(new ReportParameter("OrderRecieveDate", HelpClass.DateToString(invoice.OrderRecieveDate)));
            paramarr.Add(new ReportParameter("SupplierNumber",AppSettings.resourcemanager.GetString("SupplierNumber")+": "+ invoice.supplier.SupCode.ToString()));
            paramarr.Add(new ReportParameter("SupplierName", invoice.supplier.Name));
            paramarr.Add(new ReportParameter("EnterpriseDiscount", discount));
            paramarr.Add(new ReportParameter("DiscountValue",HelpClass.DecTostring( invoice.DiscountValue)));
            paramarr.Add(new ReportParameter("Currency",AppSettings.currency));
            paramarr.Add(new ReportParameter("ConsumerDiscount", HelpClass.DecTostring(invoice.ConsumerDiscount)));
            paramarr.Add(new ReportParameter("UserName", "دينا نعمة"));
            paramarr.Add(new ReportParameter("CreateUserId", invoice.CreateUserId.ToString()));

            paramarr.Add(new ReportParameter("Title", AppSettings.resourcemanager.GetString("ProcurementRequestTitle")));
            paramarr.Add(new ReportParameter("trDate", AppSettings.resourcemanager.GetString("trDate")));
            paramarr.Add(new ReportParameter("trTheProcurementRequest", AppSettings.resourcemanager.GetString("TheProcurementRequest")));
            paramarr.Add(new ReportParameter("trToBranch", AppSettings.resourcemanager.GetString("trToBranch")));
            paramarr.Add(new ReportParameter("trDeliveryDate", AppSettings.resourcemanager.GetString("DeliveryDate")));
            paramarr.Add(new ReportParameter("trOrderDescription", AppSettings.resourcemanager.GetString("SupplymentOrderDescription")));
            paramarr.Add(new ReportParameter("trSupplierName", AppSettings.resourcemanager.GetString("SupplierName")));
            paramarr.Add(new ReportParameter("trTotalSale", AppSettings.resourcemanager.GetString("trTotalSale")));
            paramarr.Add(new ReportParameter("trTotalCost", AppSettings.resourcemanager.GetString("trTotalPurchase")));
            paramarr.Add(new ReportParameter("trSeuenceAbbrevation", AppSettings.resourcemanager.GetString("SeuenceAbbrevation")));
            paramarr.Add(new ReportParameter("trItemCode", AppSettings.resourcemanager.GetString("ItemNumber")));
            paramarr.Add(new ReportParameter("trBarcode", AppSettings.resourcemanager.GetString("trBarcode")));
            paramarr.Add(new ReportParameter("trDescription", AppSettings.resourcemanager.GetString("trDescription")));
            paramarr.Add(new ReportParameter("trQTR", AppSettings.resourcemanager.GetString("trQTR")));
            paramarr.Add(new ReportParameter("trUnit", AppSettings.resourcemanager.GetString("trUnit")));
            paramarr.Add(new ReportParameter("trFactor", AppSettings.resourcemanager.GetString("Factor")));
            paramarr.Add(new ReportParameter("trBalance", AppSettings.resourcemanager.GetString("trBalance")));
            paramarr.Add(new ReportParameter("trPurchasePrice", AppSettings.resourcemanager.GetString("PurchasePrice")));
            paramarr.Add(new ReportParameter("trSalePrice", AppSettings.resourcemanager.GetString("SalePrice")));
            paramarr.Add(new ReportParameter("trSum", AppSettings.resourcemanager.GetString("trSum")));
            paramarr.Add(new ReportParameter("trOnly", AppSettings.resourcemanager.GetString("trOnly")));
            
            paramarr.Add(new ReportParameter("trEnterpriseDiscount", AppSettings.resourcemanager.GetString("EnterpriseDiscount")));
            paramarr.Add(new ReportParameter("trItemsDiscount", AppSettings.resourcemanager.GetString("ItemsDiscount")));

            string orderStatus = FillCombo.PurchaseOrderStatusList.Where(x => x.key == invoice.InvStatus).FirstOrDefault().value;
            paramarr.Add(new ReportParameter("OrderStatus", orderStatus));
            paramarr.Add(new ReportParameter("CurrentDateTime", DateTime.Now.ToString()));

            
           //report footer 
            paramarr.Add(new ReportParameter("trSupplyingOrderFooterStr1", AppSettings.resourcemanager.GetString("SupplyingOrderFooterStr1")));
            paramarr.Add(new ReportParameter("trSupplyingOrderFooterStr2", AppSettings.resourcemanager.GetString("SupplyingOrderFooterStr2")));
            paramarr.Add(new ReportParameter("trSupplyingOrderFooterStr3", AppSettings.resourcemanager.GetString("SupplyingOrderFooterStr3")));
            paramarr.Add(new ReportParameter("trSupplyingOrderFooterStr4", AppSettings.resourcemanager.GetString("SupplyingOrderFooterStr4")));
            paramarr.Add(new ReportParameter("trSupplyingOrderFooterStr5", AppSettings.resourcemanager.GetString("SupplyingOrderFooterStr5")));
            paramarr.Add(new ReportParameter("trSupplyingOrderFooterStr6", AppSettings.resourcemanager.GetString("SupplyingOrderFooterStr6")));
            paramarr.Add(new ReportParameter("trFrom", AppSettings.resourcemanager.GetString("trFrom")));
            paramarr.Add(new ReportParameter("trPage", AppSettings.resourcemanager.GetString("trPage")));
            paramarr.Add(new ReportParameter("trPrintDone", AppSettings.resourcemanager.GetString("trPrintDone")));
            paramarr.Add(new ReportParameter("trBy", AppSettings.resourcemanager.GetString("By")));
            paramarr.Add(new ReportParameter("trProcurementOfficer", AppSettings.resourcemanager.GetString("ProcurementOfficer")));
            paramarr.Add(new ReportParameter("trMerchandisingTeamLeader", AppSettings.resourcemanager.GetString("MerchandisingTeamLeader")));
            paramarr.Add(new ReportParameter("trStoresManager", AppSettings.resourcemanager.GetString("StoresManager")));


            //dataSet
            rep.DataSources.Add(new ReportDataSource("DataSetPurchaseDetails", invoice.PurchaseDetails));

            return paramarr;
        }
        public string GetPurchaseOrderRdlcpath()
        {
            string addpath;
            //rs.width = 224;//224 =5.7cm
            //rs.height = GetpageHeight(itemscount, 500);

            if (AppSettings.lang == "ar")
            {

                //order Ar
                addpath = @"\Reports\ar\purchaseOrder.rdlc";

            }
            else
            {
                addpath = @"\Reports\en\purchaseOrder.rdlc";
            }

            //
            string reppath = PathUp(addpath);

            return reppath;
        }
        public List<ReportParameter> fillPurchaseOrderReport(PurchaseInvoice invoice, LocalReport rep, List<ReportParameter> paramarr)
        {
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();

            string discount = "(" + HelpClass.DecTostring(invoice.CoopDiscount) + " %)";

            paramarr.Add(new ReportParameter("companyName", AppSettings.companyName == null ? "-" : AppSettings.companyName));
            paramarr.Add(new ReportParameter("companyNameAr", AppSettings.companyNameAr == null ? "-" : AppSettings.companyNameAr));

            //
            paramarr.Add(new ReportParameter("Fax", AppSettings.companyFax == null ? "-" : AppSettings.companyFax.Replace("--", "")));
            paramarr.Add(new ReportParameter("Tel", AppSettings.companyPhone == null ? "-" : AppSettings.companyPhone.Replace("--", "")));

            paramarr.Add(new ReportParameter("logoImage", "file:\\" + GetLogoImagePath()));
            paramarr.Add(new ReportParameter("com_tel_icon", "file:\\" + GetIconImagePath("phone")));
            paramarr.Add(new ReportParameter("com_fax_icon", "file:\\" + GetIconImagePath("fax")));

            paramarr.Add(new ReportParameter("OrderDate", HelpClass.DateToString(invoice.OrderDate)));
            paramarr.Add(new ReportParameter("Title", AppSettings.resourcemanager.GetString("PurchaseOrderTitle")));

            paramarr.Add(new ReportParameter("invNumber", invoice.InvNumber == null ? "-" : invoice.InvNumber.ToString()));//paramarr[6]
            paramarr.Add(new ReportParameter("SupplyingOrderNum", invoice.InvNumber == null ? "-" : invoice.SupplyingOrderNum.ToString()));//paramarr[6]
            paramarr.Add(new ReportParameter("LocationName", invoice.LocationName == null ? "-" : invoice.LocationName.ToString()));
            paramarr.Add(new ReportParameter("OrderRecieveDate", HelpClass.DateToString(invoice.OrderRecieveDate)));
            paramarr.Add(new ReportParameter("SupplierNumber", AppSettings.resourcemanager.GetString("SupplierNumber") + ": " + invoice.supplier.SupCode.ToString()));
            paramarr.Add(new ReportParameter("SupplierName", invoice.supplier.Name));
            paramarr.Add(new ReportParameter("EnterpriseDiscount", discount));
            paramarr.Add(new ReportParameter("DiscountValue", HelpClass.DecTostring(invoice.DiscountValue)));
            paramarr.Add(new ReportParameter("netCost", HelpClass.DecTostring(invoice.CostNet)));
            paramarr.Add(new ReportParameter("Currency", AppSettings.currency));
            paramarr.Add(new ReportParameter("ConsumerDiscount", HelpClass.DecTostring(invoice.ConsumerDiscount)));
            paramarr.Add(new ReportParameter("UserName", "دينا نعمة"));
            paramarr.Add(new ReportParameter("CreateUserId", invoice.CreateUserId.ToString()));

            paramarr.Add(new ReportParameter("trDate", AppSettings.resourcemanager.GetString("trDate")));
            paramarr.Add(new ReportParameter("trOrderNum", AppSettings.resourcemanager.GetString("OrderNum")));
            paramarr.Add(new ReportParameter("trTheProcurementRequest", AppSettings.resourcemanager.GetString("TheProcurementRequest")));
            paramarr.Add(new ReportParameter("trToBranch", AppSettings.resourcemanager.GetString("trToBranch")));
            paramarr.Add(new ReportParameter("trDeliveryDate", AppSettings.resourcemanager.GetString("DeliveryDate")));
            paramarr.Add(new ReportParameter("trOrderDescription", AppSettings.resourcemanager.GetString("PurchaseOrderDescription")));
            paramarr.Add(new ReportParameter("trOrderDescription2", AppSettings.resourcemanager.GetString("SupplymentOrderDescription")));
            paramarr.Add(new ReportParameter("trSupplierName", AppSettings.resourcemanager.GetString("SupplierName")));
            paramarr.Add(new ReportParameter("trTotalSale", AppSettings.resourcemanager.GetString("trTotalSale")));
            paramarr.Add(new ReportParameter("trTotalCost", AppSettings.resourcemanager.GetString("trTotalPurchase")));
            paramarr.Add(new ReportParameter("trSeuenceAbbrevation", AppSettings.resourcemanager.GetString("SeuenceAbbrevation")));
            paramarr.Add(new ReportParameter("trItemCode", AppSettings.resourcemanager.GetString("ItemNumber")));
            paramarr.Add(new ReportParameter("trBarcode", AppSettings.resourcemanager.GetString("trBarcode")));
            paramarr.Add(new ReportParameter("trDescription", AppSettings.resourcemanager.GetString("trDescription")));
            paramarr.Add(new ReportParameter("trRequiredQuantities", AppSettings.resourcemanager.GetString("RequiredQuantities")));
            paramarr.Add(new ReportParameter("trcartoon", AppSettings.resourcemanager.GetString("cartoon")));
            paramarr.Add(new ReportParameter("trpiece", AppSettings.resourcemanager.GetString("piece")));
            paramarr.Add(new ReportParameter("trUnit", AppSettings.resourcemanager.GetString("trUnit")));
            paramarr.Add(new ReportParameter("trFactor", AppSettings.resourcemanager.GetString("Factor")));

            paramarr.Add(new ReportParameter("trPurchasePrice", AppSettings.resourcemanager.GetString("PurchasePrice")));
            paramarr.Add(new ReportParameter("trSalePrice", AppSettings.resourcemanager.GetString("SalePrice")));
            paramarr.Add(new ReportParameter("trNetCost", AppSettings.resourcemanager.GetString("NetCost")));
            paramarr.Add(new ReportParameter("trOnly", AppSettings.resourcemanager.GetString("trOnly")));

            paramarr.Add(new ReportParameter("trEnterpriseDiscount", AppSettings.resourcemanager.GetString("EnterpriseDiscount")));
            paramarr.Add(new ReportParameter("CurrentDateTime", DateTime.Now.ToString()));

            //report footer 
            paramarr.Add(new ReportParameter("trPurchaseOrderFooterStr1", AppSettings.resourcemanager.GetString("PurchaseOrderFooterStr1")));
            paramarr.Add(new ReportParameter("trPurchaseOrderFooterStr2", AppSettings.resourcemanager.GetString("PurchaseOrderFooterStr2")));
            paramarr.Add(new ReportParameter("trPurchaseOrderFooterStr3", AppSettings.resourcemanager.GetString("PurchaseOrderFooterStr3")));
            paramarr.Add(new ReportParameter("trPurchaseOrderFooterStr4", AppSettings.resourcemanager.GetString("PurchaseOrderFooterStr4")));
            paramarr.Add(new ReportParameter("trPurchaseOrderFooterStr5", AppSettings.resourcemanager.GetString("PurchaseOrderFooterStr5")));
            paramarr.Add(new ReportParameter("trFrom", AppSettings.resourcemanager.GetString("trFrom")));
            paramarr.Add(new ReportParameter("trPage", AppSettings.resourcemanager.GetString("trPage")));
            paramarr.Add(new ReportParameter("trPrintDone", AppSettings.resourcemanager.GetString("trPrintDone")));
            paramarr.Add(new ReportParameter("trBy", AppSettings.resourcemanager.GetString("By")));
            paramarr.Add(new ReportParameter("trManagingDirector", AppSettings.resourcemanager.GetString("ManagingDirector")));
            paramarr.Add(new ReportParameter("trMerchandisingTeamLeader", AppSettings.resourcemanager.GetString("MerchandisingTeamLeader")));
            paramarr.Add(new ReportParameter("trChairmanPurchasingCommittee", AppSettings.resourcemanager.GetString("ChairmanPurchasingCommittee")));


            ////dataSet
            rep.DataSources.Add(new ReportDataSource("DataSetPurchaseDetails", invoice.PurchaseDetails));

            return paramarr;
        }

        public string GetReceiptOrderRdlcpath()
        {
            string addpath;
            //rs.width = 224;//224 =5.7cm
            //rs.height = GetpageHeight(itemscount, 500);

            if (AppSettings.lang == "ar")
            {

                //order Ar
                addpath = @"\Reports\ar\receiptOrder.rdlc";

            }
            else
            {
                addpath = @"\Reports\en\receiptOrder.rdlc";
            }

            //
            string reppath = PathUp(addpath);

            return reppath;
        }

        public List<ReportParameter> fillReceiptOrderReport(Receipt invoice, LocalReport rep, List<ReportParameter> paramarr)
        {
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();

            string title = "";
            switch(invoice.ReceiptType)
            {
                case "purchaseOrders":
                    title = AppSettings.resourcemanager.GetString("ReceiptPurchaseOrderTitle");
                    break; 
                case "direct":
                    title = AppSettings.resourcemanager.GetString("ReceiptDirectTitle");
                    break;
                case "vegetable":
                    title = AppSettings.resourcemanager.GetString("ReceiptVegetablesTitle");
                    break; 
                case "service":
                    title = AppSettings.resourcemanager.GetString("ReceiptServiceTitle");
                    break;
                case "free":
                    title = AppSettings.resourcemanager.GetString("ReceiptFreeTitle");
                    break;
                case "freeVegetables":
                    title = AppSettings.resourcemanager.GetString("ReceiptFreeVegetablesTitle");
                    break; 
                case "customFree":
                    title = AppSettings.resourcemanager.GetString("ReceiptCustomFreeTitle");
                    switch(invoice.CustomFreeType)
                    {
                        case "priceDifferences":
                            title += "-"+AppSettings.resourcemanager.GetString("PriceDifferences"); ;
                            break;
                        case "rent":
                            title += "-" + AppSettings.resourcemanager.GetString("Rent");
                            break; 
                        case "support":
                            title += "-" + AppSettings.resourcemanager.GetString("Support");
                            break;
                    }
                    break;
            }
            string discount = "(" + HelpClass.DecTostring(invoice.CoopDiscount) + " %)";

            paramarr.Add(new ReportParameter("companyName", AppSettings.companyName == null ? "-" : AppSettings.companyName));
            paramarr.Add(new ReportParameter("companyNameAr", AppSettings.companyNameAr == null ? "-" : AppSettings.companyNameAr));

            //
            paramarr.Add(new ReportParameter("Fax", AppSettings.companyFax == null ? "-" : AppSettings.companyFax.Replace("--", "")));
            paramarr.Add(new ReportParameter("Tel", AppSettings.companyPhone == null ? "-" : AppSettings.companyPhone.Replace("--", "")));

            paramarr.Add(new ReportParameter("logoImage", "file:\\" + GetLogoImagePath()));
            paramarr.Add(new ReportParameter("com_tel_icon", "file:\\" + GetIconImagePath("phone")));
            paramarr.Add(new ReportParameter("com_fax_icon", "file:\\" + GetIconImagePath("fax")));

            paramarr.Add(new ReportParameter("OrderDate", HelpClass.DateToString(invoice.ReceiptDate)));
            paramarr.Add(new ReportParameter("invNumber", invoice.InvNumber == null ? "-" : invoice.InvNumber.ToString()));//paramarr[6]
            paramarr.Add(new ReportParameter("InvoiceDate", invoice.SupInvoiceDate == null ? "-" : invoice.SupInvoiceDate.ToString()));
            paramarr.Add(new ReportParameter("LocationName", invoice.LocationName == null ? "-" : invoice.LocationName.ToString()));
            paramarr.Add(new ReportParameter("PurchaseOrderNum", invoice.PurchaseInvNumber ));
            paramarr.Add(new ReportParameter("SupplierNumber", AppSettings.resourcemanager.GetString("SupplierNumber") + ": " + invoice.supplier.SupCode.ToString()));
            paramarr.Add(new ReportParameter("SupplierName", invoice.supplier.Name));
            paramarr.Add(new ReportParameter("SupInvNumber", AppSettings.resourcemanager.GetString("SupplierInvNumberAbbrevation") + ": " + invoice.SupInvoiceNum.ToString()));
            paramarr.Add(new ReportParameter("Notes", invoice.Notes));
            paramarr.Add(new ReportParameter("NetPrice", HelpClass.DecTostring(invoice.TotalPrice)));
            paramarr.Add(new ReportParameter("EnterpriseDiscount", discount));
            paramarr.Add(new ReportParameter("DiscountValue", HelpClass.DecTostring(invoice.DiscountValue)));
            paramarr.Add(new ReportParameter("Currency", AppSettings.currency));
            paramarr.Add(new ReportParameter("ConsumerDiscount", HelpClass.DecTostring(invoice.ConsumerDiscount)));
            paramarr.Add(new ReportParameter("netCost", HelpClass.DecTostring(invoice.CostNet)));

            paramarr.Add(new ReportParameter("UserName", "دينا نعمة"));
            paramarr.Add(new ReportParameter("CreateUserId", invoice.CreateUserId.ToString()));

            paramarr.Add(new ReportParameter("Title", title));
            paramarr.Add(new ReportParameter("trDate", AppSettings.resourcemanager.GetString("DocumentDate")));
            paramarr.Add(new ReportParameter("trOrderNum", AppSettings.resourcemanager.GetString("Voucherno")));
            paramarr.Add(new ReportParameter("trInvoiceDate", AppSettings.resourcemanager.GetString("trInvoiceDate")));
            paramarr.Add(new ReportParameter("trPurchaseOrderNum", AppSettings.resourcemanager.GetString("PurchaseOrderNum")));
            paramarr.Add(new ReportParameter("trSupplierName", AppSettings.resourcemanager.GetString("SupplierName")));
            paramarr.Add(new ReportParameter("trNotes", AppSettings.resourcemanager.GetString("trNotes")));

            paramarr.Add(new ReportParameter("trTotalSale", AppSettings.resourcemanager.GetString("trTotalSale")));
            paramarr.Add(new ReportParameter("trTotalCost", AppSettings.resourcemanager.GetString("trTotalPurchase")));
            paramarr.Add(new ReportParameter("trSeuenceAbbrevation", AppSettings.resourcemanager.GetString("SeuenceAbbrevation")));
            paramarr.Add(new ReportParameter("trItemCode", AppSettings.resourcemanager.GetString("ItemNumber")));
            paramarr.Add(new ReportParameter("trFree", AppSettings.resourcemanager.GetString("Free")));
            paramarr.Add(new ReportParameter("trQuantity", AppSettings.resourcemanager.GetString("trQTR")));
            paramarr.Add(new ReportParameter("trQuantity", AppSettings.resourcemanager.GetString("trQTR")));
            paramarr.Add(new ReportParameter("trPiecesQuantity", AppSettings.resourcemanager.GetString("PiecesQuantity")));
            paramarr.Add(new ReportParameter("trDescription", AppSettings.resourcemanager.GetString("itemName")));
            paramarr.Add(new ReportParameter("trFactor", AppSettings.resourcemanager.GetString("Factor")));
            paramarr.Add(new ReportParameter("trPurchasePrice", AppSettings.resourcemanager.GetString("PurchasePrice")));
            paramarr.Add(new ReportParameter("trSalePrice", AppSettings.resourcemanager.GetString("SalePrice")));
            paramarr.Add(new ReportParameter("trEnterpriseDiscount", AppSettings.resourcemanager.GetString("EnterpriseDiscount")));
            paramarr.Add(new ReportParameter("trTotalConsumerDiscount", AppSettings.resourcemanager.GetString("TotalConsumerDiscount")));
            paramarr.Add(new ReportParameter("trTotalCoopDiscount", AppSettings.resourcemanager.GetString("TotalCoopDiscount")));
            paramarr.Add(new ReportParameter("trTotalFree", AppSettings.resourcemanager.GetString("TotalFree")));




            //report footer 
            paramarr.Add(new ReportParameter("CurrentDateTime", DateTime.Now.ToString()));
            paramarr.Add(new ReportParameter("trSum", AppSettings.resourcemanager.GetString("trSum")));
            paramarr.Add(new ReportParameter("trItemsCount", AppSettings.resourcemanager.GetString("ItemsCount")));
            paramarr.Add(new ReportParameter("ItemsCount", invoice.ReceiptDetails.Count().ToString()));
            paramarr.Add(new ReportParameter("TotalQuantities", invoice.ReceiptDetails.Count().ToString()));
            paramarr.Add(new ReportParameter("trTotalQuantities", AppSettings.resourcemanager.GetString("TotalQuantities")));
            paramarr.Add(new ReportParameter("trOnly", AppSettings.resourcemanager.GetString("trOnly")));
            paramarr.Add(new ReportParameter("trDocumentEditor", AppSettings.resourcemanager.GetString("DocumentEditor")));
            paramarr.Add(new ReportParameter("trFrom", AppSettings.resourcemanager.GetString("trFrom")));
            paramarr.Add(new ReportParameter("trPage", AppSettings.resourcemanager.GetString("trPage")));
            paramarr.Add(new ReportParameter("trPrintDone", AppSettings.resourcemanager.GetString("trPrintDone")));
            paramarr.Add(new ReportParameter("trBy", AppSettings.resourcemanager.GetString("By")));
            paramarr.Add(new ReportParameter("trReceiver", AppSettings.resourcemanager.GetString("Receiver")));
            paramarr.Add(new ReportParameter("trReceivingOfficer", AppSettings.resourcemanager.GetString("ReceivingOfficer")));
            paramarr.Add(new ReportParameter("trCustodyOfficial", AppSettings.resourcemanager.GetString("CustodyOfficial")));
            paramarr.Add(new ReportParameter("trViewer", AppSettings.resourcemanager.GetString("Reviewer")));

            //dataSet
            rep.DataSources.Add(new ReportDataSource("DataSetReceiptDetails", invoice.ReceiptDetails));

            return paramarr;
        }


        public string GetReturnOrderRdlcpath()
        {
            string addpath;
            //rs.width = 224;//224 =5.7cm
            //rs.height = GetpageHeight(itemscount, 500);

            if (AppSettings.lang == "ar")
            {

                //order Ar
                addpath = @"\Reports\ar\returnOrder.rdlc";

            }
            else
            {
                addpath = @"\Reports\en\returnOrder.rdlc";
            }

            //
            string reppath = PathUp(addpath);

            return reppath;
        }

        public List<ReportParameter> fillReturnOrderReport(Receipt invoice, LocalReport rep, List<ReportParameter> paramarr)
        {
            rep.EnableExternalImages = true;
            rep.DataSources.Clear();

            string title = "";
            switch (invoice.ReceiptType)
            {
                case "normalReturn":
                    title = AppSettings.resourcemanager.GetString("ReturnDocumentTitle");
                    break;
                case "vegetablesReturn":
                    title = AppSettings.resourcemanager.GetString("VegetablesReturnDocumentTitle");
                    break;                
            }
            string discount = "(" + HelpClass.DecTostring(invoice.CoopDiscount) + " %)";

            paramarr.Add(new ReportParameter("companyName", AppSettings.companyName == null ? "-" : AppSettings.companyName));
            paramarr.Add(new ReportParameter("companyNameAr", AppSettings.companyNameAr == null ? "-" : AppSettings.companyNameAr));

            //
            paramarr.Add(new ReportParameter("Fax", AppSettings.companyFax == null ? "-" : AppSettings.companyFax.Replace("--", "")));
            paramarr.Add(new ReportParameter("Tel", AppSettings.companyPhone == null ? "-" : AppSettings.companyPhone.Replace("--", "")));

            paramarr.Add(new ReportParameter("logoImage", "file:\\" + GetLogoImagePath()));
            paramarr.Add(new ReportParameter("com_tel_icon", "file:\\" + GetIconImagePath("phone")));
            paramarr.Add(new ReportParameter("com_fax_icon", "file:\\" + GetIconImagePath("fax")));

            paramarr.Add(new ReportParameter("OrderDate", HelpClass.DateToString(invoice.ReceiptDate)));
            paramarr.Add(new ReportParameter("invNumber", invoice.InvNumber == null ? "-" : invoice.InvNumber.ToString()));//paramarr[6]
            paramarr.Add(new ReportParameter("LocationName", invoice.LocationName == null ? "-" : invoice.LocationName.ToString()));
            paramarr.Add(new ReportParameter("SupplierNumber", AppSettings.resourcemanager.GetString("SupplierNumber") + ": " + invoice.supplier.SupCode.ToString()));
            paramarr.Add(new ReportParameter("SupplierName", invoice.supplier.Name));
            paramarr.Add(new ReportParameter("Notes", invoice.Notes));
            paramarr.Add(new ReportParameter("NetPrice", HelpClass.DecTostring(invoice.TotalPrice)));
            paramarr.Add(new ReportParameter("netCost", HelpClass.DecTostring(invoice.CostNet)));

            paramarr.Add(new ReportParameter("UserName", "دينا نعمة"));
            paramarr.Add(new ReportParameter("CreateUserId", invoice.CreateUserId.ToString()));

            paramarr.Add(new ReportParameter("Title", title));
            paramarr.Add(new ReportParameter("trDate", AppSettings.resourcemanager.GetString("DocumentDate")));
            paramarr.Add(new ReportParameter("trOrderNum", AppSettings.resourcemanager.GetString("Voucherno")));
            paramarr.Add(new ReportParameter("trSupplierName", AppSettings.resourcemanager.GetString("SupplierName")));
            paramarr.Add(new ReportParameter("trNotes", AppSettings.resourcemanager.GetString("trNotes")));

            paramarr.Add(new ReportParameter("trTotalSale", AppSettings.resourcemanager.GetString("trTotalSale")));
            paramarr.Add(new ReportParameter("trTotalCost", AppSettings.resourcemanager.GetString("trTotalPurchase")));
            paramarr.Add(new ReportParameter("trSeuenceAbbrevation", AppSettings.resourcemanager.GetString("SeuenceAbbrevation")));
            paramarr.Add(new ReportParameter("trItemCode", AppSettings.resourcemanager.GetString("ItemNumber")));
            paramarr.Add(new ReportParameter("trFree", AppSettings.resourcemanager.GetString("Free")));
            paramarr.Add(new ReportParameter("trQuantity", AppSettings.resourcemanager.GetString("trQTR")));
            paramarr.Add(new ReportParameter("trQuantity", AppSettings.resourcemanager.GetString("trQTR")));
            paramarr.Add(new ReportParameter("trPiecesQuantity", AppSettings.resourcemanager.GetString("PiecesQuantity")));
            paramarr.Add(new ReportParameter("trDescription", AppSettings.resourcemanager.GetString("itemName")));
            paramarr.Add(new ReportParameter("trFactor", AppSettings.resourcemanager.GetString("Factor")));
            paramarr.Add(new ReportParameter("trPurchasePrice", AppSettings.resourcemanager.GetString("PurchasePrice")));
            paramarr.Add(new ReportParameter("trSalePrice", AppSettings.resourcemanager.GetString("SalePrice")));
            paramarr.Add(new ReportParameter("trNetCost", AppSettings.resourcemanager.GetString("TotalCost")));

            //report footer 
            paramarr.Add(new ReportParameter("CurrentDateTime", DateTime.Now.ToString()));
            paramarr.Add(new ReportParameter("trSum", AppSettings.resourcemanager.GetString("trSum")));
            paramarr.Add(new ReportParameter("trItemsCount", AppSettings.resourcemanager.GetString("ItemsCount")));
            paramarr.Add(new ReportParameter("ItemsCount", invoice.ReceiptDetails.Count().ToString()));
            paramarr.Add(new ReportParameter("TotalQuantities", invoice.ReceiptDetails.Count().ToString()));
            paramarr.Add(new ReportParameter("trTotalQuantities", AppSettings.resourcemanager.GetString("TotalQuantities")));
            paramarr.Add(new ReportParameter("trOnly", AppSettings.resourcemanager.GetString("trOnly")));
            paramarr.Add(new ReportParameter("trDocumentEditor", AppSettings.resourcemanager.GetString("DocumentEditor")));
            paramarr.Add(new ReportParameter("trFrom", AppSettings.resourcemanager.GetString("trFrom")));
            paramarr.Add(new ReportParameter("trPage", AppSettings.resourcemanager.GetString("trPage")));
            paramarr.Add(new ReportParameter("trPrintDone", AppSettings.resourcemanager.GetString("trPrintDone")));
            paramarr.Add(new ReportParameter("trBy", AppSettings.resourcemanager.GetString("By")));
            paramarr.Add(new ReportParameter("trReceiver", AppSettings.resourcemanager.GetString("Receiver")));
            paramarr.Add(new ReportParameter("trCustodyOfficial", AppSettings.resourcemanager.GetString("CustodyOfficial")));
            paramarr.Add(new ReportParameter("trViewer", AppSettings.resourcemanager.GetString("Reviewer")));
            paramarr.Add(new ReportParameter("trSignature", AppSettings.resourcemanager.GetString("Signature")));
            paramarr.Add(new ReportParameter("trCivilNo", AppSettings.resourcemanager.GetString("CivilNo")));

            //dataSet
            rep.DataSources.Add(new ReportDataSource("DataSetReceiptDetails", invoice.ReceiptDetails));

            return paramarr;
        }
        public string PathUp( string addtopath)
        {
            string path = Directory.GetCurrentDirectory();
            string newPath = path + addtopath;
            try
            {
                FileAttributes attr = File.GetAttributes(newPath);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                { }
                else
                {
                    string finalDir = Path.GetDirectoryName(newPath);
                    if (!Directory.Exists(finalDir))
                        Directory.CreateDirectory(finalDir);
                    if (!File.Exists(newPath))
                        File.Create(newPath);
                }
            }
            catch { }
            return newPath;
        }

        
    }
}
