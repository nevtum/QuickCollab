using QuickCollab.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void RegisterConnection(string clientName, string sessionName)
        {
            System.Diagnostics.Debug.Assert(!IsUserAuthorized(clientName, sessionName));

            SessionInstance instance = _sessionRepo.GetSession(sessionName);

            _connRepo.RegisterConnection(clientName, instance);
        }

        public IEnumerable<string> CurrentSessions(string clientName)
        {
            return _connRepo.GetActiveConnectionsByUserName(clientName)
                .Select(c => c.SessionName);
        }

        public bool ValidatePassword(string sessionName, string password)
        {
            SessionInstance s = _sessionRepo.GetSession(sessionName);

            if (NoPasswordRequired(s))
                return true;

            if (_passwordService.SaltedPassword(password, s.Salt) == s.HashedPassword)
                return true;

            return false;
        }

        public bool IsUserAuthorized(string userName, string sessionName)
        {
            SessionInstance s = _sessionRepo.GetSession(sessionName);

            if (NoPasswordRequired(s))
                return true;

            return _connRepo.GetActiveConnectionsInSession(sessionName)
                .Any(conn => conn.ClientName == userName);
        }

        public void StartNewSession(string sessionName, bool isVisible, string password, bool persistHistory, int connectionExpiryHours)
        {
            if (_sessionRepo.SessionExists(sessionName))
                throw new Exception("Session name in use! Please try a different name!");

            SessionInstance instance = new SessionInstance()
            {
                DateCreated = DateTime.Now,
                Name = sessionName,
                IsVisible = isVisible,
                PersistHistory = persistHistory,
                ConnectionExpiryInHours = connectionExpiryHours
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

        private bool NoPasswordRequired(SessionInstance instance)
        {
            return string.IsNullOrEmpty(instance.HashedPassword);
        }
    }
}
