using LR_3.Models;
using System.ComponentModel.DataAnnotations;
namespace LR_3.DTO.TicketTier
{
    public class CreateTicketTierDTO
    {
        [Required]
        public string EventId { get; set; }
        [Required]
        public TicketCategory Category { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int AvailableSeats { get; set; }
    }
}
