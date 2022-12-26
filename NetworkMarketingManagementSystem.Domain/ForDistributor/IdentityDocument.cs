using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Domain.ForDistributor
{
    public sealed record IdentityDocument
    {
        public int DistributorId { get; set; }
        public int Type { get; set; }
        public string? Series { get; set; }
        public string? Number { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ValidUntil { get; set; }
        public string PersonalNumber { get; set; }
        public string? IssuingAgency { get; set; }

        public Distributor? Distributor { get; set; }
    }
}
