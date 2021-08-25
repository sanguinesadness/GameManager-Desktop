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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignApp.Views
{
    /// <summary>
    /// Interaction logic for ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {
        private int _charID;
        private int _playerID;

        public ShopView()
        {
            InitializeComponent();

            _charID = DataExchanger.SelectedCharacterID;
            _playerID = DataExchanger.CurrectUserID;
        }

        private void SetCharacterGold()
        {
            int goldAmount = (int)Gamemanager.ReadValue($"SELECT [gold_amount] FROM [player_characters] " +
                                            $"WHERE [character_id] = {_charID} AND [player_id] = {_playerID}");

            GoldBalance.Text = goldAmount.ToString();
        }

        private void SetItemCategories()
        {
            ItemCategories.Items.Clear();

            List<List<string>> categories = Gamemanager.GetItemCategories();

            foreach(List<string> category in categories)
            {
                int ID = int.Parse(category[0]);
                string name = category[1];
                string icon = category[2];

                ListViewItem item = CreateCategoryItem(ID, name, icon);
                ItemCategories.Items.Add(item);
            }
        }

        private void SetItemGrids()
        {
            ItemsGridContainer.Children.Clear();

            List<List<string>> categories = Gamemanager.GetItemCategories();

            foreach (List<string> category in categories)
            {
                int ID = int.Parse(category[0]);
                Border itemsGrid = CreateItemsGrid(ID);

                ItemsGridContainer.Children.Add(itemsGrid);
            }
        }

        private ListViewItem CreateCategoryItem(int ID, string name, string icon)
        {
            ListViewItem listItem = new ListViewItem()
            {
                Height = 80,
                Tag = ID
            };
            listItem.MouseUp += ItemCategory_MouseUp;

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            Image image = new Image()
            {
                Source = new BitmapImage(new Uri($"C:/Users/helio/source/repos/MaterialDesignApp/MaterialDesignApp/Images/ItemCategories/{icon}", UriKind.RelativeOrAbsolute)),
                Height = 40,
                Margin = new Thickness(125, 0, 15, 0)
            };
            RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);

            TextBlock textBlock = new TextBlock()
            {
                Text = name,
                FontSize = 28
            };

            stackPanel.Children.Add(image);
            stackPanel.Children.Add(textBlock);

            listItem.Content = stackPanel;

            return listItem;
        }

        private Border CreateItemCard(int itemID, string itemName, string image)
        {
            Border wrapper = new Border()
            {
                Tag = itemID,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(30, 25, 30, 25),
                Cursor = Cursors.Hand,
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF696969")),
                BorderThickness = new Thickness(1)
            };
            wrapper.MouseEnter += ItemCard_MouseEnter;
            wrapper.MouseLeave += ItemCard_MouseLeave;
            wrapper.PreviewMouseUp += ItemCard_PreviewMouseUp;
            wrapper.Effect = new DropShadowEffect()
            {
                Direction = -45,
                BlurRadius = 30,
                ShadowDepth = 15,
                Opacity = 0.4f
            };

            Grid overlayWrapper = new Grid();

            Border overlay = new Border()
            {
                CornerRadius = new CornerRadius(5)
            };
            overlay.Background = new ImageBrush()
            {
                Stretch = Stretch.Fill,
                ImageSource = new BitmapImage(new Uri($"C:/Users/helio/source/repos/MaterialDesignApp/MaterialDesignApp/Images/Items/{image}", UriKind.RelativeOrAbsolute)),
            };
            overlay.Effect = new BlurEffect() { Radius = 0 };

            TextBlock itemNameTextBlock = new TextBlock()
            {
                Opacity = 0,
                Text = itemName,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 18,
                TextWrapping = TextWrapping.Wrap,
                FontWeight = FontWeights.ExtraBold
            };
            itemNameTextBlock.Effect = new DropShadowEffect()
            {
                BlurRadius = 10,
                Opacity = 0.7
            };

            overlayWrapper.Children.Add(overlay);
            overlayWrapper.Children.Add(itemNameTextBlock);

            wrapper.Child = overlayWrapper;

            return wrapper;
        }

        private Border CreateItemsGrid(int categoryID)
        {
            Border wrapper = new Border()
            {
                Padding = new Thickness(50),
                Visibility = Visibility.Hidden,
                Tag = categoryID
            };

            Grid grid = new Grid();
            for (int i = 0; i < 5; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            for (int i = 0; i < 4; i++)
                grid.RowDefinitions.Add(new RowDefinition());

            wrapper.Child = grid;

            List<List<string>> items = Gamemanager.GetItemsByCategory(categoryID);

            int row = 0;
            int col = 0;
            foreach (List<string> item in items)
            {
                int ID = int.Parse(item[0]);
                string name = item[1];
                string image = item[2];

                Grid itemCardWrapper = new Grid();

                Grid.SetRow(itemCardWrapper, row);
                Grid.SetColumn(itemCardWrapper, col);

                Border itemCard = CreateItemCard(ID, name, image);
                itemCardWrapper.Children.Add(itemCard);

                grid.Children.Add(itemCardWrapper);

                col++;

                if (col >= grid.ColumnDefinitions.Count)
                {
                    row++;
                    col = 0;
                }

                if (row >= grid.RowDefinitions.Count)
                {
                    return wrapper;
                }
            }

            return wrapper;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetCharacterGold();
            SetItemCategories();
            SetItemGrids();      
        }

        private void ItemCard_MouseEnter(object sender, MouseEventArgs e)
        {
            Border item = sender as Border;

            Duration animDuration = TimeSpan.FromSeconds(0.5);

            // плавное перемещение вверх по оси Y
            TranslateTransform tr = new TranslateTransform();
            item.RenderTransform = tr;

            Point relativePoint = item.TransformToAncestor(item.Parent as Grid).Transform(new Point(0, 0));
            double marginTop = item.Margin.Top;

            DoubleAnimation translateY = new DoubleAnimation(relativePoint.Y - marginTop, -15, animDuration);
            translateY.EasingFunction = new QuadraticEase();

            tr.BeginAnimation(TranslateTransform.YProperty, translateY);

            // плавные размытие и появление надписи с названием предмета
            BlurEffect imageBorderBlur = ((item.Child as Grid).Children[0] as Border).Effect as BlurEffect;
            TextBlock itemNameBlock = (item.Child as Grid).Children[1] as TextBlock;

            DoubleAnimation blurAnim = new DoubleAnimation(imageBorderBlur.Radius, 5, animDuration);
            blurAnim.EasingFunction = new QuadraticEase();

            DoubleAnimation opacityAnim = new DoubleAnimation(itemNameBlock.Opacity, 1, animDuration);
            opacityAnim.EasingFunction = new QuadraticEase();

            imageBorderBlur.BeginAnimation(BlurEffect.RadiusProperty, blurAnim);
            itemNameBlock.BeginAnimation(OpacityProperty, opacityAnim);
        }

        private void ItemCard_MouseLeave(object sender, MouseEventArgs e)
        {
            Border item = sender as Border;

            Duration animDuration = TimeSpan.FromSeconds(0.5);

            // плавное перемещение вниз по оси Y
            TranslateTransform tr = new TranslateTransform();
            item.RenderTransform = tr;

            Point relativePoint = item.TransformToAncestor(item.Parent as Grid).Transform(new Point(0, 0));
            double marginTop = item.Margin.Top;

            DoubleAnimation translateY = new DoubleAnimation(relativePoint.Y - marginTop, 0, animDuration);
            translateY.EasingFunction = new QuadraticEase();

            tr.BeginAnimation(TranslateTransform.YProperty, translateY);

            // плавные исчезания размытости и надписи с названием предмета
            BlurEffect imageBorderBlur = ((item.Child as Grid).Children[0] as Border).Effect as BlurEffect;
            TextBlock itemNameBlock = (item.Child as Grid).Children[1] as TextBlock;

            DoubleAnimation blurAnim = new DoubleAnimation(imageBorderBlur.Radius, 0, animDuration);
            blurAnim.EasingFunction = new QuadraticEase();

            DoubleAnimation opacityAnim = new DoubleAnimation(itemNameBlock.Opacity, 0, animDuration);
            opacityAnim.EasingFunction = new QuadraticEase();

            imageBorderBlur.BeginAnimation(BlurEffect.RadiusProperty, blurAnim);
            itemNameBlock.BeginAnimation(OpacityProperty, opacityAnim);
        }

        private void ItemCard_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            int itemID = int.Parse((sender as Border).Tag.ToString());
            int itemPrice = (int)Gamemanager.ReadValue($"SELECT [price] FROM [items] WHERE [id] = {itemID}");
            string itemName = (((sender as Border).Child as Grid).Children[1] as TextBlock).Text;
            ImageSource imagePath = ((((sender as Border).Child as Grid).Children[0] as Border).Background as ImageBrush).ImageSource;

            ComboBoxItem1.IsSelected = true;
            DialogItemImage.ImageSource = imagePath;
            DialogItemName.Text = itemName;
            DialogItemName.Tag = itemID;
            DialogItemPrice.Text = (itemPrice * int.Parse(DialogItemQuantity.Text)).ToString();
            DialogItemPrice.Tag = itemPrice;

            ShowItemDialog();
        }

        private void ItemCategory_MouseUp(object sender, MouseButtonEventArgs e)
        {
            int categoryID = (int)(sender as ListViewItem).Tag;

            foreach (Border itemsGrid in ItemsGridContainer.Children)
            {
                if (categoryID == (int)itemsGrid.Tag)
                    itemsGrid.Visibility = Visibility.Visible;
                else
                    itemsGrid.Visibility = Visibility.Hidden;
            }
        }

        private void DialogItemQuantity_DropDownClosed(object sender, EventArgs e)
        {
            DialogItemPrice.Text = (int.Parse(DialogItemQuantity.Text) * int.Parse(DialogItemPrice.Tag.ToString())).ToString();
            NotEnoughMoneyError.Visibility = Visibility.Collapsed;
        }

        private void DialogPurchaseConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            int currentBalance = int.Parse(GoldBalance.Text);
            int purchaseAmount = int.Parse(DialogItemPrice.Text);

            int playerID = DataExchanger.CurrectUserID;
            int charID = DataExchanger.SelectedCharacterID;
            int itemID = int.Parse(DialogItemName.Tag.ToString());
            int itemAmount = int.Parse(DialogItemQuantity.Text);

            if (currentBalance < purchaseAmount)
            {
                NotEnoughMoneyError.Visibility = Visibility.Visible;
            }
            else
            {
                currentBalance -= purchaseAmount;

                if (!Gamemanager.AddNewItems(playerID, charID, itemID, itemAmount) || !Gamemanager.UpdateGoldBalance(playerID, charID, currentBalance))
                {
                    MessageBox.Show("Произошла непредвиденная ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ShowSuccessDialog();
                GoldBalance.Text = currentBalance.ToString();
            }
        }

        private void InventoryHyperlink_Click(object sender, RoutedEventArgs e)
        {
            DataExchanger.OpenInventory = true;
            (DataExchanger.ManagementWindow as UserWindow).OpenMyCharactersPage();
        }

        private void ShowItemDialog()
        {
            DialogHost.IsOpen = true;
            ItemInfoDialog.Visibility = Visibility.Visible;
            SuccessfulPurchaseDialog.Visibility = Visibility.Collapsed;
            NotEnoughMoneyError.Visibility = Visibility.Collapsed;
        }

        private void ShowSuccessDialog()
        {
            DialogHost.IsOpen = true;
            ItemInfoDialog.Visibility = Visibility.Collapsed;
            SuccessfulPurchaseDialog.Visibility = Visibility.Visible;
        }

        private void NotEnoughMoneyError_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NotEnoughMoneyError.Visibility = Visibility.Collapsed;
        }
    }
}