using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace hw5.Model
{
    public class ServiceContext: DbContext, IDataProvider
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<UserService> UserServices { get; set; }
        public ServiceContext() { Database.EnsureCreated(); }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:SqlServer"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserService>()
            .HasKey(us => new { us.UserId, us.ServiceId });

            modelBuilder.Entity<UserService>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserServices)
                .HasForeignKey(us => us.UserId);

            modelBuilder.Entity<UserService>()
                .HasOne(us => us.Service)
                .WithMany(s => s.UserServices)
                .HasForeignKey(us => us.ServiceId);
            base.OnModelCreating(modelBuilder);
        }

        public async Task AddItem<T>(T item)
        {
            await AddAsync(item);
            await SaveChangesAsync();
        }

        public async Task RemoveItem<T>(T item)
        {
            Remove(item);
            await SaveChangesAsync();
        }

        public async Task EditItem<T>(T item, int id) where T : class
        {
            var dbItem = await FindAsync<T>(id);
            dbItem = item;
            await SaveChangesAsync();
        }

        public async Task<T?> GetItem<T>(int id) where T: class
        {
            return await FindAsync<T>(id);
        }
        public async Task<T?> GetItem<T>(Func<T, bool> predicate) where T : class
        {
            return Set<T>().FirstOrDefault(predicate);
        }
        public async Task Save()
        {
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetItems<T>(int id) where T : class
        {
            return await UserServices.Where(u => u.User.Id == id).Select<UserService, T>(a => a.Service as T).ToListAsync();
        }
    }
}
