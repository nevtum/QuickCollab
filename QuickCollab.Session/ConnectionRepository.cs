using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCollab.Session
{
    public class ConnectionRepository : IConnectionRepository
    {
        private MongoDatabase _dbContext;
        private MongoCollection _connections;

        public ConnectionRepository()
        {
            _dbContext = new MongoClient("mongodb://user1:pa55W0rd@localhost")
                .GetServer()
                .GetDatabase("QuickCollab");

            _connections = _dbContext.GetCollection<Connection>("Connections");
        }

        public IQueryable<Connection> GetActiveConnectionsInSession(string sessionName)
        {
            var q1 = Query<Connection>.EQ(c => c.SessionName, sessionName);
            var q2 = Query<Connection>.GT(c => c.Expiration, DateTime.Now);
            var query = Query.And(q1, q2);

            return _connections
                .FindAs<Connection>(query)
                .AsQueryable();
        }

        public IQueryable<Connection> GetActiveConnectionsByUserName(string userName)
        {
            var q1 = Query<Connection>.EQ(c => c.ClientName, userName);
            var q2 = Query<Connection>.GT(c => c.Expiration, DateTime.Now);
            var query = Query.And(q1, q2);

            return _connections
                .FindAs<Connection>(query)
                .AsQueryable();
        }

        public void RegisterConnection(string userName, SessionInstance instance)
        {
            Connection conn = new Connection()
            {
                DateCreated = DateTime.Now,
                Expiration = DateTime.Now.AddHours(instance.ConnectionExpiryInHours),
                ClientName = userName,
                SessionName = instance.Name
            };

            _connections.Insert(conn);
        }

        //public void UnRegisterConnection(Connection connection)
        //{
        //    var query = Query.And(Query<Connection>.EQ(s => s.ClientName, connection.ClientName),
        //        Query<Connection>.EQ(s => s.SessionName, connection.SessionName));

        //    _connections.Remove(query);
        //}
    }
}
