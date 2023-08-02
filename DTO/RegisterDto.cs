
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace API.DTO
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
         [Required]
         [StringLength(8, MinimumLength =4)]
        public string password { get; set; }
    }
}