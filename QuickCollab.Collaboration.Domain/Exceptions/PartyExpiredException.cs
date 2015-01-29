using System;

namespace QuickCollab.Collaboration.Domain.Exceptions
{
    public class PartyExpiredException : Exception
    {
        public PartyExpiredException(string message)
            : base(message)
        {
        }
    }
}
