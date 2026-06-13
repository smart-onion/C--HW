using Microsoft.EntityFrameworkCore;
using TaskWebApp.Models;

namespace TaskWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Seed data ─────────────────────────────────────────────────────────
            modelBuilder.Entity<AppUser>().HasData(
                new AppUser
                {
                    Id             = 1,
                    Nickname       = "shadow_byte",
                    Status         = "🟢 Онлайн",
                    BirthDate      = new DateTime(1995, 3, 12),
                    RegisteredDate = new DateTime(2020, 1, 5),
                    LastLoginDate  = DateTime.UtcNow.AddMinutes(-15),
                    AvatarUrl      = "https://api.dicebear.com/7.x/pixel-art/svg?seed=shadow_byte"
                },
                new AppUser
                {
                    Id             = 2,
                    Nickname       = "nova_coder",
                    Status         = "🟡 Занят",
                    BirthDate      = new DateTime(1998, 7, 22),
                    RegisteredDate = new DateTime(2021, 6, 18),
                    LastLoginDate  = DateTime.UtcNow.AddHours(-2),
                    AvatarUrl      = "https://api.dicebear.com/7.x/pixel-art/svg?seed=nova_coder"
                },
                new AppUser
                {
                    Id             = 3,
                    Nickname       = "pixel_queen",
                    Status         = "🔴 Не беспокоить",
                    BirthDate      = new DateTime(2000, 11, 3),
                    RegisteredDate = new DateTime(2022, 3, 9),
                    LastLoginDate  = DateTime.UtcNow.AddDays(-1),
                    AvatarUrl      = "https://api.dicebear.com/7.x/pixel-art/svg?seed=pixel_queen"
                },
                new AppUser
                {
                    Id             = 4,
                    Nickname       = "ghost_runner",
                    Status         = "⚫ Офлайн",
                    BirthDate      = new DateTime(1993, 5, 17),
                    RegisteredDate = new DateTime(2019, 11, 30),
                    LastLoginDate  = DateTime.UtcNow.AddDays(-5),
                    AvatarUrl      = "https://api.dicebear.com/7.x/pixel-art/svg?seed=ghost_runner"
                },
                new AppUser
                {
                    Id             = 5,
                    Nickname       = "byte_wizard",
                    Status         = "🟢 Онлайн",
                    BirthDate      = new DateTime(1997, 9, 8),
                    RegisteredDate = new DateTime(2023, 2, 14),
                    LastLoginDate  = DateTime.UtcNow.AddMinutes(-5),
                    AvatarUrl      = "https://api.dicebear.com/7.x/pixel-art/svg?seed=byte_wizard"
                },
                new AppUser
                {
                    Id             = 6,
                    Nickname       = "neon_fox",
                    Status         = "🟡 Занят",
                    BirthDate      = new DateTime(2001, 2, 28),
                    RegisteredDate = new DateTime(2023, 8, 1),
                    LastLoginDate  = DateTime.UtcNow.AddHours(-8),
                    AvatarUrl      = "https://api.dicebear.com/7.x/pixel-art/svg?seed=neon_fox"
                }
            );
        }
    }
}