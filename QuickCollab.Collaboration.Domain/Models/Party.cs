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
        private HashSet<PassId> _existingPasses;

        public Party(string id, PartyDetails details, Secret secret, IEnumerable<PassId> existingPasses)
        {
            _id = new PartyId(id);
            _details = details;
            _secret = secret;
            _existingPasses = new HashSet<PassId>(existingPasses);
        }

        public bool Admit(Pass pass, string password = null)
        {
            if (_details.PastExpiryDate(DateTime.UtcNow))
                return false;

            if (_existingPasses.Contains(pass.PassId()))
                return true;

            if (!Authorized(password))
                return false;

            _existingPasses.Add(pass.PassId());
            return true;
        }

        public void Remove(PassId passId)
        {
            if (_details.PastExpiryDate(DateTime.UtcNow))
                return;

            _existingPasses.Remove(passId);
        }

        public IEnumerable<PassId> ExistingPasses()
        {
            return _existingPasses;
        }

        public int TotalAdmitted()
        {
            return _existingPasses.Count;
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
