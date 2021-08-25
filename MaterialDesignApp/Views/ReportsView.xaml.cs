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
using MaterialDesignApp.ReportTables;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace MaterialDesignApp.Views
{
    /// <summary>
    /// Interaction logic for ReportsView.xaml
    /// </summary>
    public partial class ReportsView : UserControl
    {
        private DataGrid _activeDataGrid;

        public ReportsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ShowContentElements()
        {
            (ContentTitle.Parent as System.Windows.Controls.Border).Visibility = Visibility.Visible;
            (ExportToExcelButton.Parent as System.Windows.Controls.Border).Visibility = Visibility.Visible;
            StartInfoContent.Visibility = Visibility.Hidden;
        }

        private void ShowMostActivePlayersTable()
        {
            MostActivePlayersDataGrid.Items.Clear();
            List<MostActivePlayersRow> table = Gamemanager.GetMostActivePlayersTable();

            int i = 1;
            foreach (var row in table)
            {
                row.Number = i++;
                MostActivePlayersDataGrid.Items.Add(row);
            }

            ShowContentElements();

            MostActivePlayersDataGrid.Visibility = Visibility.Visible;
            MostExpensiveInventoriesDataGrid.Visibility = TopRatingDataGrid.Visibility = RichestPlayersDataGrid.Visibility = HighestLevelsDataGrid.Visibility = Visibility.Hidden;

            _activeDataGrid = MostActivePlayersDataGrid;

            ContentTitle.Text = ExtractListItemText(MostActivePlayersItem);
        }

        private void ShowMostExpensiveInventoriesTable()
        {
            MostExpensiveInventoriesDataGrid.Items.Clear();
            List<MostExpensiveInventoriesRow> table = Gamemanager.GetMostExpensiveInventoriesTable();

            int i = 1;
            foreach (var row in table)
            {
                row.Number = i++;
                MostExpensiveInventoriesDataGrid.Items.Add(row);
            }

            ShowContentElements();

            MostExpensiveInventoriesDataGrid.Visibility = Visibility.Visible;
            MostActivePlayersDataGrid.Visibility = TopRatingDataGrid.Visibility = RichestPlayersDataGrid.Visibility = HighestLevelsDataGrid.Visibility = Visibility.Hidden;

            _activeDataGrid = MostExpensiveInventoriesDataGrid;

            ContentTitle.Text = ExtractListItemText(MostExpensiveInventoriesItem);
        }

        private void ShowTopRatingTable()
        {
            TopRatingDataGrid.Items.Clear();
            List<TopRatingRow> table = Gamemanager.GetTopRatingTable();

            int i = 1;
            foreach (var row in table)
            {
                row.Number = i++;
                TopRatingDataGrid.Items.Add(row);
            }

            ShowContentElements();

            TopRatingDataGrid.Visibility = Visibility.Visible;
            MostActivePlayersDataGrid.Visibility = MostExpensiveInventoriesDataGrid.Visibility = RichestPlayersDataGrid.Visibility = HighestLevelsDataGrid.Visibility = Visibility.Hidden;

            _activeDataGrid = TopRatingDataGrid;

            ContentTitle.Text = ExtractListItemText(TopRatingItem);
        }

        private void ShowRichestPlayersTable()
        {
            RichestPlayersDataGrid.Items.Clear();
            List<RichestPlayersRow> table = Gamemanager.GetRichestPlayersTable();

            int i = 1;
            foreach (var row in table)
            {
                row.Number = i++;
                RichestPlayersDataGrid.Items.Add(row);
            }

            ShowContentElements();

            RichestPlayersDataGrid.Visibility = Visibility.Visible;
            TopRatingDataGrid.Visibility = MostActivePlayersDataGrid.Visibility = MostExpensiveInventoriesDataGrid.Visibility = HighestLevelsDataGrid.Visibility = Visibility.Hidden;

            _activeDataGrid = RichestPlayersDataGrid;

            ContentTitle.Text = ExtractListItemText(RichestPlayersItem);
        }

        private void ShowHighestLevelsTable()
        {
            HighestLevelsDataGrid.Items.Clear();
            List<HighestLevelsRow> table = Gamemanager.GetHighestLevelsTable();

            int i = 1;
            foreach (var row in table)
            {
                row.Number = i++;
                HighestLevelsDataGrid.Items.Add(row);
            }

            ShowContentElements();

            HighestLevelsDataGrid.Visibility = Visibility.Visible;
            RichestPlayersDataGrid.Visibility = TopRatingDataGrid.Visibility = MostActivePlayersDataGrid.Visibility = MostExpensiveInventoriesDataGrid.Visibility = Visibility.Hidden;

            _activeDataGrid = HighestLevelsDataGrid;

            ContentTitle.Text = ExtractListItemText(HighestLevelsItem);
        }

        private string ExtractListItemText(ListViewItem item)
        {
            return ((item.Content as StackPanel).Children[1] as TextBlock).Text;
        }

        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = string.Empty;

            if (_activeDataGrid == MostActivePlayersDataGrid)
                fileName = ExtractListItemText(MostActivePlayersItem);
            else if (_activeDataGrid == MostExpensiveInventoriesDataGrid)
                fileName = ExtractListItemText(MostExpensiveInventoriesItem);
            else if (_activeDataGrid == TopRatingDataGrid)
                fileName = ExtractListItemText(TopRatingItem);
            else if (_activeDataGrid == RichestPlayersDataGrid)
                fileName = ExtractListItemText(RichestPlayersItem);
            else if (_activeDataGrid == HighestLevelsDataGrid)
                fileName = ExtractListItemText(HighestLevelsItem);

            ExportDataToExcel(_activeDataGrid, fileName);
        }

        private void ExportDataToExcel(DataGrid data, string fileName)
        {
            Microsoft.Win32.SaveFileDialog safeFileDialog = new Microsoft.Win32.SaveFileDialog()
            {
                FileName = fileName,
                DefaultExt = ".xlsx",
                Filter = "Excel Workbook|*.xlsx"
            };

            bool? result = safeFileDialog.ShowDialog();

            if (result == true)
            {
                Excel.Application app = new Excel.Application();
                Workbook workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet = workbook.Sheets[1];

                for (int j = 0; j < data.Columns.Count; j++)
                {
                    Range range = (Range)sheet.Cells[1, j + 1];
                    range.Value2 = (data.Columns[j].Header as TextBlock).Text;

                    range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    range.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
                    range.Cells.Font.Bold = true;
                    range.ColumnWidth = 15;
                    range.RowHeight = 20;
                    range.Cells.Interior.Color = XlRgbColor.rgbPurple;
                    range.Cells.Font.Color = XlRgbColor.rgbWhite;
                }

                for (int i = 0; i < data.Columns.Count; i++)
                {
                    for (int j = 0; j < data.Items.Count; j++)
                    {
                        TextBlock b = data.Columns[i].GetCellContent(data.Items[j]) as TextBlock;

                        Range range = (Range)sheet.Cells[j + 2, i + 1];
                        range.Value2 = b.Text;

                        range.Cells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        range.Cells.VerticalAlignment = XlHAlign.xlHAlignCenter;
                        range.RowHeight = 20;
                    }
                }

                try
                {
                    workbook.SaveAs(safeFileDialog.FileName);
                }
                catch (Exception)
                {
                    OpenSaveErrorDialog();
                    return;
                }

                workbook.Close();
                OpenSaveSuccessDialog();
            }
        }

        private void MostActivePlayersItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ShowMostActivePlayersTable();
        }

        private void MostExpensiveInventoriesItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ShowMostExpensiveInventoriesTable();
        }

        private void TopRatingItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ShowTopRatingTable();
        }

        private void RichestPlayersItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ShowRichestPlayersTable();
        }

        private void HighestLevelsItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ShowHighestLevelsTable();
        }

        private void OpenSaveSuccessDialog()
        {
            DialogHost.IsOpen = true;

            SaveSuccessDialog.Visibility = Visibility.Visible;
            SaveErrorDialog.Visibility = Visibility.Collapsed;
        }

        private void OpenSaveErrorDialog()
        {
            DialogHost.IsOpen = true;

            SaveSuccessDialog.Visibility = Visibility.Collapsed;
            SaveErrorDialog.Visibility = Visibility.Visible;
        }

    }
}
