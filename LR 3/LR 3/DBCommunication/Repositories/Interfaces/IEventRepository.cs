using LR_3.Models;
using LR_3.DTO.Event;
namespace LR_3.DBCommunication.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task CreateAsync(Event eventModel);
        Task<List<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(string id);
        Task UpdateAsync(Event eventModel);
        Task DeleteAsync(string id);
    }
}
