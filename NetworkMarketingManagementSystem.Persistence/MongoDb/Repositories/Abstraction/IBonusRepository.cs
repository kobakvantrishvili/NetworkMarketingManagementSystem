using NetworkMarketingManagementSystem.Persistence.MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Persistence.MongoDb.Repositories.Abstraction
{
    public interface IBonusRepository
    {
        Task CreateAsync(List<Bonus> bonuses);
        Task<List<Bonus>> ReadAsync(Expression<Func<Bonus, bool>>? expression = null);
        Task DeleteAsync(Expression<Func<Bonus, bool>>? expression = null);
    }
}
