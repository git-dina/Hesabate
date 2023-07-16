using Hesabate_POS;
//using Hesabate_POS.ApiClasses;
using MaterialDesignThemes.Wpf;
using netoaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
//using Tulpep.NotificationWindow;
using System.Globalization;
using System.Reflection;
//using System.Drawing.Printing;
namespace Hesabate_POS.Classes
{
    class HelpClass
    {
        public static bool iscodeExist = false;
        /*
        public static Agent agentModel = new Agent();
        public static Branch branchModel = new Branch();
        public static Category categoryModel = new Category();
        public static Pos posModel = new Pos();
        public static Offer offerModel = new Offer();
        */
        //public static string code;
        static public BrushConverter brushConverter = new BrushConverter();
        public static ImageBrush imageBrush = new ImageBrush();
        /*
        static public bool isAdminPermision()
        {
            if (MainWindow.userLogin.isAdmin == true)
                return true;
            return false;
        }
        static public bool isSupportPermision()
        {
            //if (MainWindow.userLogin.userId == 1 || MainWindow.userLogin.userId == 2)
            if (MainWindow.userLogin.isAdmin == true && MainWindow.userLogin.username == "Support@Increase")
                return true;
            return false;
        }
        */
        public static bool validateEmpty(string str, Path p_error)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(str))
            {
                p_error.Visibility = Visibility.Visible;
                #region Tooltip
                ToolTip toolTip = new ToolTip();
                toolTip.Content = AppSettings.resourcemanager.GetString("trIsRequired");
                toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                p_error.ToolTip = toolTip;
                #endregion
                isValid = false;
            }
            else
            {
                p_error.Visibility = Visibility.Collapsed;
            }
            return isValid;
        }
        public static bool validateEmptyCombo(ComboBox cmb, Path p_error)
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(cmb.Text) || cmb.SelectedValue == null || cmb.SelectedValue.ToString() == "0")
            {
                p_error.Visibility = Visibility.Visible;
                #region Tooltip
                ToolTip toolTip = new ToolTip();
                toolTip.Content = AppSettings.resourcemanager.GetString("trIsRequired");
                toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                p_error.ToolTip = toolTip;
                #endregion
                isValid = false;
            }
            else
            {
                p_error.Visibility = Visibility.Collapsed;
            }
            return isValid;
        }
        public static void SetValidate(Path p_error, string tr)
        {
            #region Tooltip error
            p_error.Visibility = Visibility.Visible;
            ToolTip toolTip = new ToolTip();
            toolTip.Content = AppSettings.resourcemanager.GetString(tr);
            toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
            p_error.ToolTip = toolTip;
            #endregion
        }
        public static void clearValidate(Path p_error)
        {
            p_error.Visibility = Visibility.Collapsed;
        }
        #region validate
        public static bool validate(List<string> requiredControlList, UserControl userControl)
        {
            bool isValid = true;
            try
            {
                //TextBox
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (textBox != null && path != null)
                        if (!HelpClass.validateEmpty(textBox.Text, path))
                            isValid = false;
                }
                //ComboBox
                foreach (var control in requiredControlList)
                {
                    ComboBox comboBox = FindControls.FindVisualChildren<ComboBox>(userControl).Where(x => x.Name == "cb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (comboBox != null && path != null)
                        if (!HelpClass.validateEmptyCombo(comboBox, path))
                            isValid = false;
                }
                //TextBox
                foreach (var control in requiredControlList)
                {
                    TextBlock textBlock = FindControls.FindVisualChildren<TextBlock>(userControl).Where(x => x.Name == "txt_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (textBlock != null && path != null)
                        if (!HelpClass.validateEmpty(textBlock.Text, path))
                            isValid = false;
                }
                //DatePicker
                foreach (var control in requiredControlList)
                {
                    DatePicker datePicker = FindControls.FindVisualChildren<DatePicker>(userControl).Where(x => x.Name == "dp_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (datePicker != null && path != null)
                        if (!HelpClass.validateEmpty(datePicker.Text, path))
                            isValid = false;
                }
                //TimePicker
                foreach (var control in requiredControlList)
                {
                    TimePicker timePicker = FindControls.FindVisualChildren<TimePicker>(userControl).Where(x => x.Name == "tp_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (timePicker != null && path != null)
                        if (!HelpClass.validateEmpty(timePicker.Text, path))
                            isValid = false;
                }
                //PasswordBox
                foreach (var control in requiredControlList)
                {
                    PasswordBox passwordBox = FindControls.FindVisualChildren<PasswordBox>(userControl).Where(x => x.Name == "pb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (passwordBox != null && path != null)
                        if (!HelpClass.validateEmpty(passwordBox.Password, path))
                            isValid = false;
                }
                #region Email
                IsValidEmail(userControl);
                #endregion


            }
            catch { }
            //if (!isValid)
            //{
            //    //saveNotDoneEmptyFields
            //    Toaster.ShowWarning(Window.GetWindow(userControl), message: AppSettings.resourcemanager.GetString("saveNotDoneEmptyFields"), animation: ToasterAnimation.FadeIn);
            //}
            return isValid;
        }
        public static bool validate(List<string> requiredControlList, Window userControl)
        {
            bool isValid = true;
            try
            {
                //TextBox
                foreach (var control in requiredControlList)
                {
                    TextBox textBox = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (textBox != null && path != null)
                        if (!HelpClass.validateEmpty(textBox.Text, path))
                            isValid = false;
                }
                //ComboBox
                foreach (var control in requiredControlList)
                {
                    ComboBox comboBox = FindControls.FindVisualChildren<ComboBox>(userControl).Where(x => x.Name == "cb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (comboBox != null && path != null)
                        if (!HelpClass.validateEmptyCombo(comboBox, path))
                            isValid = false;
                }
                //TextBox
                foreach (var control in requiredControlList)
                {
                    TextBlock textBlock = FindControls.FindVisualChildren<TextBlock>(userControl).Where(x => x.Name == "txt_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (textBlock != null && path != null)
                        if (!HelpClass.validateEmpty(textBlock.Text, path))
                            isValid = false;
                }
                //DatePicker
                foreach (var control in requiredControlList)
                {
                    DatePicker datePicker = FindControls.FindVisualChildren<DatePicker>(userControl).Where(x => x.Name == "dp_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (datePicker != null && path != null)
                        if (!HelpClass.validateEmpty(datePicker.Text, path))
                            isValid = false;
                }
                //TimePicker
                foreach (var control in requiredControlList)
                {
                    TimePicker timePicker = FindControls.FindVisualChildren<TimePicker>(userControl).Where(x => x.Name == "tp_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (timePicker != null && path != null)
                        if (!HelpClass.validateEmpty(timePicker.Text, path))
                            isValid = false;
                }
                //PasswordBox
                foreach (var control in requiredControlList)
                {
                    PasswordBox passwordBox = FindControls.FindVisualChildren<PasswordBox>(userControl).Where(x => x.Name == "pb_" + control)
                        .FirstOrDefault();
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (passwordBox != null && path != null)
                        if (!HelpClass.validateEmpty(passwordBox.Password, path))
                            isValid = false;
                }
                #region Email
                IsValidEmail(userControl);
                #endregion


            }
            catch { }
            return isValid;
        }
        public static bool IsValidEmail(UserControl userControl)
        {//for email
            bool isValidEmail = true;
            TextBox textBoxEmail = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_email")
                    .FirstOrDefault();
            Path pathEmail = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_email")
                    .FirstOrDefault();
            if (textBoxEmail != null && pathEmail != null)
            {
                if (textBoxEmail.Text.Equals(""))
                    return isValidEmail;
                else
                {
                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                          RegexOptions.CultureInvariant | RegexOptions.Singleline);
                    isValidEmail = regex.IsMatch(textBoxEmail.Text);

                    if (!isValidEmail)
                    {
                        pathEmail.Visibility = Visibility.Visible;
                        #region Tooltip
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = AppSettings.resourcemanager.GetString("trErrorEmailToolTip");
                        toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                        pathEmail.ToolTip = toolTip;
                        #endregion
                        isValidEmail = false;
                    }
                    else
                    {
                        pathEmail.Visibility = Visibility.Collapsed;
                    }
                }
            }
            return isValidEmail;

        }
        public static bool IsValidEmail(Window userControl)
        {//for email
            bool isValidEmail = true;
            TextBox textBoxEmail = FindControls.FindVisualChildren<TextBox>(userControl).Where(x => x.Name == "tb_email")
                    .FirstOrDefault();
            Path pathEmail = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_email")
                    .FirstOrDefault();
            if (textBoxEmail != null && pathEmail != null)
            {
                if (textBoxEmail.Text.Equals(""))
                    return isValidEmail;
                else
                {
                    Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                          RegexOptions.CultureInvariant | RegexOptions.Singleline);
                    isValidEmail = regex.IsMatch(textBoxEmail.Text);

                    if (!isValidEmail)
                    {
                        pathEmail.Visibility = Visibility.Visible;
                        #region Tooltip
                        ToolTip toolTip = new ToolTip();
                        toolTip.Content = AppSettings.resourcemanager.GetString("trErrorEmailToolTip");
                        toolTip.Style = Application.Current.Resources["ToolTipError"] as Style;
                        pathEmail.ToolTip = toolTip;
                        #endregion
                        isValidEmail = false;
                    }
                    else
                    {
                        pathEmail.Visibility = Visibility.Collapsed;
                    }
                }
            }
            return isValidEmail;

        }
        public static void clearValidate(List<string> requiredControlList, UserControl userControl)
        {
            try
            {
                foreach (var control in requiredControlList)
                {
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (path != null)
                        HelpClass.clearValidate(path);
                }
            }
            catch { }
        }
        public static void clearValidate(List<string> requiredControlList, Window userControl)
        {
            try
            {
                foreach (var control in requiredControlList)
                {
                    Path path = FindControls.FindVisualChildren<Path>(userControl).Where(x => x.Name == "p_error_" + control)
                        .FirstOrDefault();
                    if (path != null)
                        HelpClass.clearValidate(path);
                }
            }
            catch { }
        }
        #endregion

        public static void getMobile(string _mobile, ComboBox _area, TextBox _tb)
        {//mobile
            if ((_mobile != null))
            {
                string area = _mobile;
                string[] pharr = area.Split('-');
                int j = 0;
                string phone = "";

                foreach (string strpart in pharr)
                {
                    if (j == 0)
                    {
                        area = strpart;
                    }
                    else
                    {
                        phone = phone + strpart;
                    }
                    j++;
                }

                _area.Text = area;

                _tb.Text = phone.ToString();
            }
            else
            {
                _area.SelectedIndex = -1;
                _tb.Clear();
            }
        }

        public static void getPhone(string _phone, ComboBox _area, ComboBox _areaLocal, TextBox _tb)
        {//phone
            if ((_phone != null))
            {
                string area = _phone;
                string[] pharr = area.Split('-');
                int j = 0;
                string phone = "";
                string areaLocal = "";
                foreach (string strpart in pharr)
                {
                    if (j == 0)
                        area = strpart;
                    else if (j == 1)
                        areaLocal = strpart;
                    else
                        phone = phone + strpart;
                    j++;
                }

                _area.Text = area;
                _areaLocal.Text = areaLocal;
                _tb.Text = phone.ToString();
            }
            else
            {
                _area.SelectedIndex = -1;
                _areaLocal.SelectedIndex = -1;
                _tb.Clear();
            }
        }

        public static void clearImg(Button img)
        {
            Uri resourceUri = new Uri("pic/no-image-icon-125x125.png", UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);

            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            imageBrush.ImageSource = temp;
            img.Background = imageBrush;
        }
        public static decimal calcPercentage(decimal value, decimal percentage)
        {
            decimal percentageVal = (value * percentage) / 100;

            return percentageVal;
        }
        public static string DecTostring(decimal? dec)
        {
            string sdc = "0";
            if (dec == null)
            {

            }
            else
            {
                decimal dc = decimal.Parse(dec.ToString());

                switch (AppSettings.accuracy)
                {
                    case "0":
                        sdc = string.Format("{0:F0}", dc);
                        break;
                    case "1":
                        sdc = string.Format("{0:F1}", dc);
                        break;
                    case "2":
                        sdc = string.Format("{0:F2}", dc);

                        break;
                    case "3":
                        sdc = string.Format("{0:F3}", dc);
                        break;
                    default:
                        sdc = string.Format("{0:F1}", dc);
                        break;
                }
                if (dc == 0)
                    sdc = string.Format("{0:G29}", decimal.Parse(sdc));
            }


            return sdc;
        }
        public static void defaultDatePickerStyle(DatePicker dp)
        {
            dp.Loaded += delegate
            {
                var textBox1 = (TextBox)dp.Template.FindName("PART_TextBox", dp);
                if (textBox1 != null)
                {
                    textBox1.Background = dp.Background;
                    textBox1.BorderThickness = dp.BorderThickness;
                }
            };
        }
        public static string DateTodbString(DateTime? date)
        {
            string sdate = "";
            if (date != null)
            {

                //"yyyy'-'MM'-'dd'T'HH':'mm':'ss"
                sdate = date.Value.ToString("yyyy'-'MM'-'dd");


            }

            return sdate;
        }
        static public void searchInComboBox(ComboBox cbm)
        {
            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cbm.Items);
            itemsViewOriginal.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(cbm.Text)) return true;
                else
                {
                    if (((string)o).Contains(cbm.Text)) return true;
                    else return false;
                }
            });
            itemsViewOriginal.Refresh();
        }

    
        /// <summary>
        /// لمنع  الصفر بالبداية
        /// </summary>
        /// <param name="txb"></param>
        static public void InputJustNumber(ref TextBox txb)
        {
            if (txb.Text.Count() == 2 && txb.Text == "00")
            {
                string myString = txb.Text;
                myString = Regex.Replace(myString, "00", "0");
                txb.Text = myString;
                txb.Select(txb.Text.Length, 0);
                txb.Focus();
            }
        }
        static async public void ExceptionMessage(Exception ex, object window, string source, string method, bool showMessage = true)
        {
            try
            {

                //Message
                if (showMessage)
                {
                    if (ex.HResult == -2146233088)
                        Toaster.ShowError(window as Window, message: AppSettings.resourcemanager.GetString("trNoConnection"), animation: ToasterAnimation.FadeIn);
                    else if (ex.HResult != -2147467261)
                        Toaster.ShowError(window as Window, message: ex.HResult + " || " + ex.Message, animation: ToasterAnimation.FadeIn);
                }

                //- 2146233088     An error occurred while sending the request.
                //-2147467261    Void MoveNext()
                /*
                 * if (ex.HResult != -2146233088 && ex.HResult != -2147467261)
                {
                    ErrorClass errorClass = new ErrorClass();
                    errorClass.num = ex.HResult.ToString();
                    errorClass.msg = ex.Message;
                    errorClass.stackTrace = ex.StackTrace;
                    errorClass.targetSite = ex.TargetSite.ToString();
                    errorClass.posId = MainWindow.posLogin.posId;
                    errorClass.branchId = MainWindow.branchLogin.branchId;
                    errorClass.createUserId = MainWindow.userLogin.userId;
                    errorClass.programNamePos = Application.ResourceAssembly.GetName().Name;
                    errorClass.versionNamePos = AppSettings.CurrentVersion;
                    errorClass.source = source;
                    errorClass.method = method;
                    //    Assembly.GetExecutingAssembly().GetName().Version;
                    await errorClass.save(errorClass);
                }
                */
            }
            catch
            {

            }
        }

        static public void StartAwait(Grid grid, string progressRingName = "")
        {
            grid.IsEnabled = false;
            grid.Opacity = 0.6;
            MahApps.Metro.Controls.ProgressRing progressRing = new MahApps.Metro.Controls.ProgressRing();
            progressRing.Name = "prg_awaitRing" + progressRingName;
            progressRing.Foreground = App.Current.Resources["MainColor"] as Brush;
            progressRing.IsActive = true;
            Grid.SetRowSpan(progressRing, 10);
            Grid.SetColumnSpan(progressRing, 10);
            grid.Children.Add(progressRing);
        }
        static public void EndAwait(Grid grid, string progressRingName = "")
        {
            MahApps.Metro.Controls.ProgressRing progressRing = FindControls.FindVisualChildren<MahApps.Metro.Controls.ProgressRing>(grid)
                .Where(x => x.Name == "prg_awaitRing" + progressRingName).FirstOrDefault();
            grid.Children.Remove(progressRing);

            var progressRingList = FindControls.FindVisualChildren<MahApps.Metro.Controls.ProgressRing>(grid)
                 .Where(x => x.Name == "prg_awaitRing" + progressRingName);
            if (progressRingList.Count() == 0)
            {
                grid.IsEnabled = true;
                grid.Opacity = 1;
            }

        }
        /// <summary>
        /// badged name , previous count, new count
        /// </summary>
        /// <param name="badged">badged name</param>
        /// <param name="_count">previous count</param>
        /// <param name="count">new count</param>
        static public void refreshNotification(Badged badged, ref int _count, int count)
        {
            if (count != _count)
            {
                if (count > 9)
                {
                    badged.Badge = "+9";
                }
                else if (count == 0) badged.Badge = "";
                else
                    badged.Badge = count.ToString();
            }
            _count = count;
        }
        /*
        static public void drawBarcode(string barcodeStr, Image img)
        {//barcode image
            // create encoding object
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;

            if (barcodeStr != "")
            {
                System.Drawing.Bitmap serial_bitmap = (System.Drawing.Bitmap)barcode.Draw(barcodeStr, 60);
                System.Drawing.ImageConverter ic = new System.Drawing.ImageConverter();
                //generate bitmap
                img.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(serial_bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            else
                img.Source = null;
        }
        */
        /*
        static public void activateCategoriesButtons(List<Item> items, List<Category> categories, List<Button> btns)
        {
            foreach (Category cat in categories)
            {
                string btn_name = "btn_" + cat.categoryCode;
                Button categoryBtn = new Button();
                foreach (Button btn in btns)
                {
                    if (btn.Name == btn_name)
                    {
                        categoryBtn = btn;
                        break;
                    }
                }

                var isExist = items.Where(x => x.categoryId == cat.categoryId).FirstOrDefault();
                if (isExist == null)
                    categoryBtn.IsEnabled = false;
                else
                    categoryBtn.IsEnabled = true;
            }
        }
        static public void activateCategoriesButtons(List<MenuSetting> items, List<Category> categories, List<Button> btns)
        {
            foreach (Category cat in categories)
            {
                string btn_name = "btn_" + cat.categoryCode;
                Button categoryBtn = new Button();
                foreach (Button btn in btns)
                {
                    if (btn.Name == btn_name)
                    {
                        categoryBtn = btn;
                        break;
                    }
                }

                var isExist = items.Where(x => x.categoryId == cat.categoryId).FirstOrDefault();
                if (isExist == null)
                    categoryBtn.IsEnabled = false;
                else
                    categoryBtn.IsEnabled = true;
            }
        }
        */


        public static string decimalToTime(decimal remainingTime)
        {
            TimeSpan span = TimeSpan.FromMinutes(double.Parse(remainingTime.ToString()));
            var timeArr = span.ToString().Split(':');

            var hoursToMinutes = int.Parse(timeArr[0]) * 60;

            timeArr[1] = (int.Parse(timeArr[1]) + hoursToMinutes).ToString();

            //string label = (int)span.TotalMinutes + ":" + span.Seconds;
            string label = timeArr[1].ToString().PadLeft(2, '0') + ":" + Math.Round(decimal.Parse(timeArr[2])).ToString().PadLeft(2, '0');
            return label;
        }
        static public string translate_days(string str)
        {
            switch (str)
            {
                case "sat":
                    return AppSettings.resourcemanager.GetString("trSaturday"); ;
                //break;
                case "sun":
                    return AppSettings.resourcemanager.GetString("trSunday");
                //break;
                case "mon":
                    return AppSettings.resourcemanager.GetString("trMonday");
                //break;
                case "tues":
                    return AppSettings.resourcemanager.GetString("trTuesday");
                //break;
                case "wed":
                    return AppSettings.resourcemanager.GetString("trWednsday");
                //break;
                case "thur":
                    return AppSettings.resourcemanager.GetString("trThursday");
                //break;
                case "fri":
                    return AppSettings.resourcemanager.GetString("trFriday");
                //break;
                default: return str;
                    //break;
            }

        }

        public static void deleteDirectoryFiles(string rootFolder)
        {
            // Delete all files in a directory    
            string[] files = System.IO.Directory.GetFiles(rootFolder, "*.txt");
            foreach (string file in files)
            {
                try
                {
                    System.IO.File.Delete(file);
                }
                catch { }
            }
        }


        public static string ConvertInvType(string invType)
        {
            string value = "";
            value = invType;

            try
            {

                switch (value)
                {
                    //مشتريات 
                    case "p":
                        value = AppSettings.resourcemanager.GetString("trPurchaseInvoice");
                        break;
                    //فاتورة مشتريات بانتظار الادخال
                    case "pw":
                        value = AppSettings.resourcemanager.GetString("trPurchaseInvoiceWaiting");
                        break;
                    //مبيعات
                    case "s":
                        value = AppSettings.resourcemanager.GetString("trSalesInvoice");
                        break;
                    //مرتجع مبيعات
                    case "sb":
                        value = AppSettings.resourcemanager.GetString("trSalesReturnInvoice");
                        break;
                    //مرتجع مشتريات
                    case "pb":
                        value = AppSettings.resourcemanager.GetString("trPurchaseReturnInvoice");
                        break;
                    //فاتورة مرتجع مشتريات بانتظار الاخراج
                    case "pbw":
                        value = AppSettings.resourcemanager.GetString("trPurchaseReturnInvoiceWaiting");
                        break;
                    //مسودة مشتريات 
                    case "pd":
                        value = AppSettings.resourcemanager.GetString("trDraftPurchaseBill");
                        break;
                    //مسودة مبيعات
                    case "sd":
                        value = AppSettings.resourcemanager.GetString("trSalesDraft");
                        break;
                    //مسودة مرتجع مبيعات
                    case "sbd":
                        value = AppSettings.resourcemanager.GetString("trSalesReturnDraft");
                        break;
                    //مسودة مرتجع مشتريات
                    case "pbd":
                        value = AppSettings.resourcemanager.GetString("trPurchaseReturnDraft");
                        break;
                    // مسودة طلبية مبيعا 
                    case "ord":
                        value = AppSettings.resourcemanager.GetString("trDraft");
                        break;
                    //طلبية مبيعات 
                    case "or":
                        value = AppSettings.resourcemanager.GetString("trSaleOrder");
                        break;
                    //مسودة طلبية شراء 
                    case "pod":
                        value = AppSettings.resourcemanager.GetString("trDraft");
                        break;
                    //طلبية شراء 
                    case "po":
                        value = AppSettings.resourcemanager.GetString("trPurchaceOrder");
                        break;
                    // طلبية شراء أو بيع محفوظة
                    case "pos":
                    case "ors":
                        value = AppSettings.resourcemanager.GetString("trSaved");
                        break;
                    //مسودة عرض 
                    case "qd":
                        value = AppSettings.resourcemanager.GetString("trQuotationsDraft");
                        break;
                    //عرض سعر محفوظ
                    case "qs":
                        value = AppSettings.resourcemanager.GetString("trSaved");
                        break;
                    //فاتورة عرض اسعار
                    case "q":
                        value = AppSettings.resourcemanager.GetString("trQuotations");
                        break;
                    //الإتلاف
                    case "d":
                        value = AppSettings.resourcemanager.GetString("trDestructive");
                        break;
                    //النواقص
                    case "sh":
                        value = AppSettings.resourcemanager.GetString("trShortage");
                        break;
                    //مسودة  استراد
                    case "imd":
                        value = AppSettings.resourcemanager.GetString("trImportDraft");
                        break;
                    // استراد
                    case "im":
                        value = AppSettings.resourcemanager.GetString("trImport");
                        break;
                    // طلب استيراد
                    case "imw":
                        value = AppSettings.resourcemanager.GetString("trImportOrder");
                        break;
                    //مسودة تصدير
                    case "exd":
                        value = AppSettings.resourcemanager.GetString("trExportDraft");
                        break;
                    // تصدير
                    case "ex":
                        value = AppSettings.resourcemanager.GetString("trExport");
                        break;
                    // طلب تصدير
                    case "exw":
                        value = AppSettings.resourcemanager.GetString("trExportOrder");
                        break;
                    // إدخال مباشر
                    case "is":
                        value = AppSettings.resourcemanager.GetString("trDirectEntry");
                        break;
                    // مسودة إدخال مباشر
                    case "isd":
                        value = AppSettings.resourcemanager.GetString("trDirectEntryDraft");
                        break;
                    // مسودة طلب خارجي
                    case "tsd":
                        value = AppSettings.resourcemanager.GetString("trTakeAwayDraft");
                        break;
                    // طلب خارجي
                    case "ts":
                        value = AppSettings.resourcemanager.GetString("trTakeAway");
                        break;
                    // خدمة ذاتية
                    case "ss":
                        value = AppSettings.resourcemanager.GetString("trSelfService");
                        break;
                    // خدمة ذاتية مسودة
                    case "ssd":
                        value = AppSettings.resourcemanager.GetString("trSelfServiceDraft");
                        break;
                    // فاتورة استهلاك
                    case "fbc":
                        value = AppSettings.resourcemanager.GetString("consumptionInvoice");
                        break;
                    // مسودة طلب صرف
                    case "srd":
                        value = AppSettings.resourcemanager.GetString("trSpendingRequestDraft");
                        break;
                    //  طلب صرف
                    case "sr":
                        value = AppSettings.resourcemanager.GetString("trSpendingRequest");
                        break;
                    // مرتجع طلب صرف 
                    case "srb":
                        value = AppSettings.resourcemanager.GetString("trSpendingRequestReturn");
                        break;
                    //  طلب صرف في الانتظار 
                    case "srw":
                        value = AppSettings.resourcemanager.GetString("trSpendingOrderWait");
                        break;
                    default: break;
                }
                return value;
            }
            catch
            {
                return "";
            }
        }
        public static string DateToString(DateTime? date)
        {
            string sdate = "";
            if (date != null)
            {
                //DateTime ts = DateTime.Parse(date.ToString());
                // @"hh\:mm\:ss"
                //sdate = ts.ToString(@"d/M/yyyy");
                DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;

                switch (AppSettings.dateFormat)
                {
                    case "ShortDatePattern":
                        sdate = date.Value.ToString(dtfi.ShortDatePattern);
                        break;
                    case "LongDatePattern":
                        sdate = date.Value.ToString(dtfi.LongDatePattern);
                        break;
                    case "MonthDayPattern":
                        sdate = date.Value.ToString(dtfi.MonthDayPattern);
                        break;
                    case "YearMonthPattern":
                        sdate = date.Value.ToString(dtfi.YearMonthPattern);
                        break;
                    default:
                        sdate = date.Value.ToString(dtfi.ShortDatePattern);
                        break;
                }
            }

            return sdate;
        }

        public static decimal Round(decimal v)
        {
            return Math.Ceiling(v * 1000) / 1000;
        }
        public static decimal CustomRound(decimal x,int digit)
        {
            StringBuilder sb = new StringBuilder(x.ToString());
            decimal newNum = 0;

            var pointIndex = x.ToString().IndexOf(".");
            var str = x.ToString().Substring(pointIndex+1);
            var lastIndex = x.ToString().Count() - 1;
            var lastNum =int.Parse( str.Last().ToString());
            switch (lastNum)
            {
                case 0:
                case 1:
                case 2:
                    sb[lastIndex] = '0';
                    newNum = decimal.Parse(sb.ToString());
                        break;

                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    sb[lastIndex] = '5';
                    newNum = decimal.Parse(sb.ToString());

                    break;

                case 8:
                case 9:
                    sb[lastIndex] = '0';
                    var str2 = x.ToString().Substring(x.ToString().Count()-1);
                    newNum = decimal.Parse(str2) + (decimal)0.01;
                    break;
            }

            return newNum;
            //var count = CountDigitsAfterDecimal(x);

            //return decimal.Round(x - 0.001m,count, MidpointRounding.ToEven);
        }

       static int CountDigitsAfterDecimal(decimal value)
        {
            bool start = false;
            int count = 0;
            foreach (var s in value.ToString())
            {
                if (s == '.')
                {
                    start = true;
                }
                else if (start)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
