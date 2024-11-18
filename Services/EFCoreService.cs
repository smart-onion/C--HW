
using hw4.Model;
using Microsoft.EntityFrameworkCore;

namespace hw4.Services
{
    public class EFCoreService : IDataService
    {
        StoreContext db = new();

        public async Task SaveChanges()
        {
            await db.SaveChangesAsync();
        }

        public async Task<bool> Add<T>(T item) where T : class, IEntity
        {
            try
            {
                await db.AddAsync<T>(item);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Edit<T>(T item) where T : class, IEntity
        {
            try
            {
                var toEdit = db.Update<T>(item);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<T> Get<T>(int id) where T : class, IEntity
        {
            return await db.FindAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAll<T>() where T : class, IEntity
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<bool> Remove<T>(int id) where T : class, IEntity
        {
            try
            {
                var toDelete = await db.FindAsync<T>(id);
                db.Remove<T>(toDelete);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
