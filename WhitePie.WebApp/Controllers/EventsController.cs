using Microsoft.AspNetCore.Mvc;
using WhitePie.Services;
using WhitePie.ViewModels.Events;

namespace WhitePie.Controllers
{
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly EventsService _eventsService;
        public EventsController(EventsService eventsService)
        {
            _eventsService = eventsService;
        }

        public async Task<IActionResult> Index()
        {
            var eventListViewModel = new EventListViewModel();

            try
            {
                var events = await _eventsService.GetAsync();

                if (events.Any())
                {
                    eventListViewModel.Events = new List<EventItemViewModel>();
                    foreach(var item in events)
                    {
                        eventListViewModel.Events.Add(new EventItemViewModel()
                        {
                            EventTitle = item.EventName,
                            Location = item.Location,
                            Venue = item.Venue,
                            EventDescription = item.EventDescription,
                            EventTimeInfo = ParseEventDate(item.EventStartDate.Value, item.EventEndDate.Value),
                            TicketUrl = item.TicketUrl
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }

            return View(eventListViewModel);
        }

        private EventTime ParseEventDate(DateTime startDate, DateTime endDate)
        {
            var timeInfo = new EventTime();

            timeInfo.DayOfTheMonth = startDate.Day;
            timeInfo.AbbreviatedMonth = startDate.ToString("MMM");
            timeInfo.FullDayAndDate = startDate.ToString("dddd, dd MMMM yyyy");
            timeInfo.StartTime = startDate.ToString("h:mm tt");
            timeInfo.EndTime = endDate.ToString("h:mm tt");

            return timeInfo;
        }
    }
}