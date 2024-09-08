namespace HW5
{
    public class GuestEvent
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int EventId { get; set; }
        public Guest Guest { get; set; }
        public Event Event { get; set; }
        public Roles Roles { get; set; }
    }
}
