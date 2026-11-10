    namespace LR_3.Models
{
    public class TicketTier
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public TicketCategory Category { get; set; }
        public int Price { get; set; }
        public int AvailableSeats { get; set; }
    }

    public enum TicketCategory
    {
        Стандарт,      
        ВІП,           
        Фан_Зона,        
        Балкон,        
        Міт_енд_гріт,       
        Інвалідні     
    }
}
