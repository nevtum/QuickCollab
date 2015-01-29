using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Collaboration.Domain.Models
{
    // Value object
    public class PartyDetails
    {
        private readonly string _name;
        private readonly DateTime _expiryDate;

        public PartyDetails(string name, DateTime expiryDate)
        {
            _name = name;
            _expiryDate = expiryDate;
        }

        public string Name()
        {
            return _name;
        }

        public bool PastExpiryDate(DateTime date)
        {
            return date > _expiryDate;
        }
    }
}
