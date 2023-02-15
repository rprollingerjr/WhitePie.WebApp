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
            var eventListViewModel = new EventListViewModel()
            {
                Rows = new List<EventRowViewModel>()
            };

            var leftCards = new List<EventItemViewModel>();
            var rightCards = new List<EventItemViewModel>();

            try
            {
                var events = await _eventsService.GetAsync();

                if (events.Any())
                {
                    for(int e = 0; e < events.Count; e++)
                    {
                        var eventCard = new EventItemViewModel()
                        {
                            EventTitle = events[e].EventName,
                            EventDescription = events[e].EventDescription,
                            EventTimeInfo = new EventTime(),
                            VenueName = events[e].Location
                        };


                        if (e % 2 == 0)
                        {
                            leftCards.Add(eventCard);
                        }
                        else
                        {
                            rightCards.Add(eventCard);
                        }
                    }

                    for (int c = 0; c < leftCards.Count; c++)
                    {
                        var row = new EventRowViewModel
                        {
                            LeftCard = leftCards[c],
                        };

                        if (c !< rightCards.Count - 1)
                        {
                            row.RightCard = rightCards[c];
                        }
                        eventListViewModel.Rows.Add(row);
                    }

                }
                else
                {
                    eventListViewModel.Rows.Add(new EventRowViewModel
                    {
                        LeftCard = new EventItemViewModel
                        {
                            EventTitle = "No Events",
                            EventDescription = "Come back soon to see upcoming events or follow Edible Mami on social media for more up-to-date information!"
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }

            return View(eventListViewModel);
        }

        private EventTime ParseEventDate(DateTime eventDate)
        {
            return null;
        }
    }
}