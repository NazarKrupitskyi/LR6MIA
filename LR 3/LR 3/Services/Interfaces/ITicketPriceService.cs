namespace LR_3.Services.Interfaces
{
    public interface ITicketPriceService
    {
        decimal CalculateFinalPrice(decimal basePrice);
    }
}
