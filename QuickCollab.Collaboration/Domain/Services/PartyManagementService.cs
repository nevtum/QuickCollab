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
            party.Register(passId, clearPassword);

            _repository.Save(party);

            foreach (Event ev in party.GetUncommittedChanges())
            {
                // publish event
            }

            party.MarkChangesAsCommitted();
        }

        public void RemovePassFromParty(PassId passId, PartyId partyId)
        {
            Party party = _repository.GetPartyById(partyId);
            party.UnRegister(passId);

            _repository.Save(party);

            foreach (Event ev in party.GetUncommittedChanges())
            {
                // publish event
            }

            party.MarkChangesAsCommitted();
        }

        public bool EnsureAdmission(PassId passId, PartyId partyId)
        {
            Party party = _repository.GetPartyById(partyId);
            return party.EnsureAdmission(passId);
        }
    }
}
