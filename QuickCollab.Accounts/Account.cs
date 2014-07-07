using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCollab.Accounts
{
    public class Account
    {
        [BsonId]
        public string UserName { get; set; }

        public DateTime DateCreated { get; set; }

        [BsonRequired]
        public string Password { get; set; }

        public string Salt { get; set; }
    }
}
