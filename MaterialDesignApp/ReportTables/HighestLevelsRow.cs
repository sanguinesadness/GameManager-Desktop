using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignApp.ReportTables
{
    class HighestLevelsRow
    {
        public int Number { get; set; }

        public string CharacterName { get; set; }

        public int Level { get; set; }

        public string PlayerName { get; set; }

        public HighestLevelsRow(int num, string charName, int lvl, string playerName)
        {
            Number = num;
            CharacterName = charName;
            Level = lvl;
            PlayerName = playerName;
        }

        public HighestLevelsRow(string charName, int lvl, string playerName)
        {
            Number = 0;
            CharacterName = charName;
            Level = lvl;
            PlayerName = playerName;
        }
    }
}
