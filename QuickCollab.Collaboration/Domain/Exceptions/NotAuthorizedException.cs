using System;

namespace QuickCollab.Collaboration.Domain.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message)
            : base(message)
        {
        }
    }
}
