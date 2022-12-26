namespace NetworkMarketingManagementSystem.Models.DTOs
{
    public class BonusDTO
    {
        public string Id { get; set; }
        public int DistributorId { get; set; }
        public string DistributorFirstName { get; set; }
        public string DistributorLastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }
        public decimal Amount { get; set; }
    }
}
