using Hesabate_POS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Reflection;
using System.Resources;
using Hesabate_POS.View.receipts;
using System.Windows.Media.Animation;
using Hesabate_POS.Classes.ApiClasses;
using MaterialDesignThemes.Wpf;

namespace Hesabate_POS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //internal static User userLogin = new User();
        //internal static Pos posLogin;
        //internal static Branch branchLogin;

        public static DispatcherTimer timer;

        static public MainWindow mainWindow;
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                mainWindow = this;
                windowFlowDirection();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, false);
            }
        }
        void windowFlowDirection()
        {
            #region translate
            if (AppSettings.lang.Equals("en"))
            {
                AppSettings.resourcemanager = new ResourceManager("Hesabate_POS.en_file", Assembly.GetExecutingAssembly());
                grid_mainWindow.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                AppSettings.resourcemanager = new ResourceManager("Hesabate_POS.ar_file", Assembly.GetExecutingAssembly());

                grid_mainWindow.FlowDirection = FlowDirection.RightToLeft;
            }
            #endregion
        }
        bool firstLoad = true;
        public async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_mainWindow, "mainWindow_loaded");
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += timer_Tick;
                timer.Start();


                if (AppSettings.lang.Equals("en"))
                {
                    AppSettings.resourcemanager = new ResourceManager("Hesabate_POS.en_file", Assembly.GetExecutingAssembly());
                    grid_mainWindow.FlowDirection = FlowDirection.LeftToRight;
                    //txt_lang.Text = "AR";

                }
                else
                {
                    AppSettings.resourcemanager = new ResourceManager("Hesabate_POS.ar_file", Assembly.GetExecutingAssembly());
                    grid_mainWindow.FlowDirection = FlowDirection.RightToLeft;
                    //txt_lang.Text = "EN";

                }
                translate();
                btn_menu_Click(btn_menu, null);

                setHeaderValues();
                //should be moved to login page
                //await FillCombo.RefreshCompanySettings();
                //try
                //{
                //    tb_version.Text = AppSettings.CurrentVersion;
                //}
                //catch (Exception ex)
                //{
                //    HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, false);
                //}



                #region Permision
                permission();
                #endregion

                //grid_main.Children.Clear();
                //grid_main.Children.Add(uc_receiptInvoice.Instance);
                //var receiptInvoice = new uc_receiptInvoice();
                //receiptInvoice.Tag = "0";
                //receiptInvoice.tb_Notes1.Text = "0";
                //receiptInvoiceList.Add(receiptInvoice);
                //grid_main.Children.Add(receiptInvoiceList.First());
                //var receiptInvoice1 = new uc_receiptInvoice();
                //receiptInvoice1.Tag = "1";
                //receiptInvoice1.tb_Notes1.Text = "1";
                //receiptInvoiceList.Add(receiptInvoice1);
                Btn_addReceiptInvoice_Click(btn_addReceiptInvoice, null);

                //SelectAllText
                EventManager.RegisterClassHandler(typeof(System.Windows.Controls.TextBox), System.Windows.Controls.TextBox.GotKeyboardFocusEvent, new RoutedEventHandler(SelectAllText));
                //txt_rightReserved.Text = DateTime.Now.Date.Year + " © All Right Reserved for ";
                firstLoad = false;
                HelpClass.EndAwait(grid_mainWindow, "mainWindow_loaded");
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_mainWindow, "mainWindow_loaded");
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (timer != null)
                timer.Stop();
        }

        void SelectAllText(object sender, RoutedEventArgs e)
        {
            try
            {
                var textBox = sender as System.Windows.Controls.TextBox;
                if (textBox != null)
                    if (!textBox.IsReadOnly)
                        textBox.SelectAll();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void setHeaderValues()
        {
            txt_posNameTitle.Text = Translate.getResource("746");


            txt_userName.Text = AppSettings.userName;
            txt_posName.Text = GeneralInfoService.cashBoxes.Where(x => x.BoxId == AppSettings.cashBoxId).Select(x => x.Name).FirstOrDefault();
            txt_licenseNumber.Text = GeneralInfoService.GeneralInfo.MainOp.MySno;
        }
        void permission()
        {
            /*
            bool loadWindow = false;
            //loadWindow = loadingDefaultPath(AppSettings.defaultPath);
            if (!HelpClass.isAdminPermision())
                foreach (Button button in FindControls.FindVisualChildren<Button>(this))
                {
                    if (button.Tag != null)
                        if (FillCombo.groupObject.HasPermission(button.Tag.ToString(), FillCombo.groupObjects))
                        {
                            button.Visibility = Visibility.Visible;
                            if (!loadWindow)
                            {
                                button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                                loadWindow = true;
                            }
                        }
                        else button.Visibility = Visibility.Collapsed;
                }
            else
            if (!loadWindow)
                Btn_home_Click(btn_home, null);
            */
        }
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {

                txtTime.Text = DateTime.Now.ToShortTimeString();
                txtDate.Text = DateTime.Now.ToShortDateString();


            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private async void BTN_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                HelpClass.StartAwait(grid_mainWindow);

                {
                    //await close();

                    //HelpClass.deleteDirectoryFiles(Global.TMPFolder);

                    Application.Current.Shutdown();
                }


                HelpClass.EndAwait(grid_mainWindow);
            }
            catch (Exception ex)
            {

                HelpClass.EndAwait(grid_mainWindow);
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void BTN_Minimize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        /*
        void colorTextRefreash(TextBlock txt)
        {
            txt_home.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_catalog.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_storage.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_purchases.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_sales.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_kitchen.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_delivery.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_accounts.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_reports.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_sectiondata.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            txt_settings.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));

            txt.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
        }
        void fn_ColorIconRefreash(Path p)
        {
            path_iconSettings.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconSectionData.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconReports.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconAccounts.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconSales.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconKitchen.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconDelivery.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconPurchases.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconStorage.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconCatalog.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));
            path_iconHome.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FEDFB7"));

            p.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FFFFFF"));
        }
        */
        public void translate()
        {
            #region top bar 
            txt_userNameTitle.Text = Translate.getResource("994");
            txt_versionNumberTitle.Text = Translate.getResource("1338");
            #endregion

            #region side buttons
            txt_pay.Text = Translate.getResource("2162");
            txt_using.Text = Translate.getResource("1613");
            txt_toKitchen.Text = Translate.getResource("1295");
            txt_pending.Text = Translate.getResource("2154");
            txt_pendingQuery.Text = Translate.getResource("1281");//استعلام فقط نحتاج استعلام معلق
            txt_openBox.Text = Translate.getResource("2152");
            txt_selectAgent.Text = Translate.getResource("526");
            txt_invoiceDelete.Text = Translate.getResource("2153");
            txt_points.Text = Translate.getResource("654");
            txt_shiftClose.Text = "shift Close";
            txt_invoiceBonus.Text = Translate.getResource("583");
            txt_invoiceCost.Text = Translate.getResource("21");
            txt_import.Text = Translate.getResource("763");
            txt_export.Text = Translate.getResource("2186");
            txt_administration.Text = Translate.getResource("2240");
            #endregion

        }
        private void Btn_home_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var control in FindControls.FindVisualChildren<Expander>(this))
                {

                    var expander = control as Expander;
                    if (expander.Tag != null)
                        expander.IsExpanded = false;
                }

                /*
                colorTextRefreash(txt_home);
                FN_pathVisible(path_openHome);
                fn_ColorIconRefreash(path_iconHome);
                grid_main.Children.Clear();
                grid_main.Children.Add(uc_home.Instance);
                if (isHome)
                {
                    uc_home.Instance.timerAnimation();
                    isHome = false;
                }
                Button button = sender as Button;
                MainWindow.mainWindow.initializationMainTrack(button.Tag.ToString());
                */
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }
        private void Btn_userImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                //wd_userInfo w = new wd_userInfo();
                //w.ShowDialog();
                Window.GetWindow(this).Opacity = 1;
            }
            catch (Exception ex)
            {
                Window.GetWindow(this).Opacity = 1;
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }



        /*
         private void Btn_vendorsData_Click(object sender, RoutedEventArgs e)
         {

             try
             {
                 grid_main.Children.Clear();
                 grid_main.Children.Add(uc_vendorsData.Instance);
                 Button button = sender as Button;
                 secondMenuTitleActivate(button.Tag.ToString());
             }
             catch (Exception ex)
             {
                 HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
             }


         }
         */

        private void Btn_lang_Click(object sender, RoutedEventArgs e)
        {
            if (AppSettings.lang.Equals("en"))
                AppSettings.lang = "ar";
            else
                AppSettings.lang = "en";


            //update languge in main window
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;

            if (parentWindow != null)
            {
                //access property of the MainWindow class that exposes the access rights...
                if (AppSettings.lang.Equals("en"))
                {
                    AppSettings.resourcemanager = new ResourceManager("Hesabate_POS.en_file", Assembly.GetExecutingAssembly());
                    parentWindow.grid_mainWindow.FlowDirection = FlowDirection.LeftToRight;
                    //txt_lang.Text = "AR";

                }
                else
                {
                    AppSettings.resourcemanager = new ResourceManager("Hesabate_POS.ar_file", Assembly.GetExecutingAssembly());
                    parentWindow.grid_mainWindow.FlowDirection = FlowDirection.RightToLeft;
                    //txt_lang.Text = "EN";
                }
                parentWindow.translate();

            }
        }

        private void Btn_setting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_lockApp_Click(object sender, RoutedEventArgs e)
        {

        }

        #region grid0_0
        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!AppSettings.menuState)
                {
                    Storyboard sb = this.FindResource("Storyboard1") as Storyboard;
                    sb.Begin();
                    AppSettings.menuState = true;
                }
                else
                {
                    Storyboard sb = this.FindResource("Storyboard2") as Storyboard;
                    sb.Begin();
                    AppSettings.menuState = false;
                }
                if (!firstLoad)
                {
                    Properties.Settings.Default.menuState = AppSettings.menuState;
                    Properties.Settings.Default.Save();
                }


            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void btn_pay_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_using_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_toKitchen_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_pendingQuery_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_pending_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_openBox_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_selectAgent_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_invoiceDelete_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_points_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_shiftClose_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_invoiceBonus_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_invoiceCost_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_import_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_export_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_administration_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region receiptInvoice
        List<uc_receiptInvoice> receiptInvoiceList = new List<uc_receiptInvoice>();
        int receiptInvoiceListCounter = 0;
        private void receiptInvoiceSwitch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;

                var receiptInvoice = receiptInvoiceList.Where(
                        x => button.Tag.ToString().Contains(x.Tag.ToString())
                        ).FirstOrDefault();
                if (receiptInvoice != null)
                {
                    grid_main.Children.Clear();
                    grid_main.Children.Add(receiptInvoice);
                    string tagNumber = button.Tag.ToString().Split('_')[1];
                    receiptInvoiceButtonActive(tagNumber);
                }

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void receiptInvoiceButtonActive(string tag)
        {

            foreach (Button button in FindControls.FindVisualChildren<Button>(this)
                .Where(x => x.Tag != null &&  x.Tag.ToString().Contains("receiptInvoiceMainButton")))
            {
                if (button.Tag != null)
                {
                    if (button.Tag.ToString().Contains(tag))
                        button.Background = Application.Current.Resources["White"] as SolidColorBrush;
                    else
                        button.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;

                    Path path = FindControls.FindVisualChildren<Path>(button).FirstOrDefault();
                    if (path != null  && path.Tag != null)
                    {
                        if (path.Tag.ToString().Contains(tag))
                            path.Fill = Application.Current.Resources["MainColor"] as SolidColorBrush;
                        else
                            path.Fill = Application.Current.Resources["White"] as SolidColorBrush;
                    }
                    //else
                    //{
                    //    foreach (Path path1 in FindControls.FindVisualChildren<Path>(this)
                    //        .Where(x => x.Tag.ToString().Contains("receiptInvoicePath")))
                    //    {
                    //        if (path1.Tag != null)
                    //        {
                    //            if (tag == path1.Tag.ToString())
                    //                path1.Fill = Application.Current.Resources["MainColor"] as SolidColorBrush;
                    //            else
                    //                path1.Fill = Application.Current.Resources["White"] as SolidColorBrush;
                    //        }
                    //    }
                    //}

                    TextBlock textBlock = FindControls.FindVisualChildren<TextBlock>(button).FirstOrDefault();
                    if (textBlock != null  && textBlock.Tag != null)
                    {
                        if (textBlock.Tag.ToString().Contains(tag))
                            textBlock.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                        else
                            textBlock.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                    }
                }
            }
            /*
            foreach (Path path in FindControls.FindVisualChildren<Path>(this)
                .Where(x => Tag.ToString().Contains("receiptInvoicePath")))
            {
                if (path.Tag != null)
                {
                    if (tag == path.Tag.ToString())
                        path.Fill = Application.Current.Resources["MainColor"] as SolidColorBrush;
                    else
                        path.Fill = Application.Current.Resources["White"] as SolidColorBrush;
                }
            }
            foreach (TextBlock textBlock in FindControls.FindVisualChildren<TextBlock>(this)
                .Where(x => Tag.ToString().Contains("receiptInvoiceText")))
            {
                if (textBlock.Tag != null)
                {
                    if (tag == textBlock.Tag.ToString())
                        textBlock.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                    else
                        textBlock.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                }
            }
            */
        }
        private async void Btn_addReceiptInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                receiptInvoiceListCounter++;
                var receiptInvoice = new uc_receiptInvoice();
                receiptInvoice.Tag = receiptInvoiceListCounter.ToString();
                receiptInvoiceList.Add(receiptInvoice);
                //grid_main.Children.Clear();
                //grid_main.Children.Add(receiptInvoice);
                var newButton = buildReceiptInvoiceButtonTab(receiptInvoice.Tag.ToString());
                sp_receiptInvoice.Children.Add(newButton);
                if (firstLoad)
                    await Task.Delay(0050);
                receiptInvoiceCheckCount();
                receiptInvoiceSwitch_Click(newButton,null);
                //receiptInvoiceButtonActive(receiptInvoice.Tag.ToString());



            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        Button buildReceiptInvoiceButtonTab(string tag)
        {
            Button mainButton = new Button();
            mainButton.Tag = "receiptInvoiceMainButton_" + tag;
            mainButton.Padding = new Thickness(0);
            mainButton.Margin = new Thickness(5, 0, 5, 0);
            mainButton.BorderThickness = new Thickness(2);
            mainButton.Background = Application.Current.Resources["White"] as SolidColorBrush;
            mainButton.BorderBrush = Application.Current.Resources["White"] as SolidColorBrush;
            MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(mainButton, (new CornerRadius(7)));
            mainButton.Click += receiptInvoiceSwitch_Click;
            #region stackPanel
            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;
            #region receiptInvoicePath
            Path path = new Path();
            path.Tag = "receiptInvoicePath_" + tag;
            path.Stretch = Stretch.Fill;
            path.FlowDirection = FlowDirection.LeftToRight;
            path.Width =
            path.Height = 20;
            path.Data = App.Current.Resources["invoice"] as Geometry;
            path.Fill = Application.Current.Resources["MainColor"] as SolidColorBrush;
            path.Margin = new Thickness(5);
            stackPanel.Children.Add(path);
            #endregion
            #region receiptInvoiceText
            TextBlock textBlock = new TextBlock();
            textBlock.Tag = "receiptInvoiceText_" + tag;
            textBlock.Text = "#" + tag;
            textBlock.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
            textBlock.Margin = new Thickness(0,5,0,5);

            stackPanel.Children.Add(textBlock);
            #endregion
            #region receiptInvoiceCloseButton
            Button buttonClose = new Button();
            buttonClose.Tag = "receiptInvoiceCloseButton" + tag;
            buttonClose.Margin = new Thickness(2.5);
            buttonClose.Height =
            buttonClose.Width = 25;
            buttonClose.Padding = new Thickness(0);
            buttonClose.Background = Application.Current.Resources["Red"] as SolidColorBrush;
            buttonClose.BorderBrush = null;
            buttonClose.BorderThickness = new Thickness(0);
            MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonClose, (new CornerRadius(25)));
            #region materialDesign
            var ClosePackIcon = new PackIcon();
            ClosePackIcon.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
            ClosePackIcon.Height =
            ClosePackIcon.Width = 25;
            ClosePackIcon.Kind = PackIconKind.Close;
            buttonClose.Content = ClosePackIcon;
            #endregion
            buttonClose.Click += receiptInvoiceDelete_Click;
            stackPanel.Children.Add(buttonClose);
            #endregion
            mainButton.Content = stackPanel;
            #endregion

            return mainButton;
        }
        private void receiptInvoiceDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                var receiptInvoice = receiptInvoiceList.Where(
                        x => button.Tag.ToString().Contains(x.Tag.ToString())
                        ).FirstOrDefault();
                if (receiptInvoice != null)
                {
                    receiptInvoiceList.Remove(receiptInvoice);
                    if (grid_main.Children.Contains(receiptInvoice))
                    {
                        grid_main.Children.Clear();
                        var receiptInvoiceSwitch =  receiptInvoiceList.FirstOrDefault();
                        var buttonSwitch = FindControls.FindVisualChildren<Button>(this)
                        .Where(x => x.Tag != null && x.Tag.ToString()
                        .Contains("receiptInvoiceMainButton_" + receiptInvoiceSwitch.Tag)).FirstOrDefault();
                        if(buttonSwitch!= null)
                            receiptInvoiceSwitch_Click(buttonSwitch,null);
                    }
                    Button father = (button.Parent as StackPanel).Parent as Button;
                    sp_receiptInvoice.Children.Remove(father);
                    
                    receiptInvoiceCheckCount();
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void receiptInvoiceCheckCount()
        {
            if(receiptInvoiceList.Count == 1)
            {
                foreach (Button button in FindControls.FindVisualChildren<Button>(this)
               .Where(x => x.Tag != null && x.Tag.ToString().Contains("receiptInvoiceMainButton")))
                {
                    button.Visibility = Visibility.Collapsed;
                }
            }
            else if(receiptInvoiceList.Count == 2)
            {
                foreach (Button button in FindControls.FindVisualChildren<Button>(this)
                .Where(x => x.Tag != null && x.Tag.ToString().Contains("receiptInvoiceMainButton")))
                {
                    button.Visibility = Visibility.Visible;
                }
            }
        }


        #endregion


    }
}
