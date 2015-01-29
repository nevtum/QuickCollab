using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Collaboration.Domain.Models
{
    // Value object
    public class PartyDetails
    {
        private readonly Secret _secret;
        private readonly string _name;
        private readonly DateTime _expiryDate;

        public PartyDetails(string name, DateTime expiryDate, Secret secret = null)
        {
            _name = name;
            _expiryDate = expiryDate;
            _secret = secret;
        }

        public string Name()
        {
            return _name;
        }

        public bool PastExpiryDate(DateTime date)
        {
            return date > _expiryDate;
        }

        public bool Authorized(string password)
        {
            if (_secret == null)
                return true;

            return _secret.IsCorrectPassword(password);
        }
    }
}
