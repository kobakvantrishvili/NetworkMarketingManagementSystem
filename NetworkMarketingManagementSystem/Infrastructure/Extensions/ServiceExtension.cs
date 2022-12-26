using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetworkMarketingManagementSystem.Application.Abstraction;
using NetworkMarketingManagementSystem.Application.Implementation;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Models;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Repositories.Abstraction;
using NetworkMarketingManagementSystem.Persistence.MongoDb.Repositories.Implementation;
using NetworkMarketingManagementSystem.Persistence.Repositories.Abstraction;
using NetworkMarketingManagementSystem.Persistence.Repositories.Implementation;

namespace NetworkMarketingManagementSystem.Infrastructure.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServiceExtensions(this IServiceCollection services)
        {
            services.AddTransient<IDistributorRepository, DistributorRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISaleRepository, SaleRepository>();
            services.AddTransient<IBonusRepository, BonusRepository>();
            services.AddScoped<IDistributorService, DistributorService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IBonusService, BonusService>();


            services.AddSingleton<IBonusStoreDatabaseSettings>(sp => 
            sp.GetRequiredService<IOptions<BonusStoreDatabaseSettings>>().Value);

            
        }
    }
}
