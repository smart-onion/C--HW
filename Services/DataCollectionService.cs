
using hw4.Model;

namespace hw4.Services
{
    public class DataCollectionService : IDataService
    {
        static readonly List<User> users = new();
        static int id = 0;
        public async Task<bool> Add<T>(T item) where T : class, IEntity
        {
            User user = item as User;
            user.Id = id++;
            if (user == null) return false;
            users.Add(user);
            return true;
        }

        public async Task<bool> Edit<T>(T item) where T : class, IEntity
        {
            User user = item as User;
            if (user == null) return false;

            User userToEdit = users.FirstOrDefault(u => u.Id == user.Id);

            if (userToEdit == null) return false;
            userToEdit.Name = user.Name;
            userToEdit.Age = user.Age;
            return true;
        }

        public async Task<T> Get<T>(int id) where T : class, IEntity
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            return user as T;
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class, IEntity
        {
            var result = new List<T>(users as List<T>);
            
            return result;
        }

        public async Task<bool> Remove<T>(int id) where T : class, IEntity
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return false;
            users.Remove(user);
            return true;
        }

        public async Task SaveChanges()
        {
            
        }
    }
}
