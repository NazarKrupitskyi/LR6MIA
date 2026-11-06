namespace LR_3.DTO.TicketTier
{
    public class TicketStatisticDTO
    {
        public double AveragePrice { get; set; }
        public int AvailableSeatsCount { get; set; }
        public string MostExpensiveTierId { get; set; } = string.Empty;
    }
}
