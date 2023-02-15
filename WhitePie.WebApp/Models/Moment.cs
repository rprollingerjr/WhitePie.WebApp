using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WhitePie.Models
{
    [BsonIgnoreExtraElements]
    public class Moment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("file_name")]
        public string FileName { get; set; }
        [BsonElement("url")]
        public string Url { get; set; }
        [BsonElement("alt_text")]
        public string AltText { get; set; }
        [BsonElement("date_added")]
        public DateTime DateAdded { get; set; }
        [BsonElement("file_id")]
        public string FileId { get; set; }
    }
}
