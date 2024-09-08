namespace HW5
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<Guest> Guests { get; set; }
    public List<GuestEvent> GuestEvents { get; set; }
    }
}
