namespace HW5
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Event> Events { get; set; }
        public List<GuestEvent> GuestEvents { get; set; }

    }
}
