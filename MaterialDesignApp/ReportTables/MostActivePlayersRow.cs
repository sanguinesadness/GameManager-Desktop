using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignApp.ReportTables
{
    class MostActivePlayersRow
    {
        public int Number { get; set; }

        public string PlayerName { get; set; }

        public int NumberOfCharacters { get; set; }

        public int TotalHours { get; set; }

        public double AverageHours { get; set; }

        public MostActivePlayersRow(int num, string playerName, int numOfChars, int totalHours, double avgHours)
        {
            Number = num;
            PlayerName = playerName;
            NumberOfCharacters = numOfChars;
            TotalHours = totalHours;
            AverageHours = avgHours;
        }

        public MostActivePlayersRow(string playerName, int numOfChars, int totalHours, double avgHours)
        {
            Number = 0;
            PlayerName = playerName;
            NumberOfCharacters = numOfChars;
            TotalHours = totalHours;
            AverageHours = avgHours;
        }
    }
}
