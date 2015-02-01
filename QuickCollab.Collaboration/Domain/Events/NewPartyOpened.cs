using QuickCollab.Collaboration.Domain.Models;
using System;

namespace QuickCollab.Collaboration.Domain.Events
{
    public class NewPartyOpened : Event
    {
        public NewPartyOpened(PartyId id)
        {
            PartyId = id;
            DateOpened = DateTime.UtcNow;
        }

        public PartyId PartyId { get; private set; }

        public DateTime DateOpened { get; private set; }
    }
}
