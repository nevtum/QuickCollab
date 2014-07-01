using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Session
{
    public interface ISessionInstanceRepository
    {
        IQueryable<Connection> GetConnectionsInSession(string sessionName);
        IQueryable<Connection> GetConnectionsByUserName(string userName);
        void RegisterConnection(string userName, string sessionName);
        void UnRegisterConnection(Connection connection);

        IQueryable<SessionInstance> ListAllSessions();
        SessionInstance GetSession(string sessionName);
        void AddSession(SessionInstance instance);
        void DeleteSession(string sessionName);
        bool SessionExists(string sessionName);
    }
}
