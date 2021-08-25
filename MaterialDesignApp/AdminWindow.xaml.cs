using MaterialDesignApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace MaterialDesignApp
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window, IManagementWindow
    {
        private Brush _selectionBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#19FFFFFF"));
        private Brush _transparentBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFFFF"));

        private bool _settingsButtonClicked = false;
        private bool _accountButtonClicked = false;

        public AdminWindow()
        {
            InitializeComponent();
            OpenPlayersPage();
            DataExchanger.ManagementWindow = this;
        }

        public void OpenPlayersPage()
        {
            ClearNavbarButtonsSelection();
            UnselectAccountButton();
            UnselectSettingsButton();

            DataContext = new PlayersViewModel();
            Border playersButtonBorder = PlayersButton.Parent as Border;
            AddShadowEffect(playersButtonBorder, 10);
            playersButtonBorder.Background = _selectionBrush;
        }

        private void AddShadowEffect(Border navbarElement, int size)
        {
            ClearAllShadowEffects();

            Border thisWrapper = navbarElement;
            Border nextElement = null;
            Border previousElement = null;

            for (int i = 0; i < Navbar.Children.Count; i++)
            {
                Border currentElement = (Navbar.Children[i] as Border).Child as Border;
                if (currentElement == thisWrapper)
                {
                    if (i != Navbar.Children.Count - 1)
                    {
                        nextElement = (Navbar.Children[i + 1] as Border).Child as Border;
                    }
                    if (i != 0)
                    {
                        previousElement = (Navbar.Children[i - 1] as Border).Child as Border;
                    }

                    break;
                }
            }

            if (!(nextElement is null))
            {
                nextElement.BorderThickness = new Thickness(size, 0, 0, 0);
                nextElement.Margin = new Thickness(-size, 0, 0, 0);
            }

            if (!(previousElement is null))
            {
                previousElement.BorderThickness = new Thickness(0, 0, size, 0);
                previousElement.Margin = new Thickness(0, 0, -size, 0);
            }

        }

        private void ClearAllShadowEffects()
        {
            foreach (object item in Navbar.Children)
            {
                if (item is Border)
                {
                    Border child = (item as Border).Child as Border;

                    child.BorderThickness = new Thickness(0, 0, 0, 0);
                    child.Margin = new Thickness(0, 0, 0, 0);
                }
            }
        }

        private bool IsNavbarButtonSelected(Border navbarElement)
        {
            Border thisWrapper = navbarElement;
            Border nextElement = null;
            Border previousElement = null;

            for (int i = 0; i < Navbar.Children.Count; i++)
            {
                Border currentElement = (Navbar.Children[i] as Border).Child as Border;
                if (currentElement == thisWrapper)
                {
                    if (i != Navbar.Children.Count - 1)
                    {
                        nextElement = (Navbar.Children[i + 1] as Border).Child as Border;
                    }
                    if (i != 0)
                    {
                        previousElement = (Navbar.Children[i - 1] as Border).Child as Border;
                    }

                    break;
                }
            }

            if (!(nextElement is null))
            {
                return nextElement.Margin.Left != 0;
            }

            if (!(previousElement is null))
            {
                return previousElement.Margin.Right != 0;
            }

            return false;
        }

        private void ClearNavbarButtonsSelection(Border exceptionElement = null)
        {
            foreach (object item in Navbar.Children)
            {
                if (item is Border)
                {
                    Border currentElement = (item as Border).Child as Border;

                    if (!(exceptionElement is null) && currentElement == exceptionElement)
                    {
                        continue;
                    }

                    currentElement.Background = _transparentBrush;
                }
            }
        }

        private void Navbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void NavbarButton_Click(object sender, MouseButtonEventArgs e)
        {
            Border wrapper = sender as Border;

            if (wrapper.Name == "ShopWrapper" && DataExchanger.SelectedCharacterID <= 0)
            {
                return;
            }

            AddShadowEffect(wrapper, 10);
            ClearNavbarButtonsSelection(wrapper);

            UnselectSettingsButton();
            UnselectAccountButton();
            _settingsButtonClicked = false;
            _accountButtonClicked = false;
        }

        private void NavbarButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Navbar.MouseDown -= Navbar_MouseDown;

            if (sender is Border && !IsNavbarButtonSelected(sender as Border))
            {
                Border thisWrapper = sender as Border;
                thisWrapper.Background = _selectionBrush;
            }
            else if (sender is Image)
            {
                Image btn = sender as Image;

                if (btn == SettingsButton && !_settingsButtonClicked)
                    SelectSettingsButton();
                else if (btn == AccountButton && !_accountButtonClicked)
                    SelectAccountButton();
                else if (btn == CloseButton)
                    SelectCloseButton();
                else if (btn == MinimizeButton)
                    SelectMinimizeButton();
            }
        }

        private void NavbarButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Navbar.MouseDown += Navbar_MouseDown;

            if (sender is Border && !IsNavbarButtonSelected(sender as Border))
            {
                Border thisWrapper = sender as Border;
                thisWrapper.Background = _transparentBrush;
            }
            else if (sender is Image)
            {
                Image btn = sender as Image;

                if (btn == SettingsButton && !_settingsButtonClicked)
                    UnselectSettingsButton();
                else if (btn == AccountButton && !_accountButtonClicked)
                    UnselectAccountButton();
                else if (btn == CloseButton)
                    UnselectCloseButton();
                else if (btn == MinimizeButton)
                    UnselectMinimizeButton();
            }
        }

        private void CloseButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void SettingsButton_Click(object sender, MouseButtonEventArgs e)
        {
            DataContext = new SettingsViewModel();

            ClearNavbarButtonsSelection();
            ClearAllShadowEffects();

            SelectSettingsButton();
            UnselectAccountButton();

            _settingsButtonClicked = true;
            _accountButtonClicked = false;
        }

        private void AccountButton_Click(object sender, MouseButtonEventArgs e)
        {
            DataContext = new AccountViewModel();
            ClearNavbarButtonsSelection();
            ClearAllShadowEffects();

            SelectAccountButton();
            UnselectSettingsButton();

            _accountButtonClicked = true;
            _settingsButtonClicked = false;
        }

        private void SelectSettingsButton()
        {
            SettingsButton.Source = new BitmapImage(new Uri("pack://application:,,,/Images/settings_yellow.png"));
        }

        private void UnselectSettingsButton()
        {
            SettingsButton.Source = new BitmapImage(new Uri("pack://application:,,,/Images/settings.png"));
        }

        private void SelectAccountButton()
        {
            AccountButton.Source = new BitmapImage(new Uri("pack://application:,,,/Images/account_yellow.png"));
        }

        private void UnselectAccountButton()
        {
            AccountButton.Source = new BitmapImage(new Uri("pack://application:,,,/Images/account.png"));
        }

        private void SelectCloseButton()
        {
            CloseButton.Source = new BitmapImage(new Uri("pack://application:,,,/Images/close_red.png"));
        }

        private void UnselectCloseButton()
        {
            CloseButton.Source = new BitmapImage(new Uri("pack://application:,,,/Images/close_white.png"));
        }

        private void SelectMinimizeButton()
        {
            MinimizeButton.Source = new BitmapImage(new Uri("pack://application:,,,/Images/minimize_blue.png"));
        }

        private void UnselectMinimizeButton()
        {
            MinimizeButton.Source = new BitmapImage(new Uri("pack://application:,,,/Images/minimize.png"));
        }

        private void MinimizeButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void PlayersButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DataContext = new PlayersViewModel();
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DataContext = new ReportsViewModel();
        }
    }

}
