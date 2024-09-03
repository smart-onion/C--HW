using System.Text;
using System.Security.Cryptography;
namespace HW3
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = Utility.GenerateHash(value);
            }
        }
    }
}
