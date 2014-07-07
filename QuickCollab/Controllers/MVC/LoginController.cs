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

        public LoginController()
        {
            _accountsDB = new AccountsRepository();
            _hasher = new PasswordHashService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn(MemberLoginDetails details)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Index");

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
        public ActionResult CreateAccount(Account account)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("CreateAccount");

            if (_accountsDB.AccountExists(account.UserName))
                return RedirectToAction("CreateAccount");

            account.DateCreated = DateTime.Now;
            account.Salt = _hasher.GetNewSalt();
            account.Password = _hasher.SaltedPassword(account.Password, account.Salt);

            _accountsDB.AddAccount(account);

            return RedirectToAction("AccountCreated");
        }

        public ActionResult AccountCreated()
        {
            return View();
        }
    }
}
