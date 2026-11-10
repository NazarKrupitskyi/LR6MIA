using LR_3.Models;
using System.ComponentModel.DataAnnotations;

namespace LR_3.DTO.TicketTier
{
    public class UpdateTicketTierDTO
    {
        public int Price { get; set; }
        public int AvailableSeats { get; set; }
    }
}
