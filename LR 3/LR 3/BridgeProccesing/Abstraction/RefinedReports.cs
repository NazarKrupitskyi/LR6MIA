using LR_3.BridgeProccesing.Implementation;
using LR_3.Models;

namespace LR_3.BridgeProccesing.Abstraction
{
    public class ShortSalesReport : EventReport
    {
        public ShortSalesReport(IReportFormatter formatter) : base(formatter) { }

        public override string Generate(Event ev, List<TicketOrder> orders)
        {
            decimal totalRevenue = 0;
            foreach (var order in orders)
            {
                totalRevenue += order.TotalAmount;
            }

            var data = new Dictionary<string, string>
            {
                { "EventId", ev.Id.ToString() },
                { "StartDate", ev.startDate.ToString("yyyy-MM-dd HH:mm") },
                { "TotalOrders", orders.Count.ToString() },
                { "TotalRevenue", $"{totalRevenue} UAH" }
            };

            return _formatter.Format("Короткий звіт продажів", data);
        }
    }

    public class DetailedSalesReport : EventReport
    {
        public DetailedSalesReport(IReportFormatter formatter) : base(formatter) { }

        public override string Generate(Event ev, List<TicketOrder> orders)
        {
            decimal totalRevenue = 0;
            int pendingCount = 0;

            foreach (var order in orders)
            {
                totalRevenue += order.TotalAmount;
                if (order.status == Status.Очікування_оплати)
                {
                    pendingCount++;
                }
            }

            decimal averageCheck = 0;
            if (orders.Count > 0)
            {
                averageCheck = totalRevenue / orders.Count;
            }

            var data = new Dictionary<string, string>
            {
                { "EventId", ev.Id.ToString() },
                { "StartDate", ev.startDate.ToString("yyyy-MM-dd HH:mm") },
                { "TotalOrders", orders.Count.ToString() },
                { "PendingOrders", pendingCount.ToString() },
                { "TotalRevenue", $"{totalRevenue} UAH" },
                { "AverageCheck", $"{averageCheck:F2} UAH" }
            };

            return _formatter.Format("Детальний звіт продажів", data);
        }
    }
}
