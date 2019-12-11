using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace KvetonMoBank.Models
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public int BankAccountID { get; set; }
        public bool VIP { get; set; }


        public Account()
        {
            CreateDate = DateTime.Now;
        }

        public void EditAccount(string newUsername, string newPassword)
        {
            this.Username = newUsername;
            this.Password = newPassword;
        }
    }
}
