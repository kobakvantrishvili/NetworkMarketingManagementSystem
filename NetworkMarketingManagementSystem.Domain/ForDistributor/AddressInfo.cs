using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Domain.ForDistributor
{
    public sealed record AddressInfo
    {

        public int DistributorId { get; set; }
        public int Type { get; set; }
        public string Address { get; set; }

        public Distributor? Distributor { get; set; }
    }
}
