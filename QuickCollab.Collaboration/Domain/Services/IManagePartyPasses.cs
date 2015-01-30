using QuickCollab.Collaboration.Domain.Models;

namespace QuickCollab.Collaboration.Domain.Services
{
    public interface IManagePartyPasses
    {
        void AdmitPassToParty(PassId passId, PartyId partyId, string clearPassword);
        void RemovePassFromParty(PassId passId, PartyId partyId);
        bool EnsureAdmission(PassId passId, PartyId partyId);
    }
}
