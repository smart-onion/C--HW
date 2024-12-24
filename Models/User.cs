using System.ComponentModel.DataAnnotations;

namespace hw10.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreditCard { get; set; }
        public string Website { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public uint Age { get; set; }
    }
}
