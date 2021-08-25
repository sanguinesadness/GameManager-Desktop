using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignApp.Views
{
    /// <summary>
    /// Interaction logic for PlayersView.xaml
    /// </summary>
    public partial class PlayersView : UserControl
    {
        private List<Player> _playerList = new List<Player>();
        private int _selectedPlayerID = -1;

        public PlayersView()
        {
            InitializeComponent();
        }

        private void FillPlayerList()
        {
            PlayerList.Items.Clear();
            _playerList = Gamemanager.GetPlayers();

            int i = 0;
            foreach (Player player in _playerList)
            {
                i += 20;
                ListViewItem item = CreatePlayerListItem(player, i);
                PlayerList.Items.Add(item);
            }
        }

        private void FillCharactersGrid(int playerID)
        {
            CharactersDataGrid.Items.Clear();
            List<Character> characters = Gamemanager.GetPlayerCharactersV2(playerID);

            if (characters.Count == 0)
            {
                EmptyContent.Visibility = Visibility.Visible;
                ContentGrid.Visibility = InfoContent.Visibility = Visibility.Hidden;
                PlayerLoginRun.Text = Gamemanager.GetUserLogin(playerID);
            }
            else
            {
                EmptyContent.Visibility = InfoContent.Visibility = Visibility.Hidden;
                ContentGrid.Visibility = Visibility.Visible;

                int number = 1;
                foreach (Character character in characters)
                {
                    character.Number = number++;
                    character.Icon = $"/Images/HeroIcons/{character.Icon}";

                    CharactersDataGrid.Items.Add(character);
                }
            }
        }

        private ListViewItem CreatePlayerListItem(Player player, int i)
        {
            byte R = (byte)(150 - i);
            byte G = 21;
            byte B = 101;

            if (i >= 150)
            {
                R = 0;
            }

            ListViewItem item = new ListViewItem()
            {
                Tag = player.ID,
                Height = 80
            };
            item.MouseUp += PlayerListItem_MouseUp;

            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(150, 0, 0, 0)
            };

            MaterialDesignThemes.Wpf.PackIcon icon = new MaterialDesignThemes.Wpf.PackIcon()
            {
                Kind = MaterialDesignThemes.Wpf.PackIconKind.Account,
                Margin = new Thickness(0, 0, 20, 0),
                RenderTransform = new ScaleTransform(1.6, 1.6, 0.5, 0.5),
                Foreground = new SolidColorBrush(Color.FromRgb(R, G, B))
            };

            TextBlock textBlock = new TextBlock()
            {
                Text = player.Login
            };

            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(textBlock);
            item.Content = stackPanel;

            return item;
        }

        private void PlayerListItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ListViewItem srcItem = (sender as ListViewItem);

            string playerLogin = ((srcItem.Content as StackPanel).Children[1] as TextBlock).Text;
            PlayerLoginTitle.Text = playerLogin;

            int playerID = int.Parse(srcItem.Tag.ToString());
            FillCharactersGrid(playerID);
            _selectedPlayerID = playerID;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillPlayerList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            FillPlayerList();

            if (_selectedPlayerID > -1)
                FillCharactersGrid(_selectedPlayerID);

            foreach (ListViewItem listItem in PlayerList.Items)
            {
                if ((int)listItem.Tag == _selectedPlayerID)
                {
                    listItem.IsSelected = true;
                }
            }
        }

        private void BanCharacterButton_Click(object sender, RoutedEventArgs e)
        {
            OpenBanCharacterDialog();
        }

        private void UnbanCharacterButton_Click(object sender, RoutedEventArgs e)
        {
            string charName = GetSelectedCharacter().Name;
            string playerLogin = GetSelectedPlayerLogin();

            OpenUnbanCharacterDialog(playerLogin, charName);
        }

        private void ContextMenu_Opened(object sender, RoutedEventArgs e)
        {
            if (!(CharactersDataGrid.SelectedItem is null))
            {
                var character = CharactersDataGrid.SelectedItem as Character;
                
                if (character.Banned == "Да")
                {
                    BanCharacterButton.IsEnabled = false;
                    UnbanCharacterButton.IsEnabled = true;
                }
                else
                {
                    BanCharacterButton.IsEnabled = true;
                    UnbanCharacterButton.IsEnabled = false;
                }
            }
            else
            {
                BanCharacterButton.IsEnabled = UnbanCharacterButton.IsEnabled = false;
            }
        }

        private void OpenBanCharacterDialog()
        {
            DialogHost.IsOpen = true;
            BanCharacterDialog.Visibility = Visibility.Visible;
            BanSuccessDialog.Visibility = UnbanCharacterDialog.Visibility = UnbanSuccessDialog.Visibility = Visibility.Collapsed;
        }

        private void OpenBanSuccessDialog(string playerName, string charName)
        {
            DialogHost.IsOpen = true;

            BanCharacterDialog.Visibility = UnbanCharacterDialog.Visibility = Visibility.Collapsed;
            BanSuccessDialog.Visibility = Visibility.Visible;

            BannedPlayerTextBox.Text = playerName;
            BannedCharacterTextBox.Text = charName;
        }

        private void OpenUnbanCharacterDialog(string playerName, string charName)
        {
            DialogHost.IsOpen = true;

            UnbanCharacterDialog.Visibility = Visibility.Visible;
            BanCharacterDialog.Visibility = BanSuccessDialog.Visibility = UnbanSuccessDialog.Visibility = Visibility.Collapsed;

            UnbanCharacterName.Text = charName;
            UnbanPlayerName.Text = playerName;
        }

        private void OpenUnbanSuccessDialog(string playerName, string charName)
        {
            DialogHost.IsOpen = true;

            UnbanSuccessDialog.Visibility = Visibility.Visible;
            UnbanCharacterDialog.Visibility = BanCharacterDialog.Visibility = BanSuccessDialog.Visibility = Visibility.Collapsed;

            UnbannedCharacterName.Text = charName;
            UnbannedPlayerName.Text = playerName;
        }

        private void BanDialogConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            string banReason = BanDialogReasonTextBox.Text;

            Character selectedCharacter = GetSelectedCharacter();

            int charID = selectedCharacter.CharacterID;
            int playerID = selectedCharacter.PlayerID;

            string charName = selectedCharacter.Name;
            string playerLogin = GetSelectedPlayerLogin();

            bool isSuccess = Gamemanager.BanCharacter(playerID, charID, banReason);
            if (isSuccess)
            {
                FillCharactersGrid(playerID);
                OpenBanSuccessDialog(playerLogin, charName);
            }
            else
            {
                MessageBox.Show("Произошла неизвестная ошибка. Повторите, пожалуйста, позже.", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Character GetSelectedCharacter()
        {
            if (!(CharactersDataGrid.SelectedItem is null))
            {
                var character = CharactersDataGrid.SelectedItem as Character;
                return character;
            }
            else
            {
                return null;
            }
        }

        private string GetSelectedPlayerLogin()
        {
            if (!(PlayerList.SelectedItem is null))
            {
                ListViewItem item = PlayerList.SelectedItem as ListViewItem;
                return ((item.Content as StackPanel).Children[1] as TextBlock).Text;
            }
            else
            {
                return "";
            }
        }

        private void UnbanCharacterConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Character selectedCharacter = GetSelectedCharacter();

            int charID = selectedCharacter.CharacterID;
            int playerID = selectedCharacter.PlayerID;

            string charName = selectedCharacter.Name;
            string playerLogin = GetSelectedPlayerLogin();

            bool isSuccess = Gamemanager.UnbanCharacter(playerID, charID);
            if (isSuccess)
            {
                OpenUnbanSuccessDialog(playerLogin, charName);
                FillCharactersGrid(playerID);
            }
            else
            {
                MessageBox.Show("Произошла неизвестная ошибка. Повторите, пожалуйста, позже.", "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
