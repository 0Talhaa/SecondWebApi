namespace NewWebApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int NoOfGuests { get; set; }
        public DateTime  Date { get; set; }
        public int EventtypeId { get; set; }

        public virtual EventType EventType { get; set; }

    }
}
