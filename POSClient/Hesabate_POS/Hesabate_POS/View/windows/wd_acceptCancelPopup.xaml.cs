using Hesabate_POS;
using Hesabate_POS.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hesabate_POS.View.windows
{
    /// <summary>
    /// Interaction logic for wd_acceptCancelPopup.xaml
    /// </summary>
    public partial class wd_acceptCancelPopup : Window
    {
         
        //public bool isOk { get; set; }
        public bool isOk;
        public wd_acceptCancelPopup()
        {
            try
            {
                InitializeComponent();
                this.DataContext = this;
            }
            catch(Exception ex)
            { HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               
                
                    HelpClass.StartAwait(grid_main);

                #region translate
            //    if (AppSettings.lang.Equals("en"))
            //{
            //        grid_main.FlowDirection = FlowDirection.LeftToRight;
            //}
            //else
            //{
            //        grid_main.FlowDirection = FlowDirection.RightToLeft;
            //}
            translate();
                #endregion
                
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
            btn_ok.Content = Translate.getResource("27");
            btn_cancel.Content = AppSettings.resourcemanager.GetString("trCancel");
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isOk = false;
                this.Close();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void Btn_ok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isOk = true;
                this.Close();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        #region contentText
        public static readonly DependencyProperty contentTextDependencyProperty = DependencyProperty.Register("contentText",
            typeof(string),
            typeof(wd_acceptCancelPopup),
            new PropertyMetadata("DEFAULT"));
        public string contentText
        {
            set
            { SetValue(contentTextDependencyProperty, value); }
            get
            { return (string)GetValue(contentTextDependencyProperty); }
        }
        #endregion

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }


        private   void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Btn_ok_Click(null, null);
            }
        }

    }
}
