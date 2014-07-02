﻿using QuickCollab.Models;
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
        private RegistrationService _service;

        public SessionSetupController()
        {
            _service = new RegistrationService();
        }

        public ActionResult Index()
        {
            return View(new StartSettingsViewModel());
        }

        public ActionResult CreateSession(StartSettingsViewModel vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index");

            try
            {
                if (vm.Secured)
                    _service.StartNewSession(vm.SessionName, vm.Public, vm.SessionPassword, vm.PersistHistory);
                else
                    _service.StartNewSession(vm.SessionName, vm.Public, string.Empty, vm.PersistHistory);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index");
            }

            _service.RegisterConnection(vm.UserName, vm.SessionName);

            return RedirectToAction("Index", "SessionInstance", new { sessionId = vm.SessionName, password = vm.SessionPassword });
        }
    }
}
