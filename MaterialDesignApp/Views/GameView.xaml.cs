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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaterialDesignApp.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public GameView()
        {
            InitializeComponent();
        }

        private void SectionsTree_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is TreeView && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FillLocationsSection();
            FillCharactersSection();
            FillItemsSection();
            FillMobsSection();
        }

        private void FillLocationsSection()
        {
            Dictionary<int, string> locations = Gamemanager.GetAllLocations();

            foreach (KeyValuePair<int, string> location in locations)
            {
                TreeViewItem newItem = new TreeViewItem()
                {
                    Header = location.Value,
                    Tag = location.Key
                };
                newItem.MouseUp += LocationItem_Click;

                LocationsSection.Items.Add(newItem);
            }
        }

        private void FillCharactersSection()
        {
            List<List<string>> characters = Gamemanager.GetAvailableCharacters();

            foreach (List<string> character in characters)
            {
                TreeViewItem newItem = new TreeViewItem()
                {
                    Header = character[1],
                    Tag = character[5]
                };
                newItem.MouseUp += CharacterItem_Click;

                CharactersSection.Items.Add(newItem);
            }
        }

        private void FillItemsSection()
        {
            Dictionary<int, string> items = Gamemanager.GetAllItems();

            foreach (KeyValuePair<int, string> item in items)
            {
                TreeViewItem newItem = new TreeViewItem()
                {
                    Header = item.Value,
                    Tag = item.Key
                };
                newItem.MouseUp += ItemsItem_Click;

                ItemsSection.Items.Add(newItem);
            }
        }

        private void FillMobsSection()
        {
            Dictionary<int, string> mobs = Gamemanager.GetAllMobs();

            foreach (KeyValuePair<int, string> mob in mobs)
            {
                TreeViewItem newItem = new TreeViewItem()
                {
                    Header = mob.Value,
                    Tag = mob.Key
                };
                newItem.MouseUp += MobsItem_Click;

                MobsSection.Items.Add(newItem);
            }
        }

        private void LocationItem_Click(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeItem = sender as TreeViewItem;

            int locationID = int.Parse(treeItem.Tag.ToString());

            string sectionHeader = LocationsSection.Header.ToString();
            string itemHeader = treeItem.Header.ToString();

            TitleTextBox.Visibility = Visibility.Visible;
            SectionTitleRun.Text = sectionHeader;
            ItemTitleRun.Text = itemHeader;
            ItemDescription.Text = Gamemanager.GetDescription(locationID, Tables.locations);
        }

        private void CharacterItem_Click(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeItem = sender as TreeViewItem;

            int charID = int.Parse(treeItem.Tag.ToString());

            string sectionHeader = CharactersSection.Header.ToString();
            string itemHeader = treeItem.Header.ToString();

            TitleTextBox.Visibility = Visibility.Visible;
            SectionTitleRun.Text = sectionHeader;
            ItemTitleRun.Text = itemHeader;
            ItemDescription.Text = Gamemanager.GetDescription(charID, Tables.characters);
        }

        private void ItemsItem_Click(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeItem = sender as TreeViewItem;

            int itemID = int.Parse(treeItem.Tag.ToString());

            string sectionHeader = ItemsSection.Header.ToString();
            string itemHeader = treeItem.Header.ToString();

            TitleTextBox.Visibility = Visibility.Visible;
            SectionTitleRun.Text = sectionHeader;
            ItemTitleRun.Text = itemHeader;
            ItemDescription.Text = Gamemanager.GetDescription(itemID, Tables.items);
        }

        private void MobsItem_Click(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeItem = sender as TreeViewItem;

            int mobID = int.Parse(treeItem.Tag.ToString());

            string sectionHeader = MobsSection.Header.ToString();
            string itemHeader = treeItem.Header.ToString();

            TitleTextBox.Visibility = Visibility.Visible;
            SectionTitleRun.Text = sectionHeader;
            ItemTitleRun.Text = itemHeader;
            ItemDescription.Text = Gamemanager.GetDescription(mobID, Tables.mobs);
        }
    }
}
