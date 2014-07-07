using QuickCollab.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickCollab.Controllers
{
    [Authorize]
    public class SessionInstanceController : Controller
    {
        //
        // GET: /SessionInstance/

        // Not a good idea since password is visible in url
        public ActionResult Index(string sessionId)
        {
            // Note: parameter to method can only be simple types.

            // Retrieve session from repository

            return View();
        }

    }
}
