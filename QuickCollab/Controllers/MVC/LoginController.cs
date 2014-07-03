using QuickCollab.Models;
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
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn(MemberLoginDetails details)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Index");

            // To do:
            // get existing username from database
            // and authenticate if password matches

            FormsAuthentication.SetAuthCookie(details.UserName, false);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateAccount()
        {
            return View();
        }
    }
}
