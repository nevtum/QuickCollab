﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickCollab.Collaboration.Domain.Models;
using QuickCollab.Collaboration.Domain.Exceptions;

namespace QuickCollab.Collaboration.Domain.Tests
{
    [TestClass]
    public class PartyAggregateTests
    {
        [TestMethod]
        public void ShouldAdmitPassForNonSecuredParty()
        {
            DateTime expiryDate = DateTime.UtcNow.AddHours(1);
            Party party = CreateNonSecuredParty(expiryDate);
            PassId passId = CreatePassId();

            Assert.AreEqual(true, party.Admit(passId));

            // Tested again for idempotency
            Assert.AreEqual(true, party.Admit(passId));
        }

        [TestMethod]
        public void ShouldAdmitPassForSecuredParty()
        {
            DateTime expiryDate = DateTime.UtcNow.AddHours(1);
            Party party = CreateSecuredParty(expiryDate);
            PassId passId = CreatePassId();

            Assert.AreEqual(true, party.Admit(passId, "SecretPassword"));

            // Tested again for idempotency
            Assert.AreEqual(true, party.Admit(passId, "SecretPassword"));

            // Tested again for idempotency, without password
            Assert.AreEqual(true, party.Admit(passId));
        }

        [TestMethod]
        public void ShouldRejectPassForSecuredParty()
        {
            DateTime expiryDate = DateTime.UtcNow.AddHours(1);
            Party party = CreateSecuredParty(expiryDate);
            PassId passId = CreatePassId();

            Assert.AreEqual(false, party.Admit(passId, "WrongPassword"));
            Assert.AreEqual(false, party.Admit(passId, "#@@$^@&@#&"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionWhenNoPasswordProvided()
        {
            DateTime expiryDate = DateTime.UtcNow.AddHours(1);
            Party party = CreateSecuredParty(expiryDate);
            PassId passId = CreatePassId();

            Assert.AreEqual(false, party.Admit(passId));
        }

        [TestMethod]
        [ExpectedException(typeof(PartyExpiredException))]
        public void ShouldThrowExceptionWhenNonSecuredPartyExceededExpiryDate()
        {
            DateTime expiryDate = DateTime.UtcNow.AddHours(-1);
            Party party = CreateNonSecuredParty(expiryDate);
            PassId passId = CreatePassId();

            Assert.AreEqual(true, party.Admit(passId));
        }

        [TestMethod]
        [ExpectedException(typeof(PartyExpiredException))]
        public void ShouldThrowExceptionWhenSecuredPartyExceededExpiryDate1()
        {
            DateTime expiryDate = DateTime.UtcNow.AddHours(-1);
            Party party = CreateSecuredParty(expiryDate);
            PassId passId = CreatePassId();

            // Throws error despite correct password
            Assert.AreEqual(true, party.Admit(passId, "SecretPassword"));
        }

        [TestMethod]
        [ExpectedException(typeof(PartyExpiredException))]
        public void ShouldThrowExceptionWhenSecuredPartyExceededExpiryDate2()
        {
            DateTime expiryDate = DateTime.UtcNow.AddHours(-1);
            Party party = CreateSecuredParty(expiryDate);
            PassId passId = CreatePassId();

            Assert.AreEqual(true, party.Admit(passId, "WrongPassword"));
        }

        #region Helper Methods

        private Party CreateNonSecuredParty(DateTime expiryDate)
        {
            // id generated by system
            string id = Guid.NewGuid().ToString();

            PartyDetails details = new PartyDetails("MyParty", expiryDate);

            return new Party(id, details);
        }

        private Party CreateSecuredParty(DateTime expiryDate)
        {
            // id generated by system
            string id = Guid.NewGuid().ToString();

            PartyDetails details = new PartyDetails("MyParty", expiryDate);
            Secret secret = new Secret("SecretPassword");

            return new Party(id, details, secret);
        }

        private PassId CreatePassId()
        {
            // id generated by system
            string passId = Guid.NewGuid().ToString();
            return new PassId(passId);
        }

        #endregion
    }
}
