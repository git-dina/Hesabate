﻿using Hesabate_POS.Classes;
using Hesabate_POS.Classes.ApiClasses;
using Hesabate_POS.converters;
using Hesabate_POS.View.windows;
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
using Path = System.Windows.Shapes.Path;

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
        //private static uc_receiptInvoice _instance;
        //public static uc_receiptInvoice Instance
        //{
        //    get
        //    {
        //        if (_instance is null)
        //            _instance = new uc_receiptInvoice();
        //        return _instance;
        //    }
        //    set
        //    {
        //        _instance = value;
        //    }
        //}

        public static List<string> requiredControlList;
        ItemService _itemService = new ItemService();
        InvoiceService _invoiceService = new InvoiceService();
        List<CategoryModel> items = new List<CategoryModel>();
        InvoiceModel invoice = new InvoiceModel();
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //Instance = null;
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

                //clearInvoice();
                switchInvoiceDetailsType();
                switchGrid1_1("mainItemsCatalog");
                btn_allItems_Click(btn_allItems, null);

                FillCombo.fillUnits(cmb_invItmOptUnit);
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
            txt_allItems.Text = Translate.getResource("162");
            txt_invoiceTitle.Text = Translate.getResource("1128");
            //txt_external.Text = Translate.getResource("2311");
            //txt_tables.Text = Translate.getResource("167");
            //txt_takeAway.Text = Translate.getResource("2307");
            //txt_customer.Text = Translate.getResource("2145");
            //txt_internalCustomer.Text = Translate.getResource("98");

            txt_CountTitle.Text = Translate.getResource("578");
            txt_SupTotalTitle.Text = Translate.getResource("572");
            txt_ServiceTitle.Text = Translate.getResource("1152");
            txt_ServiceRateTitle.Text = Translate.getResource("658");
            txt_taxRateTitle.Text = Translate.getResource("575");
            txt_taxValueTitle.Text = Translate.getResource("361");
            txt_AutoDiscountTitle.Text = Translate.getResource("2283");
            txt_UserDiscountTitle.Text = Translate.getResource("571");
            txt_totalTitle.Text = Translate.getResource("727");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_search, Translate.getResource("2143"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_Notes1, Translate.getResource("411"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_Notes2, Translate.getResource("411"));

            btn_save.Content = Translate.getResource("2104");
            btn_newDraft.Content = Translate.getResource("8");

            #region top buttons
            //txt_printInvoice.Text = Translate.getResource("2144");
            //txt_returns.Text = Translate.getResource("133");
            //txt_discount.Text = Translate.getResource("571");
            #endregion

            #region invoice details big
            col_index.Header = "#";
            col_id.Header = Translate.getResource("567");
            col_name.Header = Translate.getResource("568");
            col_unitName.Header = Translate.getResource("427");
            col_amount.Header = Translate.getResource("491");
            col_discount.Header = Translate.getResource("571");
            col_price.Header = Translate.getResource("570");
            col_total.Header = Translate.getResource("572");

            #endregion

            #region item details
            //txt_invItmOptAmountTitle.Text = Translate.getResource("491");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_invItmOptAmount, Translate.getResource("491"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cmb_invItmOptUnit, Translate.getResource("427"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cmb_invItmOptDiscount, Translate.getResource("571"));
            //txt_invItmOptBonusTitle.Text = Translate.getResource("583");
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_invItmOptBonus, Translate.getResource("583"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(tb_invItmOptPrice, Translate.getResource("570"));
            MaterialDesignThemes.Wpf.HintAssist.SetHint(cb_invItemOptNotes, Translate.getResource("411"));
            txt_invItmOptLibraReading.Text = Translate.getResource("2161");
            txt_invItmOptQueryItem.Text = Translate.getResource("2157");
            txt_urgent.Text = Translate.getResource("2252");
            txt_invItmOptDelete.Text = Translate.getResource("5");


            txt_invItmOpsDetailsTitle.Text = Translate.getResource("28");
            txt_invItmOpsAddAndDeleteGroupTitle.Text = Translate.getResource("2284");
            #endregion
        }
        #region
        /*
        Point scrollMousePoint = new Point();
        double hOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            scrollMousePoint = e.GetPosition(scrollViewer);
            hOff = scrollViewer.HorizontalOffset;
            scrollViewer.CaptureMouse();
        }

        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.IsMouseCaptured)
            {
                scrollViewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollViewer).X));
            }
        }

        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            scrollViewer.ReleaseMouseCapture();
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + e.Delta);
        }
        */
        #endregion
        /*
        Point oldMousePosition;
        private void sp_MouseMove(object sender, MouseEventArgs e)
        {
            Point newMousePosition = Mouse.GetPosition(sp_invoiceDetailsSmall);
            //tb.Text = newMousePosition.X + " | " + newMousePosition.Y;

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                if (newMousePosition.Y < oldMousePosition.Y)
                    sv.ScrollToVerticalOffset(sv.VerticalOffset + 1);
                if (newMousePosition.Y > oldMousePosition.Y)
                    sv.ScrollToVerticalOffset(sv.VerticalOffset - 1);

                if (newMousePosition.X < oldMousePosition.X)
                    sv.ScrollToHorizontalOffset(sv.HorizontalOffset + 1);
                if (newMousePosition.X > oldMousePosition.X)
                    sv.ScrollToHorizontalOffset(sv.HorizontalOffset - 1);
            }
            else
            {
                oldMousePosition = newMousePosition;
            }
        }
        */
        private void clearInvoice()
        {
            invoice = new InvoiceModel();

            invoiceDetailsList = new List<ItemModel>();

            if (AppSettings.invoiceDetailsType == "small")
                buildInvoiceDetailsSmall(invoiceDetailsList);
            else
                refreshInvoiceDetailsBig();

            CalculateInvoiceValues();
            
            //txt_Service.Text = HelpClass.DecTostring(GeneralInfoService.GeneralInfo.MainOp.service);
            //txt_Tax.Text = HelpClass.DecTostring(GeneralInfoService.GeneralInfo.MainOp.vat);
        }
        private void btn_home_Click(object sender, RoutedEventArgs e)
        {

        }

        //bool menuState = false;
        
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
        private async void Btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);

                invoice.note = tb_Notes1.Text;
                invoice.note2 = tb_Notes2.Text;

                var res = await _invoiceService.SaveInvoice(invoiceDetailsList, invoice);
                HelpClass.EndAwait(grid_main);
            }
            catch {
                HelpClass.EndAwait(grid_main);
            }
        }
        private async void Btn_newDraft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoiceDetailsList != null && invoiceDetailsList.Count > 0)
                {
                    HelpClass.StartAwait(grid_main);


                    #region confirm window
                    wd_acceptCancelPopup w = new wd_acceptCancelPopup();
                    w.contentText = Translate.getResource("2022");
                    w.ShowDialog();
                    #endregion
                    if (w.isOk)
                    {
                        await SaveDraft();
                         clearInvoice();

                    }
                HelpClass.EndAwait(grid_main);
                }

            }
            catch
            {
                HelpClass.EndAwait(grid_main);

            }
        }

        private async Task SaveDraft()
        {
            invoice.note = tb_Notes1.Text;
            invoice.note2 = tb_Notes2.Text;

            var res = await _invoiceService.ArchiveInvoice(invoiceDetailsList, invoice);
        }
        private void Btn_stop_Click(object sender, RoutedEventArgs e)
        {

        }

        #region grid0_1

       
        private void btn_printInvoice_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Mi_printInvoiceTax_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_returns_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btn_discount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_discount w = new wd_discount();
                w.ShowDialog();
                w.discountType = invoice.discountType;
                w.manualDiscount = invoice.manualDiscount;

                if (w.isOk)
                {
                    invoice.discountType = w.discountType;
                    invoice.manualDiscount = w.manualDiscount;
                    CalculateInvoiceValues();
                }

                Window.GetWindow(this).Opacity = 1;
            }
            catch
            {
                Window.GetWindow(this).Opacity = 1;
            }
        }
        #endregion
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
            bool isLast = false;
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
                if (item.name.Length <= 20)
                        textName.Text = item.name;
                else
                    textName.Text = item.name.Substring(0, 17) + "...";

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
                        if (GeneralInfoService.items != null)
                        {
                            var item1 = GeneralInfoService.items.Where(x => x.product_id == item.id.ToString()).FirstOrDefault();

                            AddItemToInvoice(item1, item.items, item.addItems, item.deleteItems);
                        }
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
        private void AddItemToInvoice(ItemModel item, List<CategoryModel> extraItems, List<CategoryModel> addItems, List<CategoryModel> deleteItems)
        {
            var itemInInvoice = invoiceDetailsList.Where(x => x.product_id.ToString() == item.product_id).FirstOrDefault();
            if (itemInInvoice != null && item.no_w.Equals("0"))
            {
                itemInInvoice.amount++;
                itemInInvoice.total = itemInInvoice.amount * itemInInvoice.price;
            }
            else
            {
                //string extra = string.Empty;
                if (item.unit != "0")
                    item.unit_name = GeneralInfoService.GeneralInfo.units[item.unit];

                item.amount = 1;
                item.total = item.price;

                item.extraItems = extraItems;
                item.addsItems = addItems;
                item.deletesItems = deleteItems;
                invoiceDetailsList.Add(item);
            }

            if (AppSettings.invoiceDetailsType.Equals("small"))
                buildInvoiceDetailsSmall(invoiceDetailsList);
            else
                refreshInvoiceDetailsBig();
                
            CalculateInvoiceValues();
        }
        public async Task setImg(Button img, string uri)
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


                    var localUri = _itemService.GetLocalUri(uri);
                    if (localUri == "")
                    {
                        Thread t1 = new Thread(async () =>
                        {
                            temp = BitmapFrame.Create(new Uri(AppSettings.APIUri + "/" + uri));

                            string remoteUri = AppSettings.APIUri + "/" + uri;
                            var imgUrl = new Uri(remoteUri);
                            var imageData = new WebClient().DownloadData(imgUrl);
                            _itemService.SaveImage(imageData, uri);
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
                    else
                    {
                        byte[] imageBuffer = _itemService.readLocalImage(localUri);
                        var bitmapImage = new BitmapImage();
                        using (var memoryStream = new System.IO.MemoryStream(imageBuffer))
                        {
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = memoryStream;
                            bitmapImage.EndInit();
                        }
                        img.Background = new ImageBrush(bitmapImage);


                    }


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
                switchGrid1_1("mainItemsCatalog");

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
                buttonMain.Content = textName;
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

                    switchGrid1_1("mainItemsCatalog");


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

        #region invItmOps
        ItemModel selectedInvItmOps;
        /*
        private void Btn_invItmOpsBack_Click(object sender, RoutedEventArgs e)
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
        */

        private void btn_extraItems_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                switchGrid1_1(button.Tag.ToString());
                if (selectedInvItmOps != null && selectedInvItmOps.index != 0)
                {
                    MessageBox.Show($"i'm items extra for invoiceItem index : {selectedInvItmOps.index}");
                    string extra = "";

                }

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void btn_deleteItems_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ss");
        }
        void showInvItmOps()
        {
            wp_invItmOpsSetting.DataContext = selectedInvItmOps;
            if (selectedInvItmOps.addsItems.Count == 0 && selectedInvItmOps.deletesItems.Count == 0)
            {
                grid_invItmOpsAddsAndDeletesTitle.Visibility = Visibility.Collapsed;
            }
            else
            {
                grid_invItmOpsAddsAndDeletesTitle.Visibility = Visibility.Visible;
            }
            buildInvoiceItemAdds(selectedInvItmOps);
            buildInvoiceItemDeletes(selectedInvItmOps);
            buildInvoiceItemExtra(selectedInvItmOps);
            switchGrid1_1("invItmOps");
        }
        private void btn_invItmOptAmountMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ItemModel invOptItem = button.DataContext as ItemModel;
                if (invOptItem.amount >1)
                {
                    invOptItem.amount--;
                    calculateItemPrice();
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void btn_invItmOptAmountPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ItemModel invOptItem = button.DataContext as ItemModel;

                invOptItem.amount++;
                calculateItemPrice();

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void btn_invItmOptBonusMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ItemModel invOptItem = button.DataContext as ItemModel;
                if (invOptItem.bonus > 0)
                {
                    invOptItem.bonus--;
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void btn_invItmOptBonusPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ItemModel invOptItem = button.DataContext as ItemModel;

                invOptItem.bonus++;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void buildInvoiceItemAdds(ItemModel item)
        {
            sp_invItmOpsAdds.Children.Clear();

            foreach (var extra in item.addsItems)
            {

                #region groupItems
                WrapPanel wrapPanel = new WrapPanel();
                foreach (var groupItem in extra.group_items)
                {

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Margin = new Thickness(10, 5, 10, 5);
                    stackPanel.DataContext = groupItem;


                    #region groupItemName
                    TextBlock groupItemText = new TextBlock();
                    if (string.IsNullOrEmpty(groupItem.unit_name))
                        groupItemText.Text = $"{groupItem.name}";
                    else
                        groupItemText.Text = $"{groupItem.name} - {groupItem.unit_name}";

                    groupItemText.Foreground = Application.Current.Resources["Green"] as SolidColorBrush;
                    groupItemText.HorizontalAlignment = HorizontalAlignment.Center;
                    groupItemText.VerticalAlignment = VerticalAlignment.Center;
                    groupItemText.Margin = new Thickness(5);
                    stackPanel.Children.Add(groupItemText);
                    #endregion

                    #region groupItemButtonMinus
                    Button groupItemButtonMinus = new Button();
                    groupItemButtonMinus.DataContext = groupItem;
                    groupItemButtonMinus.Margin = new Thickness(2.5);
                    groupItemButtonMinus.Height =
                    groupItemButtonMinus.Width = 25;
                    groupItemButtonMinus.Padding = new Thickness(0);
                    groupItemButtonMinus.Background = Application.Current.Resources["veryLightGrey"] as SolidColorBrush;
                    groupItemButtonMinus.BorderThickness = new Thickness(0);
                    MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(groupItemButtonMinus, (new CornerRadius(5)));
                    #region materialDesign
                    var minusPackIcon = new PackIcon();
                    minusPackIcon.Foreground = Application.Current.Resources["ThickGrey"] as SolidColorBrush;
                    minusPackIcon.Height =
                    minusPackIcon.Width = 25;
                    minusPackIcon.Kind = PackIconKind.Minus;
                    groupItemButtonMinus.Content = minusPackIcon;
                    #endregion
                    groupItemButtonMinus.Click += addsItemButtonMinus_Click;
                    stackPanel.Children.Add(groupItemButtonMinus);
                    /////////////////////////////////
                    #endregion

                    #region groupItem_basicAmount
                    if (groupItem.basicAmount is null) groupItem.basicAmount = groupItem.start_amount;
                    TextBlock groupItem_basicAmount = new TextBlock();
                    var groupItem_basicAmountBinding = new System.Windows.Data.Binding("basicAmount");
                    groupItem_basicAmount.SetBinding(TextBlock.TextProperty, groupItem_basicAmountBinding);
                    groupItem_basicAmount.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    groupItem_basicAmount.HorizontalAlignment = HorizontalAlignment.Center;
                    groupItem_basicAmount.VerticalAlignment = VerticalAlignment.Center;
                    groupItem_basicAmount.Margin = new Thickness(5);
                    stackPanel.Children.Add(groupItem_basicAmount);
                    #endregion
                    #region groupItemButtonPlus
                    Button groupItemButtonPlus = new Button();
                    groupItemButtonPlus.DataContext = groupItem;
                    groupItemButtonPlus.Margin = new Thickness(2.5);
                    groupItemButtonPlus.Height =
                    groupItemButtonPlus.Width = 25;
                    groupItemButtonPlus.Padding = new Thickness(0);
                    groupItemButtonPlus.Background = Application.Current.Resources["veryLightGrey"] as SolidColorBrush;
                    groupItemButtonPlus.BorderThickness = new Thickness(0);
                    MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(groupItemButtonPlus, (new CornerRadius(5)));
                    #region materialDesign
                    var plusPackIcon = new PackIcon();
                    plusPackIcon.Foreground = Application.Current.Resources["ThickGrey"] as SolidColorBrush;
                    plusPackIcon.Height =
                    plusPackIcon.Width = 25;
                    plusPackIcon.Kind = PackIconKind.Plus;
                    groupItemButtonPlus.Content = plusPackIcon;
                    #endregion
                    groupItemButtonPlus.Click += addsItemButtonPlus_Click;
                    stackPanel.Children.Add(groupItemButtonPlus);
                    /////////////////////////////////
                    #endregion
                    #region groupItem_add_price
                    TextBlock groupItem_add_price = new TextBlock();
                    var groupItem_add_priceBinding = new System.Windows.Data.Binding("add_price");
                    groupItem_add_price.SetBinding(TextBlock.TextProperty, groupItem_add_priceBinding);
                    groupItem_add_price.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    groupItem_add_price.HorizontalAlignment = HorizontalAlignment.Center;
                    groupItem_add_price.VerticalAlignment = VerticalAlignment.Center;
                    groupItem_add_price.Margin = new Thickness(5);
                    stackPanel.Children.Add(groupItem_add_price);
                    #endregion
                    #region groupItemCurrency
                    TextBlock groupItemCurrency = new TextBlock();
                    groupItemCurrency.Text = AppSettings.currency;
                    groupItemCurrency.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    groupItemCurrency.HorizontalAlignment = HorizontalAlignment.Center;
                    groupItemCurrency.VerticalAlignment = VerticalAlignment.Center;
                    groupItemCurrency.Margin = new Thickness(0, 5, 5, 5);
                    stackPanel.Children.Add(groupItemCurrency);
                    #endregion
                    #region groupItemBorder
                    Border groupItemBorder = new Border();
                    groupItemBorder.Margin = new Thickness(5);
                    groupItemBorder.Width = 1;
                    groupItemBorder.BorderThickness = new Thickness(0); ;
                    groupItemBorder.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;

                    stackPanel.Children.Add(groupItemBorder);
                    #endregion
                    wrapPanel.Children.Add(stackPanel);
                }
                sp_invItmOpsAdds.Children.Add(wrapPanel);
                #endregion
            }
        }
        void addsItemButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                GroupItemModel groupItem = button.DataContext as GroupItemModel;
                //if (groupItem.basicAmount > 0)
                if (groupItem.basicAmount > groupItem.start_amount)
                    groupItem.basicAmount--;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void addsItemButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                GroupItemModel groupItem = button.DataContext as GroupItemModel;
                if (groupItem.basicAmount < groupItem.allow_add)
                {
                    groupItem.basicAmount++;
                    calculateItemPrice();
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void buildInvoiceItemDeletes(ItemModel item)
        {
            sp_invItmOpsDeletes.Children.Clear();

            foreach (var extra in item.deletesItems)
            {

                #region groupItems
                WrapPanel wrapPanel = new WrapPanel();
                foreach (var groupItem in extra.group_items)
                {

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Horizontal;
                    stackPanel.Margin = new Thickness(10, 5, 10, 5);
                    stackPanel.DataContext = groupItem;

                    #region groupItemName
                    TextBlock groupItemText = new TextBlock();
                    if (string.IsNullOrEmpty(groupItem.unit_name))
                        groupItemText.Text = $"{groupItem.name}";
                    else
                        groupItemText.Text = $"{groupItem.name} - {groupItem.unit_name}";

                    groupItemText.Foreground = Application.Current.Resources["Red"] as SolidColorBrush;
                    groupItemText.HorizontalAlignment = HorizontalAlignment.Center;
                    groupItemText.VerticalAlignment = VerticalAlignment.Center;
                    groupItemText.Margin = new Thickness(5);
                    stackPanel.Children.Add(groupItemText);
                    #endregion

                    #region groupItemButtonMinus
                    Button groupItemButtonMinus = new Button();
                    groupItemButtonMinus.DataContext = groupItem;
                    groupItemButtonMinus.Margin = new Thickness(2.5);
                    groupItemButtonMinus.Height =
                    groupItemButtonMinus.Width = 25;
                    groupItemButtonMinus.Padding = new Thickness(0);
                    groupItemButtonMinus.Background = Application.Current.Resources["veryLightGrey"] as SolidColorBrush;
                    groupItemButtonMinus.BorderThickness = new Thickness(0);
                    MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(groupItemButtonMinus, (new CornerRadius(5)));
                    #region materialDesign
                    var minusPackIcon = new PackIcon();
                    minusPackIcon.Foreground = Application.Current.Resources["ThickGrey"] as SolidColorBrush;
                    minusPackIcon.Height =
                    minusPackIcon.Width = 25;
                    minusPackIcon.Kind = PackIconKind.Minus;
                    groupItemButtonMinus.Content = minusPackIcon;
                    #endregion
                    groupItemButtonMinus.Click += deletesItemButtonMinus_Click;
                    stackPanel.Children.Add(groupItemButtonMinus);
                    /////////////////////////////////
                    #endregion

                    #region groupItem_basicAmount
                    if (groupItem.basicAmount is null) groupItem.basicAmount = groupItem.start_amount; 
                    TextBlock groupItem_basicAmount = new TextBlock();
                    var groupItem_basicAmountBinding = new System.Windows.Data.Binding("basicAmount");
                    groupItem_basicAmount.SetBinding(TextBlock.TextProperty, groupItem_basicAmountBinding);
                    groupItem_basicAmount.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    groupItem_basicAmount.HorizontalAlignment = HorizontalAlignment.Center;
                    groupItem_basicAmount.VerticalAlignment = VerticalAlignment.Center;
                    groupItem_basicAmount.Margin = new Thickness(5);
                    stackPanel.Children.Add(groupItem_basicAmount);
                    #endregion
                    #region groupItemButtonPlus
                    Button groupItemButtonPlus = new Button();
                    groupItemButtonPlus.DataContext = groupItem;
                    groupItemButtonPlus.Margin = new Thickness(2.5);
                    groupItemButtonPlus.Height =
                    groupItemButtonPlus.Width = 25;
                    groupItemButtonPlus.Padding = new Thickness(0);
                    groupItemButtonPlus.Background = Application.Current.Resources["veryLightGrey"] as SolidColorBrush;
                    groupItemButtonPlus.BorderThickness = new Thickness(0);
                    MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(groupItemButtonPlus, (new CornerRadius(5)));
                    #region materialDesign
                    var plusPackIcon = new PackIcon();
                    plusPackIcon.Foreground = Application.Current.Resources["ThickGrey"] as SolidColorBrush;
                    plusPackIcon.Height =
                    plusPackIcon.Width = 25;
                    plusPackIcon.Kind = PackIconKind.Plus;
                    groupItemButtonPlus.Content = plusPackIcon;
                    #endregion
                    groupItemButtonPlus.Click += deletesItemButtonPlus_Click;
                    stackPanel.Children.Add(groupItemButtonPlus);
                    /////////////////////////////////
                    #endregion
                    #region groupItem_add_price
                    TextBlock groupItem_add_price = new TextBlock();
                    var groupItem_add_priceBinding = new System.Windows.Data.Binding("add_price");
                    groupItem_add_price.SetBinding(TextBlock.TextProperty, groupItem_add_priceBinding);
                    groupItem_add_price.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    groupItem_add_price.HorizontalAlignment = HorizontalAlignment.Center;
                    groupItem_add_price.VerticalAlignment = VerticalAlignment.Center;
                    groupItem_add_price.Margin = new Thickness(5);
                    stackPanel.Children.Add(groupItem_add_price);
                    #endregion
                    #region groupItemCurrency
                    TextBlock groupItemCurrency = new TextBlock();
                    groupItemCurrency.Text = AppSettings.currency;
                    groupItemCurrency.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    groupItemCurrency.HorizontalAlignment = HorizontalAlignment.Center;
                    groupItemCurrency.VerticalAlignment = VerticalAlignment.Center;
                    groupItemCurrency.Margin = new Thickness(0, 5, 5, 5);
                    stackPanel.Children.Add(groupItemCurrency);
                    #endregion
                    #region groupItemBorder
                    Border groupItemBorder = new Border();
                    groupItemBorder.Margin = new Thickness(5);
                    groupItemBorder.Width = 1;
                    groupItemBorder.BorderThickness = new Thickness(0); ;
                    groupItemBorder.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;

                    stackPanel.Children.Add(groupItemBorder);
                    #endregion
                    wrapPanel.Children.Add(stackPanel);
                }
                sp_invItmOpsDeletes.Children.Add(wrapPanel);
                #endregion
            }
        }
        void deletesItemButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                GroupItemModel groupItem = button.DataContext as GroupItemModel;
                if (groupItem.basicAmount > 0)
                    groupItem.basicAmount--;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void deletesItemButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                GroupItemModel groupItem = button.DataContext as GroupItemModel;
                if(groupItem.basicAmount < groupItem.allow_sub)
                    groupItem.basicAmount++;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        
        void buildInvoiceItemExtra(ItemModel item)
        {
            sp_invItmOpsExtra.Children.Clear();
            if (item.extraItems != null)
            {
                foreach (var extra in item.extraItems)
                {

                    #region GroupTitle
                    Grid gridTitle = new Grid();
                    gridTitle.Margin = new Thickness(10, 5, 0, 5);
                    #region gridSettings
                    /////////////////////////////////////////////////////
                    int colCount = 2;
                    ColumnDefinition[] cd = new ColumnDefinition[colCount];
                    for (int i = 0; i < colCount; i++)
                    {
                        cd[i] = new ColumnDefinition();
                    }
                    cd[0].Width = new GridLength(1, GridUnitType.Auto);
                    cd[1].Width = new GridLength(1, GridUnitType.Star);
                    for (int i = 0; i < colCount; i++)
                    {
                        gridTitle.ColumnDefinitions.Add(cd[i]);
                    }
                    #endregion
                    #region extraTitle
                    TextBlock extraTitle = new TextBlock();
                    extraTitle.Text = $"{extra.group_name} ({extra.group_count})";
                    extraTitle.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                    extraTitle.FontWeight = FontWeights.Bold;
                    extraTitle.Margin = new Thickness(5);

                    Grid.SetColumn(extraTitle, 0);
                    gridTitle.Children.Add(extraTitle);
                    #endregion
                    #region extraBorder
                    Border extraBorder = new Border();
                    extraBorder.Height = 2;
                    extraBorder.BorderThickness = new Thickness(0); ;
                    extraBorder.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                    extraBorder.Margin = new Thickness(10, 2.5, 10, 2.5);

                    Grid.SetColumn(extraBorder, 1);
                    gridTitle.Children.Add(extraBorder);
                    #endregion
                    sp_invItmOpsExtra.Children.Add(gridTitle);
                    #endregion
                    #region groupItems
                    WrapPanel wrapPanel = new WrapPanel();
                    foreach (var groupItem in extra.group_items)
                    {

                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        stackPanel.Margin = new Thickness(10, 5, 10, 5);
                        stackPanel.DataContext = groupItem;

                        #region AssignExtraId
                        groupItem.groupName = extra.group_name;
                        #endregion
                        #region groupItemName
                        TextBlock groupItemText = new TextBlock();
                        groupItemText.Text = $"#{groupItem.id} - {groupItem.name}";
                        groupItemText.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                        groupItemText.HorizontalAlignment = HorizontalAlignment.Center;
                        groupItemText.VerticalAlignment = VerticalAlignment.Center;
                        groupItemText.Margin = new Thickness(5);
                        stackPanel.Children.Add(groupItemText);
                        #endregion

                        #region groupItemButtonMinus
                        Button groupItemButtonMinus = new Button();
                        groupItemButtonMinus.DataContext = groupItem;
                        groupItemButtonMinus.Margin = new Thickness(2.5);
                        groupItemButtonMinus.Height =
                        groupItemButtonMinus.Width = 25;
                        groupItemButtonMinus.Padding = new Thickness(0);
                        groupItemButtonMinus.Background = Application.Current.Resources["veryLightGrey"] as SolidColorBrush;
                        groupItemButtonMinus.BorderThickness = new Thickness(0);
                        MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(groupItemButtonMinus, (new CornerRadius(5)));
                        #region materialDesign
                        var minusPackIcon = new PackIcon();
                        minusPackIcon.Foreground = Application.Current.Resources["ThickGrey"] as SolidColorBrush;
                        minusPackIcon.Height =
                        minusPackIcon.Width = 25;
                        minusPackIcon.Kind = PackIconKind.Minus;
                        groupItemButtonMinus.Content = minusPackIcon;
                        #endregion
                        groupItemButtonMinus.Click += extraItemButtonMinus_Click;
                        stackPanel.Children.Add(groupItemButtonMinus);
                        /////////////////////////////////
                        #endregion

                        #region groupItem_basicAmount
                        if (groupItem.basicAmount is null) groupItem.basicAmount = groupItem.start_amount;
                        TextBlock groupItem_basicAmount = new TextBlock();
                        var groupItem_basicAmountBinding = new System.Windows.Data.Binding("basicAmount");
                        groupItem_basicAmount.SetBinding(TextBlock.TextProperty, groupItem_basicAmountBinding);
                        groupItem_basicAmount.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                        groupItem_basicAmount.HorizontalAlignment = HorizontalAlignment.Center;
                        groupItem_basicAmount.VerticalAlignment = VerticalAlignment.Center;
                        groupItem_basicAmount.Margin = new Thickness(5);
                        stackPanel.Children.Add(groupItem_basicAmount);
                        #endregion
                        #region groupItemButtonPlus
                        Button groupItemButtonPlus = new Button();
                        groupItemButtonPlus.DataContext = groupItem;
                        groupItemButtonPlus.Margin = new Thickness(2.5);
                        groupItemButtonPlus.Height =
                        groupItemButtonPlus.Width = 25;
                        groupItemButtonPlus.Padding = new Thickness(0);
                        groupItemButtonPlus.Background = Application.Current.Resources["veryLightGrey"] as SolidColorBrush;
                        groupItemButtonPlus.BorderThickness = new Thickness(0);
                        MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(groupItemButtonPlus, (new CornerRadius(5)));
                        #region materialDesign
                        var plusPackIcon = new PackIcon();
                        plusPackIcon.Foreground = Application.Current.Resources["ThickGrey"] as SolidColorBrush;
                        plusPackIcon.Height =
                        plusPackIcon.Width = 25;
                        plusPackIcon.Kind = PackIconKind.Plus;
                        groupItemButtonPlus.Content = plusPackIcon;
                        #endregion
                        groupItemButtonPlus.Click += extraItemButtonPlus_Click;
                        stackPanel.Children.Add(groupItemButtonPlus);
                        /////////////////////////////////
                        #endregion
                        #region groupItem_add_price
                        TextBlock groupItem_add_price = new TextBlock();
                        var groupItem_add_priceBinding = new System.Windows.Data.Binding("add_price");
                        groupItem_add_price.SetBinding(TextBlock.TextProperty, groupItem_add_priceBinding);
                        groupItem_add_price.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                        groupItem_add_price.HorizontalAlignment = HorizontalAlignment.Center;
                        groupItem_add_price.VerticalAlignment = VerticalAlignment.Center;
                        groupItem_add_price.Margin = new Thickness(5);
                        stackPanel.Children.Add(groupItem_add_price);
                        #endregion
                        #region groupItemCurrency
                        TextBlock groupItemCurrency = new TextBlock();
                        groupItemCurrency.Text = AppSettings.currency;
                        groupItemCurrency.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                        groupItemCurrency.HorizontalAlignment = HorizontalAlignment.Center;
                        groupItemCurrency.VerticalAlignment = VerticalAlignment.Center;
                        groupItemCurrency.Margin = new Thickness(0, 5, 5, 5);
                        stackPanel.Children.Add(groupItemCurrency);
                        #endregion
                        #region groupItemBorder
                        Border groupItemBorder = new Border();
                        groupItemBorder.Margin = new Thickness(5);
                        groupItemBorder.Width = 1;
                        groupItemBorder.BorderThickness = new Thickness(0); ;
                        groupItemBorder.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;

                        stackPanel.Children.Add(groupItemBorder);
                        #endregion
                        wrapPanel.Children.Add(stackPanel);
                    }
                    sp_invItmOpsExtra.Children.Add(wrapPanel);
                    #endregion
                }
            }
        }

        void extraItemButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                GroupItemModel groupItem = button.DataContext as GroupItemModel;
                if (groupItem.basicAmount > 0)
                    groupItem.basicAmount--;
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        void extraItemButtonPlus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                GroupItemModel groupItem = button.DataContext as GroupItemModel;

                if (checkExtraItemsCount(groupItem.groupName))
                    groupItem.basicAmount++;


            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        private bool checkExtraItemsCount(string groupName)
        {
            var group= selectedInvItmOps.extraItems.Where(x => x.group_name == groupName).FirstOrDefault();
            var sumCount = group.group_items.Select(x => x.basicAmount).Sum();
            if (sumCount == group.group_count)
                return false;
            else
                return true;
        }
        #endregion
        void switchGrid1_1(string type)
        {



            // first level
            if (type == "mainItemsCatalog")
            {
                grid_mainItemsCatalog.Visibility = Visibility.Visible;
                grid_invItmOps.Visibility = Visibility.Collapsed;
                //wp_invItmOps.Visibility = Visibility.Collapsed;
            }
            else if (type == "invItmOps")
            {
                grid_mainItemsCatalog.Visibility = Visibility.Collapsed;
                grid_invItmOps.Visibility = Visibility.Visible;


                //btn_invItmOpsBack.Tag = "mainItemsCatalog";
                //txt_invItmOpsTitle.Text = "invItmOps";
                //wp_invItmOps.Visibility = Visibility.Visible;
                //sp_extraItems.Visibility = Visibility.Collapsed;
            }
            // second level
            /*
            else if (type == "extraItems")
            {
               
                btn_invItmOpsBack.Tag = "invItmOps";
                txt_invItmOpsTitle.Text = "extraItems";
                wp_invItmOps.Visibility = Visibility.Collapsed;
                //sp_extraItems.Visibility = Visibility.Visible;
            }
            */


        }

        #endregion
        #region grid2_1


        private void btn_external_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoice != null)
                {
                    if (invoice.external.Equals("1"))
                    {
                        invoice.external = "0";

                        btn_external.Background = Application.Current.Resources["White"] as SolidColorBrush;
                        path_external.Fill = Application.Current.Resources["MainColor"] as SolidColorBrush;
                        //txt_external.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                    }
                    else
                    {
                        invoice.external = "1";
                        btn_external.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                        path_external.Fill = Application.Current.Resources["White"] as SolidColorBrush;
                        //txt_external.Foreground = Application.Current.Resources["White"] as SolidColorBrush;

                    }
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void btn_takeAway_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoice != null)
                {
                    if (invoice.takeaway.Equals("1"))
                    {
                        invoice.takeaway = "0";
                        btn_takeAway.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                        path_takeAway.Fill = Application.Current.Resources["White"] as SolidColorBrush;
                        //txt_takeAway.Foreground = Application.Current.Resources["White"] as SolidColorBrush;

                    }
                    else
                    {
                        invoice.takeaway = "1";
                        btn_takeAway.Background = Application.Current.Resources["White"] as SolidColorBrush;
                        path_takeAway.Fill = Application.Current.Resources["MainColor"] as SolidColorBrush;
                        //txt_takeAway.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                    }
                }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
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
        #region buildInvoiceDetailsBig
        private void refreshInvoiceDetailsBig()
        {
            try
            {
                int index = 1 ;
                foreach(var item in invoiceDetailsList)
                {
                    item.index = index;
                    index++;
                }
                dg_invoiceDetailsBig.ItemsSource = invoiceDetailsList;
                dg_invoiceDetailsBig.Items.Refresh();
            }
            catch { }
        }
        private void Dg_invoiceDetailsBig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                HelpClass.StartAwait(grid_main);
                //selection
                if (dg_invoiceDetailsBig.SelectedIndex != -1)
                {
                    selectedInvItmOps = dg_invoiceDetailsBig.SelectedItem as ItemModel;
                    //wp_invItmOpsSetting.DataContext = selectedInvItmOps;
                    //switchGrid1_1("invItmOps");
                    showInvItmOps();

                }
                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void locationInvoiceItemRowinDatagrid(object sender, RoutedEventArgs e)
        {
            /*
            try
            {
                HelpClass.StartAwait(grid_main);

                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow)
                    {

                        btn_addSupplierPhone.IsEnabled = false;
                        dg_supplierPhone.IsEnabled = false;
                        SupplierPhone row = (SupplierPhone)dg_supplierPhone.SelectedItems[0];
                        SupplierPhones.Remove(row);
                        RefreshSupplierPhoneDataGrid();
                    }

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                dg_supplierPhone.IsEnabled = true;
                btn_addSupplierPhone.IsEnabled = true;
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
            */
        }
        private void deleteInvoiceItemRowinDatagrid(object sender, RoutedEventArgs e)
        {
         
            try
            {
                HelpClass.StartAwait(grid_main);

                for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                    if (vis is DataGridRow)
                    {

                        //btn_addSupplierPhone.IsEnabled = false;
                        //dg_invoiceDetailsBig.IsEnabled = false;
                        ItemModel row = (ItemModel)dg_invoiceDetailsBig.SelectedItems[0];
                        invoiceDetailsList.Remove(row);
                        refreshInvoiceDetailsBig();

                        if (selectedInvItmOps.index == row.index)
                            switchGrid1_1("mainItemsCatalog");

                        CalculateInvoiceValues();
                    }

                HelpClass.EndAwait(grid_main);
            }
            catch (Exception ex)
            {
                HelpClass.EndAwait(grid_main);
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        
        }
        #endregion
        List<ItemModel> invoiceDetailsList = new List<ItemModel>();
        /*
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

            public string id;
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
            //public string extra;
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
        */
        /*
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
        */
        #region buildInvoiceDetailsSmall
        async void buildInvoiceDetailsSmall(List<ItemModel> invoiceDetailsList)
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
                gridMain.Margin = new Thickness(5, 5, 5, 5);

                #region gridSettings
                /////////////////////////////////////////////////////
                int rowCount = 5;
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
                gridRow0.Margin = new Thickness(5, 2.5, 5, 2.5);
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
                //cdRow0[2].Width = new GridLength(1, GridUnitType.Auto);
                for (int i = 0; i < colCountRow0; i++)
                {
                    gridRow0.ColumnDefinitions.Add(cdRow0[i]);
                }
                #endregion

                #region itemIndex
                TextBlock itemIndex = new TextBlock();
                itemIndex.Text = $"{item.index}-";
                itemIndex.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                itemIndex.HorizontalAlignment = HorizontalAlignment.Center;
                itemIndex.VerticalAlignment = VerticalAlignment.Center;
                itemIndex.Margin = new Thickness(5, 5, 0, 5);
                itemIndex.TextWrapping = TextWrapping.WrapWithOverflow;
                itemIndex.TextAlignment = TextAlignment.Center;
                Grid.SetColumn(itemIndex, 0);
                gridRow0.Children.Add(itemIndex);
                #endregion

                #region itemName
                TextBlock itemName = new TextBlock();
                if (item.name.Length < 21)
                    itemName.Text = item.name;
                else
                    itemName.Text = item.name.Substring(0, 17) + "...";
                itemName.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                itemName.Margin = new Thickness(5);

                Grid.SetColumn(itemName, 1);
                gridRow0.Children.Add(itemName);
                #endregion
                #region itemUnit
                if (!string.IsNullOrWhiteSpace(item.unit_name))
                {
                    TextBlock itemUnit = new TextBlock();
                    itemUnit.Text = item.unit_name;
                    itemUnit.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                    itemUnit.Margin = new Thickness(5);

                    Grid.SetColumn(itemUnit, 2);
                    gridRow0.Children.Add(itemUnit);
                }
                #endregion


                gridMain.Children.Add(gridRow0);
                #endregion
                #region gridRow1


                Grid gridRow1 = new Grid();
                gridRow1.Margin = new Thickness(5, 2.5, 5, 2.5);
                #region gridSettings
                /////////////////////////////////////////////////////
                int colCountRow1 = 9;
                ColumnDefinition[] cdRow1 = new ColumnDefinition[colCountRow1];
                for (int i = 0; i < colCountRow1; i++)
                {
                    cdRow1[i] = new ColumnDefinition();
                }
                cdRow1[0].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[1].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[2].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[3].Width = new GridLength(1, GridUnitType.Star);
                cdRow1[4].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[5].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[6].Width = new GridLength(1, GridUnitType.Star);
                cdRow1[7].Width = new GridLength(1, GridUnitType.Auto);
                cdRow1[8].Width = new GridLength(1, GridUnitType.Auto);

                for (int i = 0; i < colCountRow1; i++)
                {
                    gridRow1.ColumnDefinitions.Add(cdRow1[i]);
                }

                #endregion
                #region itemCount
                TextBlock itemCount = new TextBlock();
                var itemCountBinding = new System.Windows.Data.Binding("amount");
                itemCount.SetBinding(TextBlock.TextProperty, itemCountBinding);
                itemCount.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemCount.HorizontalAlignment = HorizontalAlignment.Center;
                itemCount.VerticalAlignment = VerticalAlignment.Center;
                itemCount.Margin = new Thickness(5);
                Grid.SetColumn(itemCount, 0);
                gridRow1.Children.Add(itemCount);
                #endregion
                #region itemCountIcon
                Path itemCountIcon = new Path();
                itemCountIcon.Fill = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemCountIcon.Stretch = Stretch.Fill;
                itemCountIcon.FlowDirection = FlowDirection.LeftToRight;
                itemCountIcon.Height =
                itemCountIcon.Width = 12;
                itemCountIcon.Data = App.Current.Resources["close"] as Geometry;

                Grid.SetColumn(itemCountIcon, 1);
                gridRow1.Children.Add(itemCountIcon);
                #endregion
                #region stackPanelPrice
                StackPanel stackPanelPrice = new StackPanel();
                stackPanelPrice.Orientation = Orientation.Horizontal;
                #region itemPrice
                TextBlock itemPrice = new TextBlock();
                var itemPriceBinding = new System.Windows.Data.Binding("price");
                itemPriceBinding.Mode = BindingMode.OneWay;
                itemPriceBinding.Converter = new accuracyConverter();
                itemPrice.SetBinding(TextBlock.TextProperty, itemPriceBinding);
                //itemPrice.FontSize = 14;
                //itemPrice.FontWeight = FontWeights.SemiBold;
                itemPrice.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemPrice.HorizontalAlignment = HorizontalAlignment.Center;
                itemPrice.VerticalAlignment = VerticalAlignment.Center;
                itemPrice.Margin = new Thickness(5, 0, 5, 0);

                stackPanelPrice.Children.Add(itemPrice);
                #endregion
                #region itemPriceAccuracy
                TextBlock itemPriceAccuracy = new TextBlock();
                itemPriceAccuracy.Text = AppSettings.currency;
                //itemPriceAccuracy.FontSize = 14;
                //itemPriceAccuracy.FontWeight = FontWeights.SemiBold;
                itemPriceAccuracy.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemPriceAccuracy.HorizontalAlignment = HorizontalAlignment.Center;
                itemPriceAccuracy.VerticalAlignment = VerticalAlignment.Center;
                itemPriceAccuracy.Margin = new Thickness(0);

                stackPanelPrice.Children.Add(itemPriceAccuracy);
                #endregion
                Grid.SetColumn(stackPanelPrice, 2);
                gridRow1.Children.Add(stackPanelPrice);
                #endregion

                #region itemDiscount
                TextBlock itemDiscount = new TextBlock();
                var itemDiscountBinding = new System.Windows.Data.Binding("discount");
                itemDiscount.SetBinding(TextBlock.TextProperty, itemDiscountBinding);
                itemDiscount.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemDiscount.HorizontalAlignment = HorizontalAlignment.Center;
                itemDiscount.VerticalAlignment = VerticalAlignment.Center;
                itemDiscount.Margin = new Thickness(5);
                Grid.SetColumn(itemDiscount, 4);
                gridRow1.Children.Add(itemDiscount);
                #endregion
                #region itemDiscountIcon
                Path itemDiscountIcon = new Path();
                itemDiscountIcon.Fill = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemDiscountIcon.Stretch = Stretch.Fill;
                itemDiscountIcon.FlowDirection = FlowDirection.LeftToRight;
                itemDiscountIcon.Height =
                itemDiscountIcon.Width = 25;
                itemDiscountIcon.Data = App.Current.Resources["discount"] as Geometry;

                Grid.SetColumn(itemDiscountIcon, 5);
                gridRow1.Children.Add(itemDiscountIcon);
                #endregion

                #region itemBonus
                TextBlock itemBonus = new TextBlock();
                var itemBonusBinding = new System.Windows.Data.Binding("bonus");
                itemBonus.SetBinding(TextBlock.TextProperty, itemBonusBinding);
                itemBonus.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemBonus.HorizontalAlignment = HorizontalAlignment.Center;
                itemBonus.VerticalAlignment = VerticalAlignment.Center;
                itemBonus.Margin = new Thickness(5);
                Grid.SetColumn(itemBonus, 7);
                gridRow1.Children.Add(itemBonus);
                #endregion
                #region itemBonusIcon
                Path itemBonusIcon = new Path();
                itemBonusIcon.Fill = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemBonusIcon.Stretch = Stretch.Fill;
                itemBonusIcon.FlowDirection = FlowDirection.LeftToRight;
                itemBonusIcon.Height =
                itemBonusIcon.Width = 25;
                itemBonusIcon.Data = App.Current.Resources["bonus"] as Geometry;

                Grid.SetColumn(itemBonusIcon, 8);
                gridRow1.Children.Add(itemBonusIcon);
                #endregion





                Grid.SetRow(gridRow1, 1);
                gridMain.Children.Add(gridRow1);
                #endregion
                #region stackPanelRow2
                StackPanel stackPanelRow2 = new StackPanel();
                stackPanelRow2.Margin = new Thickness(5,0,5,0);
                #region addsItems
                if (item.addsItems != null)
                    foreach (var extra in item.addsItems)
                    {
                        TextBlock groupItemText = new TextBlock();
                        groupItemText.Text = "";
                        foreach (var groupItem in extra.group_items)
                        {
                            string itemString = "";
                            if (groupItem.basicAmount is null) groupItem.basicAmount = groupItem.start_amount;
                            itemString = $"{groupItem.name} ({groupItem.basicAmount}) {groupItem.add_price} {AppSettings.currency}";
                            //if (string.IsNullOrWhiteSpace(groupItem.unit))
                            //    itemString = groupItem.name;
                            //else
                            //    itemString = $"{groupItem.name} - {GeneralInfoService.GeneralInfo.units[groupItem.unit]} ({groupItem.basicAmount})";

                            groupItemText.Text +=$"+ {itemString} | ";
                        }
                        groupItemText.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                        groupItemText.HorizontalAlignment = HorizontalAlignment.Left;
                        groupItemText.VerticalAlignment = VerticalAlignment.Center;
                        groupItemText.Margin = new Thickness(0, 2.5, 0, 2.5);
                        groupItemText.TextWrapping = TextWrapping.WrapWithOverflow;
                        groupItemText.TextAlignment = TextAlignment.Center;
                        stackPanelRow2.Children.Add(groupItemText);



                    }
                #endregion
                #region deletesItems
                if (item.deletesItems != null)
                    foreach (var extra in item.deletesItems)
                    {
                        TextBlock groupItemText = new TextBlock();
                        groupItemText.Text = "";
                        foreach (var groupItem in extra.group_items)
                        {
                            string itemString = "";
                            itemString = $"{groupItem.name} ({groupItem.basicAmount}) {groupItem.add_price} {AppSettings.currency}";
                            //if (string.IsNullOrWhiteSpace(groupItem.unit))
                            //    itemString = groupItem.name;
                            //else
                            //    itemString = $"{groupItem.name} - {GeneralInfoService.GeneralInfo.units[groupItem.unit]} ({groupItem.basicAmount})";

                            groupItemText.Text += $"- {itemString} | ";
                        }
                        groupItemText.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                        groupItemText.HorizontalAlignment = HorizontalAlignment.Left;
                        groupItemText.VerticalAlignment = VerticalAlignment.Center;
                        groupItemText.Margin = new Thickness(0, 2.5, 0, 2.5);
                        groupItemText.TextWrapping = TextWrapping.WrapWithOverflow;
                        groupItemText.TextAlignment = TextAlignment.Center;
                        stackPanelRow2.Children.Add(groupItemText);
                    }
                #endregion
                Grid.SetRow(stackPanelRow2, 2);
                gridMain.Children.Add(stackPanelRow2);
                #endregion
                #region buttonInfo
                Rectangle rectangleInfo = new Rectangle();
                Grid.SetRowSpan(rectangleInfo, 4);
                gridMain.Children.Add(rectangleInfo);

                Button buttonInfo = new Button();
                buttonInfo.DataContext = item;
                buttonInfo.Height = rectangleInfo.Height;
                buttonInfo.Background = null;
                buttonInfo.BorderBrush = null;
                buttonInfo.Click += buttonInfo_Click;

                Grid.SetRowSpan(buttonInfo, 4);
                gridMain.Children.Add(buttonInfo);
                #endregion
                #region gridRow3
                Grid gridRow3 = new Grid();
                gridRow3.Margin = new Thickness(2.5);
                #region gridSettings
                /////////////////////////////////////////////////////
                int colCountRow3 = 4;
                ColumnDefinition[] cdRow3 = new ColumnDefinition[colCountRow3];
                for (int i = 0; i < colCountRow3; i++)
                {
                    cdRow3[i] = new ColumnDefinition();
                }
                cdRow3[0].Width = new GridLength(1, GridUnitType.Auto);
                cdRow3[1].Width = new GridLength(1, GridUnitType.Star);
                cdRow3[2].Width = new GridLength(1, GridUnitType.Auto);
                cdRow3[3].Width = new GridLength(1, GridUnitType.Auto);
                for (int i = 0; i < colCountRow3; i++)
                {
                    gridRow3.ColumnDefinitions.Add(cdRow3[i]);
                }
                #endregion

                #region borderNotes
                /*
                Border borderNotes = new Border();
                borderNotes.CornerRadius = new CornerRadius(0);
                borderNotes.Margin = new Thickness(5);
                borderNotes.BorderThickness = new Thickness(0,0,0,1);
                borderNotes.BorderBrush = Application.Current.Resources["Grey"] as SolidColorBrush;
                borderNotes.Padding = new Thickness(0);
                borderNotes.Background = null;
                #region comboBoxNotes
                
                ComboBox comboBoxNotes = new ComboBox();
                var comboBoxNotesBinding = new System.Windows.Data.Binding("detail");
                comboBoxNotesBinding.Mode = BindingMode.TwoWay;
                comboBoxNotes.SetBinding(ComboBox.TextProperty, comboBoxNotesBinding);
                comboBoxNotes.Padding = new Thickness(10, 0, 5, 0);
                comboBoxNotes.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                comboBoxNotes.Style = Application.Current.Resources["MaterialDesignFloatingHintComboBox"] as Style; ;
                comboBoxNotes.Height = 40;
                MaterialDesignThemes.Wpf.HintAssist.SetHint(comboBoxNotes, Translate.getResource("28"));
                comboBoxNotes.BorderThickness = new Thickness(0);
                comboBoxNotes.Margin = new Thickness(0);
                comboBoxNotes.IsEditable = true;
                comboBoxNotes.StaysOpenOnEdit = true;
                comboBoxNotes.IsTextSearchEnabled = false;
                comboBoxNotes.KeyUp += Cb_invoiceItemNotes_KeyUp;
               
                borderNotes.Child = comboBoxNotes;
                #endregion

                Grid.SetColumn(borderNotes, 0);
                gridRow3.Children.Add(borderNotes);
                */
                #endregion

                #region urgent

                Button buttonUrgent = new Button();
                buttonUrgent.DataContext = item;
                buttonUrgent.Margin = new Thickness(5);
                buttonUrgent.Padding = new Thickness(5);
                buttonUrgent.BorderBrush = null;
                buttonUrgent.Content = Translate.getResource("2252");
                //if (item.isUrgent)
                //    buttonUrgent.Background = Application.Current.Resources["mediumRed"] as SolidColorBrush;
                //else
                //    buttonUrgent.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;
                ////Background = "{Binding isUrgent, Mode=OneWay, Converter={StaticResource urgentColorConverter },UpdateSourceTrigger=PropertyChanged}"
                var buttonUrgentColorBinding = new System.Windows.Data.Binding("isUrgent");
                buttonUrgentColorBinding.Mode = BindingMode.OneWay;
                buttonUrgentColorBinding.Converter = new urgentColorConverter();
                buttonUrgent.SetBinding(Button.BackgroundProperty,buttonUrgentColorBinding);



                buttonUrgent.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonUrgent, (new CornerRadius(7)));


                buttonUrgent.BorderThickness = new Thickness(0);

                buttonUrgent.Click += buttonUrgent_Click;
                Grid.SetColumn(buttonUrgent, 0);
                gridRow3.Children.Add(buttonUrgent);
                #endregion
                #region stackPanelTotal
                StackPanel stackPanelTotal = new StackPanel();
                stackPanelTotal.Orientation = Orientation.Horizontal;
                #region textTotal
                TextBlock textTotal = new TextBlock();
                var textTotalBinding = new System.Windows.Data.Binding("total");
                textTotalBinding.Mode = BindingMode.OneWay;
                textTotalBinding.Converter = new accuracyConverter();
                textTotal.SetBinding(TextBlock.TextProperty, textTotalBinding);

                textTotal.FontSize = 14;
                textTotal.FontWeight = FontWeights.Bold;
                textTotal.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                textTotal.HorizontalAlignment = HorizontalAlignment.Center;
                textTotal.VerticalAlignment = VerticalAlignment.Center;
                textTotal.Margin = new Thickness(5);

                stackPanelTotal.Children.Add(textTotal);
                #endregion
                #region textTotalAccuracy
                TextBlock textTotalAccuracy = new TextBlock();
                textTotalAccuracy.Text = AppSettings.currency;
                textTotalAccuracy.FontSize = 14;
                textTotalAccuracy.FontWeight = FontWeights.Bold;
                textTotalAccuracy.Foreground = Application.Current.Resources["MainColor"] as SolidColorBrush;
                textTotalAccuracy.HorizontalAlignment = HorizontalAlignment.Center;
                textTotalAccuracy.VerticalAlignment = VerticalAlignment.Center;
                textTotalAccuracy.Margin = new Thickness(5);

                stackPanelTotal.Children.Add(textTotalAccuracy);
                #endregion
                Grid.SetColumn(stackPanelTotal, 1);
                gridRow3.Children.Add(stackPanelTotal);
                #endregion
                #region   close
                Button buttonClose = new Button();
                buttonClose.Tag = "close-" + item.index;
                buttonClose.Margin = new Thickness(2.5);
                buttonClose.Height =
                buttonClose.Width = 25;
                buttonClose.Padding = new Thickness(0);
                buttonClose.Background = Application.Current.Resources["Red"] as SolidColorBrush;
                buttonClose.BorderThickness = new Thickness(0);
                MaterialDesignThemes.Wpf.ButtonAssist.SetCornerRadius(buttonClose, (new CornerRadius(25)));
                #region materialDesign
                var ClosePackIcon = new PackIcon();
                ClosePackIcon.Tag = "closePackIcon-" + item.product_id;
                ClosePackIcon.Foreground = Application.Current.Resources["White"] as SolidColorBrush;
                ClosePackIcon.Height =
                ClosePackIcon.Width = 25;
                ClosePackIcon.Kind = PackIconKind.Close;
                buttonClose.Content = ClosePackIcon;
                #endregion
                buttonClose.Click += buttonClose_Click;
                Grid.SetColumn(buttonClose, 2);
                gridRow3.Children.Add(buttonClose);
                /////////////////////////////////
                #endregion


                Grid.SetRow(gridRow3, 3);
                gridMain.Children.Add(gridRow3);
                #endregion
                #region gridRow4
                //if (!string.IsNullOrEmpty(item.notes))
                //{ 
                Grid gridRow4 = new Grid();
                gridRow4.Margin = new Thickness(5, 0, 5, 0);

                #region itemotes
                TextBlock itemNotes = new TextBlock();
                itemNotes.Text = item.notes;
                var itemNotesBinding = new System.Windows.Data.Binding("notes");
                itemNotes.SetBinding(TextBlock.TextProperty, itemNotesBinding);
                itemNotes.Foreground = Application.Current.Resources["textColor"] as SolidColorBrush;
                itemNotes.Margin = new Thickness(10, 0, 10, 0);
                itemNotes.TextWrapping = TextWrapping.Wrap;
                itemNotes.TextAlignment = TextAlignment.Left;
                gridRow4.Children.Add(itemNotes);
                #endregion
                Grid.SetRow(gridRow4, 4);
                gridMain.Children.Add(gridRow4);
                //}
                #endregion


                borderMain.Child = gridMain;
                #endregion
                sp_invoiceDetailsSmall.Children.Add(borderMain);
                #endregion
                index++;
            }
        }
        void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ItemModel invOptItem = button.DataContext as ItemModel;

                //int index = int.Parse(button.Tag.ToString().Replace("close-", ""));
                int index = invOptItem.index;

                invoiceDetailsList.RemoveAt(index - 1);

                if(selectedInvItmOps.index == invOptItem.index)
                switchGrid1_1("mainItemsCatalog");

                if (AppSettings.invoiceDetailsType == "small")
                    buildInvoiceDetailsSmall(invoiceDetailsList);
                else
                    refreshInvoiceDetailsBig();
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
                ItemModel invoiceDetails = button.DataContext as ItemModel;
                selectedInvItmOps = invoiceDetails;
                //wp_invItmOpsSetting.DataContext = selectedInvItmOps;
                //switchGrid1_1("invItmOps");
                showInvItmOps();

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }

        
         void buttonUrgent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                var invoiceDetails = new ItemModel();
                if (DataContext != null)
                    invoiceDetails = button.DataContext as ItemModel;
                else
                {

                    selectedInvItmOps = button.DataContext as ItemModel;

                    invoiceDetails = selectedInvItmOps;
                }
                invoiceDetails.isUrgent = !invoiceDetails.isUrgent;
                //if (invoiceDetails.isUrgent)
                //    button.Background = Application.Current.Resources["mediumRed"] as SolidColorBrush;
                //else
                //    button.Background = Application.Current.Resources["MainColor"] as SolidColorBrush;

            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private void Cb_invoiceItemNotes_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                ComboBox cbm = sender as ComboBox;
                HelpClass.searchInComboBox(cbm);
            }
            catch (Exception ex)
            { HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name); }
        }
        #endregion


        #endregion
        #region invoice
        private void calculateItemPrice()
        {
            decimal totalPrice =  selectedInvItmOps.price;
            //foreach(var group in selectedInvItmOps.extraItems) // extra groups
            //{
            //    foreach(var item in group.group_items)
            //    {

            //        if ( item.basicAmount > (item.start_amount + item.add_price_amount) && item.add_price_amount != 0)
            //        {
            //            var sup = (int)item.basicAmount - (item.start_amount + item.add_price_amount);
            //            totalPrice += sup * item.add_price;

            //        }
            //    }
            //}

            foreach (var group in selectedInvItmOps.addsItems) // adds groups
            {
                foreach (var item in group.group_items)
                {

                    if (item.basicAmount > (item.start_amount + item.add_price_amount) && item.add_price_amount != 0)
                    {
                        var sup = (int)item.basicAmount - (item.start_amount + item.add_price_amount);
                        totalPrice += sup * item.add_price;

                    }
                }
            }
          
            totalPrice = totalPrice * selectedInvItmOps.amount;
           selectedInvItmOps.total = totalPrice;
            CalculateInvoiceValues();
        }
        private void CalculateInvoiceValues()
        {
            decimal total = invoiceDetailsList.Select(x => x.total).Sum();

            //service
            decimal serviceAmount = HelpClass.calcPercentage(total, GeneralInfoService.GeneralInfo.MainOp.service);
            invoice.service = serviceAmount;
            decimal totalAfterService = total + serviceAmount;

            #region tax
            decimal taxAmount = 0;
            decimal taxPercentage = 0;

            foreach(var item in invoiceDetailsList)
            {
                taxAmount += HelpClass.calcPercentage(totalAfterService, item.tax_class) * item.amount;
            }
            if (taxAmount.Equals(0))//tax on invoice
            {
                taxAmount = HelpClass.calcPercentage(totalAfterService, GeneralInfoService.GeneralInfo.MainOp.vat);//tax value
                taxPercentage = GeneralInfoService.GeneralInfo.MainOp.vat;
            }
            else
            {
                if(totalAfterService != 0)
                    taxPercentage = (taxAmount * 100) / totalAfterService;
            }
  
            //tax
            invoice.vat_amount = taxAmount;
            invoice.vat = taxPercentage;


            #endregion
            decimal totalAfterTax = totalAfterService + taxAmount;

            #region discount
            decimal overDiscount = 0;
            decimal overDiscountPercentage = 0;

            //manual discount
           
            decimal manualDiscount = 0;
            decimal manualDiscountRate = 0;

            if (invoice.discountType == "rate")
            {
                manualDiscount = HelpClass.calcPercentage(totalAfterTax, invoice.manualDiscount);
                manualDiscountRate = invoice.manualDiscount;
            }
            else
            {
                manualDiscount = invoice.over_discount;
                manualDiscountRate = (manualDiscount * 100) / totalAfterTax;
            }
            overDiscount += manualDiscount;
        
            if(totalAfterTax != 0)
                overDiscountPercentage = (overDiscount * 100) / totalAfterTax;
            //over discount
            invoice.over_discount = overDiscount;
            invoice.over_discount_percentage = overDiscountPercentage;
            #endregion

            decimal totalNet = totalAfterTax - overDiscount;


            invoice.total_after_discount = totalNet;

            //display
            txt_Count.Text = invoiceDetailsList.Select(x => x.amount).Sum().ToString();
            txt_SupTotal.Text = HelpClass.DecTostring(invoiceDetailsList.Select(x => x.total).Sum());
            txt_Service.Text = HelpClass.DecTostring(serviceAmount);
            txt_taxValue.Text = HelpClass.DecTostring(taxAmount);
            txt_UserDiscountRate.Text = HelpClass.DecTostring(manualDiscountRate);
            txt_total.Text = HelpClass.DecTostring(totalNet);
        }

        private void tb_UserDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox tb = (TextBox)sender;
                if(tb.IsFocused)
                    CalculateInvoiceValues();
            }
            catch { }
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
        private async void tb_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                ItemModel item = GeneralInfoService.items.Where(x => x.product_id == tb_search.Text).FirstOrDefault();
                if (item == null)
                {
                    item = await _itemService.GetItemInfo(tb_search.Text, "1", invoice.CustomerId, GeneralInfoService.GeneralInfo.MainOp.price_id, tb_search.Text);

                }
                if (item != null)
                    AddItemToInvoice(item, new List<CategoryModel>(), new List<CategoryModel>(), new List<CategoryModel>());
                tb_search.Text = "";
                //HelpClass.EndAwait(grid_main);
            }
            }
            catch (Exception ex)
            {
                HelpClass.ExceptionMessage(ex, this, this.GetType().FullName, System.Reflection.MethodBase.GetCurrentMethod().Name);
            }
        }
        private  void Btn_search_Click(object sender, RoutedEventArgs e)
        {           
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_searchItem w = new wd_searchItem();
                w.ShowDialog();
                if (w.isOk)
                {
                    AddItemToInvoice(w.selectedItem, new List<CategoryModel>(), new List<CategoryModel>(), new List<CategoryModel>());
                }

                Window.GetWindow(this).Opacity = 1;
            }
            catch 
            {
                Window.GetWindow(this).Opacity = 1;
            }

        }
        private void Btn_searchBarcode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window.GetWindow(this).Opacity = 0.2;
                wd_searchItem w = new wd_searchItem();
                w.ShowDialog();
                if (w.isOk)
                {
                    AddItemToInvoice(w.selectedItem, new List<CategoryModel>(), new List<CategoryModel>(), new List<CategoryModel>());
                }

                Window.GetWindow(this).Opacity = 1;
            }
            catch
            {
                Window.GetWindow(this).Opacity = 1;
            }

        }

        #endregion

       
    }
}
