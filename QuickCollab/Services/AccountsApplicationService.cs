using System;
using QuickCollab.Accounts;
using QuickCollab.Models;
using QuickCollab.Security;

namespace QuickCollab.Services
{
    public class AccountsApplicationService : IManageAccounts
    {
        private IAccountsRepository _accounts;
        private PasswordHashService _hasher;

        public AccountsApplicationService(IAccountsRepository accounts, PasswordHashService hasher)
        {
            _accounts = accounts;
            _hasher = hasher;
        }

        public void CreateNewAccount(MemberLoginDetails details)
        {
            if (_accounts.AccountExists(details.UserName))
                throw new Exception("Account already exists");

            string salt = _hasher.GetNewSalt();

            Account account = new Account()
            {
                DateCreated = DateTime.Now,
                UserName = details.UserName,
                Password = _hasher.SaltedPassword(details.Password, salt),
                Salt = salt
            };

            _accounts.AddAccount(account);
        }

        public void AuthenticateUser(MemberLoginDetails details)
        {
            Account account = _accounts.GetAccountByUsername(details.UserName);

            if (account == null)
                throw new Exception("Invalid username or password");

            if (_hasher.SaltedPassword(details.Password, account.Salt) != account.Password)
                throw new Exception("Invalid username or password");
        }
    }
}