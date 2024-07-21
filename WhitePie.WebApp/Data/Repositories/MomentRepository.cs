using WhitePie.WebApp.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Driver;
using WhitePie.Models;

namespace WhitePie.WebApp.Data.Repositories
{
    public class MomentRepository : IMomentRepository
    {
        private readonly IMongoCollection<Moment> _moments;
        private readonly IGridFSBucket _bucket;

        public MomentRepository(MongoDbContext context)
        {
            _bucket = context.Bucket;
            _moments = context.Moments;
        }

        public async Task<IEnumerable<Moment>> GetAllMomentsAsync() =>
            await _moments.Find(_ => true).ToListAsync();

        public async Task<byte[]> GetFileBytesAsync(string fileId)
        {
            var objectId = ObjectId.Parse(fileId);
            return await _bucket.DownloadAsBytesAsync(objectId);
        }
    }
}
