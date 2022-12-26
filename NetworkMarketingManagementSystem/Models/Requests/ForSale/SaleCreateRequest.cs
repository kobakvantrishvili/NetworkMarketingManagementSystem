

namespace NetworkMarketingManagementSystem.Models.Requests.ForSale
{
    public class SaleCreateRequest
    {
        public int DistributorId { get; set; }
        public DateTime SaleDate { get; set; }
        public ICollection<SoldProductInfo> SoldProductsInfo { get; set; }
    }

    public class SoldProductInfo
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}

