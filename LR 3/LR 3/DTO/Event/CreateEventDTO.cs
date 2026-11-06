using LR_3.Models;
using System.ComponentModel.DataAnnotations;
namespace LR_3.DTO.Event
{
    public class CreateEventDTO
    {
        [Required]
        public string Title { get; set; } = "";
        public string Description { get; set; }
        [Required]
        public string location { get; set; }
        [Required]
        public DateTime startDate { get; set; }
    }
}
