using System;
using QuickCollab.Collaboration.Domain.Models;

namespace QuickCollab.Collaboration.Domain.Events
{
    public class PassRegistered : Event
    {
        public PassRegistered(PartyId partyId, PassId passId)
        {
            PartyId = partyId;
            PassId = passId;
            Time = DateTime.UtcNow;
        }

        public PartyId PartyId { get; private set; }

        public PassId PassId { get; private set; }

        public DateTime Time { get; private set; }
    }
}
