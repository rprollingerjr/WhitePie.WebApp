using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WhitePie.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Title")]
        public string EventName { get; set; }
        public DateTime? EventDate { get; set; }

        public string Location { get; set; }

        public string EventDescription { get; set; }
    }
}
