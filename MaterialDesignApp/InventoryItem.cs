using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignApp
{
    public class InventoryItem
    {
        // поле для колонки "#" в InventoryDataGrid
        public int Number { get; set; }

        public string ImagePath { get; set; }

        public string ItemName { get; set; }

        public int Amount { get; set; }

        public InventoryItem(int number, string imgPath, string itemName, int amount)
        {
            Number = number;
            ImagePath = imgPath;
            ItemName = itemName;
            Amount = amount;
        }
    }
}
