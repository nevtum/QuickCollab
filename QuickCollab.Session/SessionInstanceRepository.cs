using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Session
{
    public class SessionInstanceRepository : ISessionInstanceRepository
    {
        private MongoDatabase _dbContext;
        private MongoCollection _sessions;
        private MongoCollection _connections;

        public SessionInstanceRepository()
        {
            _dbContext = new MongoClient("mongodb://user1:pa55W0rd@localhost")
                .GetServer()
                .GetDatabase("QuickCollab");

            _sessions = _dbContext.GetCollection<SessionInstance>("Sessions");
            _connections = _dbContext.GetCollection<Connection>("Connections");
        }

        public IQueryable<Connection> GetConnectionsInSession(string sessionName)
        {
            var query = Query<Connection>.EQ(c => c.SessionName, sessionName);

            return _connections
                .FindAs<Connection>(query)
                .AsQueryable();
        }

        public void AddConnection(Connection conn, SessionInstance session)
        {
            throw new NotImplementedException();
        }

        public void RemoveConnection(Connection conn, SessionInstance session)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SessionInstance> ListAllSessions()
        {
            return _sessions.FindAllAs<SessionInstance>().AsQueryable();
        }

        public SessionInstance GetSession(string sessionName)
        {
            var query = Query<SessionInstance>.EQ(s => s.Name, sessionName);

            return _sessions
                .FindAs<SessionInstance>(query)
                .SingleOrDefault();
        }

        public void AddSession(SessionInstance instance)
        {
            System.Diagnostics.Debug.Assert(!SessionExists(instance.Name));

            _sessions.Insert(instance);
        }

        public void DeleteSession(string sessionName)
        {
            if (SessionExists(sessionName))
                _sessions.Remove(Query<SessionInstance>.EQ(s => s.Name, sessionName));
        }

        public bool SessionExists(string sessionName)
        {
            var query = Query<SessionInstance>.EQ(s => s.Name, sessionName);

            return _sessions
                .FindAs<SessionInstance>(query)
                .Any();
        }
    }
}
