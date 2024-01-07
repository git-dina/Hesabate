using Hesabate_POS.Classes;
using Hesabate_POS.Classes.ApiClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Hesabate_POS.View.windows
{
    /// <summary>
    /// Interaction logic for wd_searchItem.xaml
    /// </summary>
    public partial class wd_searchItem : Window
    {

        public wd_searchItem()
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

        public ItemModel selectedItem { get; set; }
        
        public bool isOk { get; set; }
        public static List<string> requiredControlList;

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load

            try
            {


                HelpClass.StartAwait(grid_main);
                requiredControlList = new List<string> { "", };

                #region translate

                if (AppSettings.lang.Equals("en"))
                {
                    //AppSettings.resourcemanager = new ResourceManager("POSCA.en_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    //AppSettings.resourcemanager = new ResourceManager("POSCA.ar_file", Assembly.GetExecutingAssembly());
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                }

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

            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, Translate.getResource("2143"));

            col_index.Header = Translate.getResource("469");
            col_name.Header = Translate.getResource("652");
            col_unit.Header = Translate.getResource("427");
            col_discount.Header = Translate.getResource("571");
            col_bonus.Header = Translate.getResource("583");
            col_price.Header = Translate.getResource("570");
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    //Btn_save_Click(btn_save, null);
                }
                if (!tb_search.IsFocused)
                {
                    switch (e.Key)
                    {
                        case Key.NumPad0:
                            tb_search.Text += "0";
                            break;
                        case Key.NumPad1:
                            tb_search.Text += "1";
                            break;
                        case Key.NumPad2:
                            tb_search.Text += "2";
                            break;
                        case Key.NumPad3:
                            tb_search.Text += "3";
                            break;
                        case Key.NumPad4:
                            tb_search.Text += "4";
                            break;
                        case Key.NumPad5:
                            tb_search.Text += "5";
                            break;
                        case Key.NumPad6:
                            tb_search.Text += "6";
                            break;
                        case Key.NumPad7:
                            tb_search.Text += "7";
                            break;
                        case Key.NumPad8:
                            tb_search.Text += "8";
                            break;
                        case Key.NumPad9:
                            tb_search.Text += "9";
                            break;
                        case Key.Decimal:
                            tb_search.Text += ".";
                            break;
                        case Key.Back:
                            btn_del_Click(null, null);
                            break;

                            //default:
                    }
                }
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name); }
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

        #endregion

       

        private void tb_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void Btn_search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void Search()
        {
            var items = GeneralInfoService.items.Where(x =>
                    x.id.Length >= 4 ? x.id.Substring(x.id.Length - 4, 4).Contains(tb_search.Text) : x.id.Contains(tb_search.Text)
                  ).ToList();

            dg_items.ItemsSource = items;
            dg_items.Items.Refresh();
        }
        private void dg_items_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedItem = (ItemModel)dg_items.SelectedItem;
                isOk = true;
                this.Close();
            }
            catch
            {

            }
        }
        private void btn_num_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tb_search.Text += (sender as Button).Content;
                Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tb_search.Text))
                {
                    tb_search.Text = tb_search.Text.Remove(tb_search.Text.Length - 1);
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void tb_search_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

       
    }
}
