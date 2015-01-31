using System;
using QuickCollab.Collaboration.Domain.Models;

namespace QuickCollab.Collaboration.Domain.Events
{
    public class PassUnregistered : Event
    {
        public PassUnregistered(PartyId partyId, PassId passId, DateTime time)
        {
            PartyId = partyId;
            PassId = passId;
            Time = time;
        }

        public PartyId PartyId { get; private set; }

        public PassId PassId { get; private set; }

        public DateTime Time { get; private set; }
    }
}
