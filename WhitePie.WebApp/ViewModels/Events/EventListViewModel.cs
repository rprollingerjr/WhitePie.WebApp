namespace WhitePie.ViewModels.Events
{
    public class EventListViewModel
    {
        public string PageTitle { get; set; }
        public List<EventItemViewModel> UpcomingEvents { get; set; }
        public List<EventItemViewModel> PastEvents { get; set; }
    }
}
