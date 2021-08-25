using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignApp.Views
{
    enum AnimationType
    {
        RightToLeft,
        LeftToRight
    }

    /// <summary>
    /// Interaction logic for MyCharactersView.xaml
    /// </summary>
    public partial class MyCharactersView : UserControl
    {
        private Brush _selectionBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C673AB7"));
        private Brush _transparentBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));

        private string _userLogin;
        private int _playerID;

        private bool isHeroNameCorrect = false;

        private List<List<string>> _availableCharacters;
        private List<List<string>> _playerCharacters;

        // значения { playerID, charID, name } которые идентифицируют персонажа игрока
        private List<string> _selectedCharacter = new List<string>(3);

        public MyCharactersView()
        {
            InitializeComponent();

            _userLogin = DataExchanger.CurrentUserLogin;
            _playerID = DataExchanger.CurrectUserID;

            UpdateCharacterList();
            UpdateCharacterGrid();

            if (DataExchanger.OpenInventory)
            {
                CharacterInfo.Visibility = Visibility.Hidden;
                Inventory.Visibility = Visibility.Visible;
                FillInventory();

                DataExchanger.OpenInventory = false;
            }
        }

        private Grid CreateCharacterItem(string charName, int charID, string iconName)
        {
            Grid mainGrid = new Grid();
            mainGrid.Margin = new Thickness(0, 0, 0, 50);
            mainGrid.Name = charName;

            Rectangle selectionRect = new Rectangle();
            selectionRect.Height = 45;
            selectionRect.Fill = _transparentBrush;
            selectionRect.VerticalAlignment = VerticalAlignment.Center;
            Panel.SetZIndex(selectionRect, 0);

            Border wrapper = new Border();
            wrapper.Tag = charID;
            wrapper.CornerRadius = new CornerRadius(5);
            wrapper.Width = 400;
            wrapper.Height = 85;
            wrapper.Padding = new Thickness(15, 10, 15, 10);
            wrapper.Cursor = Cursors.Hand;
            Panel.SetZIndex(wrapper, 1);
            wrapper.MouseEnter += CharacterItem_MouseEnter;
            wrapper.MouseLeave += CharacterItem_MouseLeave;
            wrapper.MouseDown += CharacterItem_MouseDown;

            wrapper.Effect = new DropShadowEffect();
            DropShadowEffect wrapperShadow = wrapper.Effect as DropShadowEffect;
            wrapperShadow.Direction = -45;
            wrapperShadow.BlurRadius = 30;
            wrapperShadow.ShadowDepth = 15;
            wrapperShadow.Opacity = 0.4;

            wrapper.Background = new LinearGradientBrush(
                (Color)ColorConverter.ConvertFromString("#681565"),
                (Color)ColorConverter.ConvertFromString("#673AB7"),
                new Point(0, 0),
                new Point(1, 1));

            StackPanel stackPanel = new StackPanel();
            stackPanel.Orientation = Orientation.Horizontal;

            Border iconWrapper = new Border()
            {
                CornerRadius = new CornerRadius(5),
                Width = 110,
                Height = 60
            };
            iconWrapper.Background = new ImageBrush()
            {
                Stretch = Stretch.Fill,
                ImageSource = new BitmapImage(new Uri($"C:/Users/helio/source/repos/MaterialDesignApp/MaterialDesignApp/Images/HeroIcons/{iconName}", UriKind.RelativeOrAbsolute)),
            };
            RenderOptions.SetBitmapScalingMode(iconWrapper.Background, BitmapScalingMode.HighQuality);

            TextBlock textBlock = new TextBlock()
            {
                Text = charName,
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 26,
                TextAlignment = TextAlignment.Left,
                TextWrapping = TextWrapping.NoWrap,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(15, 0, 15, 0),
                FontWeight = FontWeights.SemiBold
            };

            stackPanel.Children.Add(iconWrapper);
            stackPanel.Children.Add(textBlock);

            wrapper.Child = stackPanel;

            mainGrid.Children.Add(selectionRect);
            mainGrid.Children.Add(wrapper);

            return mainGrid;
        }

        private Border CreateCharacterCard(string charName, string avatarName)
        {
            string charNameNoSpaces = Regex.Replace(charName, @"\s+", "");

            Border wrapper = new Border()
            {
                Name = charNameNoSpaces,
                CornerRadius = new CornerRadius(5),
                Margin = new Thickness(40, 20, 40, 20),
                Cursor = Cursors.Hand,
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF696969")),
                BorderThickness = new Thickness(1)
            };
            wrapper.MouseEnter += CharacterCard_MouseEnter;
            wrapper.MouseLeave += CharacterCard_MouseLeave;
            wrapper.PreviewMouseUp += CharacterCard_PreviewMouseUp;
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
                ImageSource = new BitmapImage(new Uri($"C:/Users/helio/source/repos/MaterialDesignApp/MaterialDesignApp/Images/HeroAvatars/{avatarName}", UriKind.RelativeOrAbsolute)),
            };
            overlay.Effect = new BlurEffect() { Radius = 0 };

            TextBlock characterNameBlock = new TextBlock()
            {
                Opacity = 0,
                Text = charName,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 20,
                TextWrapping = TextWrapping.Wrap,
                FontWeight = FontWeights.ExtraBold
            };
            characterNameBlock.Effect = new DropShadowEffect()
            {
                BlurRadius = 10,
                Opacity = 0.7
            };

            overlayWrapper.Children.Add(overlay);
            overlayWrapper.Children.Add(characterNameBlock);

            wrapper.Child = overlayWrapper;

            return wrapper;
        }

        private void UpdateCharacterList()
        {
            if (!Gamemanager.UserExists(_playerID))
                return;

            _playerCharacters = Gamemanager.GetPlayerCharactersV1(_playerID);

            CharacterList.Children.Clear();
            foreach (List<string> charInfo in _playerCharacters)
            {
                int charID = int.Parse(charInfo[1]);
                string iconName = Gamemanager.GetCharacterInfo(charID, "icon_img");
                string charName = charInfo[0];

                Grid charElement = CreateCharacterItem(charName, charID, iconName);
                CharacterList.Children.Add(charElement);
            }
        }

        private void UpdateCharacterGrid()
        {
            CharacterGrid.Children.Clear();

            _availableCharacters = Gamemanager.GetAvailableCharacters();

            int row = 0;
            int col = 0;

            foreach (var character in _availableCharacters)
            {
                string charName = character[1];
                string avatarName = character[3];

                Border charCard = CreateCharacterCard(charName, avatarName);

                Grid cardWrapper = new Grid();
                Grid.SetRow(cardWrapper, row);
                Grid.SetColumn(cardWrapper, col);
                cardWrapper.Children.Add(charCard);

                CharacterGrid.Children.Add(cardWrapper);

                col++;

                if (col >= CharacterGrid.ColumnDefinitions.Count)
                {
                    row++;
                    col = 0;
                }

                if (row >= CharacterGrid.RowDefinitions.Count)
                {
                    return;
                }
            }
        }

        private void CharacterItem_MouseEnter(object sender, MouseEventArgs e)
        {
            Border item = sender as Border;

            TranslateTransform tr = new TranslateTransform();
            item.RenderTransform = tr;

            Point relativePoint = item.TransformToAncestor(item.Parent as Grid).Transform(new Point(0, 0));

            DoubleAnimation translateY = new DoubleAnimation(relativePoint.Y, -10, TimeSpan.FromSeconds(0.5));
            translateY.EasingFunction = new QuadraticEase();

            tr.BeginAnimation(TranslateTransform.YProperty, translateY);
        }

        private void CharacterItem_MouseLeave(object sender, MouseEventArgs e)
        {
            Border item = sender as Border;

            TranslateTransform tr = new TranslateTransform();
            item.RenderTransform = tr;

            Point relativePoint = item.TransformToAncestor(item.Parent as Grid).Transform(new Point(0, 0));

            DoubleAnimation translateY = new DoubleAnimation(relativePoint.Y, 0, TimeSpan.FromSeconds(0.5));
            translateY.EasingFunction = new QuadraticEase();

            tr.BeginAnimation(TranslateTransform.YProperty, translateY);
        }

        private void CharacterItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectCharacterItem(sender as Border);
            
            if (Inventory.Visibility == Visibility.Visible)
            {
                ChangeContent(Inventory, CharacterInfo, AnimationType.LeftToRight);
            }
        }

        private void SelectCharacterItem(Border item)
        {
            if ((string)((item.Parent as Grid).Children[0] as Rectangle).Tag == "__selected")
            {
                return;
            }

            ClearAllCharacterSelections();
            AnimateCharacterItem(item);

            List<string> characterInfo = Gamemanager.GetAllAboutCharacter(_playerID, (int)item.Tag);

            Status.Text = characterInfo[0];
            CharName.Text = characterInfo[1];
            Location.Text = characterInfo[2];
            Level.Text = characterInfo[3];
            Class.Text = characterInfo[4];
            GameTime.Text = characterInfo[5];
            GoldBalance.Text = characterInfo[6];
            RatingScore.Text = characterInfo[7];
            CreationDate.Text = characterInfo[8];

            _selectedCharacter.Clear();
            _selectedCharacter.Add(characterInfo[9]);       //playerID
            _selectedCharacter.Add(characterInfo[10]);      //chadID
            _selectedCharacter.Add(characterInfo[11]);      //name

            if (NewCharacterContent.Visibility == Visibility.Visible)
                ChangeContent(NewCharacterContent, CharacterInfoContent, AnimationType.LeftToRight);
            else
                CharacterInfoContent.Visibility = Visibility.Visible;

            if (characterInfo[0] == "Активен")
                Status.Foreground = new SolidColorBrush(Helper.GreenColor);
            else
                Status.Foreground = new SolidColorBrush(Helper.RedColor);

            DataExchanger.SelectedCharacterID = (int)item.Tag;
        }

        private void AnimateCharacterItem(Border item)
        {
            Rectangle selectionRect = (item.Parent as Grid).Children[0] as Rectangle;

            selectionRect.Fill = _selectionBrush;

            ScaleTransform st = new ScaleTransform();
            selectionRect.RenderTransform = st;

            DoubleAnimation rectExpanding = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
            rectExpanding.EasingFunction = new PowerEase();

            st.BeginAnimation(ScaleTransform.ScaleXProperty, rectExpanding);

            selectionRect.Tag = "__selected";
            CreateCharacterButton.Tag = "";
        }

        private void ClearAllCharacterSelections()
        {
            foreach (Grid itemGrid in CharacterList.Children)
            {
                Rectangle selectionRect = itemGrid.Children[0] as Rectangle;

                if ((string)selectionRect.Tag == "__selected")
                {
                    ScaleTransform st = new ScaleTransform();
                    selectionRect.RenderTransform = st;

                    DoubleAnimation rectExpanding = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.5));
                    rectExpanding.EasingFunction = new QuadraticEase();

                    st.BeginAnimation(ScaleTransform.ScaleXProperty, rectExpanding);
                    selectionRect.Tag = "";
                }
            }
        }

        private void DeleteCharacterButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.IsOpen = true;
            AskRemovalDialog.Visibility = Visibility.Visible;
            RemovalConfirmitionDialog.Visibility = MessageDialog.Visibility = Visibility.Hidden;
        }

        private void DialogYesButton_Click(object sender, RoutedEventArgs e)
        {
            bool successRemoval = Gamemanager.RemovePlayerCharacter(int.Parse(_selectedCharacter[0]), int.Parse(_selectedCharacter[1]));

            if (successRemoval)
            {
                DialogHost.IsOpen = true;
                AskRemovalDialog.Visibility = Visibility.Hidden;
                RemovalConfirmitionDialog.Visibility = Visibility.Visible;
                RemovedCharName.Text = _selectedCharacter[2];
                UpdateCharacterList();

                CharacterInfoContent.Visibility = Visibility.Hidden;

                DataExchanger.SelectedCharacterID = -1;
            }
        }

        private void CharacterCard_MouseEnter(object sender, MouseEventArgs e)
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

            // плавные размытие и появление надписи с названием персонажа
            BlurEffect imageBorderBlur = ((item.Child as Grid).Children[0] as Border).Effect as BlurEffect;
            TextBlock charNameBlock = (item.Child as Grid).Children[1] as TextBlock;

            DoubleAnimation blurAnim = new DoubleAnimation(imageBorderBlur.Radius, 5, animDuration);
            blurAnim.EasingFunction = new QuadraticEase();

            DoubleAnimation opacityAnim = new DoubleAnimation(charNameBlock.Opacity, 1, animDuration);
            opacityAnim.EasingFunction = new QuadraticEase();

            imageBorderBlur.BeginAnimation(BlurEffect.RadiusProperty, blurAnim);
            charNameBlock.BeginAnimation(OpacityProperty, opacityAnim);
        }

        private void CharacterCard_MouseLeave(object sender, MouseEventArgs e)
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

            // плавные исчезания размытости и надписи с названием персонажа
            BlurEffect imageBorderBlur = ((item.Child as Grid).Children[0] as Border).Effect as BlurEffect;
            TextBlock charNameBlock = (item.Child as Grid).Children[1] as TextBlock;

            DoubleAnimation blurAnim = new DoubleAnimation(imageBorderBlur.Radius, 0, animDuration);
            blurAnim.EasingFunction = new QuadraticEase();

            DoubleAnimation opacityAnim = new DoubleAnimation(charNameBlock.Opacity, 0, animDuration);
            opacityAnim.EasingFunction = new QuadraticEase();

            imageBorderBlur.BeginAnimation(BlurEffect.RadiusProperty, blurAnim);
            charNameBlock.BeginAnimation(OpacityProperty, opacityAnim);
        }

        private void CreateCharacterButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string)CreateCharacterButton.Tag == "__selected")
            {
                return;
            }

            ChangeContent(CharacterInfoContent, NewCharacterContent, AnimationType.RightToLeft);

            CreateCharacterButton.Tag = "__selected";
            ClearAllCharacterSelections();
        }

        private void ChangeContent(UIElement oldElement, UIElement newElement, AnimationType animType)
        {
            newElement.Visibility = Visibility.Visible;

            TimeSpan ts = TimeSpan.FromSeconds(0.5);
            Duration animDuration = ts;

            TranslateTransform tr1 = new TranslateTransform();
            newElement.RenderTransform = tr1;
            TranslateTransform tr2 = new TranslateTransform();
            oldElement.RenderTransform = tr2;

            int fromX = 0;
            int toX = 0;

            if (animType == AnimationType.LeftToRight)
            {
                fromX = -960;
            }
            else if (animType == AnimationType.RightToLeft)
            {
                fromX = 960;
            }

            DoubleAnimation translateX1 = new DoubleAnimation(fromX, toX, animDuration);
            translateX1.EasingFunction = new PowerEase();
            DoubleAnimation translateX2 = new DoubleAnimation(toX, -fromX, animDuration);
            translateX2.EasingFunction = new PowerEase();

            translateX1.Completed += (object sender, EventArgs e) => oldElement.Visibility = Visibility.Hidden;

            tr1.BeginAnimation(TranslateTransform.XProperty, translateX1);
            tr2.BeginAnimation(TranslateTransform.XProperty, translateX2);
        }

        private void HeroNameInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckHeroInput();
        }

        private void CheckHeroInput()
        {
            string errorMsg = Helper.ValidateHeroName(HeroNameInputBox.Text);

            if (string.IsNullOrEmpty(errorMsg))
            {
                Helper.SetTextFieldColor(HeroNameInputBox, Helper.GreenColor);
                HeroNameErrorBox.Visibility = Visibility.Hidden;

                isHeroNameCorrect = true;
            }
            else
            {
                Helper.SetTextFieldColor(HeroNameInputBox, Helper.RedColor);
                HeroNameErrorBox.Visibility = Visibility.Visible;
                HeroNameErrorBox.Text = errorMsg;

                isHeroNameCorrect = false;
            }
        }

        private void ConfirmCharacterCreationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isHeroNameCorrect)
            {
                CheckHeroInput();
                return;
            }

            string heroName = HeroNameInputBox.Text;
            int charID = Gamemanager.GetCharacterID(CharacterName.Text);

            bool isCharacterCreated = Gamemanager.AddPlayerCharacter(heroName, charID, _playerID);

            if (isCharacterCreated)
            {

                ReturnToGridButton_Click(ReturnToGridButton, null);
                UpdateCharacterList();
            }
            else
            {
                DialogHost.IsOpen = true;

                AskRemovalDialog.Visibility = RemovalConfirmitionDialog.Visibility = Visibility.Hidden;
                MessageDialog.Visibility = Visibility.Visible;

                MessageDialogTitle.Foreground = new SolidColorBrush(Helper.RedColor);
                MessageDialogTitle.Text = "Ошибка";
                MessageDialogText.Text = $"Вы не можете иметь больше одного персонажа типа\n{CharacterName.Text}.";
            }
        }

        private List<string> GetCharacterInfo(string charName)
        {
            Regex regex = new Regex(@"\s+");

            // className = string 0
            // name = string 1
            // iconImg = string 2
            // avatarImg = string 3
            // description = string 4
            foreach (List<string> row in _availableCharacters)
            {
                string charNameNoSpaces = regex.Replace(row[1], "");

                if (charName == charNameNoSpaces)
                {
                    return row;
                }
            }

            return null;
        }

        private void CharacterCard_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            string selectedHero = (sender as Border).Name;

            List<string> heroInfo = GetCharacterInfo(selectedHero);

            if (heroInfo is null)
            {
                MessageBox.Show("Unknown Error", selectedHero, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string heroFullName = heroInfo[1];
            string heroClass = heroInfo[0];
            string avatarName = heroInfo[3];
            string heroDescription = heroInfo[4];

            HeroAvatar.Background = new ImageBrush(new BitmapImage(new Uri($"C:/Users/helio/source/repos/MaterialDesignApp/MaterialDesignApp/Images/HeroAvatars/{avatarName}", UriKind.RelativeOrAbsolute)));
            CharacterName.Text = heroFullName;
            HeroClass.Text = heroClass;
            HeroDescription.Text = heroDescription;

            HeroNameInputBox.Text = string.Empty;
            Helper.SetTextFieldDefault(HeroNameInputBox);
            HeroNameErrorBox.Visibility = Visibility.Hidden;

            ChangeContent(HeroGrid, HeroInfo, AnimationType.LeftToRight);
        }

        private void ReturnToGridButton_Click(object sender, RoutedEventArgs e)
        {
            HeroNameInputBox.Text = string.Empty;
            Helper.SetTextFieldDefault(HeroNameInputBox);
            HeroNameErrorBox.Visibility = Visibility.Hidden;

            ChangeContent(HeroInfo, HeroGrid, AnimationType.RightToLeft);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataExchanger.SelectedCharacterID <= 0)
            {
                return;
            }

            foreach (Grid item in CharacterList.Children)
            {
                Border wrapper = item.Children[1] as Border;

                int charID = (int)wrapper.Tag;

                if (charID == DataExchanger.SelectedCharacterID)
                    SelectCharacterItem(wrapper);
            }
        }

        private void InventoryImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangeContent(CharacterInfo, Inventory, AnimationType.RightToLeft);
            FillInventory();
        }

        private void FillInventory()
        {
            InventoryDataGrid.Items.Clear();

            List<List<string>> items = Gamemanager.GetCharacterItems(DataExchanger.CurrectUserID, DataExchanger.SelectedCharacterID);

            if (items.Count == 0)
            {
                InventoryDataGrid.Visibility = Visibility.Hidden;
                EmptyInventoryMessage.Visibility = Visibility.Visible;
            }
            else
            {
                InventoryDataGrid.Visibility = Visibility.Visible;
                EmptyInventoryMessage.Visibility = Visibility.Hidden;
            }

            int num = 1;
            foreach (var item in items)
            {
                string imageSource = $"/Images/Items/{item[0]}";
                InventoryItem dataGridItem = new InventoryItem(num++, imageSource, item[1], int.Parse(item[2]));

                InventoryDataGrid.Items.Add(dataGridItem);
            }
        }

        private void InventoryImage_MouseEnter(object sender, MouseEventArgs e)
        {
            Image item = sender as Image;

            TranslateTransform tr = new TranslateTransform();
            item.RenderTransform = tr;

            Point relativePoint = item.TransformToAncestor(item.Parent as Grid).Transform(new Point(0, 0));
            double marginTop = item.Margin.Top;

            DoubleAnimation translateY = new DoubleAnimation(relativePoint.Y - marginTop, -10, TimeSpan.FromSeconds(0.5));
            translateY.EasingFunction = new QuadraticEase();

            tr.BeginAnimation(TranslateTransform.YProperty, translateY);
        }

        private void InventoryImage_MouseLeave(object sender, MouseEventArgs e)
        {
            Image item = sender as Image;

            TranslateTransform tr = new TranslateTransform();
            item.RenderTransform = tr;

            Point relativePoint = item.TransformToAncestor(item.Parent as Grid).Transform(new Point(0, 0));
            double marginTop = item.Margin.Top;

            DoubleAnimation translateY = new DoubleAnimation(relativePoint.Y - marginTop, 0, TimeSpan.FromSeconds(0.5));
            translateY.EasingFunction = new QuadraticEase();

            tr.BeginAnimation(TranslateTransform.YProperty, translateY);
        }

        private void ReturnToCharacterInfo_Click(object sender, RoutedEventArgs e)
        {
            ChangeContent(Inventory, CharacterInfo, AnimationType.LeftToRight);
        }
    }
}
