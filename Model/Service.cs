namespace hw5.Model
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<UserService> UserServices { get; set; } = new List<UserService>();
    }
}
