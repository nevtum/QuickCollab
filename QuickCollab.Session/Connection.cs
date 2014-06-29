using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCollab.Session
{
    public class Connection
    {
        [BsonRequired]
        public string SessionName { get; set; }

        [BsonRequired]
        public string UserName { get; set; }
    }
}
