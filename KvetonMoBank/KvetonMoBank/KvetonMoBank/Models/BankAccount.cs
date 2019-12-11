using System;
using System.Collections.Generic;
using System.Text;
using KvetonMoBank.Data;
using SQLite;

namespace KvetonMoBank
{
    public class BankAccount
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }
        public int ClientID { get; set; }
        public BankAccount()
        {   
            Random rnd = new Random();
            decimal number = rnd.Next(1000000000, 1999999999);
            string accountNumber = number + "/1234";
            this.AccountNumber = accountNumber;
            Balance = 100;
        }

        public bool Withdrawal(decimal amount)
        {
            if (this.Balance - amount >= 0)
            {
                this.Balance -= amount;
                return true;
            }
            else return false;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

    }
}
