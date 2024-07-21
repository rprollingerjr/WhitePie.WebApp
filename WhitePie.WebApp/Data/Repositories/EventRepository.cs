using MongoDB.Driver;
using WhitePie.Models;
using WhitePie.WebApp.Data.Interfaces;

namespace WhitePie.WebApp.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMongoCollection<Event> _events;

        public EventRepository(MongoDbContext context) 
        {
            _events = context.Events;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync() =>
           await _events.Find(_ => true).ToListAsync();

        public async Task<Event> GetEventByIdAsync(string id) =>
            await _events.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
