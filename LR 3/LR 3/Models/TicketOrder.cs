using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace LR_3.Models
{
    public class TicketOrder
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string TierId { get; set; }
        public string customer_email { get; set; }
        public string customer_phone { get; set; }
        public int RequestedSeats { get; set; }
        public string? PromoCode { get; set; }
        public Status status { get; set; }
        public DateTime CreateTime { get; set; }
        public decimal TotalAmount { get; set; } 
        public decimal RefundAmount { get; set; }
        
        public List<DigitalTicket> Tickets { get; set; } 
    }

    public enum Status 
    {
        Очікування_оплати,
        Оплачено,
        Відхилено,
        Повернено
    }

    public class DigitalTicket
    {
        public string Barcode { get; set; }
        public bool IsValid { get; set; }
    }
}
