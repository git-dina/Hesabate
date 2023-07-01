using Hesabate_POS.Classes;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Hesabate_POS.View.receipts
{
    /// <summary>
    /// Interaction logic for uc_receiptInvoice.xaml
    /// </summary>
    public partial class uc_receiptInvoice : UserControl
    {
        public uc_receiptInvoice()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private static uc_receiptInvoice _instance;
        public static uc_receiptInvoice Instance
        {
            get
            {
                if (_instance is null)
                    _instance = new uc_receiptInvoice();
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public static List<string> requiredControlList;

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Instance = null;
            GC.Collect();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {//load
            try
            {
                HelpClass.StartAwait(grid_main);

                //if (AppSettings.lang.Equals("en"))
                //{
                //    grid_main.FlowDirection = FlowDirection.LeftToRight;
                //}
                //else
                //{
                //    grid_main.FlowDirection = FlowDirection.RightToLeft;
                //}
                translate();
                requiredControlList = new List<string> { "" };


                buildItemsCard(getItems());

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

        private void btn_home_Click(object sender, RoutedEventArgs e)
        {

        }

        bool menuState = false;
        private void btn_menu_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!menuState)
                {
                    Storyboard sb = this.FindResource("Storyboard1") as Storyboard;
                    sb.Begin();
                    menuState = true;
                }
                else
                {
                    Storyboard sb = this.FindResource("Storyboard2") as Storyboard;
                    sb.Begin();
                    menuState = false;
                }


                //#region tooltipVisibility
                //FN_tooltipVisibility(BTN_menu);
                //FN_tooltipVisibility(BTN_home);
                //FN_tooltipVisibility(btn_catalog);
                //FN_tooltipVisibility(btn_storage);
                //FN_tooltipVisibility(btn_purchase);
                //FN_tooltipVisibility(btn_sales);
                //FN_tooltipVisibility(btn_delivery);
                //FN_tooltipVisibility(btn_reports);
                //FN_tooltipVisibility(btn_accounts);
                //FN_tooltipVisibility(btn_sectionData);
                //FN_tooltipVisibility(btn_settings);
                //#endregion


            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        #region validate - clearValidate - textChange - lostFocus - . . . . 
        void Clear()
        {

            //this.DataContext = new Receipt();


            // last 
            HelpClass.clearValidate(requiredControlList, this);
        }
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
        private void Btn_save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_stop_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_item_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if(button.Tag != null)
                    MessageBox.Show($"I'm button number: {button.Tag}" );
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }


        class Item
        {
           public int id;
           public string name;
           public decimal price;
           public string url;
        }
        List<Item> getItems()
        {
            Random rnd = new Random();
            List<Item> items = new List<Item>();
            for (int i = 0; i < 15; i++)
            {
                items.Add(new Item()
                {
                    id = i+1,
                    name = "Lorem ipsum dolor sit Lorem ipsum dolor sit...",
                    price= (decimal) rnd.Next(100, 9999)/100 ,
                    url = $"pic/foods/{i + 1}.png"
            });
            }
            return items;
        }
        void buildItemsCard(List<Item> items)
        {
            wp_itemsCard.Children.Clear();
            int cardWidth = 175;
            int cardHeight = 75;
            int cornerRadius = 7;
            foreach (var item in items)
            {

                #region borderMain
                Border borderMain = new Border();
                borderMain.Width = cardWidth;
                borderMain.Height = cardHeight;
                borderMain.Background = Application.Current.Resources["White"] as SolidColorBrush;
                borderMain.BorderBrush = Application.Current.Resources["Grey"] as SolidColorBrush;
                borderMain.BorderThickness = new Thickness(1);
                borderMain.Margin = new Thickness(5);
                borderMain.Padding = new Thickness(0);
                borderMain.CornerRadius = new CornerRadius(cornerRadius);

                #region buttonMain
                Button buttonMain = new Button();
                buttonMain.Tag = item.id;
                buttonMain.Width = cardWidth;
                buttonMain.Height = cardHeight;
                buttonMain.BorderBrush = null;
                buttonMain.Background = null;
                buttonMain.BorderThickness = new Thickness(0);
                buttonMain.Padding = new Thickness(0);
                buttonMain.Margin = new Thickness(0);
                buttonMain.Click += btn_item_Click;

                #region gridMain
                Grid gridMain = new Grid();
                #region gridSettings
                /////////////////////////////////////////////////////
                int rowCount = 2;
                RowDefinition[] rd = new RowDefinition[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    rd[i] = new RowDefinition();
                }
                rd[0].Height = new GridLength(1, GridUnitType.Star);
                rd[1].Height = new GridLength(1, GridUnitType.Auto);
                for (int i = 0; i < rowCount; i++)
                {
                    gridMain.RowDefinitions.Add(rd[i]);
                }
                /////////////////////////////////////////////////////
                int colCount = 2;
                ColumnDefinition[] cd = new ColumnDefinition[colCount];
                for (int i = 0; i < colCount; i++)
                {
                    cd[i] = new ColumnDefinition();
                }
                cd[0].Width = new GridLength(75, GridUnitType.Pixel);
                cd[1].Width = new GridLength(100, GridUnitType.Pixel);
                for (int i = 0; i < colCount; i++)
                {
                    gridMain.ColumnDefinitions.Add(cd[i]);
                }
                #endregion
                #region buttonImage
                Button buttonImage = new Button();
                buttonImage.BorderThickness = new Thickness(0);
                buttonImage.Padding = new Thickness(0);
                buttonImage.FlowDirection = FlowDirection.LeftToRight;
                if (AppSettings.lang == "en")
                    MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonImage, (new CornerRadius(9, 0, 0, 9)));
                else
                    MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonImage, (new CornerRadius(0, 9, 9, 0)));

                buttonImage.Height =
                buttonImage.Width = cardHeight;
                //buttonImage.Background = new ImageBrush() {
                //    Stretch = Stretch.UniformToFill,
                //    ImageSource = new BitmapImage(new Uri(item.url, UriKind.Relative))
                //};
                setImg(buttonImage, item.url);


            Grid.SetRowSpan(buttonImage, 2);
                gridMain.Children.Add(buttonImage);
                #endregion
                #region textName
                TextBlock textName = new TextBlock();
                textName.Text = item.name;
                textName.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                textName.FontSize = 12;
                textName.FontWeight = FontWeights.Regular;
                textName.TextWrapping = TextWrapping.WrapWithOverflow;
                textName.TextAlignment = TextAlignment.Center;
                textName.HorizontalAlignment = HorizontalAlignment.Center;
                textName.VerticalAlignment = VerticalAlignment.Center;
                textName.Margin = new Thickness(2.5);

                Grid.SetColumn(textName, 1);
                gridMain.Children.Add(textName);
                #endregion
                #region borderPrice
               
                Border borderPrice = new Border();
                borderPrice.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                borderPrice.Padding = new Thickness(0);
                borderPrice.Margin = new Thickness(0);
                    borderPrice.CornerRadius = new CornerRadius(0, 0, 12, 0);

                #region textPrice
                TextBlock textPrice = new TextBlock();
                textPrice.Text = item.price.ToString();
                textPrice.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                textPrice.Margin = new Thickness(5,2.5, 5, 2.5);
                textPrice.HorizontalAlignment = HorizontalAlignment.Center;
                textPrice.VerticalAlignment = VerticalAlignment.Center;
                textPrice.TextWrapping = TextWrapping.WrapWithOverflow;
                textPrice.TextAlignment = TextAlignment.Center;
                borderPrice.Child= textPrice;
                #endregion
                Grid.SetRow(borderPrice, 1);
                Grid.SetColumn(borderPrice,1);
                gridMain.Children.Add(borderPrice);
                #endregion

                buttonMain.Content = gridMain;
                #endregion
                borderMain.Child = buttonMain;
                #endregion
                wp_itemsCard.Children.Add(borderMain);
                #endregion
            }

        }
        public static void setImg(Button img, string uri)
        {
            ImageBrush imageBrush = new ImageBrush();

            Uri resourceUri = new Uri(uri, UriKind.Relative);
            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            imageBrush.ImageSource = temp;
            imageBrush.Stretch = Stretch.UniformToFill;
            img.Background = imageBrush;
        }
    }
}
