using LR_3.Controllers;
using LR_3.DBCommunication.Documents;
using LR_3.DTO.Event;
using LR_3.ErrorProccesing;
using LR_3.Models;
using LR_3.Services;
using LR_3.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.IO.File;

namespace LR_3.Controllers
{


    public class PatchEventDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? location { get; set; }
        public DateTime startDate { get; set; }
        public bool? IsUpcoming { get; set; }

    }
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponseDto>>> GetAll()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponseDto>> GetById(string id)
        {
            var eventDto = await _eventService.GetEventByIdAsync(id);

            if (eventDto == null)
                return NotFound($"Подію з ID={id} не знайдено.");

            return Ok(eventDto);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<EventResponseDto>> Create([FromBody] CreateEventDTO dto)
        {
            try
            {
                var createdEventDto = await _eventService.CreateEventAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdEventDto.Id }, createdEventDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] UpdateEventDTO patchDto)
        {
            var updatedEvent = await _eventService.UpdateEventAsync(id, patchDto);

            if (updatedEvent == null)
                return NotFound($"Подію з ID={id} не знайдено.");

            return NoContent();
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(string id, [FromBody] UpdateEventDTO eventDto)
        {
            var existingEvent = await _eventService.GetEventByIdAsync(id); 
            if (existingEvent == null)
            {
                return NotFound(); 
            }

            await _eventService.UpdateEventAsync(id, eventDto);

            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var isDeleted = await _eventService.DeleteEventAsync(id);

            if (!isDeleted)
                return NotFound($"Подію з ID={id} не знайдено.");

   

            return NoContent();
        }
    }
}
