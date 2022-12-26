using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Domain
{
    public sealed class SaleProduct
    {
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal ProductSinglePrice { get; set; }
        public decimal ProductTotalPrice { get; set; }

        public Sale Sale { get; set; }
        public Product Product { get; set; }
    }
}
