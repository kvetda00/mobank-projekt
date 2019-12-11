using System;
using System.Collections.Generic;
using System.Text;
using KvetonMoBank.Data;
using SQLite;

namespace KvetonMoBank
{
    public class Client
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

    }
}
