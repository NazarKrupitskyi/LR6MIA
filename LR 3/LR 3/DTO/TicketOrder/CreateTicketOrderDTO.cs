using LR_3.Models;
using System.ComponentModel.DataAnnotations;

namespace LR_3.DTO.TicketOrder
{
    public class CreateTicketOrderDTO
    {
        [Required]
        public string EventId { get; set; }
        [Required]
        public string TierId { get; set; }
        [Required]
        public string customer_email { get; set; }
        [Required]
        public string customer_phone { get; set; }
        [Required]
        public int RequestedSeats { get; set; }
        public string? PromoCode { get; set; }

    }
}
