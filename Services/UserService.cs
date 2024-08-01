using MongoDB.Driver;
using weather.Data;

namespace weather.Services
{

    // Services/UserService.cs



    public class UserService
    {
        private readonly IMongoCollection<ApplicationUser> _usersCollection;

        public UserService(IMongoDatabase mongoDatabase)
        {
            _usersCollection = mongoDatabase.GetCollection<ApplicationUser>("TestCollections");
        }

        public async Task AddUserAsync(ApplicationUser user)
        {
            await _usersCollection.InsertOneAsync(user);
        }
    }
}