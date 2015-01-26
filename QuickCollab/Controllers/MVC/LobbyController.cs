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

        public LobbyController(ISessionInstanceRepository repo, RegistrationService service)
        {
            _repo = repo;
            _service = service;
        }

        public ActionResult Index()
        {
            return View(_repo.ListAllSessions());
        }

        public ActionResult JoinSession(string sessionId)
        {
            if (_service.RegisterConnection(User.Identity.Name, sessionId))
                return RedirectToAction("Index", "SessionInstance", new { SessionId = sessionId });

            // present view to fill in details
            return RedirectToAction("JoinSecured", new { sessionId = sessionId });
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
                SessionParameters parameters = new SessionParameters(vm.Public, vm.PersistHistory, vm.ConnectionExpiryInHours);
                _service.StartNewSession(vm.SessionName, vm.SessionPassword, parameters);
            }
            catch (Exception e)
            {
                return View(vm);
            }

            return RedirectToAction("Index");
        }
    }
}
