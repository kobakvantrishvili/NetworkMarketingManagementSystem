using NetworkMarketingManagementSystem.Domain.ForDistributor;
using NetworkMarketingManagementSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Models
{
    public class SaleServiceModel
    {
        
        /* Used For  Both Read and Create Operations*/
        public DateTime SaleDate { get; set; }
        public int? DistributorId { get; set; }

        /* Explicitly For Read Operations */
        public int Id { get; set; }
        public string? DistributorFirstName { get; set; }
        public string? DistributorLastName { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<ProductSoldDetails>? ProductsSoldDetails { get; set; }
        public ICollection<int> ProductIds { get; set; }
        public ICollection<string> ProductNames { get; set; }
        public ICollection<string> ProductCodes { get; set; }

        /* Explicitly For Create Operations */
        public ICollection<SoldProductInfo>? SoldProductsInfo { get; set; }
        


        public class SoldProductInfo
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        public class ProductSoldDetails
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal TotalProductPrice { get; set; }
        }
    }
}
