using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Application.Models.Enums;
using NetworkMarketingManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Abstraction
{
    public interface IProductService
    {
        Task<(Status, int?)> CreateProductAsync(ProductServiceModel product);
        Task<(Status, ProductServiceModel?)> ReadProductAsync(int Id);
        Task<(Status, List<ProductServiceModel>?)> ReadAllProductAsync();
        Task<Status> UpdateProductAsync(ProductServiceModel product);
        Task<Status> DeleteProductAsync(int Id);
    }
}
