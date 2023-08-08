using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace API.DTO
{
    public class PhotoDTO
    {
        
    public int ID { get; set; }

    public string URL { get; set; }

    public bool IsMain { get; set; }
    public string PublicId { get; set; }

    }
}