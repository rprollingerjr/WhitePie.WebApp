using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WhitePie.Models.Settings;
using WhitePie.Models;

namespace WhitePie.Services
{
    public class EventsService
    {
        private readonly IMongoCollection<Event> _eventsCollection;

        public EventsService(
            IOptions<WhitePieDatabaseSettings> eventsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                eventsDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                eventsDatabaseSettings.Value.DatabaseName);

            _eventsCollection = mongoDatabase.GetCollection<Event>(
                eventsDatabaseSettings.Value.EventsCollectionName);
        }

        public async Task<List<Event>> GetAsync() =>
            await _eventsCollection.Find(_ => true).ToListAsync();

        public async Task<Event> GetAsync(string id) =>
            await _eventsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
}
