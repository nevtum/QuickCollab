using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using QuickCollab.Session;

namespace QuickCollab.Hubs
{
    [Authorize]
    public class ContentHub : Hub
    {
        private RegistrationService _registrationService;

        public ContentHub()
        {
            _registrationService = new RegistrationService();
        }

        public Task BroadcastMessage(string message)
        {
            string content = string.Format("[{0}]: {1}", Context.User.Identity.Name, message);

            return Clients.All.RecieveBroadcast(content);
        }

        public Task SendMessage(string sessionName, string message)
        {
            return new Task(() =>
            {
                if (!_registrationService.UserRegisteredWithSession(Context.ConnectionId, sessionName))
                    return;

                Clients.Group(sessionName).RecieveMessage(sessionName, message);
            });
        }

        public void JoinSession(string sessionName)
        {
            string clientName = Context.User.Identity.Name;

            if (!_registrationService.UserRegisteredWithSession(clientName, sessionName))
                return;

            Groups.Add(Context.ConnectionId, sessionName);
            Clients.OthersInGroup(sessionName).RecieveMessage(string.Format("{0} has joined session.", clientName));
        }

        public void LeaveSession(string sessionName)
        {
            string clientName = Context.User.Identity.Name;

            if (!_registrationService.UserRegisteredWithSession(clientName, sessionName))
                return;

            Groups.Remove(Context.ConnectionId, sessionName);
            Clients.OthersInGroup(sessionName).RecieveMessage(string.Format("{0} has left session.", clientName));
        }

        public override Task OnConnected()
        {
            // for testing only
            JoinSession("Open Session");

            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            // for testing only
            LeaveSession("Open Session");

            return base.OnDisconnected();
        }
    }
}