using LR_3.DTO.TicketOrder;
using LR_3.Models;

namespace LR_3.DBCommunication.Repositories.Interfaces
{
    public interface ITicketOrderReposetory
    {
        Task CreateAsync(TicketOrder ticketOrder);
        Task<List<TicketOrder>> GetAllAsync();
        Task<TicketOrder?> GetByIdAsync(string id);
        Task UpdateAsync(TicketOrder ticketOrder);
        Task DeleteAsync(string id);
    }
}
