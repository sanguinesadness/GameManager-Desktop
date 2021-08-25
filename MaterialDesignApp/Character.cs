using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MaterialDesignApp
{
    public class Character
    {
        // поле для колонки "#" в CharactersDataGrid
        public int Number { get; set; }

        public SolidColorBrush BanStatusBrush { get => Banned == "Да" ? new SolidColorBrush(Color.FromRgb(244, 67, 54)) : new SolidColorBrush(Color.FromRgb(39, 174, 96)); }

        public int PlayerID { get; set; }

        public int CharacterID { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public int CurrentLocationID { get; set; }

        public DateTime? CreationDate { get; set; }

        public int Gold { get; set; }

        public int Rating { get; set; }

        public int Level { get; set; }

        public int PlayedHours { get; set; }

        public string Banned { get; set; }

        public Character(int num, int playerID, int charID, string name, string iconPath, DateTime? creationDate,
                         int gold, int rating, int level, int playedHours, int currentLocationID, string banned)
        {
            Number = num;
            PlayerID = playerID;
            CharacterID = charID;
            Name = name;
            Icon = iconPath;
            CreationDate = creationDate;
            Gold = gold;
            Rating = rating;
            Level = level;
            PlayedHours = playedHours;
            CurrentLocationID = currentLocationID;
            Banned = banned;
        }

        public Character(int playerID, int charID, string name, string iconPath, DateTime? creationDate,
                         int gold, int rating, int level, int playedHours, int currentLocationID, string banned)
        {
            Number = 0;
            PlayerID = playerID;
            CharacterID = charID;
            Name = name;
            Icon = iconPath;
            CreationDate = creationDate;
            Gold = gold;
            Rating = rating;
            Level = level;
            PlayedHours = playedHours;
            CurrentLocationID = currentLocationID;
            Banned = banned;
        }
    }
}
