using MongoDB.Driver;
using PersonalPlanner.Models;

namespace PersonalPlanner.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        public IMongoCollection<Goal> Goal => _database.GetCollection<Goal>("Goals");

        public IMongoCollection<Budget> Budgets => _database.GetCollection<Budget>("Budgets");

        public IMongoCollection<TodoTask> Todo => _database.GetCollection<TodoTask>("Todos");
    }
}