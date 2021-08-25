using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignApp.ReportTables
{
    class MostExpensiveInventoriesRow
    {
        public int Number { get; set; }

        public string CharacterName { get; set; }

        public int NumberOfItems { get; set; }

        public int TotalValue { get; set; }

        public string PlayerName { get; set; }

        public MostExpensiveInventoriesRow(int num, string charName, int numOfItems, int totalValue, string playerName)
        {
            Number = num;
            CharacterName = charName;
            NumberOfItems = numOfItems;
            TotalValue = totalValue;
            PlayerName = playerName;
        }

        public MostExpensiveInventoriesRow(string charName, int numOfItems, int totalValue, string playerName)
        {
            Number = 0;
            CharacterName = charName;
            NumberOfItems = numOfItems;
            TotalValue = totalValue;
            PlayerName = playerName;
        }
    }
}
