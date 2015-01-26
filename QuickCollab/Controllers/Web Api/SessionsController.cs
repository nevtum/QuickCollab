using QuickCollab.Models;
using QuickCollab.Security;
using QuickCollab.Services;
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
        RegistrationService _registrationService;
        SessionListPresenter _presenter;

        public SessionsController(SessionListPresenter presenter, RegistrationService service)
        {
            _presenter = presenter;
            _registrationService = service;
        }

        public HttpResponseMessage GetSessions()
        {
            IEnumerable<SessionViewModel> sessions = _presenter.Sessions(User.Identity.Name);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, sessions);
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
