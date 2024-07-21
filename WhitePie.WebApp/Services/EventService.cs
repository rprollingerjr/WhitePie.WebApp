using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WhitePie.Models.Settings;
using WhitePie.Models;
using WhitePie.WebApp.Services.Interfaces;
using WhitePie.WebApp.Data.Interfaces;

namespace WhitePie.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _eventRepository.GetAllEventsAsync();
        }

        public async Task<Event> GetEventByIdAsync(string id)
        {
            return await _eventRepository.GetEventByIdAsync(id);
        }
    }
}
