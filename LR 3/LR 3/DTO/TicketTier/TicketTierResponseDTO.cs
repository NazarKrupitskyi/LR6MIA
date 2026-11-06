using LR_3.Models;

namespace LR_3.DTO.TicketTier
{
    public class TicketTierResponseDTO
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public TicketCategory Category { get; set; }
        public int Price { get; set; }
        public int AvailableSeats { get; set; }
    }
}
