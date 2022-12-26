using NetworkMarketingManagementSystem.Domain;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using System.Linq.Expressions;

namespace NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction
{
    public interface IProductRepository
    {
        Task<int> CreateAsync(Product product);
        Task<Product> ReadAsync(int Id);
        Task<Product> ReadNoTrackingAsync(int Id);
        Task<List<Product>> ReadNoTrackingAsync(Expression<Func<Product, bool>> predicate);
        Task<List<Product>> ReadAllNoTrackingAsync();
        Task UpdateAsync(Product product);
        Task DeleteAsync(int Id);
        Task<bool> Exists(Expression<Func<Product, bool>> predicate);
    }
}
