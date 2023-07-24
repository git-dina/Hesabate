using CefSharp;
using CefSharp.Handler;
using CefSharp.Wpf;
using Hesabate_POS.Classes;
using Hesabate_POS.Classes.ApiClasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public string uri =AppSettings.APIUri+ "/POS/pp2.php?token=" + AppSettings.token;

        public bool isOk { get; set; }
        //public static List<string> requiredControlList;

        ChromiumWebBrowser Mainchrome;
        //System.Windows.Forms.WebBrowser bmMain;
        //private external _callBackObjectForJs;

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

              
                Mainchrome = new ChromiumWebBrowser(uri);

                 //bmMain = new System.Windows.Forms.WebBrowser();
                //_callBackObjectForJs = new external(bmMain);
                 //Mainchrome.RegisterJsObject("external", _callBackObjectForJs);

                //Mainchrome.MenuHandler = new MyCustomMenuHandler();
                grid_webBrowser.Children.Add(Mainchrome);
                //Mainchrome.Dock = DockStyle.Fill;

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

            txt_title.Text = Translate.getResource("104");
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
        /*
        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                HelpClass.StartAwait(grid_main);




                isOk = true;
                this.Close();
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        */
    }

    public class external
    {
        System.Windows.Forms.WebBrowser bmMain;

        public external(System.Windows.Forms.WebBrowser bmMain1)
        {
            bmMain = bmMain1;
            bmMain.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(WDocumentCompleted);
        }
        //public string is_externalK() { return "1"; }
        public bool is_externalK()
        {
            return true;
        }
        private Int32 Int32Parse(string num)
        {
            if (num == null || num.Trim().Equals("")) return 0;
            Int32 val = 0;
            Int32.TryParse(num.Trim(), out val);
            return val;
        }
        public void showMessage(string msg)
        {//Read Note
            System.Windows.Forms.MessageBox.Show(msg);
        }
        public class Result
        {
            public string RespCode { get; set; }
            public string RespDesc { get; set; }
        }
        public string KioskPOS(double amount, string bill_id, int Currency)
        {
            try
            {
                //POS pos = new POS();
                //pos.baudRate = 9600;
                //pos.comPort = "COM" + Settings.Default.POSCom;
                //if (Currency == 400) amount *= 1000;
                //else amount *= 100;
                ////MessageBox.Show(amount.ToString());
                //string Purchasestring = pos.Purchase(bill_id, Int32Parse(amount.ToString()).ToString(), Currency, 1, 1, 120);
                //pos = null;
                string Purchasestring = "";
                string filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Responce.txt");

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine(Purchasestring);
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                    writer.Close();
                }

                Int32 position1 = Purchasestring.IndexOf('{');
                // MessageBox.Show(position1.ToString());
                Int32 position2 = Purchasestring.IndexOf('}');
                if (position1 < 0 || position2 < 0)
                {
                    if (position1 >= 0)
                    {
                        string Code = "";
                        /*solve this issue : the string is not complete*/
                        string[] tmpR = Purchasestring.Split(',');
                        for (int l = 0; l < tmpR.Length; l++)
                        {
                            if (tmpR[l].IndexOf("RespCode") >= 0)
                            {
                                string[] tmpR2 = tmpR[l].Split(':');
                                if (tmpR2.Length > 1) Code += tmpR2[1].Replace("\"", "");
                            }
                            if (Code.Length > 0)
                                if (tmpR[l].IndexOf("RespDesc") >= 0)
                                {
                                    string[] tmpR2 = tmpR[l].Split(':');
                                    if (tmpR2.Length > 1) Code += " : " + tmpR2[1].Replace("\"", "");
                                }
                        }
                        if (Code.Length > 0) return Code;
                    }

                    else return "Error Connecting : " + Purchasestring;
                }
                string returnstring = Purchasestring.Substring(position1, position2 - position1 + 1);
                Result m = JsonConvert.DeserializeObject<Result>(returnstring);
                return m.RespCode + " : " + m.RespDesc;
            }
            catch (Exception EXXX)
            {
                return EXXX.ToString();
            }
        }
       

        void WDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((System.Windows.Forms.WebBrowser)sender).Print();

            // Dispose the WebBrowser now that the task is complete. 
            /* ((WebBrowser)sender).Dispose();*/
        }
    }

    public class CustomLoadHandler : ILoadHandler
    {
        public void OnFrameLoadEnd(IWebBrowser chromiumWebBrowser, FrameLoadEndEventArgs frameLoadEndArgs)
        {
            throw new NotImplementedException();
        }

        public void OnFrameLoadStart(IWebBrowser chromiumWebBrowser, FrameLoadStartEventArgs frameLoadStartArgs)
        {
            throw new NotImplementedException();
        }

        public void OnLoadError(IWebBrowser chromiumWebBrowser, LoadErrorEventArgs loadErrorArgs)
        {
            throw new NotImplementedException();
        }

        public void OnLoadingStateChange(IWebBrowser chromiumWebBrowser, LoadingStateChangedEventArgs loadingStateChangedArgs)
        {
            throw new NotImplementedException();
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

   
}
