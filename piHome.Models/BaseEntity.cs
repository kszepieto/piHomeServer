using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace piHome.Models
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
    }
}