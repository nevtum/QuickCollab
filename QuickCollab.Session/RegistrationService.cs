using System;
using System.Linq;
using System.Collections.Generic;
using QuickCollab.Security;

namespace QuickCollab.Session
{
    public class RegistrationService
    {
        private ISessionInstanceRepository _sessionRepo;
        private IConnectionRepository _connRepo;
        private PasswordHashService _passwordService;

        public RegistrationService()
        {
            _sessionRepo = new SessionInstanceRepository();
            _connRepo = new ConnectionRepository();
            _passwordService = new PasswordHashService();
        }

        public bool RegisterConnection(string clientName, string sessionName)
        {
            if (string.IsNullOrEmpty(_sessionRepo.GetSession(sessionName).HashedPassword))
                return true;

            if (!IsUserAuthorized(clientName, sessionName))
                return false;

            SessionInstance instance = _sessionRepo.GetSession(sessionName);

            _connRepo.RegisterConnection(clientName, instance);

            return true;
        }

        public IEnumerable<string> CurrentSessions(string clientName)
        {
            return _connRepo.GetActiveConnectionsByUserName(clientName)
                .Select(c => c.SessionName);
        }

        // Review: Belongs to an entity
        public bool IsLoggingEnabled(string sessionName)
        {
            return _sessionRepo.GetSession(sessionName).PersistHistory;
        }

        public void StartNewSession(string sessionName, string password, SessionParameters parameters)
        {
            // Idempotent operation
            if (_sessionRepo.SessionExists(sessionName))
                return;

            SessionInstance instance = new SessionInstance()
            {
                DateCreated = DateTime.Now,
                Name = sessionName,
                IsVisible = parameters.IsPublic,
                PersistHistory = parameters.Persistent,
                ConnectionExpiryInHours = parameters.ConnectionExpiryInHours
            };

            if (!string.IsNullOrEmpty(password))
            {
                string salt = _passwordService.GetNewSalt();
                string hashedPassword = _passwordService.SaltedPassword(password, salt);

                instance.Salt = salt;
                instance.HashedPassword = hashedPassword;
            }

            _sessionRepo.AddSession(instance);
        }

        // Review: Belongs to an entity
        private bool NoPasswordRequired(SessionInstance instance)
        {
            return string.IsNullOrEmpty(instance.HashedPassword);
        }

        // Review: Belongs to an entity
        public bool IsUserAuthorized(string userName, string sessionName)
        {
            SessionInstance s = _sessionRepo.GetSession(sessionName);

            if (NoPasswordRequired(s))
                return true;

            return _connRepo.GetActiveConnectionsInSession(sessionName)
                .Any(conn => conn.ClientName == userName);
        }
    }
}
