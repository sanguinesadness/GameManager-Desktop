using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialDesignApp
{
    public class Player
    {
        public int ID { get; set; }

        public string Login { get; set; }

        public string Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Email { get; set; }

        public DateTime LastConnection { get; set; }

        public Player(int id, string name, string gender, DateTime? birthDate, string email, DateTime lastConnection)
        {
            ID = id;
            Login = name;
            Gender = gender;
            BirthDate = birthDate;
            Email = email;
            LastConnection = lastConnection;
        }
    }
}
