using Hesabate_POS.Classes;
using Hesabate_POS.Classes.ApiClasses;
using MaterialDesignThemes.Wpf;
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
        ItemService _itemService = new ItemService();
        List<ItemModel> items = new List<ItemModel>();

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

                await GeneralInfoService.GetMainInfo();// move to login

                

                buildInvoiceDetails(getInvoiceDetails());

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



        #region itemsCard
        /*
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
        */

        void buildItemsCard(List<ItemModel> items)
        {
            wp_itemsCard.Children.Clear();
            int cardWidth = 175;
            int cardHeight = 75;
            int cornerRadius = 7;
            bool isLast =false;
            foreach (var item in items)
            {
                if (ItemService.itemIsLast(item))
                    isLast = true;
                else
                    isLast = false;

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
                buttonMain.DataContext = item;
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
          
                setImg(buttonImage, item.img);

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
                if (isLast)
                {
                    Border borderPrice = new Border();
                    borderPrice.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                    borderPrice.Padding = new Thickness(0);
                    borderPrice.Margin = new Thickness(0);
                    borderPrice.CornerRadius = new CornerRadius(0, 0, 12, 0);

                    #region textPrice
                    TextBlock textPrice = new TextBlock();
                    textPrice.Text = item.price.ToString();
                    textPrice.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                    textPrice.Margin = new Thickness(5, 2.5, 5, 2.5);
                    textPrice.HorizontalAlignment = HorizontalAlignment.Center;
                    textPrice.VerticalAlignment = VerticalAlignment.Center;
                    textPrice.TextWrapping = TextWrapping.WrapWithOverflow;
                    textPrice.TextAlignment = TextAlignment.Center;
                    borderPrice.Child = textPrice;
                    #endregion
                    Grid.SetRow(borderPrice, 1);
                    Grid.SetColumn(borderPrice, 1);
                    gridMain.Children.Add(borderPrice);
                }
                #endregion

                buttonMain.Content = gridMain;
                #endregion
                borderMain.Child = buttonMain;
                #endregion
                wp_itemsCard.Children.Add(borderMain);
                #endregion
            }

        }
        private void btn_item_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*
                Button button = sender as Button;
                if (button.Tag != null)
                {
                    int itemId = int.Parse(button.Tag.ToString());
                    var item = _itemService.getItem(itemId, "cat");
                    // is not Last
                    if (item.items != null || item.level2 != null)
                    {
                        // categoryPath
                        categoryPath.Add(item);

                        // itemsCard
                        items = _itemService.getCatItems(item.id);
                        buildItemsCard(items);

                    }
                    else
                    {
                        MessageBox.Show("Add me to invoice");
                    }


                }
                */
                Button button = sender as Button;
                if (button.DataContext != null)
                {
                    //int itemId = int.Parse(button.Tag.ToString());
                    var item = button.DataContext as ItemModel;
                    // is not Last
                    //if ( item.level2 != null || (item.items != null && item.items.Count != 0))
                    if (!ItemService.itemIsLast(item))
                    {
                        // categoryPath
                        categoryPath.Add(item);
                        buildCategoryPath(categoryPath);

                        // itemsCard
                        items = _itemService.getCatItems(item.id);
                        buildItemsCard(items);

                    }
                    else
                    {
                        MessageBox.Show("Add me to invoice");
                    }


                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        public static void setImg(Button img, string uri)
        {
            
            ImageBrush imageBrush = new ImageBrush();

            Uri resourceUri = new Uri("pic/no-image-icon-125x125.png", UriKind.Relative); ;
            //if(uri != null)
            //    resourceUri = 

            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);
            imageBrush.ImageSource = temp;
            imageBrush.Stretch = Stretch.UniformToFill;
            img.Background = imageBrush;
        }


        #endregion
        #region category
        private void btn_allItems_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                // categoryPath
                categoryPath.Clear();
                buildCategoryPath(categoryPath);

                // itemsCard
                items = _itemService.getCatItems(0);
                buildItemsCard(items);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }


        List<ItemModel> categoryPath = new List<ItemModel>();
        void buildCategoryPath(List<ItemModel> categories)
        {
            sp_categoryPath.Children.Clear();
            foreach (var item in categories)
            {
               
                #region borderMain
                Border borderMain = new Border();
               
                borderMain.Margin = new Thickness(0);
                borderMain.Padding = new Thickness(0);
                borderMain.MinWidth = 50;
                borderMain.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                #region buttonMain
                Button buttonMain = new Button();
                buttonMain.Tag = item.id;
                buttonMain.DataContext = item;
                buttonMain.Padding = new Thickness(0);
                buttonMain.BorderBrush = null;
                buttonMain.Background = null;
                buttonMain.Height = 50;

                buttonMain.Click += btn_categoryPath_Click;
                #region textName
                TextBlock textName = new TextBlock();
                textName.Text = ">" + item.name;
                textName.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                textName.Margin = new Thickness(5);
                buttonMain.Content=textName;
                #endregion
                borderMain.Child = buttonMain;
                #endregion
                sp_categoryPath.Children.Add(borderMain);
                #endregion
            }

        }
        private void btn_categoryPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button.DataContext != null)
                {
                    var item = button.DataContext as ItemModel;
                        // categoryPath
                        categoryPath.Add(item);
                        buildCategoryPath(categoryPath);

                        // itemsCard
                        items = _itemService.getCatItems(item.id);
                        buildItemsCard(items);

                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion


        #region invoiceDetails
        class InvoiceDetails
        {
            public int id;
            public string name;
            public int count;
            public decimal price;
            public decimal total;
        }
        List<InvoiceDetails> getInvoiceDetails()
        {
            Random rnd = new Random();
            List<InvoiceDetails> invoiceDetailsList = new List<InvoiceDetails>();
            for (int i = 0; i < 5; i++)
            {
                invoiceDetailsList.Add(new InvoiceDetails()
                {
                    id = i + 1,
                    name = "Lorem ipsum dolor sit Lorem ipsum dolor sit...",
                    count = (int)rnd.Next(1, 99) ,
                    price = (decimal)rnd.Next(100, 9999) / 100,
                    total = (decimal)rnd.Next(100, 9999) / 100,
                });
            }
            return invoiceDetailsList;
        }
        void buildInvoiceDetails(List<InvoiceDetails> invoiceDetailsList)
        {
            sp_invoiceDetails.Children.Clear();
            //int cardWidth = 175;
            //int cardHeight = 75;
            int cornerRadius = 7;
            foreach (var item in invoiceDetailsList)
            {
                


                #region borderMain
                Border borderMain = new Border();
                borderMain.BorderThickness = new Thickness(1);
                borderMain.CornerRadius = new CornerRadius(cornerRadius);
                borderMain.BorderBrush = Application.Current.Resources["Grey"] as SolidColorBrush;
                borderMain.Background = null;
                borderMain.Margin = new Thickness(5);
                borderMain.Padding = new Thickness(0);

                #region gridMain
                Grid gridMain = new Grid();
                #region gridSettings
                /////////////////////////////////////////////////////
                int rowCount = 3;
                RowDefinition[] rd = new RowDefinition[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    rd[i] = new RowDefinition();
                    rd[i].Height = new GridLength(1, GridUnitType.Auto);
                    gridMain.RowDefinitions.Add(rd[i]);
                }
                #endregion

                #region gridRow1
                Grid gridRow1 = new Grid();
                gridRow1.Margin = new Thickness(5);
                #region gridSettings
                /////////////////////////////////////////////////////
                int colCountRow1 = 6;
                ColumnDefinition[] cdRow1 = new ColumnDefinition[colCountRow1];
                for (int i = 0; i < colCountRow1; i++)
                {
                    cdRow1[i] = new ColumnDefinition();
                }
                cdRow1[0].Width = new GridLength(1, GridUnitType.Star);
                cdRow1[1].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[2].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[3].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[4].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[5].Width = new GridLength(1, GridUnitType.Auto);
                for (int i = 0; i < colCountRow1; i++)
                {
                    gridRow1.ColumnDefinitions.Add(cdRow1[i]);
                }
                #endregion
                #region itemName
                TextBlock itemName = new TextBlock();
                itemName.Text = item.name;
                itemName.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                itemName.HorizontalAlignment = HorizontalAlignment.Left;
                itemName.VerticalAlignment = VerticalAlignment.Center;
                itemName.Margin = new Thickness(5);
                itemName.TextWrapping = TextWrapping.WrapWithOverflow;
                itemName.TextAlignment = TextAlignment.Center;


                Grid.SetColumn(itemName, 0);
                gridRow1.Children.Add(itemName);
                #endregion
                #region itemCount
                TextBlock itemCount = new TextBlock();
                itemCount.Text = item.count.ToString();
                itemCount.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemCount.HorizontalAlignment = HorizontalAlignment.Center;
                itemCount.VerticalAlignment = VerticalAlignment.Center;
                itemCount.Margin = new Thickness(5);
                itemCount.TextWrapping = TextWrapping.WrapWithOverflow;
                itemCount.TextAlignment = TextAlignment.Center;


                Grid.SetColumn(itemCount, 2);
                gridRow1.Children.Add(itemCount);
                #endregion
                #region itemPrice
                TextBlock itemPrice = new TextBlock();
                itemPrice.Text = item.price.ToString();
                itemPrice.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemPrice.HorizontalAlignment = HorizontalAlignment.Center;
                itemPrice.VerticalAlignment = VerticalAlignment.Center;
                itemPrice.Margin = new Thickness(5);
                itemPrice.TextWrapping = TextWrapping.WrapWithOverflow;
                itemPrice.TextAlignment = TextAlignment.Center;


                Grid.SetColumn(itemPrice, 4);
                gridRow1.Children.Add(itemPrice);
                #endregion
                #region   minus
                Button buttonMinus = new Button();
                buttonMinus.Tag = "minus-" + item.id;
                buttonMinus.Margin = new Thickness(2.5);
                buttonMinus.Height =
                buttonMinus.Width = 25;
                buttonMinus.Padding = new Thickness(0);
                buttonMinus.Background = Application.Current.Resources["veryLightGrey"] as SolidColorBrush;
                buttonMinus.BorderThickness = new Thickness(0);
                MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonMinus, (new CornerRadius(cornerRadius)));
                #region materialDesign
                var MinusPackIcon = new PackIcon();
                MinusPackIcon.Tag = "minusPackIcon-" + item.id;
                MinusPackIcon.Foreground = Application.Current.Resources["ThickGrey"] as SolidColorBrush;
                MinusPackIcon.Height =
                MinusPackIcon.Width = 25;
                MinusPackIcon.Kind = PackIconKind.Minus;
                buttonMinus.Content = MinusPackIcon;
                #endregion
                buttonMinus.Click += buttonMinus_Click;

                Grid.SetColumn(buttonMinus, 1);
                gridRow1.Children.Add(buttonMinus);
                /////////////////////////////////

                #endregion
                #region   plus
                Button buttonPlus = new Button();
                buttonPlus.Tag = "plus-" + item.id;
                buttonPlus.Margin = new Thickness(2.5);
                buttonPlus.Height =
                buttonPlus.Width = 25;
                buttonPlus.Padding = new Thickness(0);
                buttonPlus.Background = Application.Current.Resources["veryLightGrey"] as SolidColorBrush;
                buttonPlus.BorderThickness = new Thickness(0);
                MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonPlus, (new CornerRadius(cornerRadius)));
                #region materialDesign
                var PlusPackIcon = new PackIcon();
                PlusPackIcon.Tag = "plusPackIcon-" + item.id;
                PlusPackIcon.Foreground = Application.Current.Resources["ThickGrey"] as SolidColorBrush;
                PlusPackIcon.Height =
                PlusPackIcon.Width = 25;
                PlusPackIcon.Kind = PackIconKind.Plus;
                buttonPlus.Content = PlusPackIcon;
                #endregion
                buttonPlus.Click += buttonPlus_Click;
                Grid.SetColumn(buttonPlus, 3);
                gridRow1.Children.Add(buttonPlus);
                /////////////////////////////////
                #endregion
                #region   close
                Button buttonClose = new Button();
                buttonClose.Tag = "close-" + item.id;
                buttonClose.Margin = new Thickness(2.5);
                buttonClose.Height =
                buttonClose.Width = 25;
                buttonClose.Padding = new Thickness(0);
                buttonClose.Background = Application.Current.Resources["Red"] as SolidColorBrush;
                buttonClose.BorderThickness = new Thickness(0);
                MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonClose, (new CornerRadius(cornerRadius)));
                #region materialDesign
                var ClosePackIcon = new PackIcon();
                ClosePackIcon.Tag = "closePackIcon-" + item.id;
                ClosePackIcon.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                ClosePackIcon.Height =
                ClosePackIcon.Width = 25;
                ClosePackIcon.Kind = PackIconKind.Close;
                buttonClose.Content = ClosePackIcon;
                #endregion
                buttonClose.Click += buttonClose_Click;
                Grid.SetColumn(buttonClose, 5);
                gridRow1.Children.Add(buttonClose);
                /////////////////////////////////
                #endregion
                gridMain.Children.Add(gridRow1);
                #endregion
                #region stackPanelRow2
                StackPanel stackPanelRow2 = new StackPanel();
                stackPanelRow2.Margin = new Thickness(5,0,5,0);
                #region extraItems
                List<string> extraItems = new List<string>() { "+ extra item", "- item", };
                foreach (var extra in extraItems)
                {
                    TextBlock extraItem = new TextBlock();
                    extraItem.Text = extra;
                    extraItem.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    extraItem.HorizontalAlignment = HorizontalAlignment.Left;
                    extraItem.VerticalAlignment = VerticalAlignment.Center;
                    extraItem.Margin = new Thickness(5);
                    extraItem.TextWrapping = TextWrapping.WrapWithOverflow;
                    extraItem.TextAlignment = TextAlignment.Center;

                    stackPanelRow2.Children.Add(extraItem);
                }
                #endregion
                Grid.SetRow(stackPanelRow2, 1);
                gridMain.Children.Add(stackPanelRow2);
                #endregion
                #region gridRow3
                Grid gridRow3 = new Grid();
                gridRow3.Margin = new Thickness(5);
                #region gridSettings
                /////////////////////////////////////////////////////
                int colCountRow3 = 2;
                ColumnDefinition[] cdRow3 = new ColumnDefinition[colCountRow1];
                for (int i = 0; i < colCountRow3; i++)
                {
                    cdRow3[i] = new ColumnDefinition();
                }
                cdRow3[0].Width = new GridLength(1, GridUnitType.Star);
                cdRow3[1].Width = new GridLength(1, GridUnitType.Auto);
                for (int i = 0; i < colCountRow3; i++)
                {
                    gridRow3.ColumnDefinitions.Add(cdRow3[i]);
                }
                #endregion
                #region borderNotes
                Border borderNotes = new Border();
                borderNotes.CornerRadius = new CornerRadius(0);
                borderNotes.Margin = new Thickness(5);
                borderNotes.BorderThickness = new Thickness(0,0,0,1);
                borderNotes.BorderBrush = Application.Current.Resources["Grey"] as SolidColorBrush;
                borderNotes.Padding = new Thickness(0);
                borderNotes.Background = null;
                #region textBoxNotes
                TextBox textBoxNotes = new TextBox();
                textBoxNotes.Height = 40;
                textBoxNotes.BorderThickness = new Thickness(0);
                textBoxNotes.Margin = new Thickness(0);
                textBoxNotes.Padding = new Thickness(10,0,5,0);
                textBoxNotes.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                textBoxNotes.TextWrapping = TextWrapping.Wrap;
                //Style="{StaticResource MaterialDesignFloatingHintTextBox}"
                textBoxNotes.Style = Application.Current.Resources["MaterialDesignFloatingHintTextBox"] as Style; ;

                // materialDesign:TextFieldAssist.CharacterCounterStyle="{Binding}"
                MaterialDesignThemes.Wpf.TextFieldAssist.SetCharacterCounterStyle(textBoxNotes, null);
                MaterialDesignThemes.Wpf.HintAssist.SetHint(textBoxNotes, "Notes1...");

                borderNotes.Child = textBoxNotes;
                #endregion

                Grid.SetColumn(borderNotes, 0);
                gridRow3.Children.Add(borderNotes);
                #endregion
                #region textTotal
                TextBlock textTotal = new TextBlock();
                textTotal.Text = item.total.ToString();
                textTotal.FontSize = 14;
                textTotal.FontWeight = FontWeights.SemiBold;
                textTotal.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                textTotal.HorizontalAlignment = HorizontalAlignment.Center;
                textTotal.VerticalAlignment = VerticalAlignment.Center;
                textTotal.Margin = new Thickness(5);
                textTotal.TextWrapping = TextWrapping.WrapWithOverflow;
                textTotal.TextAlignment = TextAlignment.Center;

                Grid.SetColumn(textTotal, 1);
                gridRow3.Children.Add(textTotal);
                #endregion



                Grid.SetRow(gridRow3, 2);
                gridMain.Children.Add(gridRow3);
                #endregion 


                borderMain.Child = gridMain;
                #endregion
                sp_invoiceDetails.Children.Add(borderMain);
                #endregion

            }
        }
        void buttonMinus_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Button button = sender as Button;
                int id = int.Parse(button.Tag.ToString().Replace("minus-", ""));
                MessageBox.Show($"I'm minus button number: {id}");
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }

        }
        void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                int id = int.Parse(button.Tag.ToString().Replace("plus-", ""));
                MessageBox.Show($"I'm plus button number: {id}");
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                int id = int.Parse(button.Tag.ToString().Replace("close-", ""));
                MessageBox.Show($"I'm close button number: {id}");
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        #endregion


        
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

        private void btn_invoiceCost_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_transform_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
