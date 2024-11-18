using hw4.Model;

namespace hw4.Services
{
    public interface IDataService
    {
        public Task SaveChanges();
        public Task<bool> Add<T>(T item) where T : class, IEntity;
        public Task<bool> Edit<T>(T item) where T : class, IEntity;
        public Task<bool> Remove<T>(int id) where T : class, IEntity;
        public Task<T> Get<T>(int id) where T : class, IEntity;
        public Task<IEnumerable<T>> GetAll<T>() where T : class, IEntity;
    }
}
