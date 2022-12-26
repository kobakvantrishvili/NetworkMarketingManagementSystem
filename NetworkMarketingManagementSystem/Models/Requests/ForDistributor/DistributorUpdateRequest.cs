using NetworkMarketingManagementSystem.Models.DTOs;

namespace NetworkMarketingManagementSystem.Models.Requests.ForDistributor
{
    public class DistributorUpdateRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public bool Sex { get; set; }
        public byte[]? Image { get; set; }
        public int? ReferredBy { get; set; }

        public IdentityDocumentDTO IdentityDocument { get; set; }
        public ContactInfoDTO ContactInfo { get; set; }
        public AddressInfoDTO AddressInfo { get; set; }
    }
}
