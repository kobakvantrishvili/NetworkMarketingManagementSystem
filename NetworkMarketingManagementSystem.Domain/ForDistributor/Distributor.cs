using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Domain.ForDistributor
{
    public sealed class Distributor
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool Sex { get; set; }
        public byte[]? Image { get; set; }
        public int? ReferredBy { get; set; }
        public int References { get; set; }
        public int Level { get; set; }

        // Navigation Properties
        public ICollection<Sale>? Sales { get; set; }
        public IdentityDocument IdentityDocument { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public AddressInfo AddressInfo { get; set; }
    }
}
