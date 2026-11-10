using LR_3.DTO.TicketOrder;
using LR_3.DTO.TicketTier;
using LR_3.ErrorProccesing;
using LR_3.Models;
namespace LR_3.Services.Interface
{
    public interface ITicketOrderService
    {
        Task<IEnumerable<TicketOrderResponseDTO>> GetAllOrdersAsync();
        Task<TicketOrderResponseDTO?> GetOrderByIdAsync(string id);
        Task<TicketOrderResponseDTO> CreateOrderAsync(CreateTicketOrderDTO dto);
        Task<TicketOrderResponseDTO?> UpdateOrderAsync(string id, UpdateTicketOrderDTO dto);
        Task<bool> DeleteOrderAsync(string id);
        Task<TicketOrderResponseDTO> PayOrderAsync(string id);
        Task<string> MassRefundAsync(string eventId);
    }
}
