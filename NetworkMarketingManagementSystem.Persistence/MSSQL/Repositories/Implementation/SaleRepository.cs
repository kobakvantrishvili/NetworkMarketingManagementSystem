using Microsoft.EntityFrameworkCore;
using NetworkMarketingManagementSystem.Domain;
using NetworkMarketingManagementSystem.Persistence.MSSQL;
using NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction;
using System.Linq.Expressions;

namespace NetworkMarketingManagementSystem.Persistence.Repositories.Implementation
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _appDbContext;

        public SaleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CreateAsync(Sale sale)
        {
            await _appDbContext.Sales.AddAsync(sale);
            await _appDbContext.SaveChangesAsync();
            return sale.Id;
        }

        /*
        public async Task UpdateAsync(Sale sale)
        {
            var entity = _appDbContext.Sales.Update(sale);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var entity = await ReadAsync(Id);
            _appDbContext.Sales.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }*/

        public async Task<Sale> ReadNoTrackingAsync(int Id)
        {
            return await _appDbContext.Sales
                .Include(x => x.SaleProducts)
                .ThenInclude(x => x.Product)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Sale>> ReadNoTrackingAsync(Expression<Func<Sale, bool>> predicate)
        {
            return await _appDbContext.Sales
                .Include(x => x.SaleProducts)
                .ThenInclude(x => x.Product)
                .Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<List<Sale>> ReadAllNoTracking()
        {
            return await _appDbContext.Sales
                .Include(x => x.SaleProducts)
                .ThenInclude(x => x.Product)
                .AsNoTracking().ToListAsync();
        }

        public async Task<bool> Exists(Expression<Func<Sale, bool>> predicate)
        {
            return await _appDbContext.Sales.AnyAsync(predicate);
        }
    }
}
