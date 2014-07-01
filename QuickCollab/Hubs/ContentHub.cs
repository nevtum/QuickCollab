using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using QuickCollab.Session;

namespace QuickCollab.Hubs
{
    public class ContentHub : Hub
    {
        private ISessionInstanceRepository _repo;

        public ContentHub()
        {
            _repo = new SessionInstanceRepository();
        }

        public void BroadcastMessage(string message)
        {
            Clients.All.RecieveBroadcast(message);
        }

        [Authorize]
        public void SendMessage(string sessionName, string message)
        {
            if (!Context.User.Identity.IsAuthenticated)
                return;

            if (!UserRegisteredWithSession(Context.User.Identity.Name, sessionName))
                return;
            
            Clients.Group(sessionName).RecieveMessage(sessionName, message);
        }

        public override Task OnDisconnected()
        {
            foreach (Connection conn in _repo.GetConnectionsByUserName(Context.User.Identity.Name))
            {
                _repo.UnRegisterConnection(conn);
            }

            return base.OnDisconnected();
        }

        // perhaps an extension method?
        private bool UserRegisteredWithSession(string userName, string sessionName)
        {
            return _repo.GetConnectionsInSession(sessionName)
                .Any(conn => conn.ClientName == userName);
        }
    }
}