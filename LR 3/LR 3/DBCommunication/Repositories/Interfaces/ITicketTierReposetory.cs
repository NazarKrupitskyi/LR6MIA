using LR_3.DTO.Event;
using LR_3.DTO.TicketTier;
using LR_3.Models;

namespace LR_3.DBCommunication.Repositories.Interfaces
{
    public interface ITicketTierReposetory
    {
        Task CreateAsync(TicketTier ticketTier);
        Task<List<TicketTier>> GetAllAsync();
        Task<TicketTier?> GetByIdAsync(string id);
        Task UpdateAsync(TicketTier ticketTier);
        Task DeleteAsync(string id);
        Task<List<TicketTier>> GetByEventIdAsync(string eventId);
    }
}
