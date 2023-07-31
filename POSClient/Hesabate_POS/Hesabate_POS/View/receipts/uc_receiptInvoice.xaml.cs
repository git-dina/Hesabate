using Hesabate_POS.Classes;
using Hesabate_POS.Classes.ApiClasses;
using Hesabate_POS.converters;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        List<CategoryModel> items = new List<CategoryModel>();
        InvoiceModel invoice = new InvoiceModel();
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

                switchInvoiceDetailsType();

                //invoiceDetailsList = getInvoiceDetails();
                //buildInvoiceDetailsSmall(invoiceDetailsList);

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
            txt_allItems.Text = Translate.getResource("391");
            txt_invoiceTitle.Text = Translate.getResource("1128");
            txt_external.Text = Translate.getResource("695");
            txt_tables.Text = Translate.getResource("167");
            txt_customer.Text = Translate.getResource("2145");
            txt_CountTitle.Text = Translate.getResource("578");
            txt_SupTotalTitle.Text = Translate.getResource("572");

            txt_TaxTitle.Text = Translate.getResource("1342");
            txt_DiscountTitle.Text = Translate.getResource("1066");
            txt_totalTitle.Text = Translate.getResource("727");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, Translate.getResource("2143"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_Notes1, Translate.getResource("411"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_Notes2, Translate.getResource("411"));

            btn_save.Content = Translate.getResource("2104");
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


        #region grid1_1
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

        async Task buildItemsCard(List<CategoryModel> items)
        {
            wp_itemsCard.Children.Clear();
            int cardWidth = 175;
            int cardHeight = 75;
            int cornerRadius = 7;
            bool isLast =false;
            string cardColor;
            foreach (var item in items)
            {
                if (ItemService.itemIsLast(item))
                    isLast = true;
                else
                    isLast = false;

                if (string.IsNullOrWhiteSpace(item.color))
                    cardColor = "#7062FB";
                else
                    cardColor = "#" + item.color;



                #region borderMain
                Border borderMain = new Border();
                borderMain.Width = cardWidth;
                borderMain.Height = cardHeight;
                borderMain.Background = Application.Current.Resources["White"] as SolidColorBrush;
                //borderMain.BorderBrush = Application.Current.Resources["Grey"] as SolidColorBrush;
                borderMain.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom(cardColor));
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
                int rowCount = 3;
                RowDefinition[] rd = new RowDefinition[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    rd[i] = new RowDefinition();
                }
                rd[0].Height = new GridLength(1, GridUnitType.Auto);
                rd[1].Height = new GridLength(1, GridUnitType.Star);
                rd[2].Height = new GridLength(1, GridUnitType.Auto);
                for (int i = 0; i < rowCount; i++)
                {
                    gridMain.RowDefinitions.Add(rd[i]);
                }
                /////////////////////////////////////////////////////
                int colCount = 3;
                ColumnDefinition[] cd = new ColumnDefinition[colCount];
                for (int i = 0; i < colCount; i++)
                {
                    cd[i] = new ColumnDefinition();
                }
                cd[0].Width = new GridLength(75, GridUnitType.Pixel);
                cd[1].Width = new GridLength(1, GridUnitType.Pixel);
                cd[2].Width = new GridLength(99, GridUnitType.Pixel);
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

                await setImg(buttonImage, item.img);

                Grid.SetRowSpan(buttonImage, 3);
                gridMain.Children.Add(buttonImage);
                #endregion

                #region 
                Border borderCol1 = new Border();
                borderCol1.Width = 1;
                borderCol1.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(cardColor));

                Grid.SetRowSpan(borderCol1, 3);
                Grid.SetColumn(borderCol1, 1);
                gridMain.Children.Add(borderCol1);

                #endregion


                #region categoryColor
                //if (!isLast)
                //{
                //    Border categoryColor = new Border();
                //    categoryColor.Height =
                //    categoryColor.Width = 15;
                //    categoryColor.HorizontalAlignment =HorizontalAlignment.Right ;
                //    categoryColor.Margin = new Thickness(5,2.5,5,2.5);
                //    //categoryColor.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                //    categoryColor.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(cardColor));
                //    categoryColor.CornerRadius = new CornerRadius(75);

                //    Grid.SetRow(categoryColor, 0);
                //    Grid.SetColumn(categoryColor, 2);
                //    gridMain.Children.Add(categoryColor);
                //}
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

                Grid.SetRow(textName, 1);
                Grid.SetColumn(textName, 2);
                gridMain.Children.Add(textName);
                #endregion
                #region borderPrice
                Border borderPrice = new Border();
                //borderPrice.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                borderPrice.Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(cardColor));
                borderPrice.Padding = new Thickness(0);
                borderPrice.Margin = new Thickness(0);
                borderPrice.CornerRadius = new CornerRadius(0, 0, 12, 0);

                #region textPrice

                TextBlock textPrice = new TextBlock();
                if (isLast)
                {
                    textPrice.Text = HelpClass.DecTostring(item.price);
                }
                textPrice.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                textPrice.Margin = new Thickness(5, 2.5, 5, 2.5);
                textPrice.HorizontalAlignment = HorizontalAlignment.Center;
                textPrice.VerticalAlignment = VerticalAlignment.Center;
                textPrice.TextWrapping = TextWrapping.WrapWithOverflow;
                textPrice.TextAlignment = TextAlignment.Center;
                borderPrice.Child = textPrice;

                #endregion
                Grid.SetRow(borderPrice, 2);
                Grid.SetColumn(borderPrice, 2);
                gridMain.Children.Add(borderPrice);
                #endregion

                // add last
                #region rectangleCard
                Rectangle rectangleCard = new Rectangle();
                rectangleCard.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#99F0F8FF"));
                rectangleCard.Opacity = 0;
                rectangleCard.RadiusX = 10;
                rectangleCard.RadiusY = 10;
                rectangleCard.MouseEnter += rectangle_MouseEnter;
                rectangleCard.MouseLeave += Button_MouseLeave;

                Grid.SetRowSpan(rectangleCard, 3);
                Grid.SetColumnSpan(rectangleCard, 3);
                gridMain.Children.Add(rectangleCard);
                #endregion

                buttonMain.Content = gridMain;
                #endregion
                borderMain.Child = buttonMain;
                #endregion
                wp_itemsCard.Children.Add(borderMain);
                #endregion
            }

        }
        private async void btn_item_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button.DataContext != null)
                {
                    //int itemId = int.Parse(button.Tag.ToString());
                    var item = button.DataContext as CategoryModel;
                    // is not Last
                    //if ( item.level2 != null || (item.items != null && item.items.Count != 0))
                    if (!ItemService.itemIsLast(item))
                    {
                        // categoryPath
                        //categoryPath.Add(item);
                      
                        categoryPath = _itemService.getCategoryPath(item.id).ToList();
                        buildCategoryPath(categoryPath);
                      

                        // itemsCard
                        items = _itemService.getCatItems(item.id);
                        await buildItemsCard(items);

                    }
                    else
                    {
                        AddItemToInvoice(item.id,item.name,item.price,item.no_w);
                        //MessageBox.Show("Add me to invoice");
                    }


                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        //private void AddItemToInvoice(CategoryModel item)
        private void AddItemToInvoice(int itemId,string itemName, decimal itemPrice,string hasSerial)
        {
            var itemInInvoice = invoiceDetailsList.Where(x => x.id == itemId).FirstOrDefault();
            if (itemInInvoice != null && hasSerial.Equals("0"))
            {
                itemInInvoice.count++;
                itemInInvoice.total = itemInInvoice.count * itemInInvoice.price;
            }
            else
            {
                string extra = string.Empty;
               
                invoiceDetailsList.Add(new InvoiceDetails()
                {
                    id = itemId,
                    name = itemName,
                    price = itemPrice,
                    count = 1,
                    total = itemPrice,
                    extra = extra
                });
            }

            buildInvoiceDetailsSmall(invoiceDetailsList);
            CalculateInvoiceValues();
        }
        public async  Task setImg(Button img, string uri)
        {
            
            ImageBrush imageBrush = new ImageBrush();
            BitmapFrame temp;

            imageBrush.Stretch = Stretch.UniformToFill;
            Uri resourceUri = new Uri("pic/no-image-icon-125x125.png", UriKind.Relative);

            StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
            temp = BitmapFrame.Create(streamInfo.Stream);
            imageBrush.ImageSource = temp;
            img.Background = imageBrush;

            if (uri != null && uri != "")
            {
                try
                {
                   // using(MemoryStream ms = await ItemService.DownloadImageAsync(AppSettings.APIUri, uri))

                    Thread t1 = new Thread(async () =>
                    {
                        temp = BitmapFrame.Create(new Uri(AppSettings.APIUri + "/" + uri));

                        string remoteUri = AppSettings.APIUri + "/" + uri;
                        var imgUrl = new Uri(remoteUri);
                        var imageData = new WebClient().DownloadData(imgUrl);
                        this.Dispatcher.Invoke(() =>
                        {

                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.StreamSource = new MemoryStream(imageData);
                            bitmapImage.EndInit();

                            //imageBrush.ImageSource = temp;
                            imageBrush.ImageSource = bitmapImage;
                            img.Background = imageBrush;
                        });

                    });
                    t1.Start();

                }
                catch {
                    streamInfo = Application.GetResourceStream(resourceUri);
                    temp = BitmapFrame.Create(streamInfo.Stream);
                    imageBrush.ImageSource = temp;
                    img.Background = imageBrush;
                }
            }          

        }
        private void rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                Rectangle rectangle = sender as Rectangle;
                rectangle.Opacity = 0.3;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                Rectangle rectangle = sender as Rectangle;
                rectangle.Opacity = 0.0;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion
        #region category
        private async void btn_allItems_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                // categoryPath
                categoryPath.Clear();
                buildCategoryPath(categoryPath);

                // itemsCard
                items = _itemService.getCatItems(0);
                await buildItemsCard(items);
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }


        List<CategoryModel> categoryPath = new List<CategoryModel>();
        void buildCategoryPath(List<CategoryModel> categories)
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
        private async void btn_categoryPath_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                if (button.DataContext != null)
                {
                    var item = button.DataContext as CategoryModel;
                    // categoryPath
                    //categoryPath.Add(item);
                  
                    categoryPath = _itemService.getCategoryPath(item.id).ToList();
                    buildCategoryPath(categoryPath);
                    
                        // itemsCard
                        items = _itemService.getCatItems(item.id);
                       await buildItemsCard(items);

                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        #endregion

        #region invoiceItemOptions
        InvoiceDetails selectedInvoiceItemOptions;
        private void Btn_invoiceItemOptionsBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                switchGrid1_1(button.Tag.ToString());

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private void btn_extraItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                    switchGrid1_1(button.Tag.ToString() );
                if (selectedInvoiceItemOptions != null && selectedInvoiceItemOptions.index != 0)
                {
                    MessageBox.Show($"i'm items extra for invoiceItem index : {selectedInvoiceItemOptions.index}");
                }

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void btn_deleteItems_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion
        void switchGrid1_1(string type)
        {
            


            // first level
            if (type == "mainItemsCatalog")
            {
                grid_mainItemsCatalog.Visibility = Visibility.Visible;
                grid_invoiceItemOptions.Visibility = Visibility.Collapsed;
            }
            else if (type == "invoiceItemOptions")
            {
                grid_mainItemsCatalog.Visibility = Visibility.Collapsed;
                grid_invoiceItemOptions.Visibility = Visibility.Visible;


                btn_invoiceItemOptionsBack.Tag = "mainItemsCatalog";
                txt_invoiceItemOptionsTitle.Text = "invoiceItemOptions";
                wp_invoiceItemOptions.Visibility = Visibility.Visible;
            }
            // second level
            else if (type == "extraItems")
            {
               
                btn_invoiceItemOptionsBack.Tag = "invoiceItemOptions";
                txt_invoiceItemOptionsTitle.Text = "extraItems";
                wp_invoiceItemOptions.Visibility = Visibility.Collapsed;
                //wp_extraItems.Visibility = Visibility.Visible;
            }


            
        }

        #endregion

        #region invoiceDetails
        void switchInvoiceDetailsType()
        {
            if (AppSettings.invoiceDetailsType == "big")
            {
                grid_invoiceDetailsBig.Visibility = Visibility.Visible;
                grid_invoiceDetailsSmall.Visibility = Visibility.Collapsed;
              
                cd_main1.Width = new GridLength(55, GridUnitType.Star);
                cd_main2.Width = new GridLength(45, GridUnitType.Star);
            }
            else if (AppSettings.invoiceDetailsType == "small")
            {
                grid_invoiceDetailsBig.Visibility = Visibility.Collapsed;
                grid_invoiceDetailsSmall.Visibility = Visibility.Visible;

                cd_main1.Width = new GridLength(65, GridUnitType.Star);
                cd_main2.Width = new GridLength(35, GridUnitType.Star);
            }
        }



        List<InvoiceDetails> invoiceDetailsList = new List<InvoiceDetails>();
        class InvoiceDetails:  INotifyPropertyChanged
        {
            //public int index { get; set; }
            private int _index;
            public int index
            {
                get => _index;
                set
                {
                    if (_index == value) return;

                    _index = value;
                    OnPropertyChanged();
                }
            }

            public int id;
            public string name;
            public string unit;
            public string nameUnit
            {
                get
                {
                    return (string.IsNullOrWhiteSpace(unit) ? $"{name}" : $"{name} - {unit}");
                }
                set
                {
                    nameUnit = value;
                }
            }
            //public int count;
            private int _count;
            public int count
            {
                get => _count;
                set
                {
                    if (_count == value) return;

                    _count = value;
                    OnPropertyChanged();
                }
            }

            public decimal discount;
            //public decimal price;
            private decimal _price;
            public decimal price
            {
                get => _price;
                set
                {
                    if (_price == value) return;

                    _price = value;
                    OnPropertyChanged();
                }
            }
            //public decimal total;
            private decimal _total;
            public decimal total
            {
                get => _total;
                set
                {
                    if (_total == value) return;

                    _total = value;
                    OnPropertyChanged();
                }
            }
            public string extra;
            public List<ItemModel> extraItems =new List<ItemModel>();
            public List<ItemModel> deleteItems =new List<ItemModel>();
            //public string notes;
            private string _notes;
            public string notes
            {
                get => _notes;
                set
                {
                    if (_notes == value) return;

                    _notes = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
        }
        List<InvoiceDetails> getInvoiceDetails()
        {
            Random rnd = new Random();
            List<InvoiceDetails> invoiceDetailsList = new List<InvoiceDetails>();
            List<ItemModel> extra = new List<ItemModel>();
            List<ItemModel> delete = new List<ItemModel>();
            for (int i = 0; i < 2; i++)
            {
                extra.Add(new ItemModel()
                {
                    id = i + 1,
                    name = "Lorem ipsum dolor sit Lorem ipsum dolor sit...",
                    count = rnd.Next(1, 15),
                });
            }
             for (int i = 0; i < 2; i++)
            {
                extra.Add(new ItemModel()
                {
                    id = i + 1,
                    name = "Lorem ipsum dolor sit Lorem ipsum dolor sit...",
                    count = rnd.Next(1, 15),
                });
            }
            for (int i = 0; i < 5; i++)
            {
                invoiceDetailsList.Add(new InvoiceDetails()
                {
                    id = i + 1,
                    name = "Lorem ipsum dolor sit Lorem ipsum dolor sit...",
                    count = rnd.Next(1, 99),
                    price = (decimal)rnd.Next(100, 9999) / 100,
                    total = (decimal)rnd.Next(100, 9999) / 100,
                    discount = (decimal)rnd.Next(100, 9999) / 100,
                    notes = "Lorem ipsum dolor sit Lorem ipsum dolor sit...",
                    extraItems = extra,
                    deleteItems = delete
                });
            }
            return invoiceDetailsList;
        }
        void buildInvoiceDetailsSmall(List<InvoiceDetails> invoiceDetailsList)
        {
            sp_invoiceDetailsSmall.Children.Clear();
            //int cardWidth = 175;
            //int cardHeight = 75;
            int cornerRadius = 7;
            int index = 1;
            foreach (var item in invoiceDetailsList)
            {
                item.index = index;
                #region borderMain
                Border borderMain = new Border();
                borderMain.DataContext = item;
                borderMain.BorderThickness = new Thickness(1);
                borderMain.CornerRadius = new CornerRadius(cornerRadius);
                borderMain.BorderBrush = Application.Current.Resources["Grey"] as SolidColorBrush;
                borderMain.Background = null;
                borderMain.Margin = new Thickness(5);
                borderMain.Padding = new Thickness(0);

                #region gridMain
                Grid gridMain = new Grid();
                gridMain.Margin = new Thickness(5,5,5,5);

                #region gridSettings
                /////////////////////////////////////////////////////
                int rowCount = 4;
                RowDefinition[] rd = new RowDefinition[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    rd[i] = new RowDefinition();
                    rd[i].Height = new GridLength(1, GridUnitType.Auto);
                    gridMain.RowDefinitions.Add(rd[i]);
                }
                #endregion

                #region gridRow0
                Grid gridRow0 = new Grid();
                gridRow0.Margin = new Thickness(0,2.5,0, 2.5);
                #region gridSettings
                /////////////////////////////////////////////////////
                int colCountRow0 = 3;
                ColumnDefinition[] cdRow0 = new ColumnDefinition[colCountRow0];
                for (int i = 0; i < colCountRow0; i++)
                {
                    cdRow0[i] = new ColumnDefinition();
                }
                cdRow0[0].Width = new GridLength(1, GridUnitType.Auto);
                cdRow0[1].Width = new GridLength(1, GridUnitType.Star);
                cdRow0[2].Width = new GridLength(1, GridUnitType.Auto);
                for (int i = 0; i < colCountRow0; i++)
                {
                    gridRow0.ColumnDefinitions.Add(cdRow0[i]);
                }
                #endregion
                #region itemIndex
                TextBlock itemIndex = new TextBlock();
                itemIndex.Text = $"{item.index}-" ;
                itemIndex.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                itemIndex.HorizontalAlignment = HorizontalAlignment.Center;
                itemIndex.VerticalAlignment = VerticalAlignment.Center;
                itemIndex.Margin = new Thickness(5,5,0,5);
                itemIndex.TextWrapping = TextWrapping.WrapWithOverflow;
                itemIndex.TextAlignment = TextAlignment.Center;


                Grid.SetColumn(itemIndex, 0);
                gridRow0.Children.Add(itemIndex);
                #endregion
                #region itemName
                TextBlock itemName = new TextBlock();

                if (string.IsNullOrWhiteSpace(item.unit))
                    itemName.Text = item.name;
                else
                    itemName.Text = $"{item.name} - {item.unit}";

                itemName.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                itemName.HorizontalAlignment = HorizontalAlignment.Left;
                itemName.VerticalAlignment = VerticalAlignment.Center;
                itemName.Margin = new Thickness(5);
                itemName.TextWrapping = TextWrapping.WrapWithOverflow;
                itemName.TextAlignment = TextAlignment.Center;


                Grid.SetColumn(itemName, 1);
                gridRow0.Children.Add(itemName);
                #endregion
                #region itemPrice
                TextBlock itemPrice = new TextBlock();
                //itemPrice.Text = item.price+"$";
                var itemPriceBinding = new System.Windows.Data.Binding("price");
                itemPriceBinding.Mode = BindingMode.OneWay;
                itemPriceBinding.Converter =new accuracyConverter();
                itemPrice.SetBinding(TextBlock.TextProperty, itemPriceBinding);

                itemPrice.FontSize = 12;
                itemPrice.FontWeight = FontWeights.SemiBold;
                itemPrice.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                itemPrice.HorizontalAlignment = HorizontalAlignment.Center;
                itemPrice.VerticalAlignment = VerticalAlignment.Center;
                itemPrice.Margin = new Thickness(5);
                itemPrice.TextWrapping = TextWrapping.WrapWithOverflow;
                itemPrice.TextAlignment = TextAlignment.Center;

                Grid.SetColumn(itemPrice, 2);
                gridRow0.Children.Add(itemPrice);
                #endregion
                gridMain.Children.Add(gridRow0);
                #endregion

                #region stackPanelRow1
                StackPanel stackPanelRow1 = new StackPanel();
                stackPanelRow1.Margin = new Thickness(0, 2.5, 0, 2.5);
                #region extraItems
                //List<string> extraItems = new List<string>() { "+ extra item", "- item", };
                if (item.extra != null)
                //foreach (var extra in extraItems)
                {
                    TextBlock extraItem = new TextBlock();
                    extraItem.Text = item.extra;
                    extraItem.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    extraItem.HorizontalAlignment = HorizontalAlignment.Left;
                    extraItem.VerticalAlignment = VerticalAlignment.Center;
                    extraItem.Margin = new Thickness(0, 2.5, 0, 2.5);
                    extraItem.TextWrapping = TextWrapping.WrapWithOverflow;
                    extraItem.TextAlignment = TextAlignment.Center;

                    stackPanelRow1.Children.Add(extraItem);
                }
                #endregion
                Grid.SetRow(stackPanelRow1, 1);
                gridMain.Children.Add(stackPanelRow1);
                #endregion
                #region gridRow2
                Grid gridRow2 = new Grid();
                gridRow2.Margin = new Thickness(0, 2.5, 0, 2.5);
                #region gridSettings
                /////////////////////////////////////////////////////
                int colCountRow2 = 6;
                ColumnDefinition[] cdRow2 = new ColumnDefinition[colCountRow2];
                for (int i = 0; i < colCountRow2; i++)
                {
                    cdRow2[i] = new ColumnDefinition();
                }
                cdRow2[0].Width = new GridLength(1, GridUnitType.Auto);
                cdRow2[1].Width = new GridLength(1, GridUnitType.Auto);
                cdRow2[2].Width = new GridLength(1, GridUnitType.Auto);
                cdRow2[3].Width = new GridLength(1, GridUnitType.Star);
                cdRow2[4].Width = new GridLength(1, GridUnitType.Auto);
                cdRow2[5].Width = new GridLength(1, GridUnitType.Auto);
                for (int i = 0; i < colCountRow2; i++)
                {
                    gridRow2.ColumnDefinitions.Add(cdRow2[i]);
                }
                #endregion
                #region itemCount
                TextBlock itemCount = new TextBlock();

                //itemCount.Text = item.count.ToString();
                var itemCountBinding = new System.Windows.Data.Binding("count");
                //itemCountBinding.Mode = BindingMode.OneWay;
                itemCount.SetBinding(TextBlock.TextProperty, itemCountBinding);

                itemCount.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemCount.HorizontalAlignment = HorizontalAlignment.Center;
                itemCount.VerticalAlignment = VerticalAlignment.Center;
                itemCount.Margin = new Thickness(5);
                itemCount.TextWrapping = TextWrapping.WrapWithOverflow;
                itemCount.TextAlignment = TextAlignment.Center;


                Grid.SetColumn(itemCount, 1);
                gridRow2.Children.Add(itemCount);
                #endregion
                
                #region   minus
                Button buttonMinus = new Button();
                buttonMinus.Tag = "minus-" + item.index;
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

                Grid.SetColumn(buttonMinus, 0);
                gridRow2.Children.Add(buttonMinus);
                /////////////////////////////////

                #endregion
                #region   plus
                Button buttonPlus = new Button();
                buttonPlus.Tag = "plus-" + item.index;
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
                Grid.SetColumn(buttonPlus, 2);
                gridRow2.Children.Add(buttonPlus);
                /////////////////////////////////
                #endregion
                #region   info
                Button buttonInfo = new Button();
                buttonInfo.DataContext = item;
                buttonInfo.Tag = "info-" + item.index;
                buttonInfo.Margin = new Thickness(2.5);
                buttonInfo.Height =
                buttonInfo.Width = 25;
                buttonInfo.Padding = new Thickness(0);
                buttonInfo.Background = Application.Current.Resources["White"] as SolidColorBrush;
                MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonInfo, (new CornerRadius(25)));
                #region materialDesign
                System.Windows.Shapes.Path infoIcon = new System.Windows.Shapes.Path();
                infoIcon.Tag = "infoIcon-" + item.id;
                infoIcon.Fill = Application.Current.Resources["MainColor"] as SolidColorBrush;
                infoIcon.Stretch = Stretch.Fill;
                infoIcon.Height =
                infoIcon.Width = 25;
                infoIcon.Data = App.Current.Resources["infoCircle"] as Geometry;

                buttonInfo.Content = infoIcon;
                #endregion
                buttonInfo.Click += buttonInfo_Click;
                Grid.SetColumn(buttonInfo, 4);
                gridRow2.Children.Add(buttonInfo);
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
                MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonClose, (new CornerRadius(25)));
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
                gridRow2.Children.Add(buttonClose);
                /////////////////////////////////
                #endregion
                Grid.SetRow(gridRow2, 2);
                gridMain.Children.Add(gridRow2);
                #endregion
                

                #region gridRow3
                Grid gridRow3 = new Grid();
                gridRow3.Margin = new Thickness(0, 2.5, 0, 2.5);
                #region gridSettings
                /////////////////////////////////////////////////////
                int colCountRow3 = 2;
                ColumnDefinition[] cdRow3 = new ColumnDefinition[colCountRow3];
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
                var textBoxNotesBinding = new System.Windows.Data.Binding("notes");
                textBoxNotesBinding.Mode = BindingMode.TwoWay;
                textBoxNotes.SetBinding(TextBox.TextProperty, textBoxNotesBinding);

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
                //textTotal.Text = item.total + "$";
                var textTotalBinding = new System.Windows.Data.Binding("total");
                textTotalBinding.Mode = BindingMode.OneWay;
                textTotalBinding.Converter =new accuracyConverter();
                textTotal.SetBinding(TextBlock.TextProperty, textTotalBinding);

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



                Grid.SetRow(gridRow3, 3);
                gridMain.Children.Add(gridRow3);
                #endregion 


                borderMain.Child = gridMain;
                #endregion
                sp_invoiceDetailsSmall.Children.Add(borderMain);
                #endregion
                index++;
            }
        }
        void buttonMinus_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Button button = sender as Button;
                int index = int.Parse(button.Tag.ToString().Replace("minus-", ""));
                int itemCount = invoiceDetailsList[index - 1].count;
                if (itemCount >1)
                {
                    invoiceDetailsList[index - 1].count--;
                    invoiceDetailsList[index - 1].total = invoiceDetailsList[index - 1].count * invoiceDetailsList[index - 1].price;
                    //buildInvoiceDetailsSmall(invoiceDetailsList);
                    CalculateInvoiceValues();
                }
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
                int index = int.Parse(button.Tag.ToString().Replace("plus-", ""));
               

                invoiceDetailsList[index-1].count++;
                invoiceDetailsList[index - 1].total = invoiceDetailsList[index - 1].count * invoiceDetailsList[index - 1].price;
                //buildInvoiceDetailsSmall(invoiceDetailsList);

                CalculateInvoiceValues();
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void buttonInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                int index = int.Parse(button.Tag.ToString().Replace("info-", ""));
                InvoiceDetails invoiceDetails = button.DataContext as InvoiceDetails;
                selectedInvoiceItemOptions = invoiceDetails;
                switchGrid1_1("invoiceItemOptions");

                
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


        void switchInvoiceType(string type)
        {
            
        }

        #endregion
        #region invoice
        private void CalculateInvoiceValues()
        {
            txt_Count.Text =  invoiceDetailsList.Select(x => x.count).Sum().ToString();
            txt_SupTotal.Text = HelpClass.DecTostring(invoiceDetailsList.Select(x => x.price).Sum());
            txt_total.Text = HelpClass.DecTostring(invoiceDetailsList.Select(x => x.price).Sum());
        }
        #endregion

        #region grid0_0
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




        #endregion

        #region search

        //private async void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (tb_search.Text != "")
        //        {
        //            HelpClass.StartAwait(grid_main);
 
        //            var item =await _itemService.GetItemInfo(tb_search.Text, "0", invoice.CustomerId, GeneralInfoService.GeneralInfo.MainOp.price_id);
        //            if(item != null)
        //            {
        //                AddItemToInvoice(item.id,item.name,item.min_p,item.no_w);
        //            }
        //            //else
        //            //    HelpClass
        //            HelpClass.EndAwait(grid_main);
        //        }
        //    }
        //    catch { }
        //}
        private void tb_search_KeyDown(object sender, KeyEventArgs e)
        {
                try
                {
                    if (e.Key == Key.Return)
                    {
                        Btn_search_Click(btn_search, null);
                    }
                }
                catch (Exception ex)
                {
                    HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
                }
        }
        private async void Btn_search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tb_search.Text != "")
                {
                    HelpClass.StartAwait(grid_main);

                    var item = await _itemService.GetItemInfo(tb_search.Text, "1", invoice.CustomerId, GeneralInfoService.GeneralInfo.MainOp.price_id, tb_search.Text);
                    if (item != null)
                    {
                        AddItemToInvoice(item.id, item.name, item.min_p,item.no_w);
                    }
                    //else
                    //    HelpClass

                    tb_search.Text = "";
                    HelpClass.EndAwait(grid_main);
                }
            }
            catch { }
        }
        #endregion

        

      
    }
}
