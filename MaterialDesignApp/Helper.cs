using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialDesignApp
{
    static class Helper
    {
        // основная палитра приложения
        public static Color RedColor => Color.FromRgb(244, 67, 54);
        public static Color PurpleColor => Color.FromRgb(103, 58, 183);
        public static Color GreenColor => Color.FromRgb(39, 174, 96);
        public static Color GrayColor => Color.FromRgb(118, 118, 118);

        private static List<string> _userLogins = new List<string>();
        private static List<string> _userEmails = new List<string>();

        public static void RequestLogins()
        {
            _userLogins = Gamemanager.GetColumnInfo(Tables.players, "login");
        }

        public static void RequestEmails()
        {
            _userEmails = Gamemanager.GetColumnInfo(Tables.players, "email");
        }

        public static bool UserExists(string login)
        {
            return _userLogins.Contains(login);
        }

        public static void SetTextFieldColor(DependencyObject element, Color color)
        {
            MaterialDesignThemes.Wpf.TextFieldAssist.SetUnderlineBrush(element, new SolidColorBrush(color));

            if (element is PasswordBox)
                (element as PasswordBox).BorderBrush = new SolidColorBrush(color);
            else if (element is TextBox)
                (element as TextBox).BorderBrush = new SolidColorBrush(color);
            else if (element is DatePicker)
                (element as DatePicker).BorderBrush = new SolidColorBrush(color);
        }

        public static void SetTextFieldDefault(DependencyObject element)
        {
            Color purpleColor = Color.FromRgb(103, 58, 183);
            Color grayColor = Color.FromRgb(118, 118, 118);

            MaterialDesignThemes.Wpf.TextFieldAssist.SetUnderlineBrush(element, new SolidColorBrush(purpleColor));

            if (element is PasswordBox)
                (element as PasswordBox).BorderBrush = new SolidColorBrush(grayColor);
            else if (element is TextBox)
                (element as TextBox).BorderBrush = new SolidColorBrush(grayColor);
            else if (element is DatePicker)
                (element as DatePicker).BorderBrush = new SolidColorBrush(grayColor);
        }

        /// <summary>
        /// Проверяет строку на соответствие правилам составления Имени персонажа для его создания
        /// </summary>
        /// <param name="heroName"></param>
        /// <returns>Строка с текстом ошибки; Пустая строка в случае успешной проверки</returns>
        public static string ValidateHeroName(string heroName)
        {
            if (string.IsNullOrEmpty(heroName))
            {
                return "Поле обязательно для заполнения.";
            }
            else if (heroName.Contains(" ") || !Regex.IsMatch(heroName, @"^[a-zA-Z0-9]+$"))
            {
                return "Только цифры и символы латинского алфавита.";
            }
            else if (heroName.Length < 3 || heroName.Length > 30)
            {
                return "Имя должно содержать от 3 до 30 символов.";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Проверяет строку на соответствие правилам составления Логина для регистрации
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Строка с текстом ошибки; Пустая строка в случае успешной проверки</returns>
        public static string ValidateLogin(string login, bool isForLogin)
        {
            if (string.IsNullOrEmpty(login))
            {
                return "Поле обязательно для заполнения.";
            }
            else if (login.Contains(" ") || !Regex.IsMatch(login, @"^[a-zA-Z0-9]+$"))
            {
                return "Только цифры и символы латинского алфавита.";
            }
            else if (login.Length < 3 || login.Length > 20)
            {
                return "Логин должен содержать от 3 до 20 символов.";
            }
            else if (!isForLogin && UserExists(login))
            {
                return "Пользователь с таким логином уже существует.";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Проверяет строку на соответствие правилам составления Пароля для регистрации
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Строка с текстом ошибки; Пустая строка в случае успешной проверки</returns>
        public static string ValidatePassword(string password)
        {
            if (password.Length == 0)
            {
                return "Поле обязательно для заполнения.";
            }
            else if (password.Length < 6 || password.Length > 50)
            {
                return "Пароль должен содержать от 6 до 50 символов.";
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Проверяет строку на соответствие правилам составления Электронного адреса
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Строка с текстом ошибки; Пустая строка в случае успешной проверки</returns>
        public static string ValidateEmail(string email, bool isForLogin)
        {
            if (string.IsNullOrEmpty(email))
            {
                return "Поле обязательно для заполнения.";
            }
            else if (!IsValidEmail(email))
            {
                return "Неверный формат электронной почты.";
            }
            else if (!isForLogin && _userEmails.Contains(email))
            {
                return "Этот адрес уже используется.";
            }
            else
            {
                return "";
            }
        }

        // функция проверки электронной почты с ресурса
        // https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Зашифровывает переданный пароль с помощью добавления "соли" и хэш-алгоритма SHA256
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Кортеж (зашифрованный пароль, соль)</returns>
        public static (string, string) EncryptPassword(string password)
        {
            string salt = GetRandomSalt();
            string saltedPassword = GetHashString(password + salt);

            return (saltedPassword, salt);
        }

        /// <summary>
        /// Зашифровывает переданный пароль с помощью переданной "соли"
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>Зашифрованный пароль</returns>
        public static string EncryptPassword(string password, string salt)
        {
            string saltedPassword = GetHashString(password + salt);
            return saltedPassword;
        }

        private static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        private static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private static string GetRandomSalt()
        {
            // стандартная длина соли - 36 символв
            string salt = Guid.NewGuid().ToString();
            return salt;
        }

        public static int GetSaltLength()
        {
            return GetRandomSalt().Length;
        }

        public static int GetHashLength()
        {
            return GetHashString("random string").Length;
        }
    }
}
