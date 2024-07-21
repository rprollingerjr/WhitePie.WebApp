using WhitePie.Models;

namespace WhitePie.WebApp.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(string id);
    }
}
