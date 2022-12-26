using Microsoft.EntityFrameworkCore;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using NetworkMarketingManagementSystem.Persistence.MSSQL;
using NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction;
using System.Linq.Expressions;

namespace NetworkMarketingManagementSystem.Persistence.Repositories.Implementation
{
    public class DistributorRepository : IDistributorRepository
    {
        private readonly AppDbContext _appDbContext;

        public DistributorRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> CreateAsync(Distributor distributor)
        {
            await _appDbContext.Distributors.AddAsync(distributor);
            await _appDbContext.SaveChangesAsync();
            return distributor.Id;
        }

        public async Task UpdateAsync(Distributor distributor)
        {
            _appDbContext.Distributors.Update(distributor);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var entity = await ReadAsync(Id);
            _appDbContext.Distributors.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Distributor> ReadAsync(int Id)
        {
            return await _appDbContext.Distributors
                .Include(x => x.IdentityDocument)//include does left join
                .Include(x => x.ContactInfo)    
                .Include(x => x.AddressInfo)
                .SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Distributor>> ReadAsync(Expression<Func<Distributor, bool>> predicate)
        {
            return await _appDbContext.Distributors
                .Include(x => x.IdentityDocument)
                .Include(x => x.ContactInfo)
                .Include(x => x.AddressInfo)
                .Where(predicate).ToListAsync();
        }

        public async Task<Distributor> ReadNoTrackingAsync(int Id)
        {
            return await _appDbContext.Distributors
                .Include(x => x.IdentityDocument)
                .Include(x => x.ContactInfo)
                .Include(x => x.AddressInfo).AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Distributor>> ReadNoTrackingAsync(Expression<Func<Distributor, bool>> predicate)
        {
            return await _appDbContext.Distributors
                .Include(x => x.IdentityDocument)
                .Include(x => x.ContactInfo)
                .Include(x => x.AddressInfo)
                .Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<List<Distributor>> ReadAllNoTrackingAsync()
        {
            return await _appDbContext.Distributors.Include(x => x.IdentityDocument).Include(x => x.ContactInfo).Include(x => x.AddressInfo).AsNoTracking().ToListAsync();
        }

        public async Task<bool> Exists(Expression<Func<Distributor, bool>> predicate)
        {
            return await _appDbContext.Distributors.AnyAsync(predicate);
        }

        public void Detach(Distributor distributor)
        {
            _appDbContext.Entry(distributor).State = EntityState.Detached;
        }

        public void Attach(Distributor distributor)
        {
            _appDbContext.Attach(distributor);
            _appDbContext.Entry(distributor).State = EntityState.Modified;
        }


    }
}
