using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMarketingManagementSystem.Application.Models
{
    public class BonusServiceModel
    {
        public string Id { get; set; }
        public int DistributorId { get; set; }
        public string DistributorFirstName { get; set; }
        public string DistributorLastName { get; set; }
        public List<int> SaleIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
    }
}
