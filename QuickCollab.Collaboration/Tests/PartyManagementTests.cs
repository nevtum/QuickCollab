using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickCollab.Collaboration.Domain.Models;
using QuickCollab.Collaboration.Domain.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuickCollab.Collaboration.Tests
{
    [TestClass]
    public class PartyManagementTests
    {
        [TestMethod]
        public void ShouldStorePassIdAfterAdmission()
        {
            IPartyRepository repo = new MockPartyRepository();
            IManagePartyPasses service = new PartyManagementService(repo);

            service.AdmitPassToParty(new PassId("5325"), new PartyId("123"), "SecretPassword");

            Party party = repo.GetPartyById(new PartyId("123"));

            IEnumerable<PassId> ids = party.ExistingPasses();;

            Assert.AreEqual(3, ids.Count());
            Assert.AreEqual(true, ids.Any(passId => passId.Id() == "1"));
            Assert.AreEqual(true, ids.Any(passId => passId.Id() == "2"));
            Assert.AreEqual(true, ids.Any(passId => passId.Id() == "5325"));
        }

        [TestMethod]
        public void ShouldNotStorePassIdAfterRejectedAdmission()
        {
            IPartyRepository repo = new MockPartyRepository();
            IManagePartyPasses service = new PartyManagementService(repo);

            service.AdmitPassToParty(new PassId("5325"), new PartyId("123"), "WrongPassword");

            Party party = repo.GetPartyById(new PartyId("123"));

            IEnumerable<PassId> ids = party.ExistingPasses();

            Assert.AreEqual(2, ids.Count());
            Assert.AreEqual(true, ids.Any(passId => passId.Id() == "1"));
            Assert.AreEqual(true, ids.Any(passId => passId.Id() == "2"));
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
    }

}
