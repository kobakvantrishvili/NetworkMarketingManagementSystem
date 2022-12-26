using NetworkMarketingManagementSystem.Domain.ForDistributor;
using System.Linq.Expressions;

namespace NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction
{
    public interface IDistributorRepository
    {
        Task<int> CreateAsync(Distributor distributor);
        Task UpdateAsync(Distributor distributor);
        Task DeleteAsync(int Id);
        Task<Distributor> ReadAsync(int Id);
        Task<List<Distributor>> ReadAsync(Expression<Func<Distributor, bool>> predicate);
        Task<Distributor> ReadNoTrackingAsync(int Id);
        Task<List<Distributor>> ReadNoTrackingAsync(Expression<Func<Distributor, bool>> predicate);
        Task<List<Distributor>> ReadAllNoTrackingAsync();
        Task<bool> Exists(Expression<Func<Distributor, bool>> predicate);
        void Detach(Distributor distributor);
        void Attach(Distributor distributor);
    }
}
