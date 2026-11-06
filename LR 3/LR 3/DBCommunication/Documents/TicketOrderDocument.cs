using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using LR_3.Models;

namespace LR_3.DBCommunication.Documents
{
    public class TicketOrderDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("eventid")]
        public string EventId { get; set; }
        [BsonElement("tierid")]
        public string TierId { get; set; }
        [BsonElement("customeremail")]
        public string customer_email { get; set; }
        [BsonElement("customerphone")]
        public string customer_phone { get; set; }
        [BsonElement("requestedseats")]
        public int RequestedSeats { get; set; }
        [BsonElement("promocode")]
        public string? PromoCode { get; set; }
        [BsonElement("status")]
        public Status status { get; set; }
        [BsonElement("creratetime")]
        public DateTime CreateTime { get; set; }
        [BsonElement("totalamount")]
        public decimal TotalAmount { get; set; }
        [BsonElement("refundamount")]
        public decimal RefundAmount { get; set; }

        [BsonElement("tickets")]
        public List<DigitalTicket> Tickets { get; set; }
    }
}
