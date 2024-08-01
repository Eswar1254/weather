using MongoDB.Driver;
using weather.Data;
using weather;

namespace weather.Data
{
    public class ApplicationDbContext
    {
        private readonly IMongoDatabase _database;

        public ApplicationDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<ApplicationUser> Users => _database.GetCollection<ApplicationUser>("Users");

        // You can add other collections here as needed, e.g.,
        // public IMongoCollection<TestRecord> TestRecords => _database.GetCollection<TestRecord>("TestRecords");
    }
}