using Microsoft.EntityFrameworkCore;
using NetworkMarketingManagementSystem.Domain;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using NetworkMarketingManagementSystem.Persistence.MSSQL;
using NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction;
using System.Linq.Expressions;

namespace NetworkMarketingManagementSystem.Persistence.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CreateAsync(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
            return product.Id;
        }

        public async Task UpdateAsync(Product product)
        {
            var entity = _appDbContext.Products.Update(product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var entity = await ReadAsync(Id);
            _appDbContext.Products.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Product> ReadAsync(int Id)
        {
            return await _appDbContext.Products.SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Product> ReadNoTrackingAsync(int Id)
        {
            return await _appDbContext.Products.AsNoTracking().SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Product>> ReadAllNoTrackingAsync()
        {
            return await _appDbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<List<Product>> ReadNoTrackingAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _appDbContext.Products.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<bool> Exists(Expression<Func<Product, bool>> predicate)
        {
            return await _appDbContext.Products.AnyAsync(predicate);
        }

    }
}
