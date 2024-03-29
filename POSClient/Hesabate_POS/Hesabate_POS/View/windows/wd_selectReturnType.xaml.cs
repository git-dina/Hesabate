﻿using Hesabate_POS.Classes;
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
    /// Interaction logic for wd_selectReturnType.xaml
    /// </summary>
    public partial class wd_selectReturnType : Window
    {

        public wd_selectReturnType()
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

        public string returnType { get; set; }
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
                //FillCombo.FillReturnType(cmb_returnType);


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

            txt_title.Text = "نوع الإرجاع";
            txt_full.Text = "ارجاع فاتورة";
            txt_manual.Text = "يدوي";
            txt_replace.Text = "استبدال";
            //btn_save.Content = Translate.getResource("27");
        }

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    //Btn_save_Click(btn_save, null);
                }
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name); }
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
        //private void Btn_save_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        returnType = cmb_returnType.SelectedValue.ToString();
        //        isOk = true;

        //        this.Close();
        //    }
        //    catch
        //    {

        //    }
        //}
        
        //private void btn_normal_Click(object sender, RoutedEventArgs e)
        //{
        //    returnType = "0";
        //    isOk = true;
        //    this.Close();
        //}

        private void btn_full_Click(object sender, RoutedEventArgs e)
        {
            returnType = "1";
            isOk = true;
            this.Close();
        }

        private void btn_replace_Click(object sender, RoutedEventArgs e)
        {
            returnType = "2";
            isOk = true;
            this.Close();
        }

        private void btn_manual_Click(object sender, RoutedEventArgs e)
        {
            returnType = "3";
            isOk = true;
            this.Close();
        }
    }
}
