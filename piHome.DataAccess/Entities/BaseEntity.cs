using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace piHome.DataAccess.Entities
{
    public class BaseEntity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
