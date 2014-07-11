using QuickCollab.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickCollab.Controllers.MVC
{
    public class LobbyController : Controller
    {
        private ISessionInstanceRepository _repo;

        public LobbyController()
        {
            _repo = new SessionInstanceRepository();
        }

        public ActionResult Index()
        {
            return View(_repo.ListAllSessions());
        }

        public ActionResult JoinSession(string sessionId)
        {
            // check if room is secured
            // check if user already allowed in secured room
            // present view to fill in details

            return RedirectToAction("Index", "SessionInstance", new { SessionId = sessionId });
        }
    }
}
