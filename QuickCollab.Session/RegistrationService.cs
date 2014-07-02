﻿using QuickCollab.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCollab.Session
{
    public class RegistrationService
    {
        private ISessionInstanceRepository _repo;
        private PasswordHashService _passwordService;

        public RegistrationService()
        {
            _repo = new SessionInstanceRepository();
            _passwordService = new PasswordHashService();
        }

        public void RegisterConnection(string clientName, string sessionName)
        {
            if (UserRegisteredWithSession(clientName, sessionName))
                return;

            _repo.RegisterConnection(clientName, sessionName);
        }

        public void UnRegisterConnection(string clientName, string sessionName)
        {
            if (!UserRegisteredWithSession(clientName, sessionName))
                return;

            Connection conn = _repo
                .GetConnectionsByUserName(clientName)
                .Single(c => c.SessionName == sessionName);

            _repo.UnRegisterConnection(conn);
        }

        public IEnumerable<string> CurrentSessions(string clientName)
        {
            return _repo.GetConnectionsByUserName(clientName)
                .Select(c => c.SessionName);
        }

        public bool ValidatePassword(string sessionName, string password)
        {
            SessionInstance s = _repo.GetSession(sessionName);

            // No password required
            if (string.IsNullOrEmpty(s.HashedPassword))
                return true;

            if (_passwordService.SaltedPassword(password, s.Salt) == s.HashedPassword)
                return true;

            return false;
        }

        public bool UserRegisteredWithSession(string userName, string sessionName)
        {
            return _repo.GetConnectionsInSession(sessionName)
                .Any(conn => conn.ClientName == userName);
        }

        public void StartNewSession(string sessionName, bool isVisible, string password, bool persistHistory)
        {
            if (_repo.SessionExists(sessionName))
                throw new Exception("Session name in use! Please try a different name!");

            SessionInstance instance = new SessionInstance()
            {
                DateCreated = DateTime.Now,
                Name = sessionName,
                IsVisible = isVisible,
                PersistHistory = persistHistory
            };

            if (!string.IsNullOrEmpty(password))
            {
                string salt = _passwordService.GetNewSalt();
                string hashedPassword = _passwordService.SaltedPassword(password, salt);

                instance.Salt = salt;
                instance.HashedPassword = hashedPassword;
            }

            _repo.AddSession(instance);
        }
    }
}
