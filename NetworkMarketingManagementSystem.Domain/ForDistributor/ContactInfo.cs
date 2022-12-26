using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Domain.ForDistributor
{
    public sealed record ContactInfo
    {
        public int DistributorId { get; set; }
        public int Type { get; set; }
        public string Contact { get; set; }

        public Distributor? Distributor { get; set; }
    }
}
