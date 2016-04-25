using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace piHome.Models.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }
    }
}
