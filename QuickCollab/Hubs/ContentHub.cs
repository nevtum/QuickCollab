﻿using System;
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
        private IConnectionRepository _connRepo;

        public ContentHub(RegistrationService service, IConnectionRepository connRepo)
        {
            _registrationService = service;
            _connRepo = connRepo;
        }

        public Task BroadcastMessage(string message)
        {
            string content = string.Format("[{0}]: {1}", Context.User.Identity.Name, message);

            LogData("Broadcast", message);

            return Clients.All.RecieveBroadcast(content);
        }

        public Task SendMessage(string sessionName, string message)
        {
            return new Task(() =>
            {
                if (!_registrationService.IsUserAuthorized(Context.ConnectionId, sessionName))
                    return;

                if (_registrationService.IsLoggingEnabled(sessionName))
                    LogData(sessionName, message);

                Clients.Group(sessionName).RecieveMessage(sessionName, message);
            });
        }

        public void JoinSession(string sessionName)
        {
            string clientName = Context.User.Identity.Name;

            if (!_registrationService.IsUserAuthorized(clientName, sessionName))
                return;

            Groups.Add(Context.ConnectionId, sessionName);
            Clients.OthersInGroup(sessionName).RecieveMessage(string.Format("{0} has joined session.", clientName));
        }

        public void LeaveSession(string sessionName)
        {
            string clientName = Context.User.Identity.Name;

            if (!_registrationService.IsUserAuthorized(clientName, sessionName))
                return;

            Groups.Remove(Context.ConnectionId, sessionName);
            Clients.OthersInGroup(sessionName).RecieveMessage(string.Format("{0} has left session.", clientName));
        }

        public override Task OnDisconnected()
        {
            foreach (string session in _connRepo.GetActiveConnectionsByUserName(Context.User.Identity.Name)
                .Select(c => c.SessionName))
            {
                LeaveSession(session);
            }

            return base.OnDisconnected();
        }

        private void LogData(string sessionName, string message)
        {
            throw new NotImplementedException();
        }
    }
}