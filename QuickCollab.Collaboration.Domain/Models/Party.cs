using QuickCollab.Collaboration.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Collaboration.Domain.Models
{
    // Aggregate root
    public class Party
    {
        #region Fields

        private PartyId _id;
        private PartyDetails _details;
        private Secret _secret;
        private HashSet<PassId> _existingPasses;

        #endregion

        #region Constructors

        public Party(string id, PartyDetails details)
            : this(id, details, new List<PassId>())
        {
        }

        public Party(string id, PartyDetails details, IEnumerable<PassId> existingPasses)
            : this(id, details, existingPasses, null)
        {
        }

        public Party(string id, PartyDetails details, Secret secret)
            : this(id, details, new List<PassId>(), secret)
        {
        }

        public Party(string id, PartyDetails details, IEnumerable<PassId> existingPasses, Secret secret)
        {
            _id = new PartyId(id);
            _details = details;
            _existingPasses = new HashSet<PassId>(existingPasses);
            _secret = secret;
        }

        #endregion

        #region Public Methods

        public bool Admit(PassId passId, string password = null)
        {
            if (_details.ExceededExpiryDate(DateTime.UtcNow))
                throw new PartyExpiredException("Party has exceeded expiration date!");

            if (_existingPasses.Contains(passId))
                return true;

            if (!Authorized(password))
                return false;

            _existingPasses.Add(passId);
            return true;
        }

        public void Remove(PassId passId)
        {
            if (_details.ExceededExpiryDate(DateTime.UtcNow))
                return;

            _existingPasses.Remove(passId);
        }

        #endregion

        #region Private Methods

        private bool Authorized(string password)
        {
            if (_secret == null)
                return true;

            return _secret.IsCorrectPassword(password);
        }

        #endregion
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
