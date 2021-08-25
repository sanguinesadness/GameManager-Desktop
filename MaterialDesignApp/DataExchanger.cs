using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignApp
{
    public static class DataExchanger
    {
        public static string CurrentUserLogin { get; set; }

        public static int CurrectUserID { get => Gamemanager.GetUserId(CurrentUserLogin); }

        public static IManagementWindow ManagementWindow { get; set; }

        public static int SelectedCharacterID { get; set; }

        public static bool OpenInventory { get; set; }

        public static void ClearData()
        {
            CurrentUserLogin = default(string);
            SelectedCharacterID = default(int);
            OpenInventory = default(bool);
        }
    }
}
