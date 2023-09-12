using JwtAuthMicroservice.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace JwtAuthMicroservice.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);

            _users = database.GetCollection<User>("users");
        }

        public List<User> Get() => _users.Find(user => true).ToList();

        public User Get(string id) => _users.Find<User>(user => user.Id == id).FirstOrDefault();
        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }
        public User GetByUsername(string username) => _users.Find<User>(user => user.Username == username).FirstOrDefault();


    }
}
