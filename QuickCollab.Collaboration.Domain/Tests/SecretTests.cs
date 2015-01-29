using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickCollab.Collaboration.Domain.Models;
using QuickCollab.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCollab.Collaboration.Domain.Tests
{
    [TestClass]
    public class SecretTests
    {
        [TestMethod]
        public void ShouldReturnTrueForCorrectPassword()
        {
            bool result = TestPassword("MyPassword");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void ShouldReturnFalseForIncorrectPassword()
        {
            bool result = TestPassword("WrongPassword");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionForNullPassword()
        {
            bool result = TestPassword(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShouldThrowExceptionForEmptyPassword()
        {
            bool result = TestPassword(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowExceptionForInvalidSecret()
        {
            Secret secret = new Secret(null, PasswordHashService.GetNewSalt());
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ShouldThrowExceptionForInvalidSalt()
        {
            Secret secret = new Secret("saltedPassword", null);
        }

        private bool TestPassword(string password)
        {
            string secretPassword = "MyPassword";
            string salt = PasswordHashService.GetNewSalt();

            Secret secret = new Secret(PasswordHashService.SaltedPassword(secretPassword, salt), salt);

            return secret.IsCorrectPassword(password);
        }
    }
}
