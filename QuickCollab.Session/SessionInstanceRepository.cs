using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Session
{
    public class SessionInstanceRepository : ISessionInstanceRepository
    {
        public IQueryable<SessionInstance> ListAllSessions()
        {
            throw new NotImplementedException();
        }

        public SessionInstance GetSession(string sessionName)
        {
            throw new NotImplementedException();
        }

        public void AddSession(SessionInstance instance)
        {
            // store into database
        }

        public void DeleteSession(string sessionName)
        {
            throw new NotImplementedException();
        }

        public bool SessionExists(string sessionName)
        {
            // Have proper logic in here.
            return false;
        }
    }
}
