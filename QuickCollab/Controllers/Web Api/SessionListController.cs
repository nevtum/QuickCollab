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
    //[Authorize]
    public class SessionListController : ApiController
    {
        ISessionInstanceRepository _repo;
        RegistrationService _registrationService;
        PasswordHashService _hashService;

        public SessionListController()
        {
            _repo = new SessionInstanceRepository();
            _registrationService = new RegistrationService();
            _hashService = new PasswordHashService();
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
        public HttpResponseMessage AuthorizeSession(string id, string password)
        {
            SessionInstance instance = _repo.GetSession(id);

            if (instance == null)
                return Request.CreateResponse(System.Net.HttpStatusCode.NotFound);

            if (string.IsNullOrEmpty(instance.HashedPassword))
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new Exception("Room is not secured! Incorrect post type"));

            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(instance.Salt));

            if (_hashService.SaltedPassword(password, instance.Salt) != instance.HashedPassword)
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, new Exception("Incorrect password!"));

            if (!_registrationService.UserRegisteredWithSession(User.Identity.Name, id))
                _registrationService.RegisterConnection(User.Identity.Name, id);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK);
        }
    }
}
