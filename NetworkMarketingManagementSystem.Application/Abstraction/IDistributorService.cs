using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Application.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Abstraction
{
    public interface IDistributorService
    {
        Task<(Status, int?)> CreateDistributorAsync(DistributorServiceModel distributor);
        Task<Status> UpdateDistributorAsync(DistributorServiceModel distributor);
        Task<Status> DeleteDistributorAsync(int Id);
        Task<(Status, DistributorServiceModel?)> ReadDistributorAsync(int Id);
        Task<(Status, List<DistributorServiceModel>?)> ReadAllDistributorAsync();
    }
}
