using LR_3.BridgeProccesing.Implementation;
using LR_3.Models;

namespace LR_3.BridgeProccesing.Abstraction
{
    public abstract class EventReport
    {
        protected readonly IReportFormatter _formatter;

        protected EventReport(IReportFormatter formatter)
        {
            _formatter = formatter;
        }

        public abstract string Generate(Event ev, List<TicketOrder> orders);
    }
}
