using System;

namespace QuickCollab.Session
{
    public class SessionParameters
    {
        public SessionParameters(bool isPublic, bool persistent, int connectionExpiryHrs)
        {
            IsPublic = isPublic;
            Persistent = persistent;
            ConnectionExpiryInHours = connectionExpiryHrs;
        }

        public bool IsPublic { get; private set; }

        public bool Persistent { get; private set; }

        public int ConnectionExpiryInHours { get; private set; }
    }
}
