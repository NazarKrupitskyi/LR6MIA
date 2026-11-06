using AutoMapper;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.DTO.Event;
using LR_3.Models;
using LR_3.Services.Interfaces;

namespace LR_3.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper; 

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventResponseDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EventResponseDto>>(events);
        }

        public async Task<EventResponseDto?> GetEventByIdAsync(string id)
        {
            var ev = await _eventRepository.GetByIdAsync(id);
            if (ev == null) return null;
            return _mapper.Map<EventResponseDto>(ev);
        }

        public async Task<EventResponseDto> CreateEventAsync(CreateEventDTO dto)
        {
            var newEvent = _mapper.Map<Event>(dto);
            await _eventRepository.CreateAsync(newEvent);
            return _mapper.Map<EventResponseDto>(newEvent);
        }

        public async Task<EventResponseDto?> UpdateEventAsync(string id, UpdateEventDTO dto)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(id);
            if (existingEvent == null) return null;
            _mapper.Map(dto, existingEvent);
            await _eventRepository.UpdateAsync(existingEvent);
            return _mapper.Map<EventResponseDto>(existingEvent);
        }

        public async Task<bool> DeleteEventAsync(string id)
        {
            var existingEvent = await _eventRepository.GetByIdAsync(id);
            if (existingEvent == null) return false;
            await _eventRepository.DeleteAsync(id);
            return true;
        }
    }
}
