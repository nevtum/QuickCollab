using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCollab.Session
{
    public interface IConnectionRepository
    {
        IQueryable<Connection> GetActiveConnectionsInSession(string sessionName);
        IQueryable<Connection> GetActiveConnectionsByUserName(string userName);
        void RegisterConnection(string userName, SessionInstance instance);
        //void UnRegisterConnection(Connection connection);
    }
}
