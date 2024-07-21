using WhitePie.Models;
using WhitePie.ViewModels;

namespace WhitePie.ViewModels
{
    public class IndexViewModel
    {
        public List<MomentsViewModel> MomentsViewModel { get; set; }
        public Events.EventListViewModel EventsViewModel { get; set; }
    }
}
