using QuickCollab.Collaboration.Domain.Models;

namespace QuickCollab.Collaboration.Domain.Services
{
    public interface IPartyRepository
    {
        Party GetPartyById(PartyId id);
        void Save(Party party);
    }
}
