namespace hw5.Model
{
    public interface IDataProvider
    {
        public Task AddItem<T>(T item);
        public Task RemoveItem<T>(T item);
        public Task EditItem<T>(T item, int id) where T : class;
        public Task<T?> GetItem<T>(int id)where T: class;
        public Task<T?> GetItem<T>(Func<T, bool> predicate) where T : class;
        public Task<IEnumerable<T>> GetItems<T>(int id) where T : class;
        public Task Save();
    }
}
