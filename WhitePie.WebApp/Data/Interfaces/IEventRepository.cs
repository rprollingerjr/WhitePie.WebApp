using WhitePie.Models;

namespace WhitePie.WebApp.Data.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(string id);
    }
}
