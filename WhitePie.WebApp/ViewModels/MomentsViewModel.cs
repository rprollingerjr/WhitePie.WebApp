using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WhitePie.ViewModels
{
    public class MomentsViewModel
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public string AltText { get; set; }
        public DateTime DateAdded { get; set; }
        public string FileId { get; set; }
    }
}
