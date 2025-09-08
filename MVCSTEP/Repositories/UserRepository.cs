using MVCSTEP.Models;

namespace MVCSTEP.Repositories;

public class UserRepository
{
    public List<User> Users { get; set; }
 
    public UserRepository()
    {
        Users = new List<User>()
        {
            new User() { Id = 1, Email = "admin@gmail.com", Password = "123456",CreatedAt = DateTime.Now.AddYears(-20), Role = new Role{ Name = "Admin" } },
            new User() { Id = 2, Email = "marry@gmail.com", Password = "qwerty", CreatedAt = DateTime.Now.AddYears(-15) , Role = new Role{ Name = "Editor" } },
            new User() { Id = 3, Email = "alex@gmail.com", Password = "123456", CreatedAt = DateTime.Now.AddYears(-10) , Role = new Role{ Name = "Visitor" } }
        };
    }
}