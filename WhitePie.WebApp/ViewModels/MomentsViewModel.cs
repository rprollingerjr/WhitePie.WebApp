using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using WhitePie.Models;

namespace WhitePie.ViewModels
{
    public class MomentsViewModel
    {
        public string Id { get; set; }
        public string AltText { get; set; }
        public string Extension { get; set; }

        public MomentsViewModel(Moment model) 
        {
            this.Id = model.FileId;
            this.AltText = model.AltText;
            this.Extension = model.Extension;
        }
    }
}
