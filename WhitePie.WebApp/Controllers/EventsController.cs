using Microsoft.AspNetCore.Mvc;
using WhitePie.Services;
using WhitePie.ViewModels.Events;

namespace WhitePie.Controllers
{
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
            var eventRowViewModel = new List<EventRowViewModel>();

            var leftCards = new List<EventItemViewModel>();
            var rightCards = new List<EventItemViewModel>();

            try
            {
                var events = await _eventsService.GetAsync();

                if (events.Any())
                {
                    for(int i = 0; i < events.Count; i++)
                    {
                        var eventCard = new EventItemViewModel()
                        {
                            EventTitle = events[i].EventName,
                            EventDescription = events[i].EventDescription,
                            EventTimeInfo = new EventTime(),
                            VenueName = events[i].Location
                        };


                        if (i % 2 == 0)
                        {
                            leftCards.Add(eventCard);
                        }
                        else
                        {
                            rightCards.Add(eventCard);
                        }
                    }

                    

                }
                else
                {
                    eventRowViewModel.Add(new EventRowViewModel
                    {
                        LeftCard = new EventItemViewModel
                        {
                            EventTitle = "No Events",
                            EventDescription = "Come back soon to see upcoming events or follow Edible Mami on social media for more up-to-date information!"
                        }
                    });

                    eventListViewModel.Rows.AddRange(eventRowViewModel);
                }
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("The events service failed.");
            }

            return View(eventListViewModel);
        }
    }
}