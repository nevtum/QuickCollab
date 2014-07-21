using MongoDB.Bson;
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
        public ObjectId Id { get; set; }

        [BsonRequired]
        public DateTime DateCreated { get; set; }

        [BsonRequired]
        public DateTime Expiration { get; set; }

        [BsonRequired]
        public string SessionName { get; set; }

        [BsonRequired]
        public string ClientName { get; set; }
    }
}
