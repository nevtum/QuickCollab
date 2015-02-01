using System;
using System.Collections.Generic;
using QuickCollab.Collaboration.Domain.Models;

namespace QuickCollab.Collaboration.Domain.Services
{
    public interface IPartyRepository
    {
        IEnumerable<PartySummary> OpenParties(DateTime asOfDate);
        Party GetPartyById(PartyId id);
        void Save(Party party);
    }
}
