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

            Account account = _accountsDB.GetAccountByUsername(details.UserName);

            if (account == null)
                return View(details); // Account doesn't exist

            if (_hasher.SaltedPassword(details.Password, account.Salt) != account.Password)
                return View(details); // Incorrect login

            FormsAuthentication.SetAuthCookie(details.UserName, false);

            return RedirectToAction("Index", "Home");
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

            if (_accountsDB.AccountExists(details.UserName))
                return RedirectToAction("CreateAccount");

            string salt = _hasher.GetNewSalt();

            Account account = new Account()
            {
                DateCreated = DateTime.Now,
                UserName = details.UserName,
                Password = _hasher.SaltedPassword(details.Password, salt),
                Salt = salt
            };

            _accountsDB.AddAccount(account);

            return RedirectToAction("AccountCreated");
        }

        public ActionResult AccountCreated()
        {
            return View();
        }
    }
}
