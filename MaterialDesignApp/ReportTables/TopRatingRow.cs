using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignApp.ReportTables
{
    class TopRatingRow
    {
        public int Number { get; set; }

        public string CharacterName { get; set; }

        public int Rating { get; set; }

        public string PlayerName { get; set; }

        public TopRatingRow(int num, string charName, int rating, string playerName)
        {
            Number = num;
            CharacterName = charName;
            Rating = rating;
            PlayerName = playerName;
        }

        public TopRatingRow(string charName, int rating, string playerName)
        {
            Number = 0;
            CharacterName = charName;
            Rating = rating;
            PlayerName = playerName;
        }
    }
}
