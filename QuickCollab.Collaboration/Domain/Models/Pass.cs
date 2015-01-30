using System;

namespace QuickCollab.Collaboration.Domain.Models
{
    public class Pass
    {
        private PassId _passId;

        public Pass(string id)
        {
            _passId = new PassId(id);
        }

        public PassId PassId()
        {
            return _passId;
        }
    }

    public class PassId
    {
        private readonly string _id;

        public PassId(string id)
        {
            _id = id;
        }

        public string Id()
        {
            return _id;
        }
    }
}
