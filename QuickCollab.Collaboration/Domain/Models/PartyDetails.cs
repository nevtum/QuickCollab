using System;

namespace QuickCollab.Collaboration.Domain.Models
{
    // Value object
    public class PartyDetails
    {
        private readonly string _name;
        private readonly DateTime _expiryDate;

        public PartyDetails(string name, DateTime expiryDateUTC)
        {
            _name = name;
            _expiryDate = expiryDateUTC;
        }

        public string Name()
        {
            return _name;
        }

        public bool ExceededExpiryDate(DateTime date)
        {
            return date > _expiryDate;
        }
    }
}
