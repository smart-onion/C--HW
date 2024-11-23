using hw5.Model;

namespace hw5.Services
{
    public class FlyTicketServiceProvider : IMyServiceProvider
    {
        readonly IDataProvider _context;

        public FlyTicketServiceProvider(IDataProvider context) { _context = context; }

        public async Task<bool> Edit(User user, Service service)
        {
            var dbUser = await _context.GetItem<User>(user.Id);
            if (dbUser != null)
            {
                var userService = await _context.GetItem<Service>(s => s.Id == service.Id);
                if (userService != null)
                {
                    userService.Description = service.Description;

                    await _context.Save();
                    return true;
                }
            }
            return false;
        }

        public async Task<string> GetServiceInfo(int userId)
        {
            var user = await _context.GetItem<User>(userId);
            if (user != null) return $"<a href=\"/home\">Home</a>\nName:{user.Name}\nEmail:{user.Email}\nWelcome to fly ticket service provider";
            return "User never register!";
        }

        public async Task<bool> Register(User user, Service service)
        {
            var dbUser = await _context.GetItem<User>(user.Id);
            if (dbUser != null)
            {
                dbUser.UserServices.Add(new UserService() { Service = service, User = user });

                await _context.Save();
                return true;
            }
            return false;
        }
    }
}
