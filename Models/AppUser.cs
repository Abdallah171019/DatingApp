using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Extensions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace API.Entities
{
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
        [BsonElement("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }       
         [BsonElement("KnownAs")]
        public string KnownAs { get; set; }
        [BsonElement("Created")]
        public DateTime Created { get; set; } = DateTime.UtcNow; //when the user created inside the db
        [BsonElement("LastActive")]
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        [BsonElement("Gender")]
        public string Gender { get; set; } 
        [BsonElement("Introduction")]
        public string Introduction { get; set; }        
        [BsonElement("LookingFor")]
        public string LookingFor { get; set; }
        [BsonElement("Interests")]
        public string Interests { get; set; }
        [BsonElement("City")]
        public string City { get; set; }
        [BsonElement("Country")]
        public string Country { get; set; }
        [BsonElement("PhotoURL")]
        public string PhotoURL { get; set; }

        [BsonElement("Photos")]
        public List<Photos> Photos { get; set; } = new List<Photos>();
   /*       
 public int GetAge(){
    return DateOfBirth.CalculateAge();
   }   
   */
    }
}