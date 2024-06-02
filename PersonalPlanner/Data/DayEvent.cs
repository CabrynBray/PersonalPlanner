using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Calendar.Data
{
    public class DayEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int DayEventId { get; set; }

        public string Note {  get; set; }

        public DateTime EventDate { get; set; } = new DateTime(1900, 1, 1);

        [BsonIgnore]
        public DateTime FromDate { get; set; } = new DateTime(1900, 1, 1);

        [BsonIgnore]
        public DateTime ToDate { get; set; } = new DateTime(1900, 1, 1);

        [BsonIgnore]
        public string DateValue { get; set; }

        [BsonIgnore]
        public string DayName {  get; set; }

        [BsonIgnore]
        public string Message { get; set; }
    }
}
