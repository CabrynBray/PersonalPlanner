using MongoDB.Driver;
using PersonalPlanner.Data;
using PersonalPlanner.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalPlanner.Services
{
    public class BudgetService
    {
        private readonly IMongoCollection<Budget> _budgets;

        public BudgetService(MongoDBContext context)
        {
            _budgets = context.Budgets;
        }

        public async Task<List<Budget>> GetBudgetsAsync()
        {
            return await _budgets.Find(_ => true).ToListAsync();
        }

        public async Task<Budget> GetBudgetAsync(string id)
        {
            return await _budgets.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddBudgetAsync(Budget budget)
        {
            await _budgets.InsertOneAsync(budget);
        }

        public async Task UpdateBudgetAsync(Budget budget)
        {
            await _budgets.ReplaceOneAsync(b => b.Id == budget.Id, budget);
        }

        public async Task DeleteBudgetAsync(string id)
        {
            await _budgets.DeleteOneAsync(b => b.Id == id);
        }
    }
}
