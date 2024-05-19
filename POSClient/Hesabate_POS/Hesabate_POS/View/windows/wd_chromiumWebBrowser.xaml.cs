using CefSharp;
using CefSharp.Handler;
using CefSharp.Wpf;
using ControlzEx.Standard;
using Hesabate_POS.Classes;
using Hesabate_POS.Classes.ApiClasses;
using Hesabate_POS.Properties;
using Newtonsoft.Json;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Hesabate_POS.View.windows
{
    /// <summary>
    /// Interaction logic for wd_chromiumWebBrowser.xaml
    /// </summary>
    public partial class wd_chromiumWebBrowser : Window
    {

        public wd_chromiumWebBrowser()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private void Btn_colse_Click(object sender, RoutedEventArgs e)
        {
            isOk = false;
            this.Close();
        }

        public string url{get;set;}
        public string title{get;set;}

        public bool isOk { get; set; }
        public string returnedValue { get; set; }

        ChromiumWebBrowser Mainchrome;
        System.Windows.Forms.WebBrowser bmMain;
        private external _callBackObjectForJs;

        string x;
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load

            try
            {


                HelpClass.StartAwait(grid_main);
                //requiredControlList = new List<string> { "", };

                #region translate

                //if (AppSettings.lang.Equals("en"))
                //{
                //    grid_main.FlowDirection = FlowDirection.LeftToRight;
                //}
                //else
                //{
                //    grid_main.FlowDirection = FlowDirection.RightToLeft;
                //}
                translate();
                #endregion

                string windowURL = AppSettings.APIUri  + url;
                Mainchrome = new ChromiumWebBrowser(windowURL);

                 bmMain = new System.Windows.Forms.WebBrowser();

                 _callBackObjectForJs = new external(bmMain,this);
               // Mainchrome.RegisterJsObject("external", _callBackObjectForJs);

                CefSharpSettings.WcfEnabled = true;
                Mainchrome.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
                Mainchrome.JavascriptObjectRepository.Register("external", _callBackObjectForJs, isAsync: false, options: BindingOptions.DefaultBinder);

                Mainchrome.LoadHandler = new CustomLoadHandler(Mainchrome);
                // Add this  for request handler
                Mainchrome.RequestHandler = new CustomRequestHandler();

                grid_webBrowser.Children.Add(Mainchrome);

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }


        private void translate()
        {

            txt_title.Text = title;
        }

       
        private void HandleKeyPress(object sender, System.Windows.Input.KeyEventArgs e)
        {
            /*
            try
            {
                if (e.Key == Key.Return)
                {
                    Btn_save_Click(btn_save, null);
                }
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name); }
       */
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;

                this.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }
        #region events
        /*
        string input;
        decimal _decimal = 0;
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {


                //only  digits
                TextBox textBox = sender as TextBox;
                HelpClass.InputJustNumber(ref textBox);
                if (textBox.Tag.ToString() == "int")
                {
                    Regex regex = new Regex("[^0-9]");
                    e.Handled = regex.IsMatch(e.Text);
                }
                else if (textBox.Tag.ToString() == "decimal")
                {
                    input = e.Text;
                    e.Handled = !decimal.TryParse(textBox.Text + input, out _decimal);

                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void Code_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                //only english and digits
                Regex regex = new Regex("^[a-zA-Z0-9. -_?]*$");
                if (!regex.IsMatch(e.Text))
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }
        private void Spaces_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                e.Handled = e.Key == Key.Space;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        */
        /*
        private void ValidateEmpty_TextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void validateEmpty_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.validate(requiredControlList, this);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        */
        #endregion
     
    }

    public class external
    {
        System.Windows.Forms.WebBrowser bmMain;
        wd_chromiumWebBrowser mainWindowBrowser;
        string printerName;
        private Bitmap bitmap;
        int last_p_line = 0;
        string MyPrint_string = "";
        public external(System.Windows.Forms.WebBrowser bmMain1, wd_chromiumWebBrowser _mainWindowBrowser, string printer="")
        {
            bmMain = bmMain1;
            mainWindowBrowser = _mainWindowBrowser;
            printerName = printer;
            //bmMain.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WDocumentCompleted);
        }
        //public string is_externalK() { return "1"; }
        public bool is_externalK()
        {
            return true;
        }
        //public bool MyDialog(string s1, string s2)
        //{
        //    System.Windows.Forms.MessageBox.Show(s1 + " ==>"+ s2);
        //    wd_chromiumWebBrowser dialogWindow = new wd_chromiumWebBrowser();
        //    dialogWindow.url = s1;
        //    var hr = dialogWindow.ShowDialog();
        //    return true;
        //}
        private Int32 Int32Parse(string num)
        {
            if (num == null || num.Trim().Equals("")) return 0;
            Int32 val = 0;
            Int32.TryParse(num.Trim(), out val);
            return val;
        }

        public class Result
        {
            public string RespCode { get; set; }
            public string RespDesc { get; set; }
        }
        public string KioskPOS(string metaData)
        {
            try
            {
                var data = metaData.Split('&');
                mainWindowBrowser.returnedValue = data[0];
                mainWindowBrowser.isOk = true;
                //Yasin
                mainWindowBrowser.Dispatcher.Invoke(() =>
                {
                    mainWindowBrowser.Close();
                });

                return metaData;
            }
            catch (Exception EXXX)
            {
                return EXXX.ToString();
            }
        }

        public void AmmarPrint(string url)
        {
            try
            {
                bmMain.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WDocumentCompleted);
                bmMain.Navigate( url);
            }
            catch (Exception EXXX)
            {
            }

        }
        public void PrintToPrinter(string url)
        {
            try
            {  
                bmMain.Navigate( url);
                bmMain.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WDocumentCompleted2);
                // MessageBox.Show(Settings.Default.StartPage + "/" + url);
            }
            catch (Exception EXXX)
            {
               // MessageBox.Show(EXXX.ToString());
            }

        } 
        public void PrintToPrinter2(string url)
        {
            try
            {
                printerName = this.printerName.Trim();
                bmMain.Navigate( url);
                bmMain.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WDocumentCompleted3);
            }
            catch (Exception EXXX)
            {
               // MessageBox.Show(EXXX.ToString());
            }

        }

        //public string KioskPOS(double amount, string bill_id, int Currency)
        //{
        //    try
        //    {
        //        //POS pos = new POS();
        //        //pos.baudRate = 9600;
        //        //pos.comPort = "COM" + Settings.Default.POSCom;
        //        //if (Currency == 400) amount *= 1000;
        //        //else amount *= 100;
        //        ////MessageBox.Show(amount.ToString());
        //        //string Purchasestring = pos.Purchase(bill_id, Int32Parse(amount.ToString()).ToString(), Currency, 1, 1, 120);
        //        //pos = null;
        //        string Purchasestring = "";
        //        string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Responce.txt");

        //        using (StreamWriter writer = new StreamWriter(filePath, true))
        //        {
        //            writer.WriteLine(Purchasestring);
        //            writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
        //            writer.Close();
        //        }

        //        Int32 position1 = Purchasestring.IndexOf('{');
        //        // MessageBox.Show(position1.ToString());
        //        Int32 position2 = Purchasestring.IndexOf('}');
        //        if (position1 < 0 || position2 < 0)
        //        {
        //            if (position1 >= 0)
        //            {
        //                string Code = "";
        //                /*solve this issue : the string is not complete*/
        //                string[] tmpR = Purchasestring.Split(',');
        //                for (int l = 0; l < tmpR.Length; l++)
        //                {
        //                    if (tmpR[l].IndexOf("RespCode") >= 0)
        //                    {
        //                        string[] tmpR2 = tmpR[l].Split(':');
        //                        if (tmpR2.Length > 1) Code += tmpR2[1].Replace("\"", "");
        //                    }
        //                    if (Code.Length > 0)
        //                        if (tmpR[l].IndexOf("RespDesc") >= 0)
        //                        {
        //                            string[] tmpR2 = tmpR[l].Split(':');
        //                            if (tmpR2.Length > 1) Code += " : " + tmpR2[1].Replace("\"", "");
        //                        }
        //                }
        //                if (Code.Length > 0) return Code;
        //            }

        //            else return "Error Connecting : " + Purchasestring;
        //        }
        //        string returnstring = Purchasestring.Substring(position1, position2 - position1 + 1);
        //        Result m = JsonConvert.DeserializeObject<Result>(returnstring);
        //        return m.RespCode + " : " + m.RespDesc;
        //    }
        //    catch (Exception EXXX)
        //    {
        //        return EXXX.ToString();
        //    }
        //}


        void WDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            PrinterSettings settings = new PrinterSettings();
            //settings.PrinterName = "";
            ((System.Windows.Forms.WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            /* ((WebBrowser)sender).Dispose();*/
        } 
        void WDocumentCompleted2(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

            try
            {

                Printers.SetDefaultPrinter(printerName);
                // Print the document now that it is fully loaded.
                ((System.Windows.Forms.WebBrowser)sender).Print();

                Task.Delay(1000);

                // Dispose the WebBrowser now that the task is complete. 
                ((System.Windows.Forms.WebBrowser)sender).Dispose();
            }
            catch (Exception ex)
            {
              
            }
           // PrinterSettings settings = new PrinterSettings();
            //settings.PrinterName = printerName;

            //((System.Windows.Forms.WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            /* ((WebBrowser)sender).Dispose();*/
        }
        void WDocumentCompleted3(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            using (PrintDocument pd = new PrintDocument())
            {
                pd.PrinterSettings.PrinterName = printerName;
                pd.DefaultPageSettings.PaperSize =  new PaperSize("MyPaper", 900, 1500);
                pd.PrintController = new StandardPrintController();
                pd.PrintPage += new PrintPageEventHandler(MyPrint);

                pd.Print();
            }
        }


        //private void MyPrint(object sender, PrintPageEventArgs ev)
        //{
        //    try
        //    {
        //        float yPos = 0;
        //        float leftMargin = 10;
        //        float topMargin = 10;
        //        //float righMargin = ev.MarginBounds.Right;
        //        float righMargin = ev.PageBounds.Width - 30;
        //        float midMargin = (ev.PageBounds.Width / 2) - 10;
        //        System.Drawing.Font printFont = new System.Drawing.Font("Arial", 12 + 9);
        //        StringFormat cellformat = new StringFormat();
        //        cellformat.Trimming = StringTrimming.None;
        //        yPos = topMargin;
        //        string[] line = MyPrint_string.Split('ª');
        //        RectangleF recte;
        //        int height = 10;
        //        if (last_p_line == 0)
        //        {
        //            if (line.Length >= 17)
        //            {
        //                for (int ic = 0; ic < 17; ic++)
        //                {
        //                    string[] info_data = line[ic].Split('`');
        //                    if (info_data.Length >= 2 && info_data[1].Trim().Length > 0 && info_data[1].Trim().Length > 0)
        //                    {
        //                        if (info_data[0].Trim().Length > 0)
        //                        {
        //                            //get_bill_by the height

        //                            ev.Graphics.DrawString(info_data[0].Trim(), printFont, System.Drawing.Brushes.Black, leftMargin, yPos, cellformat);

        //                            //ev.Graphics.DrawString(info_data[1].Trim(), printFont, Brushes.Black, midMargin, yPos, cellformat);
        //                            height = (int)ev.Graphics.MeasureString(info_data[1].Trim(), printFont, (int)midMargin).Height > height ? (int)ev.Graphics.MeasureString(info_data[1].Trim(), printFont, (int)midMargin).Height : height;
        //                            ev.Graphics.DrawString(info_data[1].Trim(), printFont, new SolidBrush(System.Drawing.Color.Black), new RectangleF(midMargin, yPos, midMargin, height), cellformat);

        //                            yPos += height;// printFont.GetHeight(ev.Graphics);
        //                        }
        //                        else
        //                        {
        //                            height = (int)ev.Graphics.MeasureString(info_data[1].Trim(), printFont, (int)midMargin).Height > height ? (int)ev.Graphics.MeasureString(info_data[1].Trim(), printFont, (int)midMargin).Height : height;
        //                            ev.Graphics.DrawString(info_data[1].Trim(), printFont, new SolidBrush(System.Drawing.Color.Black), new RectangleF(midMargin, yPos, midMargin, height), cellformat);

        //                            yPos += height;// printFont.GetHeight(ev.Graphics);
        //                        }
        //                    }
        //                }

        //                string[] info_datax = line[17].Split('`');
        //                ev.Graphics.DrawString(info_datax[0].Trim(), printFont, System.Drawing.Brushes.Black, leftMargin, yPos, cellformat);
        //                ev.Graphics.DrawString(info_datax[1].Trim(), printFont, System.Drawing.Brushes.Black, righMargin - 30, yPos, cellformat);
        //                yPos += printFont.GetHeight(ev.Graphics);
        //                ev.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Black), leftMargin, yPos, leftMargin + ev.PageBounds.Width - 30, yPos);
        //                yPos += 10;

        //                for (int j = 19; j < line.Length; j += 2)
        //                {

        //                    recte = new RectangleF(leftMargin, yPos, righMargin - 30, ev.Graphics.MeasureString(line[j], printFont,
        //                    Convert.ToInt32(righMargin) - 30).Height);

        //                    ev.Graphics.DrawString(line[j], printFont, System.Drawing.Brushes.Black, recte, cellformat);
        //                    ev.Graphics.DrawString(line[j + 1], printFont, System.Drawing.Brushes.Black, righMargin - 30, yPos, cellformat);
        //                    yPos += recte.Height;
        //                    ev.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Gray), leftMargin, yPos, leftMargin + ev.PageBounds.Width - 50, yPos);
        //                    yPos += 10;
        //                    last_p_line = j;
        //                    if (yPos > ev.MarginBounds.Bottom)
        //                    {
        //                        ev.HasMorePages = true;
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //                for (int j = 0; j < line.Length; j++)
        //                {
        //                    ev.Graphics.DrawString(line[j], printFont, System.Drawing.Brushes.Black, leftMargin, yPos, cellformat);
        //                    yPos += printFont.GetHeight(ev.Graphics);
        //                }
        //            ev.HasMorePages = false;
        //            last_p_line = 0;
        //        }
        //        else
        //        {
        //            for (int j = last_p_line + 2; j < line.Length; j += 2)
        //            {
        //                recte = new RectangleF(leftMargin, yPos, righMargin - 30, ev.Graphics.MeasureString(line[j], printFont,
        //                Convert.ToInt32(righMargin) - 30).Height);

        //                ev.Graphics.DrawString(line[j], printFont, System.Drawing.Brushes.Black, recte, cellformat);
        //                ev.Graphics.DrawString(line[j + 1], printFont, System.Drawing.Brushes.Black, righMargin - 30, yPos, cellformat);
        //                yPos += recte.Height;
        //                ev.Graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Gray), leftMargin, yPos, leftMargin + ev.PageBounds.Width - 50, yPos);
        //                yPos += 10;
        //                last_p_line = j;
        //                if (yPos > ev.MarginBounds.Bottom)
        //                {
        //                    ev.HasMorePages = true;
        //                    return;
        //                }
        //            }
        //        }
        //        ev.HasMorePages = false;
        //        last_p_line = 0;
        //    }
        //    catch (Exception Ex)
        //    {
        //       // WriteErrorToFile(Ex, "Error 65");
        //    }
        //}
        private void MyPrint(object sender, PrintPageEventArgs ev)
        {
            try
            {


                var scrollH = bmMain.Document.Body.ScrollRectangle.Height;
                var scrollW = bmMain.Document.Body.ScrollRectangle.Width;
                int left = ev.MarginBounds.Left;
                int right = ev.MarginBounds.Right;
                int top = ev.MarginBounds.Top;
                int bottom = ev.MarginBounds.Bottom;
                //bitmap = new Bitmap((right-left)+scrollW, (bottom-top)+scrollH);
                //bmMain.DrawToBitmap(bitmap, new System.Drawing.Rectangle(new System.Drawing.Point(left,top), new System.Drawing.Size(bitmap.Width,bitmap.Height)));

                Bitmap bitmap = new Bitmap(bmMain.Width, bmMain.Height);
                bmMain.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height));
                ev.Graphics.DrawImage(bitmap, 0,0);
                //ev.Graphics.DrawImage(bitmap, new System.Drawing.Rectangle(0, 0, (right - left) + scrollW, (bottom - top) + scrollH));

                //ev.Graphics.DrawImage(bitmap, new System.Drawing.Rectangle(0, 0, 1000, 1500));
                bitmap.Save("output.png", ImageFormat.Png);
            }
            catch (Exception Ex)
            {

            }
        }
    }

    public class CustomLoadHandler : ILoadHandler
    {
        ChromiumWebBrowser bmMain;
        public CustomLoadHandler(ChromiumWebBrowser bmMain1)
        {
            bmMain = bmMain1;
        }
        public void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
        {
        }

        public void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
        {
           
        }

        public void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
        {
            
        }

        public void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs e)
        {
           
            //bmMain.ExecuteScriptAsync("CefSharp.BindObjectAsync(\"external\",\"bound\");");
        }
    }
    public class MyCustomMenuHandler : IContextMenuHandler
    {
        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            // Remove any existent option using the Clear method of the model
            //
            model.Clear();

            Console.WriteLine("Context menu opened !");

            // You can add a separator in case that there are more items on the list
            if (model.Count > 0)
            {
                model.AddSeparator();
            }


            // Add a new item to the list using the AddItem method of the model
            //  model.AddItem((CefMenuCommand)26501, "Options");
            /* model.AddItem((CefMenuCommand)26502, "Close DevTools");

             // Add a separator
             model.AddSeparator();

             // Add another example item
             model.AddItem((CefMenuCommand)26503, "Display alert message");*/
        }
       
        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {
            // React to the first ID (show dev tools method)
            if (commandId == (CefMenuCommand)26501)
            {
                //using (OptionsForm of = new OptionsForm())
                //{
                //    of.ShowDialog();
                //}
                return true;
            }
            /*               if (commandId == (CefMenuCommand)26501)
                           {
                               browser.GetHost().ShowDevTools();
                               return true;
                           }

                           // React to the second ID (show dev tools method)
                           if (commandId == (CefMenuCommand)26502)
                           {
                               browser.GetHost().CloseDevTools();
                               return true;
                           }

                           // React to the third ID (Display alert message)
                           if (commandId == (CefMenuCommand)26503)
                           {
                               MessageBox.Show("An example alert message ?");
                               return true;
                           }
           */
            // Any new item should be handled through a new if statement


            // Return false should ignore the selected option of the user !
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {

        }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false;
        }
    }
   
    public class MyCustomResourceRequestHandler : CefSharp.Handler.ResourceRequestHandler
    {
        private readonly System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();

        protected override IResponseFilter GetResourceResponseFilter(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            if (response.StatusCode == 200)
            {
                

            }
            return new CefSharp.ResponseFilter.StreamResponseFilter(memoryStream);
        }

        protected override void OnResourceLoadComplete(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            //You can now get the data from the stream
            var bytes = memoryStream.ToArray();

            if (response.Charset == "utf-8")
            {
                //var str = System.Text.Encoding.UTF8.GetString(bytes);
                //Console.WriteLine("In OnResourceLoadComplete : " + str.Substring(0, 10) + " <...>");
            }
            else
            {
              
                //Deal with different encoding here
            }
        }
    }

    public class CustomRequestHandler : CefSharp.Handler.RequestHandler
    {
         string invoiceQuery = AppSettings.APIUri.Replace("\\\\", "//") + "/search/pos_search/desktop_search/_1api.php?token=" + AppSettings.token;
        protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
        {
            
            Console.WriteLine("In GetResourceRequestHandler : " + request.Url);
            //Only intercept specific Url's
            if (request.Url == "http://s.hesabate.com/POS/pp2.php?token=S0N2V1Y4YmdzSktqVEJzTkRqSldDdFp1ckY3YkJKUE9yYVNEdE9KMjdQWWtQVFRvcWt6RHNWUlZYaWxFd1VrUEtUa1dHU3hZSW9kYXBLS1FXUzVIbng1cHVMZ05oZkgzM2hPT09TRVpBbHpzZnlTMmFseG9Nc2JiUDE5THNjc25iTk1MRkJnRE50Mit4TXJVUVhEbGhKWUhCLyt2U0NIeUR1Yjg5ay9JNnMrd0NvRzRZaFYzdnpvTW15VzltaHNNbzJJR1NKemI0a3lGV204S3Z6V2hycGRvbWtNTldMM3licUlDalBxMVJCWVRwdWZJMkFDYXNJTWFBTXdSYnZ4Zg==" || request.Url == "https://cefsharp.github.io/")
            {
                return new MyCustomResourceRequestHandler();
            }
            else if (request.Url ==  invoiceQuery )
            {
                return new MyCustomResourceRequestHandler();
            }
            //Default behaviour, url will be loaded normally.
            return null;
        }



    }

}
