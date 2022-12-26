using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Domain
{
    public sealed class Product
    {
        public int Id { get; set;  }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<SaleProduct> SaleProducts { get; set; }
    }
}
