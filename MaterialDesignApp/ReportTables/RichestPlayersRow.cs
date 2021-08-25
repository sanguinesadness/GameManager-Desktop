using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignApp.ReportTables
{
    class RichestPlayersRow
    {
        public int Number { get; set; }

        public string PlayerName { get; set; }

        public int NumberOfCharacters { get; set; }

        public int TotalGold { get; set; }

        public RichestPlayersRow(int num, string playerName, int numOfChars, int totalGold)
        {
            Number = num;
            PlayerName = playerName;
            NumberOfCharacters = numOfChars;
            TotalGold = totalGold;
        }

        public RichestPlayersRow(string playerName, int numOfChars, int totalGold)
        {
            Number = 0;
            PlayerName = playerName;
            NumberOfCharacters = numOfChars;
            TotalGold = totalGold;
        }
    }
}
