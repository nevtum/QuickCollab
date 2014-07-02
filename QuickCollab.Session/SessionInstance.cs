using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickCollab.Session
{
    public class SessionInstance
    {
        [BsonId]
        public string Name { get; set; }

        [BsonRequired]
        public DateTime DateCreated { get; set; }
        
        [BsonRequired]
        public bool IsVisible { get; set; }

        [BsonRequired]
        public bool PersistHistory { get; set; }

        public string HashedPassword { get; set; }
        
        public string Salt { get; set; }
    }
}
