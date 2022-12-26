using MongoDB.Driver;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Models;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Repositories.Abstraction;
using System.Linq.Expressions;

namespace NetworkMarketingManagementSystem.Persistence.MongoDb.Repositories.Implementation
{
    public class BonusRepository : IBonusRepository
    {
        private readonly IMongoCollection<Bonus> _bonuses;
        public BonusRepository(IBonusStoreDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _bonuses = database.GetCollection<Bonus>(settings.BonusCollectionName);
        }

        public async Task CreateAsync(List<Bonus> bonuses)
        {
            if (!bonuses.Any())
                return;
            await _bonuses.InsertManyAsync(bonuses);
        }

        public async Task<List<Bonus>> ReadAsync(Expression<Func<Bonus, bool>>? expression = null)
        {
            if (expression is null)
                return await (await _bonuses.FindAsync(x => true)).ToListAsync();
            
            return await (await _bonuses.FindAsync(expression)).ToListAsync();
        }

        public async Task DeleteAsync(Expression<Func<Bonus, bool>>? expression = null)
        {
            if (expression is null)
                await _bonuses.DeleteManyAsync(x => true);

            await _bonuses.DeleteManyAsync(expression);
        }
    }
}
