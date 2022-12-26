using NetworkMarketingManagementSystem.Domain;
using static NetworkMarketingManagementSystem.Application.Models.SaleServiceModel;

namespace NetworkMarketingManagementSystem.Models.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public int DistributorId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<ProductSoldDetails>? ProductsSoldDetails { get; set; }
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
