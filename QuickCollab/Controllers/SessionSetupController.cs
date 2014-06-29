using QuickCollab.Models;
using QuickCollab.Security;
using QuickCollab.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickCollab.Controllers
{
    public class SessionSetupController : Controller
    {
        private ISessionInstanceRepository _sessionManager;

        public SessionSetupController()
        {
            _sessionManager = new SessionInstanceRepository();
        }

        // use constructor when dependency injection included
        //public SessionSetupController(ISessionInstanceRepository repository)
        //{
        //    _sessionManager = repository;
        //}

        public ActionResult Index()
        {
            return View(new SessionStartSettings());
        }

        public ActionResult CreateSession(SessionStartSettings settings)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            // Check that no duplicates currently exist
            if (_sessionManager.SessionExists(settings.SessionName))
                return View("Session name in use! Please try a different name!");

            SessionInstance instance = new SessionInstance()
            {
                DateCreated = DateTime.Now,
                Name = settings.SessionName,
                IsVisible = settings.IsVisible,
            };

            if (settings.WithPassword)
            {
                PasswordHashService hasher = new PasswordHashService();
                string salt = hasher.GetNewSalt();
                string hashedPassword = hasher.SaltedPassword(settings.SessionPassword, salt);

                instance.Salt = salt;
                instance.HashedPassword = hashedPassword;
            }

            _sessionManager.AddSession(instance);

            Connection conn = new Connection()
            {
                UserName = settings.UserName,
                SessionName = settings.SessionName
            };

            _sessionManager.AddConnection(conn, instance);

            return RedirectToAction("Index", "SessionInstance", new { sessionId = instance.Name, password = settings.SessionPassword });
        }
    }
}
