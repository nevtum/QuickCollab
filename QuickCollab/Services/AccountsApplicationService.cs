using System;
using QuickCollab.Accounts;
using QuickCollab.Models;
using QuickCollab.Security;

namespace QuickCollab.Services
{
    public class AccountsApplicationService : IManageAccounts
    {
        private IAccountsRepository _accounts;

        public AccountsApplicationService(IAccountsRepository accounts)
        {
            _accounts = accounts;
        }

        public void CreateNewAccount(MemberLoginDetails details)
        {
            if (_accounts.AccountExists(details.UserName))
                throw new Exception("Account already exists");

            string salt = PasswordHashService.GetNewSalt();

            Account account = new Account()
            {
                DateCreated = DateTime.Now,
                UserName = details.UserName,
                Password = PasswordHashService.SaltedPassword(details.Password, salt),
                Salt = salt
            };

            _accounts.AddAccount(account);
        }

        public void AuthenticateUser(MemberLoginDetails details)
        {
            Account account = _accounts.GetAccountByUsername(details.UserName);

            if (account == null)
                throw new Exception("Invalid username or password");

            if (PasswordHashService.SaltedPassword(details.Password, account.Salt) != account.Password)
                throw new Exception("Invalid username or password");
        }
    }
}