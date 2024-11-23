namespace hw5.Model
{
    public class UserService
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

    }
}
