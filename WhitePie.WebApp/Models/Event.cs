using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WhitePie.Models
{
    public class Event
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Name")]
        public string EventName { get; set; }
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }

        public string EventDescription { get; set; }
        public string TicketUrl { get; set; }
    }
}
