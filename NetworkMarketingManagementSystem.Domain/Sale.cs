using NetworkMarketingManagementSystem.Domain.ForDistributor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Domain
{
    public sealed class Sale
    {
        public int Id { get; set; }
        public int? DistributorId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalPrice { get; set; }

        public Distributor Distributor { get; set; }
        public ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
