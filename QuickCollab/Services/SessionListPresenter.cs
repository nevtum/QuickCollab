using System;
using System.Linq;
using System.Collections.Generic;
using QuickCollab.Models;
using QuickCollab.Session;

using System.Web.Http;

namespace QuickCollab.Services
{
    public class SessionListPresenter
    {
        private ISessionInstanceRepository _repo;
        private RegistrationService _service;

        public SessionListPresenter(ISessionInstanceRepository repo, RegistrationService service)
        {
            _repo = repo;
            _service = service;
        }

        public IEnumerable<SessionViewModel> Sessions(string username)
        {
            IEnumerable<SessionViewModel> sessions = _repo.ListAllSessions()
                .Where(s => s.IsVisible == true)
                .Select(s => new SessionViewModel()
                {
                    DateCreated = s.DateCreated,
                    SessionName = s.Name,
                    Secured = !string.IsNullOrEmpty(s.HashedPassword),
                    ConnectionExpiryInHours = s.ConnectionExpiryInHours,
                    IsUserAuthorized = _service.IsUserAuthorized(username, s.Name),
                    IsVisible = s.IsVisible,
                    PersistHistory = s.PersistHistory
                });

            return sessions;
        }
    }
}