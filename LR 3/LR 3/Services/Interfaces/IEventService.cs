using LR_3.DTO.Event;
using LR_3.Models;

namespace LR_3.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventResponseDto>> GetAllEventsAsync();
        Task<EventResponseDto?> GetEventByIdAsync(string id);
        Task<EventResponseDto> CreateEventAsync(CreateEventDTO dto);
        Task<EventResponseDto?> UpdateEventAsync(string id, UpdateEventDTO dto);
        Task<bool> DeleteEventAsync(string id);
    }
}
