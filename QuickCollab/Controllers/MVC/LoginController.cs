using System;
using System.Web.Mvc;
using System.Web.Security;
using QuickCollab.Models;
using QuickCollab.Services;

namespace QuickCollab.Controllers.MVC
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private IManageAccounts _service;

        public LoginController(IManageAccounts service)
        {
            _service = service;
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
                _service.AuthenticateUser(details);
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
                _service.CreateNewAccount(details);
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
    }
}
