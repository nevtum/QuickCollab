using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCollab.Accounts
{
    public interface IAccountsRepository
    {
        bool AccountExists(string username);
        void AddAccount(Account account);
        void DeleteAccount(string username);
        Account GetAccountByUsername(string username);
    }
}
