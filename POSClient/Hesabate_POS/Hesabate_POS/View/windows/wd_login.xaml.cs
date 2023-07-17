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

namespace Hesabate_POS.View.windows
{
    /// <summary>
    /// Interaction logic for wd_login.xaml
    /// </summary>
    public partial class wd_login : Window
    {

        public wd_login()
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

        public bool isOk { get; set; }
        public static List<string> requiredControlList;
        public List<LanguageModel> languages = new List<LanguageModel>();
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {//load

            try
            {


                HelpClass.StartAwait(grid_main);
                //requiredControlList = new List<string> { "userName", "password", "idCard", "language", };
                requiredControlList = new List<string> { "", };

                #region translate
                /*
                if (AppSettings.lang.Equals("en"))
                {
                    grid_main.FlowDirection = FlowDirection.LeftToRight;
                }
                else
                {
                    grid_main.FlowDirection = FlowDirection.RightToLeft;
                }
                */
                translate();
                #endregion

                await FillCombo.fillLanguages(cb_language);
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
            
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    Btn_login_Click(btn_login, null);
                }
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void btn_minimize_Click(object sender, RoutedEventArgs e)
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
        private void pb_password_PasswordChanged(object sender, RoutedEventArgs e)
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

        //bool logInProcessing = false;
        AuthService _authService = new AuthService();
        ItemService _itemService = new ItemService();
        private async void Btn_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btn_login.IsEnabled = false;
                txt_message.Text = "";
                string res = "";
                if (tb_userName.Text != "" && pb_password.Password != "")
                {
                    var res1 = await _authService.Login(tb_userName.Text, pb_password.Password);
                    res = Convert.ToString(res1);
                }
                else if (tb_idCard.Text != "")
                {
                    var res1 = await _authService.Login(tb_idCard.Text);
                    res = Convert.ToString(res1);
                }

                // show message
                if (res != "")
                    txt_message.Text = "";
                    #region  selectBox
                HelpClass.StartAwait(grid_form);               

                if (res == "")
                {
                    pb_main.Visibility = Visibility.Visible;
                    pb_main.Value = 0;

                    int taskCount = 3;
                    await GeneralInfoService.GetMainInfo();//general info, buttons-cat, tables ,...
                    pb_main.Value += 100 / taskCount;
                    await GeneralInfoService.GetLanguagesTerms((int)cb_language.SelectedValue);// get selected language terms
                    pb_main.Value += 100 / taskCount;
                    await _itemService.GetItems();
                    pb_main.Value = 100;
                }
                if (res == "" && AppSettings.cashBoxId == "0")
                {
                    Window.GetWindow(this).Opacity = 0.0;
                    wd_selectBox w = new wd_selectBox();
                    w.ShowDialog();
                    if (w.isOk)
                    {
                        //open main window and close this window
                        MainWindow main = new MainWindow();
                        main.Show();
                        this.Close();
                    }

                    pb_main.Visibility = Visibility.Collapsed;
                    pb_main.Value = 0;
                    Window.GetWindow(this).Opacity = 1;
                }
                else if(res == "")
                {
                    //open main window and close this window
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                HelpClass.EndAwait(grid_form);
                #endregion


                btn_login.IsEnabled = true;

            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_form);
                btn_login.IsEnabled = true;
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void cb_language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_language.SelectedValue != null)
            {
                var lang = GeneralInfoService.Languages.Where(x => x.id == (int)cb_language.SelectedValue).FirstOrDefault();
                AppSettings.dir = lang.dir;
            }
        }
    }
}
