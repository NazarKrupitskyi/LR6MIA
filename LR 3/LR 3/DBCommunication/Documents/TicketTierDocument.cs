using LR_3.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LR_3.DBCommunication.Documents
{
    public class TicketTierDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("eventid")]
        public string EventId { get; set; }
        [BsonElement("category")]
        public TicketCategory Category { get; set; }
        [BsonElement("price")]
        public int Price { get; set; }
        [BsonElement("availableseats")]
        public int AvailableSeats { get; set; }
    }
}
