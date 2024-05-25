using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonalPlanner.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string user_name { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}