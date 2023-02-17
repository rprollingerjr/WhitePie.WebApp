using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WhitePie.ViewModels
{
    public class MomentsViewModel
    {
        public string AltText { get; set; }
        public string FileAsBase64 { get; set; }
        public string Extension { get; set; }
    }
}
