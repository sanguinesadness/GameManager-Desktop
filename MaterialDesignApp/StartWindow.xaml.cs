using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
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
using System.Windows.Shapes;

namespace MaterialDesignApp
{
    public enum Genders
    {
        Мужской,
        Женский,
        Другое
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        // флаги для проверки регистрационных данных
        private bool isLoginCorrect = false;
        private bool isPassword1Correct = false;
        private bool isPassword2Correct = false;
        private bool isBirthCorrect = true;
        private bool isEmailCorrect = false;

        public StartWindow()
        {
            InitializeComponent();

            Helper.RequestEmails();
            Helper.RequestLogins();
        }

        #region Common methods

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void CloseButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CloseButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MouseDown -= Window_MouseDown;
        }

        private void CloseButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.MouseDown += Window_MouseDown;
        }

        private void TextField_MouseEnter(object sender, MouseEventArgs e)
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

        private void TextField_MouseLeave(object sender, MouseEventArgs e)
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

        #endregion

        #region LoginPage methods

        private void RefreshLoginPage()
        {
            LoginTextBox1.Clear();
            LoginError1.Visibility = Visibility.Hidden;
            Helper.SetTextFieldDefault(LoginTextBox1);

            PasswordBox1.Clear();
            PasswordError1.Visibility = Visibility.Hidden;
            Helper.SetTextFieldDefault(PasswordBox1);

            RememberPasswordCheckBox.IsChecked = false;
        }

        private void TextGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // убираем текст ошибки и меняем цвет текстового поля на стандартный
            var textField = (sender as Grid).Children[0];
            var errorElement = (sender as Grid).Children[1];
            Helper.SetTextFieldDefault(textField);
            errorElement.Visibility = Visibility.Hidden;
        }

        private void LoginButton1_Click(object sender, RoutedEventArgs e)
        {
            bool loginIsCorrect = false;
            bool passwordIsCorrect = false;

            string login = LoginTextBox1.Text;
            string password = PasswordBox1.Password;

            string errorMsg = Helper.ValidateLogin(login, true);

            if (string.IsNullOrEmpty(errorMsg))
            {
                LoginError1.Visibility = Visibility.Hidden;
                Helper.SetTextFieldDefault(LoginTextBox1);

                loginIsCorrect = true;
            }
            else
            {
                LoginError1.Visibility = Visibility.Visible;
                LoginError1.Text = errorMsg;
                Helper.SetTextFieldColor(LoginTextBox1, Helper.RedColor);

                loginIsCorrect = false;
            }

            errorMsg = Helper.ValidatePassword(password);

            if (string.IsNullOrEmpty(errorMsg))
            {
                PasswordError1.Visibility = Visibility.Hidden;
                Helper.SetTextFieldDefault(PasswordBox1);

                passwordIsCorrect = true;
            }
            else
            {
                PasswordError1.Visibility = Visibility.Visible;
                PasswordError1.Text = errorMsg;
                Helper.SetTextFieldColor(PasswordBox1, Helper.RedColor);

                passwordIsCorrect = false;
            }

            if (loginIsCorrect && passwordIsCorrect)
            {
                bool isSuccessLogin = Gamemanager.LoginUser(login, password);

                if (isSuccessLogin)
                {
                    RefreshLoginPage();

                    DataExchanger.CurrentUserLogin = login;
                    string userRole = Gamemanager.GetUserRole(DataExchanger.CurrectUserID);

                    if (userRole == "Player")
                    {
                        UserWindow userWindow = new UserWindow();
                        userWindow.Show();
                    }
                    else
                    {
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                    }

                    this.Close();
                }
                else
                {
                    LoginDialogHost.IsOpen = true;
                }
            }
        }

        private void RegButton1_Click(object sender, RoutedEventArgs e)
        {
            RegPage.Visibility = Visibility.Visible;
            LoginPage.Visibility = Visibility.Visible;

            TranslateTransform tr1 = new TranslateTransform();
            RegPage.RenderTransform = tr1;
            TranslateTransform tr2 = new TranslateTransform();
            LoginPage.RenderTransform = tr2;

            DoubleAnimation translateX1 = new DoubleAnimation(1050, 0, TimeSpan.FromSeconds(0.5));
            translateX1.EasingFunction = new PowerEase();
            DoubleAnimation translateX2 = new DoubleAnimation(0, -1050, TimeSpan.FromSeconds(0.5));
            translateX2.EasingFunction = new PowerEase();

            tr1.BeginAnimation(TranslateTransform.XProperty, translateX1);
            tr2.BeginAnimation(TranslateTransform.XProperty, translateX2);

            RefreshLoginPage();
        }

        private void LoginTextBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                PasswordBox1.Focus();
            }
        }

        private void PasswordBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                LoginTextBox1.Focus();
            }
        }

        private void LoginKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton1_Click(sender, e);
            }
        }

        #endregion

        #region RegPage methods
        private void RefreshRegPage()
        {
            LoginTextBox2.Clear();
            RegPasswordBox1.Clear();
            RegPasswordBox2.Clear();
            EmailTextBox.Clear();
            BirthPicker.Text = "";

            foreach (var selection in GenderSelection.Children)
            {
                (selection as RadioButton).IsChecked = false;
            }
            (GenderSelection.Children[0] as RadioButton).IsChecked = true;

            LoginError2.Visibility = Visibility.Hidden;
            RegPasswordError1.Visibility = Visibility.Hidden;
            RegPasswordError2.Visibility = Visibility.Hidden;
            EmailError.Visibility = Visibility.Hidden;

            Helper.SetTextFieldDefault(LoginTextBox2);
            Helper.SetTextFieldDefault(RegPasswordBox1);
            Helper.SetTextFieldDefault(RegPasswordBox2);
            Helper.SetTextFieldDefault(EmailTextBox);
            Helper.SetTextFieldDefault(BirthPicker);
        }

        private void HaveAccountButton_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransform tr1 = new TranslateTransform();
            RegPage.RenderTransform = tr1;
            TranslateTransform tr2 = new TranslateTransform();
            LoginPage.RenderTransform = tr2;

            DoubleAnimation translateX1 = new DoubleAnimation(0, 1050, TimeSpan.FromSeconds(0.5));
            translateX1.EasingFunction = new PowerEase();
            DoubleAnimation translateX2 = new DoubleAnimation(-1050, 0, TimeSpan.FromSeconds(0.5));
            translateX2.EasingFunction = new PowerEase();

            tr1.BeginAnimation(TranslateTransform.XProperty, translateX1);
            tr2.BeginAnimation(TranslateTransform.XProperty, translateX2);

            RefreshRegPage();
        }

        private void LoginTextBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox2.Text))
            {
                LoginError2.Visibility = Visibility.Hidden;
                Helper.SetTextFieldDefault(LoginTextBox2);
            }
        }

        private void LoginTextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            string errorMsg = Helper.ValidateLogin(LoginTextBox2.Text, false);

            if (string.IsNullOrEmpty(errorMsg))
            {
                Helper.SetTextFieldColor(LoginTextBox2, Helper.GreenColor);
                LoginError2.Visibility = Visibility.Hidden;

                isLoginCorrect = true;
            }
            else
            {
                Helper.SetTextFieldColor(LoginTextBox2, Helper.RedColor);
                LoginError2.Visibility = Visibility.Visible;
                LoginError2.Text = errorMsg;

                isLoginCorrect = false;
            }
        }

        private void RegPasswordBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RegPasswordBox1.Password))
            {
                RegPasswordError1.Visibility = Visibility.Hidden;
                Helper.SetTextFieldDefault(RegPasswordBox1);
            }

            if (!isPassword1Correct)
            {
                RegPasswordBox1.PasswordChanged += RegPasswordBox1_PasswordChanged;
            }
        }

        private void RegPasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string errorMsg = Helper.ValidatePassword(RegPasswordBox1.Password);

            if (string.IsNullOrEmpty(errorMsg))
            {
                isPassword1Correct = true;
            }
            else
            {
                RegPasswordError1.Text = errorMsg;
                isPassword1Correct = false;
            }

            if (isPassword1Correct)
            {
                Helper.SetTextFieldColor(RegPasswordBox1, Helper.GreenColor);
                RegPasswordError1.Visibility = Visibility.Hidden;
            }
            else
            {
                Helper.SetTextFieldColor(RegPasswordBox1, Helper.RedColor);
                RegPasswordError1.Visibility = Visibility.Visible;
            }

            if (RegPasswordBox2.Password.Length == 0)
            {
                RegPasswordError2.Text = "Поле обязательно для заполнения.";
                isPassword2Correct = false;
            }
            else if (RegPasswordBox1.Password != RegPasswordBox2.Password)
            {
                Helper.SetTextFieldColor(RegPasswordBox2, Helper.RedColor);
                RegPasswordError2.Visibility = Visibility.Visible;
                RegPasswordError2.Text = "Пароли не совпадают.";
                isPassword2Correct = false;
            }
            else
            {
                Helper.SetTextFieldColor(RegPasswordBox2, Helper.GreenColor);
                RegPasswordError2.Visibility = Visibility.Hidden;

                isPassword2Correct = true;
            }
        }

        private void RegPasswordBox2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(RegPasswordBox2.Password))
            {
                RegPasswordError2.Visibility = Visibility.Hidden;
                Helper.SetTextFieldDefault(RegPasswordBox2);
            }

            if (!isPassword2Correct)
            {
                RegPasswordBox2.PasswordChanged += RegPasswordBox2_PasswordChanged;
            }
        }

        private void RegPasswordBox2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RegPasswordBox2.Password.Length == 0)
            {
                RegPasswordError2.Text = "Поле обязательно для заполнения.";
                isPassword2Correct = false;
            }
            else if (RegPasswordBox1.Password != RegPasswordBox2.Password)
            {
                RegPasswordError2.Text = "Пароли не совпадают.";
                isPassword2Correct = false;
            }
            else
            {
                isPassword2Correct = true;
            }

            if (isPassword2Correct)
            {
                Helper.SetTextFieldColor(RegPasswordBox2, Helper.GreenColor);
                RegPasswordError2.Visibility = Visibility.Hidden;
            }
            else
            {
                Helper.SetTextFieldColor(RegPasswordBox2, Helper.RedColor);
                RegPasswordError2.Visibility = Visibility.Visible;
            }
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
            string errorMsg = Helper.ValidateEmail(EmailTextBox.Text, false);

            if (string.IsNullOrEmpty(errorMsg))
            {
                Helper.SetTextFieldColor(EmailTextBox, Helper.GreenColor);
                EmailError.Visibility = Visibility.Hidden;

                isEmailCorrect = true;
            }
            else
            {
                Helper.SetTextFieldColor(EmailTextBox, Helper.RedColor);
                EmailError.Visibility = Visibility.Visible;
                EmailError.Text = errorMsg;

                isEmailCorrect = false;
            }
        }

        private void BirthPicker_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            if (BirthPicker.SelectedDate.HasValue && BirthPicker.SelectedDate > DateTime.Now)
            {
                Helper.SetTextFieldColor(BirthPicker, Helper.RedColor);
                isBirthCorrect = false;
            }
            else if (BirthPicker.SelectedDate.HasValue && BirthPicker.SelectedDate < DateTime.Now)
            {
                Helper.SetTextFieldColor(BirthPicker, Helper.GreenColor);
                isBirthCorrect = true;
            }
            else
            {
                Helper.SetTextFieldDefault(BirthPicker);
                isBirthCorrect = true;
            }
        }

        private void LoginTextBox2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                RegPasswordBox1.Focus();
            }

            if (e.Key == Key.Right)
            {
                EmailTextBox.Focus();
            }
        }

        private void RegPasswordBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                LoginTextBox2.Focus();
            }

            if (e.Key == Key.Down)
            {
                RegPasswordBox2.Focus();
            }

            if (e.Key == Key.Right)
            {
                BirthPicker.Focus();
            }
        }

        private void RegPasswordBox2_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                RegPasswordBox1.Focus();
            }

            if (e.Key == Key.Right)
            {
                GenderSelection.Children[1].Focus();
            }
        }

        private void EmailTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                LoginTextBox2.Focus();
            }

            if (e.Key == Key.Down)
            {
                BirthPicker.Focus();
            }
        }

        private void BirthPicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                EmailTextBox.Focus();
            }

            if (e.Key == Key.Left)
            {
                RegPasswordBox1.Focus();
            }

            if (e.Key == Key.Down)
            {
                GenderSelection.Children[1].Focus();
            }
        }

        private void RegKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RegButton2_Click(sender, e);
            }
        }

        // нажатие на кнопку регистрации -> проверка входных данных и добавление пользователя в БД
        private void RegButton2_Click(object sender, RoutedEventArgs e)
        {
            if (isLoginCorrect && isPassword1Correct && isPassword2Correct && isEmailCorrect && isBirthCorrect)
            {
                string login = LoginTextBox2.Text;
                string password = RegPasswordBox1.Password;
                string email = EmailTextBox.Text;
                DateTime? birthDate = BirthPicker.SelectedDate;
                Genders gender = Genders.Мужской;

                for (int i = 0; i < GenderSelection.Children.Count; i++)
                {
                    var radioButton = (GenderSelection.Children[i] as RadioButton);

                    if (radioButton.IsChecked == true)
                    {
                        gender = (Genders) i;
                    }
                }

                bool isSuccess = Gamemanager.RegisterUser(login, password, email, birthDate, gender);

                if (isSuccess)
                {
                    RegDialogHost.IsOpen = true;
                    DialogUserLogin.Text = login;
                    RegDialogError.Visibility = Visibility.Hidden;
                    RegDialogSuccess.Visibility = Visibility.Visible;
                    RefreshRegPage();

                    Helper.RequestEmails();
                    Helper.RequestLogins();
                }
                else
                {
                    RegDialogHost.IsOpen = true;
                    RegDialogError.Visibility = Visibility.Visible;
                    RegDialogSuccess.Visibility = Visibility.Hidden;
                }

                return;
            }

            if (string.IsNullOrEmpty(LoginTextBox2.Text))
            {
                Helper.SetTextFieldColor(LoginTextBox2, Helper.RedColor);
                LoginError2.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(RegPasswordBox1.Password))
            {
                Helper.SetTextFieldColor(RegPasswordBox1, Helper.RedColor);
                RegPasswordError1.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(RegPasswordBox2.Password))
            {
                Helper.SetTextFieldColor(RegPasswordBox2, Helper.RedColor);
                RegPasswordError2.Visibility = Visibility.Visible;
            }

            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                Helper.SetTextFieldColor(EmailTextBox, Helper.RedColor);
                EmailError.Visibility = Visibility.Visible;
            }
        }

        #endregion
    }
}