using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuickCollab.Collaboration.Messaging
{
    public class EventSubscribers
    {
        private IList<Action<object>> _delegates = new List<Action<object>>();

        public void Register<T>(Action<T> action)
        {
            if (_delegates.Contains((e) => action((T)e)))
                throw new InvalidOperationException("Cannot register same handler more than once!");

            _delegates.Add((e) => action((T)e));
        }

        public void Unregister<T>(Action<T> action)
        {
            if (!_delegates.Contains((e) => action((T)e)))
                throw new InvalidOperationException("Handler does not exist to unregister!");

            _delegates.Remove((e) => action((T)e));
        }

        public void Invoke<T>(T domainEvent)
        {
            foreach (Action<object> func in _delegates)
            {
                func.Invoke(domainEvent);
            }
        }
    }

}
