using LR_3.Models;

namespace LR_3.DTO.Event
{
    public class EventResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public EventStatus Status { get; set; } 
    }
}
