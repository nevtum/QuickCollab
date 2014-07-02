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
        private RegistrationService _registrationService;

        public ContentHub()
        {
            _registrationService = new RegistrationService();
        }

        public Task BroadcastMessage(string message)
        {
            string content = string.Format("[{0}]: {1}", DateTime.Now, message);

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

        public Task JoinSession(string sessionName, string password = "")
        {
            return new Task(() =>
            {
                if (!Context.User.Identity.IsAuthenticated)
                    return;

                if (!_registrationService.ValidatePassword(sessionName, password))
                    return;

                _registrationService.RegisterConnection(Context.ConnectionId, sessionName);

                Groups.Add(Context.ConnectionId, sessionName);

                Clients.Group(sessionName)
                    .RecieveMessage(sessionName, string.Format("{0} has joined session.", Context.User.Identity));
            });
        }

        public Task LeaveSession(string sessionName)
        {
            return new Task(() =>
            {
                if (!_registrationService.UserRegisteredWithSession(Context.ConnectionId, sessionName))
                    return;

                _registrationService.UnRegisterConnection(Context.ConnectionId, sessionName);

                Groups.Remove(Context.ConnectionId, sessionName);

                Clients.Group(sessionName)
                    .RecieveMessage(sessionName, string.Format("{0} has left session.", Context.User.Identity));
            });
        }

        public override Task OnDisconnected()
        {
            return new Task(() =>
            {
                foreach (string sessionName in _registrationService.CurrentSessions(Context.ConnectionId))
                {
                    LeaveSession(sessionName);
                }
            });
        }
    }
}