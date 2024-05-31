using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp.DevTools.IO;
using Hesabate_POS.Classes.ApiClasses;
using System.Net.Http;
using System.Net;
using System.Drawing.Printing;
using System.Drawing;

namespace Hesabate_POS.Classes
{
    public class PrintObject
    {
        private HttpClient client = AppSettings.httpClient;
        List<PrintObject> printObject;
        int currentPrint = 0;

        public Setting setting { get; set; }
        public List<InvoiceInfo> inviceInfo { get; set; }
        public List<TableHeader> tableHeader { get; set; }
        public List<List<string>> tableData { get; set; }


        public async Task< List<PrintObject>> GetPrintObjects(string invoiceId)
        {
            var printObject = new List<PrintObject>();

            var url = "/print/p5_p1.php?token="+AppSettings.token+"&handid=23";
            var request = new HttpRequestMessage(HttpMethod.Post, AppSettings.APIUri + url);

            //var content = new MultipartFormDataContent();
            //content.Add(new StringContent("UEF0bkZuamFlalVRU282SVlSbVBtYlBLaXNOalhTbk5lYk9JS2h2KzNrQTB4YjkxUUNMdjdNYWc0SVRhZnQ4Ti96bG1DamhET2t3VE1OZUhxZ3JyTlJQd2l0eTExRWJjNnh6QkxRNmxZTmcrbWNCUkNpN1ZTeENPVk5ZR0JtMWlBUjRHdlFmVk5vQ21TQTZVbTRhMS95eWJjektyYTNMV2ZMblFqVUFidE5TWWl5QnBDTHZGVUFoTVY0djliNGV4L2g3SXlCR00xQ1hsY242RklpcXBIeVNBMUVDcGdJemJKQTB6YkUxZGtWWT0="), "token");
            //content.Add(new StringContent("23"), "handid");
            //request.Content = content;
            var response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                printObject = JsonConvert.DeserializeObject<List<PrintObject>>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
            }


            return printObject;
        }

        public async Task PrintToPrinter(string invoiceId)
        {
            try
            {
                printObject = await GetPrintObjects(invoiceId);
                foreach (var pr in printObject)
                {
                    try
                    {
                        PrintDocument prtdoc = new PrintDocument();

                        prtdoc.DefaultPageSettings.PaperSize = new PaperSize("Custom", pr.setting.width, prtdoc.PrinterSettings.DefaultPageSettings.PaperSize.Height);
                        prtdoc.DefaultPageSettings.PaperSize.RawKind = 119;
                        prtdoc.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = 119;
                        prtdoc.DefaultPageSettings.Landscape = false;

                        prtdoc.PrintController = new System.Drawing.Printing.StandardPrintController();
                        prtdoc.PrinterSettings.PrinterName = pr.setting.printerName;
                        //prtdoc.PrinterSettings.PrinterName = "Snagit 2020";

                        prtdoc.PrintPage += new PrintPageEventHandler(MyPrint);
                        prtdoc.Print();
                        currentPrint++;

                    }
                    catch (Exception es) { }
                }
            
                printObject = new List<PrintObject>();
                currentPrint = 0;
            }
            catch (Exception Ex)
            {

            }
        }

        private void MyPrint( object sender, PrintPageEventArgs ev)
        {
            try
            {
                float yPos = 0;
                float leftMargin = printObject[currentPrint].setting.leftMargin;
                float topMargin = printObject[currentPrint].setting.topMargin;
                float width = printObject[currentPrint].setting.width;
                string font = printObject[currentPrint].setting.font;
                int fontSize = printObject[currentPrint].setting.fontSize;

                float righMargin = ev.PageBounds.Width - 30;
                float midMargin = (ev.PageBounds.Width / 2) - 10;

                System.Drawing.Font printFont = new System.Drawing.Font(font, fontSize);
                StringFormat cellformat = new StringFormat();
                cellformat.Trimming = StringTrimming.None;
                yPos = topMargin;

                int h = 10;
                int height = 10;
                int cellWidth = 0;

                //print invoice Info
                foreach (var info in printObject[currentPrint].inviceInfo)
                {
                    if (info.title != "" || info.title != null)
                    {
                        //ev.Graphics.DrawString(info.Trim(), printFont, System.Drawing.Brushes.Black, leftMargin, yPos, cellformat);
                        cellWidth = (int)ev.Graphics.MeasureString(info.title.Trim(), printFont, (int)midMargin).Width + 30;
                        ev.Graphics.DrawString(info.title.Trim(), printFont, System.Drawing.Brushes.Black, righMargin - cellWidth, yPos, cellformat);
                        height = (int)ev.Graphics.MeasureString(info.title.Trim(), printFont, (int)midMargin).Height > height ? (int)ev.Graphics.MeasureString(info.title.Trim(), printFont, (int)midMargin).Height : height;
                        yPos += height + h;
                    }
                }
                ev.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Gray), leftMargin, yPos, leftMargin + ev.PageBounds.Width - 50, yPos);
                yPos += h;


                cellWidth = 30;
                float titleHeight = 0;
                foreach (var headerData in printObject[currentPrint].tableHeader)
                {
                    cellWidth += (int)ev.Graphics.MeasureString(headerData.title.Trim(), printFont, (int)midMargin).Width + 30;
                    //ev.Graphics.DrawString(headerData.title.Trim(), printFont, System.Drawing.Brushes.Black, righMargin - cellWidth, yPos, cellformat);

                     var titleHeight1= ev.Graphics.MeasureString(headerData.title.Trim(), printFont, headerData.width, StringFormat.GenericDefault).Height;
                    var rect = new RectangleF(righMargin - cellWidth, yPos, headerData.width, yPos+ titleHeight);
                    ev.Graphics.DrawString(headerData.title.Trim(), printFont, System.Drawing.Brushes.Black,rect);

                    headerData.xPoint = cellWidth;
                    titleHeight = titleHeight > titleHeight1 ? titleHeight : titleHeight1;        
                }
                yPos += titleHeight;
                //yPos += printFont.GetHeight(ev.Graphics);

                ev.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Black), leftMargin, yPos, leftMargin + ev.PageBounds.Width - 30, yPos);
                yPos += h;

                int j = 0;

                foreach (var tableData in printObject[currentPrint].tableData)
                {
                    j = 0;
                    float dataHeight = 0;
                    foreach (var raw in tableData)
                    {
                        var dataHeight1 = ev.Graphics.MeasureString(raw.Trim(), printFont, printObject[currentPrint].tableHeader[j].width, StringFormat.GenericDefault).Height;
                        var rect = new RectangleF(righMargin - printObject[currentPrint].tableHeader[j].xPoint, yPos, printObject[currentPrint].tableHeader[j].width, yPos + dataHeight); 
                        ev.Graphics.DrawString(raw.Trim(), printFont, System.Drawing.Brushes.Black, rect);
                    dataHeight = dataHeight > dataHeight1 ? dataHeight : dataHeight1;
                        
                        //ev.Graphics.DrawString(raw.Trim(), printFont, System.Drawing.Brushes.Black, new RectangleF(righMargin - printObject[currentPrint].tableHeader[j].xPoint, yPos, printObject[currentPrint].tableHeader[j].width, yPos));

                        j++;

                    }
                    yPos += dataHeight;
                    //yPos += printFont.GetHeight(ev.Graphics);

                    ev.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Black), leftMargin, yPos, leftMargin + ev.PageBounds.Width - 30, yPos);
                    yPos += h;
                    if (yPos > ev.MarginBounds.Bottom)
                    {
                        ev.HasMorePages = true;
                        return;
                    }
                }


                ev.HasMorePages = false;

            }
            catch (Exception Ex)
            {
                // WriteErrorToFile(Ex, "Error 65");
            }
        }
    }

    public class Setting
    {
        public int width { get; set; }
        public string printerName { get; set; }
        public string font { get; set; } = "Arial";
        public int fontSize { get; set; } = 14;
        public int leftMargin { get; set; } = 14;
        public int topMargin { get; set; } = 14;
    }

    public class InvoiceInfo
    {
        public string title { get; set; }
    }

    public class TableHeader
    {
        public string title { get; set; }
        public int xPoint { get; set; }
        public int width { get; set; } = 40;
    }
    
}
