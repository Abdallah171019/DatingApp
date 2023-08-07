using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace API.Entities
{
    public class Photos
    {
    [BsonElement("ID")]
    public string ID { get; set; }

    [BsonElement("URL")]
    public string URL { get; set; }

    [BsonElement("IsMain")]
    public bool IsMain { get; set; }

    [BsonElement("PublicId")]
    public string PublicId { get; set; }
        
    }
}