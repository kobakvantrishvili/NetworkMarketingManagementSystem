using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Application.Models.Enums;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Abstraction
{
    public interface IBonusService
    {
        Task<Status> CreateBonusesAsync(DateTime startDate, DateTime endDate);
        Task<(Status, BonusServiceModel)> ReadBonusAsync(string Id);
        Task<(Status, IQueryable<BonusServiceModel>)> ReadAllBonusesAsync();
        Task<Status> DeleteBonusesAsync(DateTime startDate, DateTime endDate);
    }
}
