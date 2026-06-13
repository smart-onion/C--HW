namespace TaskWebApp.Models
{
    public class AppUser
    {
        public int      Id               { get; set; }
        public string   Nickname         { get; set; } = string.Empty;
        public string   Status           { get; set; } = string.Empty;
        public DateTime BirthDate        { get; set; }
        public DateTime RegisteredDate   { get; set; }
        public DateTime LastLoginDate    { get; set; }
        public string   AvatarUrl        { get; set; } = string.Empty;
    }
}