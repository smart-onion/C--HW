namespace HW4
{
    public class UserSettings
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string Description { get; set; }
        public double WorkingHours { get; set; }

        public int UserId { get; set; } 
        public User User { get; set; }
    }
}
