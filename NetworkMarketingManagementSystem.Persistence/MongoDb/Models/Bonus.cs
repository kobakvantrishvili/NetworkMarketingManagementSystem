using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NetworkMarketingManagementSystem.Persistence.MongoDb.Models
{
    public class Bonus
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("distributorid")]
        public int DistributorId { get; set; }


        [BsonElement("distributorfirstname")]
        public string DistributorFirstName { get; set; }


        [BsonElement("distributorlastname")]
        public string DistributorLastName { get; set; }


        [BsonElement("saleids")]
        public List<int> SaleIds { get; set; }


        [BsonElement("startdate")]
        public DateTime StartDate { get; set; }


        [BsonElement("enddate")]
        public DateTime EndtDate { get; set; }


        [BsonElement("amount")]
        public decimal Amount { get; set; }
    }
}
