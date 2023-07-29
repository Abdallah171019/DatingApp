using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace API.Entities
{
    [BsonIgnoreExtraElements]
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("username")]
        public string UserName { get; set; } = null!;
        [BsonElement("PasswordHash")]
        public byte[] PasswordHash{get; set;}
        [BsonElement("PasswordSalt")]
        public byte[] PasswordSalt { get; set; }

        
    }
}