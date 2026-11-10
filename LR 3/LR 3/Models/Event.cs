using System.ComponentModel.DataAnnotations;

namespace LR_3.Models
{
    public class Event
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле 'Title' є обов'язковим.")]
        [MinLength(3, ErrorMessage = "Назва має містити щонайменше 3 символи.")]
        [MaxLength(40, ErrorMessage = "Назва не може перевищувати 40 символів.")]
        public string Title { get; set; } = "";
        public string Description { get; set; }
        public string location { get; set; }
        public DateTime startDate { get; set; }
        public EventStatus status { get; set; } = EventStatus.Published;
    }
    
    public enum EventStatus 
    {
       Published,
       Postponed,
       Cancelled
    };
}
