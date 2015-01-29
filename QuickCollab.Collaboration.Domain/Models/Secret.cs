using System;
using System.Text;
using System.Security.Cryptography;
using QuickCollab.Security;

namespace QuickCollab.Collaboration.Domain.Models
{
    public class Secret
    {
        #region Fields

        private string _hashedPassword;
        private string _salt;

        #endregion

        #region Constructor

        public Secret(string hashedPassword, string salt)
        {
            SetHashedPassword(hashedPassword);
            SetSalt(salt);
        }

        #endregion

        public bool IsCorrectPassword(string clearPassword)
        {
            if (string.IsNullOrEmpty(clearPassword))
                throw new InvalidOperationException("Must provide a password!");

            string saltedPassword = PasswordHashService.SaltedPassword(clearPassword, _salt);
            return saltedPassword == _hashedPassword;
        }

        #region Private Methods

        private void SetSalt(string salt)
        {
            if (string.IsNullOrEmpty(salt))
                throw new NullReferenceException("Salt");

            _salt = salt;
        }

        private void SetHashedPassword(string hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                throw new NullReferenceException("Password");

            _hashedPassword = hashedPassword;
        }

        #endregion
    }
}
