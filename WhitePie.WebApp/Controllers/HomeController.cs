using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WhitePie.Models;
using WhitePie.Services;
using WhitePie.ViewModels;
using WhitePie.ViewModels.Events;
using WhitePie.WebApp.Services.Interfaces;

namespace WhitePie.WebApp.Controllers
{
    public class HomeController : BaseController<HomeController>
    {
        private readonly IEventService _eventService;
        private readonly IMomentService _momentService;
        public HomeController(IMomentService momentService, IEventService eventService)
        {
            _momentService = momentService;
            _eventService = eventService;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                MomentsViewModel = new List<MomentsViewModel>(),
                EventsViewModel = await EventsGetAll()
            };
            try
            {
                var moments = await _momentService.GetAllMomentsAsync();

                foreach (var moment in moments) 
                {
                    viewModel.MomentsViewModel.Add(new MomentsViewModel(moment)); 
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Image(string Id, string Extension)
        {
            var fileBytes = await _momentService.GetFileBytesAsync(Id);
            
            return File(fileBytes, $"image/{Extension}");
        }

        public async Task<EventListViewModel> EventsGetAll()
        {
            var eventListViewModel = new EventListViewModel();

            try
            {
                var events = await _eventService.GetAllEventsAsync();
                events = events.OrderBy(_ => _.EventStartDate).ToList();

                if (events.Any())
                {
                    var UpcomingEvents = new List<EventItemViewModel>();
                    var PastEvents = new List<EventItemViewModel>();
                    foreach (var item in events)
                    {
                        var newEvent = new EventItemViewModel()
                        {
                            EventTitle = item.EventName,
                            City = item.Location,
                            Venue = item.Venue,
                            EventDescription = item.EventDescription,
                            EventTimeInfo = ParseEventDate(item.EventStartDate.Value, item.EventEndDate.Value),
                            TicketUrl = item.TicketUrl
                        };

                        if (item.EventStartDate < DateTime.Now)
                        {
                            PastEvents.Add(newEvent);
                        }
                        else
                        {
                            UpcomingEvents.Add(newEvent);
                        }

                    }

                    eventListViewModel.UpcomingEvents = UpcomingEvents;

                    if (PastEvents.Any())
                    {
                        eventListViewModel.PastEvents = PastEvents;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }

            return eventListViewModel;
        }

        [Route("maintenance")]
        public IActionResult Maintenance()
        {
            return View();
        }

        private EventTime ParseEventDate(DateTime startDate, DateTime endDate)
        {
            var timeInfo = new EventTime();

            timeInfo.DayOfTheMonth = startDate.Day;
            timeInfo.AbbreviatedMonth = startDate.ToString("MMM");
            timeInfo.FullDayAndDate = startDate.ToString("dddd, dd MMMM yyyy");
            timeInfo.StartTime = startDate.ToString("h:mm tt");
            timeInfo.EndTime = endDate.ToString("h:mm tt");
            timeInfo.Year = startDate.Year;

            return timeInfo;
        }
    }
}