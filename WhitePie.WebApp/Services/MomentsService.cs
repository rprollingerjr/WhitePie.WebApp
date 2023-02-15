using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System.Buffers.Text;
using WhitePie.Models;
using WhitePie.Models.Settings;

namespace WhitePie.Services
{
    public class MomentsService
    {
        private readonly IMongoCollection<Moment> _momentsCollection;
        private readonly IMongoDatabase _database;
        public MomentsService(
            IOptions<MomentsDatabaseSettings> momentsDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                momentsDatabaseSettings.Value.ConnectionString);

            _database = mongoClient.GetDatabase(
                momentsDatabaseSettings.Value.DatabaseName);

            _momentsCollection = _database.GetCollection<Moment>(
                momentsDatabaseSettings.Value.MomentsCollectionName);
        }

        public async Task<List<Moment>> GetAsync() =>
            await _momentsCollection.Find(_ => true).ToListAsync();

        public async Task<Moment> GetAsync(string id) =>
            await _momentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<string> GetMomentBytesAsync(string id)
        {
            var bucket = new GridFSBucket(_database, new GridFSBucketOptions
            {
                BucketName = "momentfiles",
                ReadPreference = ReadPreference.Primary
            });

            var fileObjectId = ObjectId.Parse(id);

            var file = await bucket.DownloadAsBytesAsync(fileObjectId);

            return Convert.ToBase64String(file);
        }
    }
}
