using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace QuickCollab.Hubs
{
    public class ContentHub : Hub
    {
        public void BroadcastMessage(string message)
        {
            Clients.All.NotifyBroadcasted(message);
        }
    }
}