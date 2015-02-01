using System;

namespace QuickCollab.Collaboration.Domain.Exceptions
{
    public class DuplicatePartyNameException : Exception
    {
        public DuplicatePartyNameException(string message)
            : base(message)
        {
        }
    }
}
