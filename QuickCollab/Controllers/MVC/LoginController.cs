using QuickCollab.Accounts;
using QuickCollab.Models;
using QuickCollab.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuickCollab.Controllers.MVC
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private IAccountsRepository _accountsDB;
        private PasswordHashService _hasher;

        public LoginController(IAccountsRepository accounts)
        {
            _accountsDB = new AccountsRepository();
            _hasher = new PasswordHashService();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(MemberLoginDetails details)
        {
            if (!ModelState.IsValid)
                return View(details);

            try
            {
                AuthenticateUser(details);
                FormsAuthentication.SetAuthCookie(details.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return View(details);
            }
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(MemberLoginDetails details)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("CreateAccount");

            try
            {
                CreateNewAccount(details);
            }
            catch (Exception e)
            {
                return RedirectToAction("CreateAccount");
            }

            return RedirectToAction("AccountCreated");
        }

        public ActionResult AccountCreated()
        {
            return View();
        }

        // Domain logic, should reside in a service
        private void AuthenticateUser(MemberLoginDetails details)
        {
            Account account = _accountsDB.GetAccountByUsername(details.UserName);

            if (account == null)
                throw new Exception("Invalid username or password");

            if (_hasher.SaltedPassword(details.Password, account.Salt) != account.Password)
                throw new Exception("Invalid username or password");
        }

        // Domain logic, should reside in a service
        private void CreateNewAccount(MemberLoginDetails details)
        {
            if (_accountsDB.AccountExists(details.UserName))
                throw new Exception("Account already exists");

            string salt = _hasher.GetNewSalt();

            Account account = new Account()
            {
                DateCreated = DateTime.Now,
                UserName = details.UserName,
                Password = _hasher.SaltedPassword(details.Password, salt),
                Salt = salt
            };

            _accountsDB.AddAccount(account);
        }
    }
}
