using QuickCollab.Collaboration.Domain.Models;

namespace QuickCollab.Collaboration.Domain.Services
{
    /// <summary>
    /// To do: Dependency on domain event publisher
    /// </summary>
    public class PartyManagementService : IManagePartyPasses
    {
        private IPartyRepository _repository;

        public PartyManagementService(IPartyRepository repository)
        {
            _repository = repository;
        }

        public void AdmitPassToParty(PassId passId, PartyId partyId, string clearPassword = null)
        {
            Party party = _repository.GetPartyById(partyId);
            bool result = party.Admit(passId, clearPassword);

            if (result)
            {
                // publish admission granted event
                _repository.Save(party);
            }
            else
            {
                // publish admission rejected event
            }
        }

        public void RemovePassFromParty(PassId passId, PartyId partyId)
        {
            Party party = _repository.GetPartyById(partyId);
            party.Remove(passId);

            // publish passId removed event
        }

        public bool EnsureAdmission(PassId passId, PartyId partyId)
        {
            Party party = _repository.GetPartyById(partyId);
            return party.Admit(passId);
        }
    }
}
