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
        private ISessionInstanceRepository _sessionManager;

        public ContentHub()
        {
            _sessionManager = new SessionInstanceRepository();
        }

        public void BroadcastMessage(string message)
        {
            Clients.All.NotifyBroadcasted(message);
        }

        public override Task OnDisconnected()
        {
            // Find connection and Remove from session

            return base.OnDisconnected();
        }
    }
}