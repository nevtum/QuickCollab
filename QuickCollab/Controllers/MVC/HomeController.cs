using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuickCollab.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("SignIn", "Login");
        }
    }
}
