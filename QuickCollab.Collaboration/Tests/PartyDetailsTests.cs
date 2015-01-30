using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickCollab.Collaboration.Domain.Models;

using System;

namespace QuickCollab.Collaboration.Domain.Tests
{
    [TestClass]
    public class PartyDetailsTests
    {
        [TestMethod]
        public void ShouldReturnFalseIfNotPastExpiryDate()
        {
            DateTime expiryDate = new DateTime(2015, 1, 29, 7, 28, 50);
            PartyDetails details = new PartyDetails("MyParty", expiryDate);

            DateTime testDate1 = new DateTime(2015, 1, 29, 7, 28, 12);
            DateTime testDate2 = new DateTime(2015, 1, 29, 7, 28, 50);

            Assert.AreEqual(false, details.ExceededExpiryDate(testDate1));
            Assert.AreEqual(false, details.ExceededExpiryDate(testDate2));
        }

        [TestMethod]
        public void ShouldReturnTrueIfPastExpiryDate()
        {
            DateTime expiryDate = new DateTime(2015, 1, 29, 7, 28, 50);
            PartyDetails details = new PartyDetails("MyParty", expiryDate);

            DateTime testDate1 = new DateTime(2015, 1, 29, 7, 28, 51);
            DateTime testDate2 = new DateTime(2016, 1, 23, 5, 23, 14);

            Assert.AreEqual(true, details.ExceededExpiryDate(testDate1));
            Assert.AreEqual(true, details.ExceededExpiryDate(testDate2));
        }
    }
}
