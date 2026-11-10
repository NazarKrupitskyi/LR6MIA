using AutoMapper;
using LR_3.DBCommunication.Repositories;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.DTO.TicketTier;
using LR_3.Models;
using LR_3.Services.Interfaces;

namespace LR_3.Services
{
    public class TicketTierService : ITicketTierService
    {
        private readonly ITicketTierReposetory _tierRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public TicketTierService(ITicketTierReposetory tierRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _tierRepository = tierRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketTierResponseDTO>> GetAllTiersAsync()
        {
            var tiers = await _tierRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<TicketTierResponseDTO>>(tiers);
        }

        public async Task<TicketTierResponseDTO?> GetTierByIdAsync(string id)
        {
            var tier = await _tierRepository.GetByIdAsync(id);
            if (tier == null) return null;

            return _mapper.Map<TicketTierResponseDTO>(tier);
        }

        public async Task<TicketTierResponseDTO> CreateTierAsync(CreateTicketTierDTO dto)
        {
            var ev = await _eventRepository.GetByIdAsync(dto.EventId);
            if (ev == null)
            {
                throw new KeyNotFoundException("Подію не знайдено. Неможливо створити категорію квитків.");
            }

            var tier = _mapper.Map<TicketTier>(dto);
            await _tierRepository.CreateAsync(tier);

            return _mapper.Map<TicketTierResponseDTO>(tier);
        }

        public async Task<TicketTierResponseDTO?> UpdateTierAsync(string id, UpdateTicketTierDTO dto)
        {
            var existingTier = await _tierRepository.GetByIdAsync(id);
            if (existingTier == null) return null;

            _mapper.Map(dto, existingTier);
            await _tierRepository.UpdateAsync(existingTier);

            return _mapper.Map<TicketTierResponseDTO>(existingTier);
        }

        public async Task<bool> DeleteTierAsync(string id)
        {
            var existingTier = await _tierRepository.GetByIdAsync(id);
            if (existingTier == null) return false;

            await _tierRepository.DeleteAsync(id);
            return true;
        }

        public async Task<TicketStatisticDTO> GetTicketTierStatistic(string eventid)
        {
            if (string.IsNullOrWhiteSpace(eventid))
            {
                throw new ArgumentException("ID події не може бути порожнім.", nameof(eventid));
            }

            var tiers = await _tierRepository.GetByEventIdAsync(eventid);

            if (tiers == null || !tiers.Any())
            {
                throw new InvalidOperationException("Для даної події не знайдено жодної категорії квитків.");
            }

            var stats = new TicketStatisticDTO
            {
                AveragePrice = tiers.Average(t => t.Price),

                AvailableSeatsCount = tiers.Sum(t => t.AvailableSeats),

                MostExpensiveTierId = tiers.OrderByDescending(t => t.Price).First().Id
            };

            return stats;
        }
    }
}
