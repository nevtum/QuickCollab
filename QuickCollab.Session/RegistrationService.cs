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
        private ISessionInstanceRepository _repo;

        public RegistrationService()
        {
            _repo = new SessionInstanceRepository();
        }

        public void RegisterConnection(string clientName, string sessionName)
        {
            _repo.RegisterConnection(clientName, sessionName);
        }

        public void StartNewSession(string sessionName, bool isVisible, string password)
        {
            if (_repo.SessionExists(sessionName))
                throw new Exception("Session name in use! Please try a different name!");

            SessionInstance instance = new SessionInstance()
            {
                DateCreated = DateTime.Now,
                Name = sessionName,
                IsVisible = isVisible,
            };

            if (string.IsNullOrEmpty(password))
            {
                PasswordHashService hasher = new PasswordHashService();
                string salt = hasher.GetNewSalt();
                string hashedPassword = hasher.SaltedPassword(password, salt);

                instance.Salt = salt;
                instance.HashedPassword = hashedPassword;
            }

            _repo.AddSession(instance);
        }
    }
}
