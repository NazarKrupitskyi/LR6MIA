using LR_3.DTO.TicketTier;
using LR_3.Models;

namespace LR_3.Services.Interfaces
{
    public interface ITicketTierService
    {
        Task<IEnumerable<TicketTierResponseDTO>> GetAllTiersAsync();
        Task<TicketTierResponseDTO?> GetTierByIdAsync(string id);
        Task<TicketTierResponseDTO> CreateTierAsync(CreateTicketTierDTO dto);
        Task<TicketTierResponseDTO?> UpdateTierAsync(string id, UpdateTicketTierDTO dto);
        Task<bool> DeleteTierAsync(string id);
        Task<TicketStatisticDTO> GetTicketTierStatistic(string eventid);
    }
}
