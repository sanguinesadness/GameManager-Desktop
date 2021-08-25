using MaterialDesignApp.ReportTables;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MaterialDesignApp
{
    enum Tables
    {
        players,
        characters,
        character_classes,
        character_bans,
        player_characters,
        player_roles,
        genders,
        locations,
        items,
        mobs,
        item_categories,
        inventories
    }

    static class Gamemanager
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public static int ExecuteQuery(string sqlQuery)
        {
            int affectedRows = -1;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                affectedRows = command.ExecuteNonQuery();
            }

            return affectedRows;
        }

        public static bool RegisterUser(string login, string password, string email, DateTime? birthDate, Genders gender)
        {
            string sqlQuery;

            (string, string) passSaltTuple = Helper.EncryptPassword(password);
            string encryptedPass = passSaltTuple.Item1 + passSaltTuple.Item2;

            int genderID = GetGenderID(gender);

            string birthStr;
            if (birthDate.HasValue)
            {
                birthStr = $"{birthDate.Value.Year}-{birthDate.Value.Month}-{birthDate.Value.Day}";
                sqlQuery = $"INSERT INTO [{Tables.players.ToString()}] ([login], [password], [birth_date], [gender_id], [email])" +
                           $"VALUES ('{login}', '{encryptedPass}', '{birthStr}', '{genderID}', '{email}')";
            }
            else
            {
                sqlQuery = $"INSERT INTO [{Tables.players.ToString()}] ([login], [password], [gender_id], [email])" +
                           $"VALUES ('{login}', '{encryptedPass}', '{genderID}', '{email}')";
            }

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool LoginUser(string login, string password)
        {
            // получаем (пароль, соль) пользователя из таблицы БД
            (string, string) passAndSalt = GetUserPassword(login);

            // пользователь не найден -> вход не выполняется
            if (passAndSalt.Item1 is null || passAndSalt.Item2 is null)
            {
                return false;
            }

            string passFromDB = passAndSalt.Item1;
            string saltFromDB = passAndSalt.Item2;

            // зишифровываем пароль, введенный пользователем с солью, использованной при его регистрации
            string userPassword = Helper.EncryptPassword(password, saltFromDB);

            // хеш-ключи сходятся -> пользователь ввел правильный пароль
            return passFromDB == userPassword;
        }

        public static List<string> GetUserInfo(string login)
        {
            List<string> result = new List<string>();
            string sqlQuery = $"SELECT [id], [login], [birth_date], [gender_id], [email], [last_connection] FROM [{Tables.players}]" +
                              $"WHERE [login] = '{login}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    for (int i = 0; i < sqlReader.FieldCount; i++)
                    {
                        result.Add(sqlReader.GetString(i));
                    }
                }
            }

            return result;
        }

        public static int GetUserId(string login)
        {
            int userID = -1;
            string sqlQuery = $"SELECT [id] FROM [{Tables.players.ToString()}]" +
                              $"WHERE [login] = '{login}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return userID;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    userID = sqlReader.GetInt32(0);
                }
            }

            return userID;
        }

        public static int GetGenderID(Genders gender)
        {
            int genderID = -1;
            string sqlQuery = $"SELECT [id] FROM [{Tables.genders.ToString()}]" +
                              $"WHERE [name] = '{gender.ToString()}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return genderID;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    genderID = sqlReader.GetInt32(0);
                }
            }

            return genderID;
        }

        public static string GetGenderName(int genderID)
        {
            string result = string.Empty;
            string sqlQuery = $"SELECT [name] FROM [{Tables.genders.ToString()}]" +
                              $"WHERE [id] = '{genderID}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = sqlReader.GetString(0);
                }
            }

            return result;
        }

        public static string GetUserLogin(int userID)
        {
            string userLogin = "";
            string sqlQuery = $"SELECT [login] FROM [{Tables.players.ToString()}]" +
                              $"WHERE [id] = '{userID}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return userLogin;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    userLogin = sqlReader.GetString(0);
                }
            }

            return userLogin;
        }

        public static (string, string) GetUserPassword(string login)
        {
            (string, string) passAndSalt = (null, null);

            if (!UserExists(login))
            {
                return passAndSalt;
            }

            string sqlQuery = $"SELECT [password] FROM [{Tables.players.ToString()}]" +
                              $"WHERE [login] = '{login}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return passAndSalt;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();

                    string value = sqlReader.GetString(0);

                    // извлекаем зашифрованный пароль
                    passAndSalt.Item1 = value.Substring(0, Helper.GetHashLength());
                    // извлекаем использованную при шифровании соль
                    passAndSalt.Item2 = value.Substring(value.Length - Helper.GetSaltLength());
                }
            }

            return passAndSalt;
        }

        public static List<string> GetColumnInfo(Tables table, string columnName)
        {
            List<string> result = new List<string>();
            string sqlQuery = $"SELECT [{columnName}] FROM [{table.ToString()}]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        result.Add(sqlReader.GetString(0));
                    }
                }
            }

            return result;
        }

        public static string GetPlayerCharacterInfo(string charName, string columnName)
        {
            string result = "";
            string sqlQuery = $"SELECT [{columnName}] FROM [{Tables.player_characters.ToString()}] " +
                              $"WHERE [name] = '{charName}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = sqlReader.GetValue(0).ToString();
                }
            }

            return result;
        }

        public static string GetCharacterClassName(int classID)
        {
            string result = "";
            string sqlQuery = $"SELECT [name] FROM [{Tables.character_classes.ToString()}] " +
                              $"WHERE [id] = '{classID}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = sqlReader.GetValue(0).ToString();
                }
            }

            return result;
        }

        public static List<List<string>> GetPlayerCharactersV1(int playerID)
        {
            List<List<string>> result = new List<List<string>>();
            string sqlQuery = $"SELECT [name], [character_id], [current_location_id], [creation_date], [lvl], [rating_scores], [gold_amount], [played_hours] FROM [{Tables.player_characters.ToString()}] " +
                              $"WHERE [player_id] = '{playerID}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        List<string> row = new List<string>();

                        for (int i = 0; i < 8; i++)
                            row.Add(sqlReader.GetValue(i).ToString());

                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static List<Character> GetPlayerCharactersV2(int playerID)
        {
            List<Character> result = new List<Character>();
            string sqlQuery = $"SELECT [name], [character_id], [current_location_id], [creation_date], [lvl], [rating_scores], [gold_amount], [played_hours], [current_location_id] FROM [{Tables.player_characters.ToString()}] " +
                              $"WHERE [player_id] = '{playerID}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        string name = sqlReader.GetString(0);
                        int charID = sqlReader.GetInt32(1);
                        int locationID = sqlReader.GetInt32(2);
                        DateTime? creationDate = sqlReader.GetDateTime(3);
                        int lvl = sqlReader.GetInt32(4);
                        int rating = sqlReader.GetInt32(5);
                        int gold = sqlReader.GetInt32(6);
                        int playedHours = sqlReader.GetInt32(7);
                        int currentLocationID = sqlReader.GetInt32(8);

                        string iconPath = ReadValue($"SELECT [icon_img] FROM [{Tables.characters.ToString()}] " +
                                                    $"WHERE [id] = {charID}").ToString();

                        string banned = "Нет";
                        if (IsCharacterBanned(playerID, charID))
                            banned = "Да";

                        Character character = new Character(playerID, charID, name, iconPath, creationDate, gold, rating, lvl, playedHours, currentLocationID, banned);

                        result.Add(character);
                    }
                }
            }


            return result;

        }

        public static List<List<string>> GetAvailableCharacters()
        {
            List<List<string>> result = new List<List<string>>();
            string sqlQuery = $"SELECT [class_id], [name], [icon_img], [avatar_img], [description], [id] FROM [{Tables.characters.ToString()}]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    int classID, ID;
                    string name, iconImg, avatarImg, description;

                    while (sqlReader.Read())
                    {
                        classID = sqlReader.GetInt32(0);
                        name = sqlReader.GetString(1);
                        iconImg = sqlReader.GetString(2);
                        avatarImg = sqlReader.GetString(3);
                        description = sqlReader.GetString(4);
                        ID = sqlReader.GetInt32(5);

                        string className = GetCharacterClassName(classID);

                        List<string> row = new List<string>() { className, name, iconImg, avatarImg, description, ID.ToString() };
                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static string GetCharacterInfo(int charID, string columnName)
        {
            string result = "";
            string sqlQuery = $"SELECT [{columnName}] FROM [{Tables.characters.ToString()}]" +
                              $"WHERE [id] = '{charID}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = sqlReader.GetValue(0).ToString();
                }
            }

            return result;
        }

        public static int GetCharacterID(string charName)
        {
            int result = -1;
            string sqlQuery = $"SELECT [id] FROM [{Tables.characters.ToString()}] " +
                              $"WHERE [name] = '{charName}'";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = sqlReader.GetInt32(0);
                }
            }

            return result;
        }

        public static string GetCharacterName(int charID)
        {
            string result = string.Empty;
            string sqlQuery = $"SELECT [name] FROM [{Tables.characters.ToString()}] " +
                              $"WHERE [id] = {charID}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = sqlReader.GetString(0);
                }
            }

            return result;
        }

        public static bool UserExists(string login)
        {
            return Helper.UserExists(login);
        }

        public static bool UserExists(int userID)
        {
            string userLogin = GetUserLogin(userID);
            return Helper.UserExists(userLogin);
        }

        public static bool IsCharacterBanned(int playerID, int charID)
        {
            string sqlQuery = $"SELECT * FROM [{Tables.character_bans.ToString()}]" +
                              $"WHERE [player_id] = '{playerID}' AND [character_id] = {charID};";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return false;
                }

                return sqlReader.HasRows;
            }
        }

        public static List<string> GetAllAboutCharacter(int playerID, int charID)
        {
            List<string> result = new List<string>();
            string sqlQuery = $"SELECT [name], [current_location_id], [creation_date], [lvl], [rating_scores], [gold_amount], [played_hours] FROM [{Tables.player_characters.ToString()}] " +
                              $"WHERE [player_id] = {playerID} AND [character_id] = {charID}";

            int currentLocationID, lvl, ratingScores, goldAmount, playedHours;
            string creationDate, playerCharName;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();

                    playerCharName = sqlReader.GetString(0);
                    currentLocationID = sqlReader.GetInt32(1);
                    creationDate = sqlReader.GetValue(2).ToString();
                    lvl = sqlReader.GetInt32(3);
                    ratingScores = sqlReader.GetInt32(4);
                    goldAmount = sqlReader.GetInt32(5);
                    playedHours = sqlReader.GetInt32(6);

                    string userStatus = IsCharacterBanned(playerID, charID) ? "Заблокирован" : "Активен";

                    // извлечение названия локации
                    sqlQuery = $"SELECT [name] FROM [{Tables.locations.ToString()}] " +
                               $"WHERE [id] = '{currentLocationID}'";
                    command = new SqlCommand(sqlQuery, connection);
                    sqlReader.Close();
                    sqlReader = command.ExecuteReader();
                    sqlReader.Read();
                    string currentLocationStr = sqlReader.GetString(0);

                    // извлечение названия персонажа
                    string characterName = GetCharacterName(charID);

                    // извлечение названия класса персонажа
                    sqlQuery = $"SELECT [name] FROM [character_classes] " +
                               $"WHERE [id] = (SELECT [class_id] FROM [characters] WHERE[id] = {charID})";
                    command.Dispose();
                    command = new SqlCommand(sqlQuery, connection);
                    sqlReader.Close();
                    sqlReader = command.ExecuteReader();
                    sqlReader.Read();
                    string charClassName = sqlReader.GetString(0);

                    // добавление результатов в список
                    result.Add(userStatus);
                    result.Add(characterName);
                    result.Add(currentLocationStr);
                    result.Add(lvl.ToString());
                    result.Add(charClassName);
                    result.Add(playedHours.ToString());
                    result.Add(goldAmount.ToString());
                    result.Add(ratingScores.ToString());
                    result.Add(creationDate.Split(' ')[0]);
                    result.Add(playerID.ToString());
                    result.Add(charID.ToString());
                    result.Add(playerCharName);
                }
            }

            return result;
        }

        public static bool RemovePlayerCharacter(int playerID, int charID)
        {
            string sqlQuery = $"DELETE FROM [player_characters] " +
                              $"WHERE [player_id] = {playerID} AND [character_id] = {charID}";

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool AddPlayerCharacter(string name, int charID, int playerID)
        {
            DateTime dateNow = DateTime.Now;
            string dateNowFormatted = $"{dateNow.Year}-{dateNow.Month}-{dateNow.Day}";

            const int DEFAULT_LOCATION = 3;

            Random rnd = new Random();
            int goldAmount = rnd.Next(10, 5000);
            int charLvl = rnd.Next(1, 30);
            int playedHours = rnd.Next(1, 100);
            int ratingScores = rnd.Next(100, 7200);

            string sqlQuery = $"INSERT INTO [{Tables.player_characters.ToString()}] " +
                              $"([character_id], [name], [creation_date], [current_location_id], [gold_amount], [lvl], [played_hours], [player_id], [rating_scores]) " +
                              $"VALUES({charID}, '{name}', '{dateNowFormatted}', {DEFAULT_LOCATION}, {goldAmount}, {charLvl}, {playedHours}, {playerID}, {ratingScores})";

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static Dictionary<int, string> GetAllLocations()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string sqlQuery = $"SELECT [id], [name] FROM [{Tables.locations.ToString()}]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        result.Add(sqlReader.GetInt32(0), sqlReader.GetString(1));
                    }
                }
            }

            return result;
        }

        public static Dictionary<int, string> GetAllItems()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string sqlQuery = $"SELECT [id], [name] FROM [{Tables.items.ToString()}]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        result.Add(sqlReader.GetInt32(0), sqlReader.GetString(1));
                    }
                }
            }

            return result;
        }

        public static Dictionary<int, string> GetAllMobs()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            string sqlQuery = $"SELECT [id], [name] FROM [{Tables.mobs.ToString()}]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        result.Add(sqlReader.GetInt32(0), sqlReader.GetString(1));
                    }
                }
            }

            return result;
        }

        public static string GetDescription(int ID, Tables table)
        {
            string result = string.Empty;
            string sqlQuery = $"SELECT [description] FROM [{table.ToString()}] " +
                              $"WHERE [id] = {ID}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = sqlReader.GetString(0);
                }
            }

            return result;
        }

        public static object ReadValue(string sqlQuery)
        {
            object result = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = sqlReader.GetValue(0);
                }
            }

            return result;
        }

        public static List<List<string>> GetItemCategories()
        {
            List<List<string>> result = new List<List<string>>();
            string sqlQuery = $"SELECT [id], [name], [icon] FROM [{Tables.item_categories.ToString()}]";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        List<string> row = new List<string>();

                        row.Add(sqlReader["id"].ToString());
                        row.Add(sqlReader["name"].ToString());
                        row.Add(sqlReader["icon"].ToString());

                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static List<List<string>> GetItemsByCategory(int categoryID)
        {
            List<List<string>> result = new List<List<string>>();
            string sqlQuery = $"SELECT [id], [name], [icon] FROM [{Tables.items.ToString()}] " +
                              $"WHERE [category_id] = {categoryID}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return null;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        List<string> row = new List<string>();

                        row.Add(sqlReader["id"].ToString());
                        row.Add(sqlReader["name"].ToString());
                        row.Add(sqlReader["icon"].ToString());

                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static bool AddNewItems(int playerID, int charID, int itemID, int amount)
        {
            string sqlQuery = $"INSERT INTO [{Tables.inventories.ToString()}] ([player_id], [character_id], [item_id], [amount]) " +
                              $"VALUES ({playerID}, {charID}, {itemID}, {amount});";

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return UpdateExistedItems(playerID, charID, itemID, amount);
            }
        }

        public static bool UpdateExistedItems(int playerID, int charID, int itemID, int addAmount)
        {
            string selectQuery = $"SELECT [amount] FROM [{Tables.inventories.ToString()}] " +
                                 $"WHERE [player_id] = {playerID} AND [character_id] = {charID} AND [item_id] = {itemID};";

            int currentAmount = (int)ReadValue(selectQuery);
            int newAmount = currentAmount + addAmount;

            string updateQuery = $"UPDATE [{Tables.inventories.ToString()}] " +
                                 $"SET [amount] = {newAmount} " +
                                 $"WHERE [player_id] = {playerID} AND [character_id] = {charID} AND [item_id] = {itemID};";

            try
            {
                ExecuteQuery(updateQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool UpdateGoldBalance(int playerID, int charID, int goldAmount)
        {
            string sqlQuery = $"UPDATE [{Tables.player_characters}] " +
                              $"SET [gold_amount] = {goldAmount} " +
                              $"WHERE [player_id] = {playerID} AND [character_id] = {charID}";

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static List<List<string>> GetCharacterItems(int playerID, int charID)
        {
            List<List<string>> result = new List<List<string>>();
            string sqlQuery = $"SELECT [item_id], [amount] FROM [{Tables.inventories.ToString()}] " +
                              $"WHERE [player_id] = {playerID} AND [character_id] = {charID};";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        int itemID = sqlReader.GetInt32(0);
                        int amount = sqlReader.GetInt32(1);

                        string itemName = ReadValue($"SELECT [name] FROM [{Tables.items.ToString()}] " +
                            $"WHERE [id] = {itemID};").ToString();
                        string itemIcon = ReadValue($"SELECT [icon] FROM [{Tables.items.ToString()}] " +
                                                    $"WHERE [id] = {itemID};").ToString();

                        List<string> row = new List<string>() { itemIcon, itemName, amount.ToString() };
                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static List<object> GetPlayerInfo(int playerID)
        {
            List<object> result = new List<object>();

            string sqlQuery = $"SELECT [login], [birth_date], [gender_id], [email] FROM [{Tables.players.ToString()}] " +
                              $"WHERE [id] = {playerID}";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();

                    string login = sqlReader.GetString(0);

                    DateTime? birthDate = null;
                    try { birthDate = sqlReader.GetDateTime(1); }
                    catch { }

                    string genderName = GetGenderName(sqlReader.GetInt32(2));
                    string email = sqlReader.GetString(3);

                    result.Add(login);
                    result.Add(email);
                    result.Add(birthDate);
                    result.Add(genderName);
                }
            }

            return result;
        }

        public static bool UpdatePlayerInfo(int playerID, DateTime? birthDate, Genders gender, string email)
        {
            int genderID = GetGenderID(gender);

            string dateFormatted = string.Empty;
            if (birthDate.HasValue)
            {
                DateTime date = birthDate.Value;
                dateFormatted = $"{date.Year}-{date.Month}-{date.Day}";
            }

            string sqlQuery = $"UPDATE [{Tables.players.ToString()}] " +
                              $"SET [birth_date] = '{dateFormatted}', [gender_id] = {genderID}, [email] = '{email}' " +
                              $"WHERE [id] = {playerID};";

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool UpdatePlayerInfo(int playerID, string password, DateTime? birthDate, Genders gender, string email)
        {
            int genderID = GetGenderID(gender);

            (string, string) passSaltTuple = Helper.EncryptPassword(password);
            string encryptedPass = passSaltTuple.Item1 + passSaltTuple.Item2;

            string dateFormatted = string.Empty;
            if (birthDate.HasValue)
            {
                DateTime date = birthDate.Value;
                dateFormatted = $"{date.Year}-{date.Month}-{date.Day}";
            }

            string sqlQuery = $"UPDATE [{Tables.players.ToString()}] " +
                              $"SET [password] = '{encryptedPass}', [birth_date] = '{dateFormatted}', [gender_id] = {genderID}, [email] = '{email}' " +
                              $"WHERE [id] = {playerID};";

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static string GetUserRole(int userID)
        {
            string selectRoleID = $"SELECT [role_id] FROM [{Tables.players.ToString()}] " +
                                  $"WHERE [id] = {userID}";

            string selectRoleName = $"SELECT [name] FROM [{Tables.player_roles.ToString()}] " +
                                    $"WHERE [id] = ({selectRoleID});";

            return ReadValue(selectRoleName).ToString();
        }

        public static List<Player> GetPlayers()
        {
            List<Player> result = new List<Player>();

            string selectPlayerRoleID = $"SELECT [id] FROM [{Tables.player_roles.ToString()}] " +
                                        $"WHERE [name] = 'Player'";
            string selectPlayers = $"SELECT [id], [login], [birth_date], [gender_id], [email], [last_connection] FROM [{Tables.players.ToString()}] " +
                                   $"WHERE [role_id] = ({selectPlayerRoleID});";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(selectPlayers, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        int id = sqlReader.GetInt32(0);
                        string login = sqlReader.GetString(1);

                        DateTime? birthDate = null;
                        if (!sqlReader.IsDBNull(2))
                            birthDate = sqlReader.GetDateTime(2);

                        string gender = GetGenderName(sqlReader.GetInt32(3));
                        string email = sqlReader.GetString(4);
                        DateTime lastConnection = sqlReader.GetDateTime(5);

                        Player player = new Player(id, login, gender, birthDate, email, lastConnection);
                        result.Add(player);
                    }
                }
            }

            return result;
        }

        public static bool BanCharacter(int playerID, int charID, string reason)
        {
            DateTime dateNow = DateTime.Now;
            string dateNowFormatted = $"{dateNow.Year}-{dateNow.Month}-{dateNow.Day} {dateNow.Hour}:{dateNow.Minute}:{dateNow.Second}:{dateNow.Millisecond}";

            string sqlQuery = $"INSERT INTO [{Tables.character_bans.ToString()}] " +
                              $"([player_id], [character_id], [date], [reason]) " +
                              $"VALUES ({playerID}, {charID}, '{dateNowFormatted}', '{reason}');";

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static bool UnbanCharacter(int playerID, int charID)
        {
            string sqlQuery = $"DELETE FROM [{Tables.character_bans.ToString()}] " +
                              $"WHERE [player_id] = {playerID} AND [character_id] = {charID};";

            try
            {
                ExecuteQuery(sqlQuery);
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static List<MostActivePlayersRow> GetMostActivePlayersTable()
        {
            List<MostActivePlayersRow> result = new List<MostActivePlayersRow>();
            string sqlQuery = $"SELECT [players].[login] as PlayerName, a.NumberOfCharacters, a.TotalHours, ROUND((CAST(a.TotalHours as FLOAT) / CAST(a.NumberOfCharacters as FLOAT)), 1) AS AverageHours FROM " +
                              $"(SELECT[player_id], COUNT(*) AS NumberOfCharacters, SUM([played_hours]) AS TotalHours FROM[{Tables.player_characters.ToString()}] GROUP BY[player_id]) a " +
                              $"INNER JOIN[players] " +
                              $"ON a.player_id = players.id " +
                              $"ORDER BY a.TotalHours DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        string playerName = sqlReader.GetString(0);
                        int numOfChars = sqlReader.GetInt32(1);
                        int totalHours = sqlReader.GetInt32(2);
                        double avgHours = sqlReader.GetDouble(3);

                        MostActivePlayersRow row = new MostActivePlayersRow(playerName, numOfChars, totalHours, avgHours);
                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static List<MostExpensiveInventoriesRow> GetMostExpensiveInventoriesTable()
        {
            List<MostExpensiveInventoriesRow> result = new List<MostExpensiveInventoriesRow>();
            string sqlQuery = $"SELECT [name] as CharacterName, NumberOfItems, TotalValue, PlayerName FROM " +
                              $"(SELECT [character_id], [player_id], NumberOfItems, TotalValue, [login] as PlayerName FROM " +
                              $"(SELECT [character_id], [player_id], COUNT([item_id]) as NumberOfItems, SUM(TotalPrice) as TotalValue FROM " +
                              $"(SELECT [character_id], [player_id], [item_id], ([amount] * [price]) as TotalPrice FROM " +
                              $"(SELECT [character_id], [player_id], [item_id], [amount] FROM [{Tables.inventories.ToString()}]) a " +
                              $"INNER JOIN [{Tables.items.ToString()}] ON [{Tables.items.ToString()}].[id] = a.[item_id]) b " +
                              $"GROUP BY [character_id], [player_id]) c " +
                              $"INNER JOIN [{Tables.players.ToString()}] ON c.[player_id] = [{Tables.players.ToString()}].[id]) d " +
                              $"INNER JOIN [{Tables.player_characters.ToString()}] ON d.[character_id] = [{Tables.player_characters.ToString()}].character_id AND d.[player_id] = [{Tables.player_characters.ToString()}].[player_id] " +
                              $"ORDER BY TotalValue DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        string charName = sqlReader.GetString(0);
                        int numOfItems = sqlReader.GetInt32(1);
                        int totalValue = sqlReader.GetInt32(2);
                        string playerName = sqlReader.GetString(3);

                        MostExpensiveInventoriesRow row = new MostExpensiveInventoriesRow(charName, numOfItems, totalValue, playerName);
                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static List<TopRatingRow> GetTopRatingTable()
        {
            List<TopRatingRow> result = new List<TopRatingRow>();
            string sqlQuery = $"SELECT [name] as CharacterName, [rating_scores] as Rating, [login] as PlayerName FROM " +
                              $"(SELECT [name], [player_id], [rating_scores] FROM [{Tables.player_characters.ToString()}]) a " +
                              $"INNER JOIN [{Tables.players.ToString()}] ON a.[player_id] = [{Tables.players.ToString()}].[id] " +
                              $"ORDER BY Rating DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        string charName = sqlReader.GetString(0);
                        int rating = sqlReader.GetInt32(1);
                        string playerName = sqlReader.GetString(2);

                        TopRatingRow row = new TopRatingRow(charName, rating, playerName);
                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static List<RichestPlayersRow> GetRichestPlayersTable()
        {
            List<RichestPlayersRow> result = new List<RichestPlayersRow>();
            string sqlQuery = $"SELECT [login] as PlayerName, NumberOfCharacters, TotalGold FROM " +
                              $"(SELECT [player_id], COUNT([character_id]) as NumberOfCharacters, SUM([gold_amount]) as TotalGold " +
                              $"FROM [{Tables.player_characters.ToString()}] " +
                              $"GROUP BY [player_id]) a " +
                              $"INNER JOIN [{Tables.players.ToString()}] ON a.[player_id] = [{Tables.players.ToString()}].[id] " +
                              $"ORDER BY [TotalGold] DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        string playerName = sqlReader.GetString(0);
                        int numOfChars = sqlReader.GetInt32(1);
                        int totalGold = sqlReader.GetInt32(2);

                        RichestPlayersRow row = new RichestPlayersRow(playerName, numOfChars, totalGold);
                        result.Add(row);
                    }
                }
            }

            return result;
        }

        public static List<HighestLevelsRow> GetHighestLevelsTable()
        {
            List<HighestLevelsRow> result = new List<HighestLevelsRow>();
            string sqlQuery = $"SELECT [name] as CharacterName, [lvl] as Level, [login] AS PlayerName FROM " +
                              $"(SELECT [player_id], [name], [lvl] FROM [{Tables.player_characters.ToString()}]) a " +
                              $"INNER JOIN [{Tables.players.ToString()}] ON a.[player_id] = [{Tables.players.ToString()}].[id] " +
                              $"ORDER BY [Level] DESC;";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                SqlDataReader sqlReader;

                try
                {
                    sqlReader = command.ExecuteReader();
                }
                catch (SqlException)
                {
                    return result;
                }

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        string charName = sqlReader.GetString(0);
                        int lvl = sqlReader.GetInt32(1);
                        string playerName = sqlReader.GetString(2);

                        HighestLevelsRow row = new HighestLevelsRow(charName, lvl, playerName);
                        result.Add(row);
                    }
                }
            }

            return result;
        }
    }
}
