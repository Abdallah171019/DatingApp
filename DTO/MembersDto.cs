using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTO
{
    public class MembersDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; } = null!;
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow; //when the user created inside the db
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public string Gender { get; set; } 
        public string Introduction { get; set; }        
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoURL { get; set; }
        public List<PhotoDTO> Photos { get; set; } 
    }
}