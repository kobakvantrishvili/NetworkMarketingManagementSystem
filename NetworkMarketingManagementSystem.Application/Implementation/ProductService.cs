using Mapster;
using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Application.Models.Enums;
using NetworkMarketingManagementSystem.Domain;
using NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction;
using NetworkMarketingManagementSystem.Persistence.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<(Status, int?)> CreateProductAsync(ProductServiceModel product)
        {
            if (product is null)
                return (Status.BadRequest, null);

            var productExists = await _productRepository.Exists(x => x.Code == product.Code);
            if (productExists)
                return (Status.Conflict, null);

            var productId = await _productRepository.CreateAsync(product.Adapt<Product>());
            return (Status.Created, productId);
        }

        public async Task<Status> UpdateProductAsync(ProductServiceModel product)
        {
            if (product is null)
                return Status.BadRequest;

            var prdct = await _productRepository.ReadAsync(product.Id);
            if(prdct is null)
                return Status.NotFound;
            if(prdct.Code != product.Code)
            {
                if(await _productRepository.Exists(x => x.Code == product.Code))
                    return Status.Conflict;
            }

            prdct.Code = product.Code;
            prdct.Name = product.Name;
            prdct.Price = product.Price;

            await _productRepository.UpdateAsync(prdct);
            return Status.Success;
        }

        public async Task<Status> DeleteProductAsync(int Id)
        {
            var productExists = await _productRepository.Exists(x => x.Id == Id);
            if (!productExists)
                return Status.NotFound;

            await _productRepository.DeleteAsync(Id);
            return Status.Success;
        }

        //public async Task<(Status, ProductServiceModel?)> ReadProductAsync(int Id)
        //{
        //    var product = await _productRepository.ReadAsync(Id);
        //    if (product is null)
        //        return (Status.NotFound, null);

        //    return (Status.Success, product.Adapt<ProductServiceModel>());
        //}

        public async Task<(Status, ProductServiceModel?)> ReadProductAsync(int Id) // No Tracking
        {
            var product = await _productRepository.ReadNoTrackingAsync(Id);
            if (product is null)
                return (Status.NotFound, null);

            return (Status.Success, product.Adapt<ProductServiceModel>());
        }

        public async Task<(Status, List<ProductServiceModel>?)> ReadAllProductAsync() // No Tracking
        {
            var productList = (await _productRepository.ReadAllNoTrackingAsync()).Adapt<List<ProductServiceModel>>();
            if (!productList.Any())
                return (Status.NotFound, null);

            return (Status.Success, productList);
        }
    }
}
