using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Collaboration.Domain.Models
{
    // Entity
    public class Party
    {
        private PartyId _id;
        private PartyDetails _details;
        private Secret _secret;
        private HashSet<PassId> _admittedPasses;

        public Party(string id, PartyDetails details, Secret secret)
        {
            _id = new PartyId(id);
            _details = details;
            _secret = secret;
            _admittedPasses = new HashSet<PassId>();
        }

        public bool Admit(Pass pass, string password = null)
        {
            if (_details.PastExpiryDate(DateTime.UtcNow))
                return false;

            if (_admittedPasses.Contains(pass.PassId()))
                return true;

            if (!Authorized(password))
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

        private bool Authorized(string password)
        {
            if (_secret == null)
                return true;

            return _secret.IsCorrectPassword(password);
        }
    }

    /// <summary>
    /// A new Id must be generated when a new
    /// Party is created.
    /// </summary>
    public class PartyId
    {
        private readonly string _id;

        public PartyId(string id)
        {
            _id = id;
        }

        public string Id()
        {
            return _id;
        }
    }
}
