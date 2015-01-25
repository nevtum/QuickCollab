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
        PasswordHashService _hashService;

        public SessionsController(ISessionInstanceRepository repo, RegistrationService service, PasswordHashService hasher)
        {
            _repo = repo;
            _registrationService = service;
            _hashService = hasher;
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

        // Too much domain logic in controller
        [HttpPost]
        public HttpResponseMessage Authorize(SessionRegistration request)
        {
            SessionInstance instance = _repo.GetSession(request.SessionId);

            if (instance == null)
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);

            if (string.IsNullOrEmpty(instance.HashedPassword))
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new Exception("Room is not secured! Incorrect post type"));

            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(instance.Salt));

            if (_hashService.SaltedPassword(request.Password, instance.Salt) != instance.HashedPassword)
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new Exception("Incorrect password!"));

            if (!_registrationService.IsUserAuthorized(User.Identity.Name, request.SessionId))
                _registrationService.RegisterConnection(User.Identity.Name, request.SessionId);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }
}
