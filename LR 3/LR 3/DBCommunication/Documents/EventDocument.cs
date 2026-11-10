using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using LR_3.Models;
namespace LR_3.DBCommunication.Documents
{
    public class EventDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("title")]
        public string Title { get; set; } = "";
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("location")]
        public string location { get; set; }
        [BsonElement("startdate")]
        public DateTime startDate { get; set; }
        [BsonElement("status")]
        public EventStatus status { get; set; } = EventStatus.Published;
        
    }
   
    }
