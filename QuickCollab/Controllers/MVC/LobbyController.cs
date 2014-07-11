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
            // check if user already allowed in secured room
            // present view to fill in details

            return RedirectToAction("Index", "SessionInstance", new { SessionId = sessionId });
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
                    _service.StartNewSession(vm.SessionName, vm.Public, vm.SessionPassword, vm.PersistHistory);
                else
                    _service.StartNewSession(vm.SessionName, vm.Public, string.Empty, vm.PersistHistory);
            }
            catch (Exception e)
            {
                return View(vm);
            }

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            _service.RegisterConnection(ticket.Name, vm.SessionName);

            return RedirectToAction("Index");
        }
    }
}
