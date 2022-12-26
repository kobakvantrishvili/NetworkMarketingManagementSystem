using NetworkMarketingManagementSystem.Application.Models.Enums;
using NetworkMarketingManagementSystem.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Abstraction
{
    public interface ISaleService
    {
        Task<(Status, int?)> CreateSaleAsync(SaleServiceModel sale);
        //Task<Status> UpdateSaleAsync(SaleServiceModel sale);
        //Task<Status> DeleteSaleAsync(int Id);
        Task<(Status, SaleServiceModel?)> ReadSaleAsync(int Id);
        Task<(Status, IQueryable<SaleServiceModel>?)> ReadAllSaleAsync();
    }
}
