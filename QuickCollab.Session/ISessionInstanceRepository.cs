using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Session
{
    public interface ISessionInstanceRepository
    {
        IQueryable<SessionInstance> ListAllSessions();
        SessionInstance GetSession(string sessionName);
        void AddSession(SessionInstance instance);
        void DeleteSession(string sessionName);
        bool SessionExists(string sessionName);
    }
}
