using LR_3.Services.Interfaces;

namespace LR_3.Services
{
    public class StandardTicketCalculator : ITicketPriceService
    {
        public decimal CalculateFinalPrice(decimal basePrice)
        {
            return basePrice;
        }

        public class PremiumTicketCalculator : ITicketPriceService
        {
            public decimal CalculateFinalPrice(decimal basePrice)
            {
                return basePrice + (basePrice * 0.20m);
            }
        }

        public class DisabledTicketCalculator : ITicketPriceService
        {
            public decimal CalculateFinalPrice(decimal basePrice)
            {
                return basePrice * 0.50m;
            }
        }
    }
}
