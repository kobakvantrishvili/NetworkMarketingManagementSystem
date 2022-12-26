using NetworkMarketingManagementSystem.Domain.ForDistributor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Models
{
    public class DistributorServiceModel
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

        public IdentityDocumentServiceModel IdentityDocument { get; set; }
        public ContactInfoServiceModel ContactInfo { get; set; }
        public AddressInfoServiceModel AddressInfo { get; set; }


    }
}
