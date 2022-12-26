using Mapster;
using NetworkMarketingManagementSystem.Application.Models;
using NetworkMarketingManagementSystem.Domain;
using NetworkMarketingManagementSystem.Domain.ForDistributor;
using NetworkMarketingManagementSystem.Models.DTOs;
using NetworkMarketingManagementSystem.Models.Requests.ForDistributor;
using NetworkMarketingManagementSystem.Models.Requests.ForProduct;
using NetworkMarketingManagementSystem.Models.Requests.ForSale;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Models;

namespace NetworkMarketingManagementSystem.Infrastructure.Mapping
{
    public static class MapsterConfig
    {
        public static void RegisterMapsterMappings(this IServiceCollection services)
        {
            //Distributor
            TypeAdapterConfig<DistributorCreateRequest, DistributorServiceModel> //Maps directly since member variable names are the same
                .NewConfig();

            TypeAdapterConfig<DistributorUpdateRequest, DistributorServiceModel>
                .NewConfig();

            TypeAdapterConfig<DistributorServiceModel, DistributorDTO>
                .NewConfig();

            TypeAdapterConfig<DistributorServiceModel, Distributor>
                .NewConfig()
                .TwoWays();

            //Product
            TypeAdapterConfig<ProductCreateRequest, ProductServiceModel>
                .NewConfig();

            TypeAdapterConfig<ProductUpdateRequest, ProductServiceModel>
                .NewConfig();

            TypeAdapterConfig<ProductServiceModel, ProductDTO>
                .NewConfig();

            TypeAdapterConfig<ProductServiceModel, Product>
                .NewConfig()
                .TwoWays();

            //Sale
            TypeAdapterConfig<SaleCreateRequest, SaleServiceModel>
                .NewConfig();

            TypeAdapterConfig<SaleServiceModel, SaleDTO>
                .NewConfig();

            //Bonus
            TypeAdapterConfig<BonusServiceModel, BonusDTO>
                .NewConfig();

            TypeAdapterConfig<BonusServiceModel, Bonus>
                .NewConfig()
                .TwoWays();
        }
    }
}
