using System;
using System.Linq;
using System.Collections.Generic;
using QuickCollab.Collaboration.Messaging;
using QuickCollab.Collaboration.Domain.Models;
using QuickCollab.Collaboration.Domain.Events;
using QuickCollab.Collaboration.Domain.Exceptions;

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

        /// <summary>
        /// To do: write unit test,
        /// verify business rules.
        /// </summary>
        public void OpenNewParty(PartyDetails details, string clearPassword = null)
        {
            IEnumerable<PartySummary> openParties = _repository.OpenParties(DateTime.UtcNow);

            if (openParties.Any(p => p.Details.HasDuplicateName(details)))
                throw new DuplicatePartyNameException("Another open party exists with the same name!");

            string id = Guid.NewGuid().ToString();

            Party party;

            if (clearPassword == null)
                party = new Party(id, details, new Secret(clearPassword));
            else
                party = new Party(id, details);

            _repository.Save(party);

            _publisher.Publish<NewPartyOpened>(new NewPartyOpened(new PartyId(id)));
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
