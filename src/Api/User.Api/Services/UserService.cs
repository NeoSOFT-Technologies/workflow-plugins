using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using User.Api.Models;

namespace User.Api.Services
{
    public class UserService
    {
        private readonly IMongoCollection<ApplicationUser> _users;

        public UserService(IUserStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<ApplicationUser>(settings.UsersCollectionName);
        }

        public List<ApplicationUser> Get() =>
            _users.Find(user => true).ToList();

        public ApplicationUser Get(string id) =>
            _users.Find<ApplicationUser>(user => user.Id == id).FirstOrDefault();

        public ApplicationUser Create(ApplicationUser user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, ApplicationUser userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(ApplicationUser userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}
