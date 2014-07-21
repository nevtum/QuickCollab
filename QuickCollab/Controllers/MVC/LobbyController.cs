using QuickCollab.Models;
using QuickCollab.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QuickCollab.Controllers.MVC
{
    public class LobbyController : Controller
    {
        private ISessionInstanceRepository _repo;
        private RegistrationService _service;

        public LobbyController()
        {
            _repo = new SessionInstanceRepository();
            _service = new RegistrationService();
        }

        public ActionResult Index()
        {
            return View(_repo.ListAllSessions());
        }

        public ActionResult JoinSession(string sessionId)
        {
            // check if room is secured
            if (string.IsNullOrEmpty(_repo.GetSession(sessionId).HashedPassword))
            {
                // To do. Expiry of user registered with session.
                if (!_service.UserRegisteredWithSession(User.Identity.Name, sessionId))
                    _service.RegisterConnection(User.Identity.Name, sessionId);

                return RedirectToAction("Index", "SessionInstance", new { SessionId = sessionId });
            }

            // present view to fill in details
            return RedirectToAction("JoinSecured", new { sessionId = sessionId });
        }

        public ActionResult JoinSecured(string sessionId)
        {
            return View(sessionId);
        }

        public ActionResult JoinSecured(string sessionId, string password)
        {
            return View();
        }

        public ActionResult CreateSession()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSession(StartSettingsViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                if (vm.Secured)
                    _service.StartNewSession(vm.SessionName, vm.Public, vm.SessionPassword, vm.PersistHistory, vm.ConnectionExpiryInHours);
                else
                    _service.StartNewSession(vm.SessionName, vm.Public, string.Empty, vm.PersistHistory, vm.ConnectionExpiryInHours);
            }
            catch (Exception e)
            {
                return View(vm);
            }

            return RedirectToAction("Index");
        }
    }
}
