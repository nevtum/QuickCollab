using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickCollab.Collaboration.Messaging;
using QuickCollab.Collaboration.Domain.Models;
using QuickCollab.Collaboration.Domain.Events;
using QuickCollab.Collaboration.Domain.Services;
using QuickCollab.Collaboration.Domain.Exceptions;

namespace QuickCollab.Collaboration.Tests
{
    [TestClass]
    public class PartyManagementTests
    {
        [TestMethod]
        public void ShouldStorePassIdAfterAdmission()
        {
            PassRegistered ev = null;

            EventPublisher publisher = new EventPublisher();

            publisher.Subscribe<PassRegistered>((e) =>
            {
                ev = e;
            });

            IPartyRepository repo = new MockPartyRepository();
            IManagePartyPasses service = new PartyManagementService(repo, publisher);

            service.AdmitPassToParty(new PassId("5325"), new PartyId("123"), "SecretPassword");

            Assert.IsNotNull(ev);
            Assert.AreEqual("123", ev.PartyId.Id());
            Assert.AreEqual("5325", ev.PassId.Id());
        }

        [TestMethod]
        public void ShouldNotStorePassIdAfterRejectedAdmission()
        {
            bool triggered = false;

            EventPublisher publisher = new EventPublisher();

            publisher.Subscribe<PassRegistered>(e =>
            {
                triggered = true;
            });

            IPartyRepository repo = new MockPartyRepository();
            IManagePartyPasses service = new PartyManagementService(repo, publisher);

            bool threwException = false;

            try
            {
                service.AdmitPassToParty(new PassId("5325"), new PartyId("123"), "WrongPassword");
            }
            catch(NotAuthorizedException e)
            {
                threwException = true;
            }

            Assert.AreEqual(true, threwException);
            Assert.AreEqual(false, triggered);
        }
    }

    public class MockPartyRepository : IPartyRepository
    {
        private Party _parties;

        public MockPartyRepository()
        {
            DateTime expiryDate = DateTime.UtcNow.AddHours(2);
            PartyDetails details = new PartyDetails("MyParty", expiryDate);
            Secret secret = new Secret("SecretPassword");
            PassId[] storedIds = new[] { new PassId("1"), new PassId("2") };

            _parties = new Party("123", details, storedIds, secret);
        }

        public Party GetPartyById(PartyId id)
        {
            if (id.Id() != "123")
                throw new InternalTestFailureException("Test database! Please use id 123 only!");

            return new Party(_parties);
        }

        public void Save(Party party)
        {
            _parties = new Party(party);
        }

        public IEnumerable<PartySummary> OpenParties(DateTime asOfDate)
        {
            throw new NotImplementedException();
        }
    }

}
