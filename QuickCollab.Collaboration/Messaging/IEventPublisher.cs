using System.Collections.Generic;
using QuickCollab.Collaboration.Domain.Events;

namespace QuickCollab.Collaboration.Messaging
{
    public interface IEventPublisher
    {
        void Publish<T>(T domainEvent) where T : Event;
        void Publish<T>(IEnumerable<T> domainEvents) where T : Event;
    }
}
