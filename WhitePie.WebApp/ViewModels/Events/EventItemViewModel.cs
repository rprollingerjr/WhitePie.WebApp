namespace WhitePie.ViewModels.Events
{
    public class EventItemViewModel
    {
        public string EventTitle { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }
        public EventTime EventTimeInfo { get; set; }
        public string EventDescription { get; set; }
        public string TicketUrl { get; set; }
    }

    public class EventTime
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string EndDateTime { get; set; }
        public string FullDayAndDate { get; set; }
        public string AbbreviatedMonth { get; set; }
        public int DayOfTheMonth { get; set; }

    }
}
