using MongoDB.Driver;
using PersonalPlanner.Models;
using Calendar.Data;

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

        public IMongoCollection<DayEvent> DayEvent => _database.GetCollection<DayEvent>("DayEvent");

        public IMongoCollection<HourEvent> HourEvent => _database.GetCollection<HourEvent>("HourEvent");

        public IMongoCollection<TodoTask> Todo => _database.GetCollection<TodoTask>("Todos");
    }
}