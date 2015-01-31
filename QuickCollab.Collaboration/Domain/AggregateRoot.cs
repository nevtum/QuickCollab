using System.Collections.Generic;
using QuickCollab.Collaboration.Domain.Events;

namespace QuickCollab.Collaboration.Domain
{
    /// <summary>
    /// Slimmed down version of LucidCQRS.
    /// </summary>
    public abstract class AggregateRoot
    {
        private readonly IList<Event> _newChanges = new List<Event>();

        public IEnumerable<Event> GetUncommittedChanges()
        {
            return _newChanges;
        }

        public void MarkChangesAsCommitted()
        {
            _newChanges.Clear();
        }

        protected void AddNewChange(Event e)
        {
            _newChanges.Add(e);
        }
    }
}
