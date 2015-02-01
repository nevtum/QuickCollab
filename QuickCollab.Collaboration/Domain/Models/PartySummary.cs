using System;

namespace QuickCollab.Collaboration.Domain.Models
{
    public class PartySummary
    {
        public PartyId PartyId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public PartyDetails Details { get; set; }

        public bool IsPasswordRequired { get; set; }
    }
}
