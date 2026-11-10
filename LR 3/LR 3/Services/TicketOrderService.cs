using AutoMapper;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.DTO.TicketOrder;
using LR_3.Models;
using LR_3.Services.Interface;

namespace LR_3.Services
{
    public class TicketOrderService : ITicketOrderService
    {
        private readonly ITicketOrderReposetory _orderRepo;
        private readonly IEventRepository _eventRepo;
        private readonly ITicketTierReposetory _tierRepo;
        private readonly IMapper _mapper;

        public TicketOrderService(ITicketOrderReposetory orderRepo,IEventRepository eventRepo, ITicketTierReposetory tierRepo, IMapper mapper)
        { _orderRepo = orderRepo;
            _eventRepo = eventRepo;
            _tierRepo = tierRepo;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TicketOrderResponseDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<TicketOrderResponseDTO>>(orders);
        }

        public async Task<TicketOrderResponseDTO?> GetOrderByIdAsync(string id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            return order == null ? null : _mapper.Map<TicketOrderResponseDTO>(order);
        }

        public async Task<TicketOrderResponseDTO?> UpdateOrderAsync(string id, UpdateTicketOrderDTO dto)
        {
            var existingOrder = await _orderRepo.GetByIdAsync(id);
            if (existingOrder == null) return null;

            _mapper.Map(dto, existingOrder); 
            await _orderRepo.UpdateAsync(existingOrder);

            return _mapper.Map<TicketOrderResponseDTO>(existingOrder);
        }

        public async Task<bool> DeleteOrderAsync(string id)
        {
            var existingOrder = await _orderRepo.GetByIdAsync(id);
            if (existingOrder == null) return false;

            await _orderRepo.DeleteAsync(id);
            return true;
        }

       
        public async Task<TicketOrderResponseDTO> CreateOrderAsync(CreateTicketOrderDTO dto)
        {
            var ev = await _eventRepo.GetByIdAsync(dto.EventId);
            if (ev == null) throw new KeyNotFoundException("Подію не знайдено.");

            if (ev.startDate.ToUniversalTime() <= DateTime.UtcNow)
                throw new InvalidOperationException("Ця подія вже минула або недоступна для бронювання.");

            var tier = await _tierRepo.GetByIdAsync(dto.TierId);
            if (tier == null) throw new KeyNotFoundException("Категорію квитків не знайдено.");

            if (tier.AvailableSeats < dto.RequestedSeats)
                throw new InvalidOperationException($"Недостатньо квитків. Залишилось вільних місць: {tier.AvailableSeats}");

            tier.AvailableSeats -= dto.RequestedSeats;
            await _tierRepo.UpdateAsync(tier);

            var newOrder = _mapper.Map<TicketOrder>(dto);
            newOrder.status = Status.Очікування_оплати; 
            newOrder.CreateTime = DateTime.UtcNow;
            newOrder.Tickets = new List<DigitalTicket>();

            await _orderRepo.CreateAsync(newOrder);

            return _mapper.Map<TicketOrderResponseDTO>(newOrder);
        }

        public async Task<TicketOrderResponseDTO> PayOrderAsync(string orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Замовлення не знайдено.");

            if (order.status != Status.Очікування_оплати)
                throw new InvalidOperationException($"Замовлення не можна оплатити, бо воно має статус: {order.status}");

            var tier = await _tierRepo.GetByIdAsync(order.TierId);
            if (tier == null) throw new KeyNotFoundException("Категорію квитків для цього замовлення не знайдено.");

            if ((DateTime.UtcNow - order.CreateTime).TotalMinutes > 15)
            {
                order.status = Status.Відхилено;
                await _orderRepo.UpdateAsync(order);

                tier.AvailableSeats += order.RequestedSeats;
                await _tierRepo.UpdateAsync(tier);

                throw new InvalidOperationException("Час на оплату вичерпано (більше 15 хв). Бронь скасовано.");
            }

            decimal basePrice = tier.Price * order.RequestedSeats;

            decimal discount = 0;
            if (order.PromoCode == "student") discount = basePrice * 0.15m; 

            order.TotalAmount = (basePrice - discount) * 1.05m;
            order.status = Status.Оплачено;

            var rnd = new Random();
            for (int i = 0; i < order.RequestedSeats; i++)
            {
                order.Tickets.Add(new DigitalTicket
                {
                    Barcode = rnd.Next(1000, 9999).ToString(),
                    IsValid = true
                });
            }

            await _orderRepo.UpdateAsync(order);

            return _mapper.Map<TicketOrderResponseDTO>(order);
        }

        public async Task<string> MassRefundAsync(string eventId)
        {
            var ev = await _eventRepo.GetByIdAsync(eventId);
            if (ev == null) throw new KeyNotFoundException("Подію не знайдено.");

            ev.status = EventStatus.Cancelled;
            await _eventRepo.UpdateAsync(ev);

            var allOrders = await _orderRepo.GetAllAsync();
            var eventOrders = allOrders.Where(o => o.EventId == eventId && o.status == Status.Оплачено).ToList();

            if (!eventOrders.Any())
                return "Подію скасовано, але оплачених замовлень для неї не знайдено.";

            decimal totalRefunded = 0;

            foreach (var order in eventOrders)
            {
                order.status = Status.Повернено;
                order.RefundAmount = order.TotalAmount;

                foreach (var ticket in order.Tickets)
                {
                    ticket.IsValid = false;
                }

                await _orderRepo.UpdateAsync(order);
                totalRefunded += order.RefundAmount;
            }

            return $"Успішно скасовано {eventOrders.Count} замовлень. Загальна сума повернення: {totalRefunded} грн.";
        }
    }
}