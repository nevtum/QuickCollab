using QuickCollab.Models;
using QuickCollab.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace QuickCollab.Controllers
{
    [Authorize]
    public class SessionListController : ApiController
    {
        ISessionInstanceRepository _repo;

        public SessionListController()
        {
            _repo = new SessionInstanceRepository();
        }

        public HttpResponseMessage GetSessions()
        {
            IEnumerable<SessionViewModel> sessions = _repo.ListAllSessions()
                .Where(s => s.IsVisible == true)
                .Select(s => new SessionViewModel()
                {
                    SessionName = s.Name,
                    Secured = !string.IsNullOrEmpty(s.HashedPassword),
                    ConnectionExpiryInHours = s.ConnectionExpiryInHours,
                    IsVisible = s.IsVisible,
                    PersistHistory = s.PersistHistory
                });

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, sessions);
        }
    }
}
