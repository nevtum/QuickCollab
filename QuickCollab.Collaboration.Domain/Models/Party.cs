using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Collaboration.Domain.Models
{
    // Entity
    public class Party
    {
        private HashSet<PassId> _admittedPasses;
        private PartyDetails _details;

        public Party(PartyDetails details)
        {
            _details = details;
            _admittedPasses = new HashSet<PassId>();
        }

        public bool Admit(Pass pass, string password = null)
        {
            if (_details.PastExpiryDate(DateTime.UtcNow))
                return false;

            if (!_details.Authorized(password))
                return false;

            _admittedPasses.Add(pass.PassId());
            return true;
        }

        public void Remove(PassId passId)
        {
            if (_details.PastExpiryDate(DateTime.UtcNow))
                return;

            _admittedPasses.Remove(passId);
        }

        public IEnumerable<PassId> ActivePasses()
        {
            return _admittedPasses;
        }

        public int TotalAdmitted()
        {
            return _admittedPasses.Count;
        }
    }
}
