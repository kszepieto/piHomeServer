using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace piHome.Models.Auth
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
    }
}