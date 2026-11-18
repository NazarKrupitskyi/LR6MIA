using LR_3.Models;
using LR_3.Services;
using LR_3.Services.Interfaces;
using static LR_3.Services.StandardTicketCalculator;

namespace LR_3.FactoriesProccesing
{
    public class TicketPriceFactory
    {
        private readonly IEnumerable<ITicketPriceService> _calculators;

        public TicketPriceFactory(IEnumerable<ITicketPriceService> calculators)
        {
            _calculators = calculators;
        }

        public ITicketPriceService GetCalculator(TicketCategory tierType)
        {
            return tierType switch
            {
                TicketCategory.Стандарт => _calculators.OfType<StandardTicketCalculator>().First(),
                TicketCategory.ВІП => _calculators.OfType<PremiumTicketCalculator>().First(),
                TicketCategory.Міт_енд_гріт => _calculators.OfType<PremiumTicketCalculator>().First(),
                TicketCategory.Інвалідні => _calculators.OfType<DisabledTicketCalculator>().First(),
                _ => throw new InvalidOperationException("Невідомий тип квитка")
            };
        }
    }
}
