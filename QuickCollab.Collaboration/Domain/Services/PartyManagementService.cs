using QuickCollab.Collaboration.Domain.Models;
using QuickCollab.Collaboration.Domain.Events;
using QuickCollab.Collaboration.Messaging;

namespace QuickCollab.Collaboration.Domain.Services
{
    public class PartyManagementService : IManagePartyPasses
    {
        private IPartyRepository _repository;
        private IEventPublisher _publisher;

        public PartyManagementService(IPartyRepository repository, IEventPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }

        public void AdmitPassToParty(PassId passId, PartyId partyId, string clearPassword = null)
        {
            Party party = _repository.GetPartyById(partyId);
            party.Register(passId, clearPassword);

            _repository.Save(party);

            foreach (Event ev in party.GetUncommittedChanges())
                _publisher.Publish(ev);

            party.MarkChangesAsCommitted();
        }

        public void RemovePassFromParty(PassId passId, PartyId partyId)
        {
            Party party = _repository.GetPartyById(partyId);
            party.UnRegister(passId);

            _repository.Save(party);

            foreach (Event ev in party.GetUncommittedChanges())
                _publisher.Publish(ev);

            party.MarkChangesAsCommitted();
        }

        public bool EnsureAdmission(PassId passId, PartyId partyId)
        {
            Party party = _repository.GetPartyById(partyId);
            return party.EnsureAdmission(passId);
        }
    }
}
