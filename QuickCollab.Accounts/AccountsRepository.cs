using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCollab.Accounts
{
    public class AccountsRepository : IAccountsRepository
    {
        private MongoDatabase _dbContext;
        private MongoCollection _accounts;

        public AccountsRepository()
        {
            _dbContext = new MongoClient("mongodb://user1:pa55W0rd@localhost")
                .GetServer()
                .GetDatabase("QuickCollab");

            _accounts = _dbContext.GetCollection<Account>("Accounts");
        }

        public bool AccountExists(string username)
        {
            return GetAccountByUsername(username) != null;
        }

        public void AddAccount(Account account)
        {
            _accounts.Insert(account);
        }

        public void DeleteAccount(string username)
        {
            var query = Query<Account>.EQ(acc => acc.UserName, username);

            _accounts.Remove(query);
        }

        public Account GetAccountByUsername(string username)
        {
            var query = Query<Account>.EQ(acc => acc.UserName, username);

            return _accounts
                .FindAs<Account>(query)
                .SingleOrDefault();
        }
    }
}
