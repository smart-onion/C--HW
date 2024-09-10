using Microsoft.EntityFrameworkCore;

namespace HW61
{
    public static class DbManager
    {
        static ApplicationContext db;

        public static void SetDbContext(ApplicationContext db)
        {
            DbManager.db = db;
        }

        public static ApplicationContext GetDbContext()
        {
            return db;
        }

        public static void AddItem<T>(T item)
        {
            db.Add(item);
            db.SaveChanges();
        }

        public static void RemoveItem<T>(T item) where T : class
        {
            db.Remove(item);
            db.SaveChanges();
        }


    }
}
