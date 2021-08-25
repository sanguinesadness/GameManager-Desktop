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
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        private bool _isNewPasswordCorrect = true;
        private bool _isEmailCorrect = false;
        private bool _isBirthCorrect = false;

        private string _currentLogin;
        private string _currentEmail;
        private DateTime? _currentDateBirth;
        private string _currentGender;

        public AccountView()
        {
            InitializeComponent();
        }

        private void SetDefaultValues()
        {
            LoginTextBox.Text = _currentLogin;
            EmailTextBox.Text = _currentEmail;
            BirthPicker.SelectedDate = _currentDateBirth;
            NewPasswordBox.Password = string.Empty;

            foreach (RadioButton radioButton in GenderSelection.Children)
            {
                if (radioButton.Content.ToString() == _currentGender)
                    radioButton.IsChecked = true;
                else
                    radioButton.IsChecked = false;
            }
        }

        private void LogOutListItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangeDataContent.Visibility = Visibility.Hidden;
            OpenLogoutDialog();
        }

        private void OpenLogoutDialog()
        {
            LogoutDialog.Visibility = Visibility.Visible;
            SuccessChangesDialog.Visibility = FailChangesDialog.Visibility = AdmitChangesDialog.Visibility = Visibility.Collapsed;

            DialogHost.IsOpen = true;
        }

        private void OpenSuccessChangesDialog()
        {
            LogoutDialog.Visibility = FailChangesDialog.Visibility = AdmitChangesDialog.Visibility = Visibility.Collapsed;
            SuccessChangesDialog.Visibility = Visibility.Visible;

            DialogHost.IsOpen = true;
        }

        private void OpenFailChangesDialog()
        {
            LogoutDialog.Visibility = SuccessChangesDialog.Visibility = AdmitChangesDialog.Visibility = Visibility.Collapsed;
            FailChangesDialog.Visibility = Visibility.Visible;

            DialogHost.IsOpen = true;
        }

        private void OpenAdmitChangesDialog()
        {
            AdmitChangesDialog.Visibility = Visibility.Visible;
            LogoutDialog.Visibility = SuccessChangesDialog.Visibility = FailChangesDialog.Visibility = Visibility.Collapsed;
            DialogPasswordBox.Password = string.Empty;

            DialogHost.IsOpen = true;
        }

        private void LogOut()
        {
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
            (DataExchanger.ManagementWindow as Window).Close();

            DataExchanger.ClearData();
        }

        private void DialogYesButton_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }

        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                EmailError.Visibility = Visibility.Hidden;
                Helper.SetTextFieldDefault(EmailTextBox);
            }
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                return;
            }
            else if (EmailTextBox.Text == _currentEmail)
            {
                _isEmailCorrect = true;
                Helper.SetTextFieldDefault(EmailTextBox);
                EmailError.Visibility = Visibility.Hidden;

                return;
            }

            string errorMsg = Helper.ValidateEmail(EmailTextBox.Text, false);

            if (string.IsNullOrEmpty(errorMsg))
            {
                Helper.SetTextFieldColor(EmailTextBox, Helper.GreenColor);
                EmailError.Visibility = Visibility.Hidden;

                _isEmailCorrect = true;
            }
            else
            {
                Helper.SetTextFieldColor(EmailTextBox, Helper.RedColor);
                EmailError.Visibility = Visibility.Visible;
                EmailError.Text = errorMsg;

                _isEmailCorrect = false;
            }
        }

        private void UpdateAllInfo()
        {
            var playerInfo = Gamemanager.GetPlayerInfo(DataExchanger.CurrectUserID);

            _currentLogin = playerInfo[0].ToString();
            _currentEmail = playerInfo[1].ToString();
            _currentGender = playerInfo[3].ToString();
            _currentDateBirth = (DateTime?)playerInfo[2];

            SetDefaultValues();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAllInfo();

            if (DataExchanger.ManagementWindow is AdminWindow)
            {
                FirstColumn.Width = new GridLength(528);
                SecondColumn.Width = new GridLength(912);
                ThirdColumn.Width = new GridLength(0);
            }
        }

        private void EmailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                EmailTextBox.Text = _currentEmail;

                _isEmailCorrect = true;
                Helper.SetTextFieldDefault(EmailTextBox);
                EmailError.Visibility = Visibility.Hidden;

                return;
            }
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Helper.GrayColor);
            SolidColorBrush purpleBrush = new SolidColorBrush(Helper.PurpleColor);

            if (sender is TextBox)
            {
                TextBox textBox = (sender as TextBox);
                SolidColorBrush currentBorderBrush = (SolidColorBrush)textBox.BorderBrush;

                if (new SolidColorBrushComparer().Equals(grayBrush, currentBorderBrush))
                {
                    textBox.BorderBrush = purpleBrush;
                }
            }
            else if (sender is PasswordBox)
            {
                PasswordBox passwordBox = (sender as PasswordBox);
                SolidColorBrush currentBorderBrush = (SolidColorBrush)passwordBox.BorderBrush;

                if (new SolidColorBrushComparer().Equals(grayBrush, currentBorderBrush))
                {
                    passwordBox.BorderBrush = purpleBrush;
                }
            }
            else if (sender is DatePicker)
            {
                DatePicker datePicker = (sender as DatePicker);
                SolidColorBrush currentBorderBrush = (SolidColorBrush)datePicker.BorderBrush;

                if (new SolidColorBrushComparer().Equals(grayBrush, currentBorderBrush))
                {
                    datePicker.BorderBrush = purpleBrush;
                }
            }
        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush grayBrush = new SolidColorBrush(Helper.GrayColor);
            SolidColorBrush purpleBrush = new SolidColorBrush(Helper.PurpleColor);

            if (sender is TextBox)
            {
                TextBox textBox = (sender as TextBox);
                SolidColorBrush currentBorderBrush = (SolidColorBrush)textBox.BorderBrush;

                if (new SolidColorBrushComparer().Equals(purpleBrush, currentBorderBrush))
                {
                    textBox.BorderBrush = grayBrush;
                }
            }
            else if (sender is PasswordBox)
            {
                PasswordBox passwordBox = (sender as PasswordBox);
                SolidColorBrush currentBorderBrush = (SolidColorBrush)passwordBox.BorderBrush;

                if (new SolidColorBrushComparer().Equals(purpleBrush, currentBorderBrush))
                {
                    passwordBox.BorderBrush = grayBrush;
                }
            }
            else if (sender is DatePicker)
            {
                DatePicker datePicker = (sender as DatePicker);
                SolidColorBrush currentBorderBrush = (SolidColorBrush)datePicker.BorderBrush;

                if (new SolidColorBrushComparer().Equals(purpleBrush, currentBorderBrush))
                {
                    datePicker.BorderBrush = grayBrush;
                }
            }
        }

        private void BirthPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BirthPicker.SelectedDate.HasValue && BirthPicker.SelectedDate == _currentDateBirth)
            {
                Helper.SetTextFieldDefault(BirthPicker);
                _isBirthCorrect = true;
            }
            else if (BirthPicker.SelectedDate.HasValue && BirthPicker.SelectedDate > DateTime.Now)
            {
                Helper.SetTextFieldColor(BirthPicker, Helper.RedColor);
                _isBirthCorrect = false;
            }
            else if (BirthPicker.SelectedDate.HasValue && BirthPicker.SelectedDate < DateTime.Now)
            {
                Helper.SetTextFieldColor(BirthPicker, Helper.GreenColor);
                _isBirthCorrect = true;
            }
            else
            {
                Helper.SetTextFieldDefault(BirthPicker);
                _isBirthCorrect = true;
            }
        }

        private void RestoreDefaultsButton_Click(object sender, RoutedEventArgs e)
        {
            SetDefaultValues();
        }

        private void BirthPicker_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!BirthPicker.SelectedDate.HasValue)
            {
                BirthPicker.SelectedDate = _currentDateBirth;

                _isBirthCorrect = true;
                Helper.SetTextFieldDefault(BirthPicker);

                return;
            }
        }

        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isNewPasswordCorrect || !_isBirthCorrect || !_isEmailCorrect)
            {
                return;
            }
            else
            {
                OpenAdmitChangesDialog();
            }
        }

        private void ChangeDataListItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChangeDataContent.Visibility = Visibility.Visible;
        }

        private void NewPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NewPasswordBox.Password))
            {
                NewPasswordError.Visibility = Visibility.Hidden;
                Helper.SetTextFieldDefault(NewPasswordBox);
            }
        }

        private void NewPasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                EmailTextBox.Focus();
            }
        }

        private void SaveChanges_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveChangesButton_Click(sender, e);
            }
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NewPasswordBox.Password))
            {
                _isNewPasswordCorrect = true;
                Helper.SetTextFieldDefault(NewPasswordBox);
                NewPasswordError.Visibility = Visibility.Hidden;
                return;
            }

            string errorMsg = Helper.ValidatePassword(NewPasswordBox.Password);

            if (string.IsNullOrEmpty(errorMsg))
            {
                Helper.SetTextFieldColor(NewPasswordBox, Helper.GreenColor);
                NewPasswordError.Visibility = Visibility.Hidden;

                _isNewPasswordCorrect = true;
            }
            else
            {
                Helper.SetTextFieldColor(NewPasswordBox, Helper.RedColor);
                NewPasswordError.Visibility = Visibility.Visible;
                NewPasswordError.Text = errorMsg;

                _isNewPasswordCorrect = false;
            }
        }

        private void BirthPicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                EmailTextBox.Focus();
            }

        }

        private void EmailTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                BirthPicker.Focus();
            }

            if (e.Key == Key.Up)
            {
                NewPasswordBox.Focus();
            }
        }

        private void AdmitDialogConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DialogPasswordBox.Password))
            {
                DialogPasswordError.Visibility = Visibility.Visible;
                DialogPasswordError.Text = "Поле обязательно для заполнения";
                Helper.SetTextFieldColor(DialogPasswordBox, Helper.RedColor);

                return;
            }

            if (!Gamemanager.LoginUser(DataExchanger.CurrentUserLogin, DialogPasswordBox.Password))
            {
                DialogPasswordError.Visibility = Visibility.Visible;
                DialogPasswordError.Text = "Неверный пароль";
                Helper.SetTextFieldColor(DialogPasswordBox, Helper.RedColor);

                return;
            }

            string password = NewPasswordBox.Password;
            string email = EmailTextBox.Text;
            DateTime? birthDate = BirthPicker.SelectedDate;

            Genders gender = Genders.Другое;
            foreach (RadioButton radioButton in GenderSelection.Children)
            {
                if (radioButton.IsChecked == true)
                {
                    switch (radioButton.Content.ToString())
                    {
                        case "Мужской":
                            gender = Genders.Мужской;
                            break;
                        case "Женский":
                            gender = Genders.Женский;
                            break;
                        default:
                            gender = Genders.Другое;
                            break;
                    }
                }
            }

            bool infoChanged;

            if (string.IsNullOrEmpty(password))
            {
                infoChanged = Gamemanager.UpdatePlayerInfo(DataExchanger.CurrectUserID, birthDate, gender, email);
            }
            else
            {
                infoChanged = Gamemanager.UpdatePlayerInfo(DataExchanger.CurrectUserID, password, birthDate, gender, email);
            }

            if (infoChanged)
            {
                OpenSuccessChangesDialog();
            }
            else
            {
                OpenFailChangesDialog();
            }
        }

        private void DialogPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(DialogPasswordBox.Password))
            {
                DialogPasswordError.Visibility = Visibility.Hidden;
                Helper.SetTextFieldDefault(DialogPasswordBox);
            }
        }

        private void DialogPasswordBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                AdmitDialogConfirmButton_Click(sender, e);
            }
        }

        private void DialogOkButton1_Click(object sender, RoutedEventArgs e)
        {
            Helper.RequestEmails();
            UpdateAllInfo();

            Helper.SetTextFieldDefault(NewPasswordBox);
            Helper.SetTextFieldDefault(EmailTextBox);
            Helper.SetTextFieldDefault(BirthPicker);
        }
    }
}
