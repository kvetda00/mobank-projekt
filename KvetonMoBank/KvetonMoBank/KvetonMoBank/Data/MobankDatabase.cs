using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using KvetonMoBank.Models;
using System.Threading.Tasks;

namespace KvetonMoBank.Data
{
    public class MobankDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public MobankDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Account>().Wait();
            _database.CreateTableAsync<BankAccount>().Wait();
            _database.CreateTableAsync<Client>().Wait();
        }

        public Task<List<Account>> GetAccountsAsync()
        {
            return _database.Table<Account>().ToListAsync();
        }

        public Task<List<BankAccount>> GetBankAccountsAsync()
        {
            return _database.Table<BankAccount>().ToListAsync();
        }

        public Task<List<Client>> GetClientsAsync()
        {
            return _database.Table<Client>().ToListAsync();
        }

        public Task<Account> GetAccountAsync(int id)
        {
            return _database.Table<Account>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<BankAccount> GetBankAccountAsync(int id)
        {
            return _database.Table<BankAccount>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<Client> GetClientAsync(int id)
        {
            return _database.Table<Client>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveAccountAsync(Account Account)
        {
            if (Account.ID != 0)
            {
                return _database.UpdateAsync(Account);
            }
            else
            {
                return _database.InsertAsync(Account);
            }
        }

        public Task<int> SaveBankAccountAsync(BankAccount BankAccount)
        {
            if (BankAccount.ID != 0)
            {
                return _database.UpdateAsync(BankAccount);
            }
            else
            {
                return _database.InsertAsync(BankAccount);
            }
        }

        public Task<int> SaveClientAsync(Client Client)
        {
            if (Client.ID != 0)
            {
                return _database.UpdateAsync(Client);
            }
            else
            {
                return _database.InsertAsync(Client);
            }
        }

        public Task<int> DeleteAccountAsync(Account Account)
        {
            return _database.DeleteAsync(Account);
        }

        public Task<int> DeleteBankAccountAsync(BankAccount BankAccount)
        {
            return _database.DeleteAsync(BankAccount);
        }

        public Task<int> DeleteClientAsync(Client Client)
        {
            return _database.DeleteAsync(Client);
        }
    }
}
