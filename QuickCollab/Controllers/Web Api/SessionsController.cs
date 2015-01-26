using QuickCollab.Models;
using QuickCollab.Security;
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
    public class SessionsController : ApiController
    {
        ISessionInstanceRepository _repo;
        RegistrationService _registrationService;

        public SessionsController(ISessionInstanceRepository repo, RegistrationService service)
        {
            _repo = repo;
            _registrationService = service;
        }

        public HttpResponseMessage GetSessions()
        {
            IEnumerable<SessionViewModel> sessions = _repo.ListAllSessions()
                .Where(s => s.IsVisible == true)
                .Select(s => new SessionViewModel()
                {
                    DateCreated = s.DateCreated,
                    SessionName = s.Name,
                    Secured = !string.IsNullOrEmpty(s.HashedPassword),
                    ConnectionExpiryInHours = s.ConnectionExpiryInHours,
                    IsUserAuthorized = _registrationService.IsUserAuthorized(User.Identity.Name, s.Name),
                    IsVisible = s.IsVisible,
                    PersistHistory = s.PersistHistory,
                    Uri = Url.Link("DefaultApi", new { controller = "SessionList", id = s.Name }),
                });

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, sessions);
        }

        public HttpResponseMessage GetSession(string id)
        {
            SessionInstance instance = _repo.GetSession(id);

            if (instance == null)
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, instance);
        }

        [HttpPost]
        public HttpResponseMessage Authorize(SessionRegistration request)
        {
            try
            {
                _registrationService.Authorize(User.Identity.Name, request.SessionId, request.Password);
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new Exception(e.Message));
            }
        }
    }
}
