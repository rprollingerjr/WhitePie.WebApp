using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using WhitePie.Models;
using MongoDB.Driver.GridFS;

namespace WhitePie.WebApp.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly GridFSBucket _momentBucket;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
            _momentBucket = new GridFSBucket(_database, new GridFSBucketOptions
            {
                BucketName = "momentfiles",
                ReadPreference = ReadPreference.Primary 
            });
        }

        public IMongoCollection<Event> Events => _database.GetCollection<Event>("Events");

        public IMongoCollection<Moment> Moments => _database.GetCollection<Moment>("Moments");

        public GridFSBucket Bucket => _momentBucket;
    }
}
