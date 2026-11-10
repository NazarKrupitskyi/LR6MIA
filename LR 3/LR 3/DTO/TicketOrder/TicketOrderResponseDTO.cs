using LR_3.Models;

namespace LR_3.DTO.TicketOrder
{
    public class TicketOrderResponseDTO
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

    public class DigitalTicketResponseDto
    {
        public string Barcode { get; set; } = string.Empty;
        public bool IsValid { get; set; }
    }
}
