namespace LR_3.Services.Interfaces
{
    public interface IEventReportsService
    {
        Task<string> BuildEventReportAsync(string eventId, string reportType, string format);
    }
}
