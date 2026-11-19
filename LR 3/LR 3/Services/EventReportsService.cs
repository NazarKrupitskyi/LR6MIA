using LR_3.BridgeProccesing.Abstraction;
using LR_3.BridgeProccesing.Implementation;
using LR_3.DBCommunication.Repositories.Interfaces;
using LR_3.Models;

namespace LR_3.Services
{
    public class EventReportsService
    {
        private readonly IEventRepository _eventRepo;
        private readonly ITicketOrderReposetory _orderRepo;
        private readonly CsvReportFormatter _csvFormatter;
        private readonly JsonReportFormatter _jsonFormatter;
        private readonly PlainTextReportFormatter _textFormatter;

        public EventReportsService(
            IEventRepository eventRepo,
            ITicketOrderReposetory orderRepo,
            CsvReportFormatter csvFormatter,
            JsonReportFormatter jsonFormatter,
            PlainTextReportFormatter textFormatter)
        {
            _eventRepo = eventRepo;
            _orderRepo = orderRepo;
            _csvFormatter = csvFormatter;
            _jsonFormatter = jsonFormatter;
            _textFormatter = textFormatter;
        }

        public async Task<string> BuildEventReportAsync(string eventId, string reportType, string format)
        {
            var ev = await _eventRepo.GetByIdAsync(eventId);
            if (ev == null) throw new KeyNotFoundException("Подію не знайдено.");

            var allOrders = await _orderRepo.GetAllAsync();
            var eventOrders = new List<TicketOrder>();
            foreach (var order in allOrders)
            {
                if (order.EventId == eventId)
                {
                    eventOrders.Add(order);
                }
            }

            IReportFormatter formatter;
            if (format.ToLower() == "csv")
            {
                formatter = _csvFormatter;
            }
            else if (format.ToLower() == "json")
            {
                formatter = _jsonFormatter;
            }
            else
            {
                formatter = _textFormatter;
            }

            EventReport report;
            if (reportType.ToLower() == "detailed")
            {
                report = new DetailedSalesReport(formatter);
            }
            else
            {
                report = new ShortSalesReport(formatter);
            }

            return report.Generate(ev, eventOrders);
        }
    }
}
