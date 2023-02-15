namespace WhitePie.ViewModels.Events
{
    public class EventItemViewModel
    {
        public string EventTitle { get; set; }
        public string VenueName { get; set; }
        public EventTime EventTimeInfo { get; set; }
        public string EventDescription { get; set; }
    }

    public class EventTime
    {
        public DateTime FullDateTime { get; set; }
        public string AbbreviatedMonth { get; set; }
        public int Year { get; set; }

    }
}
