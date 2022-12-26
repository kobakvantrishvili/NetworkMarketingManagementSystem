using NetworkMarketingManagementSystem.Domain;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction
{
    public interface ISaleRepository
    {
        Task<int> CreateAsync(Sale sale);
        Task<Sale> ReadNoTrackingAsync(int Id);
        Task<List<Sale>> ReadNoTrackingAsync(Expression<Func<Sale, bool>> predicate);
        Task<List<Sale>> ReadAllNoTracking();
        Task<bool> Exists(Expression<Func<Sale, bool>> predicate);
    }
}
