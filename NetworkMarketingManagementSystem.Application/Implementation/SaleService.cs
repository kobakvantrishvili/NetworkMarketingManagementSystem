using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Application.Models.Enums;
using NetworkMarketingManagementSystem.Domain;
using NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction;
using Mapster;
using NetworkMarketingManagementSystem.Persistence.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NetworkMarketingManagementSystem.Application.Models.SaleServiceModel;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using Microsoft.EntityFrameworkCore;

namespace NetworkMarketingManagementSystem.Application.Implementation
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IDistributorRepository _distributorRepository;
        private readonly IProductRepository _productRepository;

        public SaleService(ISaleRepository saleRepository, IProductRepository productRepository, IDistributorRepository distributorRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _distributorRepository = distributorRepository;
        }

        public async Task<(Status, int?)> CreateSaleAsync(SaleServiceModel sale)
        {
            // Check if such distributor Exists
            if (!await _distributorRepository.Exists(x => x.Id == sale.DistributorId)) 
                return (Status.BadRequest, null);

            // Check if such products exist
            var products = await _productRepository.ReadNoTrackingAsync(x => sale.SoldProductsInfo.Select(a => a.ProductId).ToList().Contains(x.Id));
            if(products.Count != sale.SoldProductsInfo.Count) 
                return (Status.BadRequest, null);


            var saleProducts = new List<SaleProduct>();
            foreach (var soldProduct in sale.SoldProductsInfo)
            {
                saleProducts.Add(new SaleProduct
                {
                    ProductId = soldProduct.ProductId,
                    ProductCode = products.First(x => x.Id == soldProduct.ProductId).Code,
                    ProductName = products.First(x => x.Id == soldProduct.ProductId).Name,
                    ProductSinglePrice = products.First(x => x.Id == soldProduct.ProductId).Price,
                    ProductTotalPrice = soldProduct.Quantity * products.First(x => x.Id == soldProduct.ProductId).Price
                });
            }

            var newSale = new Sale()
            {
                DistributorId = sale.DistributorId,
                SaleDate = sale.SaleDate,
                TotalPrice = saleProducts.Select(x => x.ProductTotalPrice).Sum(),
                //TotalPrice = sale.SoldProductsInfo.Select(x => x.Quantity * products.First(a => a.Id == x.ProductId).Price).Sum(),
                SaleProducts = saleProducts
            };

            return (Status.Created, await _saleRepository.CreateAsync(newSale));
        }

        public async Task<(Status, SaleServiceModel?)> ReadSaleAsync(int Id)
        {
            // Check if Sale exists
            var sale = await _saleRepository.ReadNoTrackingAsync(Id);
            if(sale is null)
                return (Status.NotFound, null);

            var products = await _productRepository.ReadNoTrackingAsync(x => sale.SaleProducts.Select(a => a.ProductId).ToList().Contains(x.Id));

            var soldproductsdetails = new List<ProductSoldDetails>();
            foreach (var product in products)
            {
                soldproductsdetails.Add(new ProductSoldDetails
                {
                    Id = product.Id,
                    Code = sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductCode).First(),
                    Name = sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductName).First(),
                    Price = sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductSinglePrice).First(),
                    TotalProductPrice = sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductTotalPrice).First(),
                    Quantity = (int)((sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductTotalPrice).First())
                                    / sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductSinglePrice).First()),
                });
            }

            var SaleServiceModel = new SaleServiceModel
            {
                Id = sale.Id,
                DistributorId = sale.DistributorId,
                SaleDate = sale.SaleDate,
                TotalPrice = sale.TotalPrice,
                ProductsSoldDetails = soldproductsdetails
            };

            return (Status.Success, SaleServiceModel);
        }

        public async Task<(Status, IQueryable<SaleServiceModel>?)> ReadAllSaleAsync()
        {
            var sales = await _saleRepository.ReadAllNoTracking();

            var productIds = sales.SelectMany(x => x.SaleProducts.Select(x => x.ProductId));
            var products = await _productRepository.ReadNoTrackingAsync(x => productIds.Contains(x.Id));

            List<SaleServiceModel> saleServiceModels = new List<SaleServiceModel>();

            foreach(var sale in sales)
            {
                var productsInSale = products.Where(x => sale.SaleProducts.Select(a => a.ProductId).ToList().Contains(x.Id));

                var soldproductsdetails = new List<ProductSoldDetails>();
                foreach (var product in productsInSale)
                {
                    soldproductsdetails.Add(new ProductSoldDetails
                    {
                        Id = product.Id,
                        Code = sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductCode).First(),
                        Name = sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductName).First(),
                        Price = sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductSinglePrice).First(),
                        TotalProductPrice = sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductTotalPrice).First(),
                        Quantity = (int)((sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductTotalPrice).First())
                                    / sale.SaleProducts.Where(x => (x.ProductId == product.Id) && (x.SaleId == sale.Id)).Select(x => x.ProductSinglePrice).First()),
                    });
                }

                var SaleServiceModel = new SaleServiceModel
                {
                    Id = sale.Id,
                    DistributorId = sale.DistributorId,
                    SaleDate = sale.SaleDate,
                    TotalPrice = sale.TotalPrice,
                    ProductsSoldDetails = soldproductsdetails
                };

                saleServiceModels.Add(SaleServiceModel);
            }

            return (Status.Success, saleServiceModels.AsQueryable());
        }
    }
}
