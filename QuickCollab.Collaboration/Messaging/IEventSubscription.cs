using System;
using QuickCollab.Collaboration.Domain.Events;

namespace QuickCollab.Collaboration.Messaging
{
    public interface IEventSubscription
    {
        void Subscribe<T>(Action<T> action) where T : Event;
        void ReleaseHandlers<T>() where T : Event;
    }
}
