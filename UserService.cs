namespace HW3
{
    public static class UserService
    {
         public static UserContext db = new UserContext();

         static UserService() { }

        public static bool Register(User user)
        {
            try
            {
                db.User.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                Console.WriteLine("User already exist! Press any key to continue...");
                Console.ReadKey();
                return false;
            }
        }

        public static bool VerifyUser(User user)
        {
            var users = db.User.ToList();
            foreach (var usr in users)
            {
                if (user.Username == usr.Username && user.Password == usr.Password) return true;
            }
            return false;
        }
    }
}
