using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaStar.Models;

namespace PizzaStar.Data;

public class DbInit
{
    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync("Admin") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        if (await roleManager.FindByNameAsync("Editor") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("Editor"));
        }

        if (await roleManager.FindByNameAsync("Client") == null)
        {
            await roleManager.CreateAsync(new IdentityRole("Client"));
        }

        string adminEmail = "admin@gmail.com", adminPassword = "192837";
        if (await userManager.FindByNameAsync(adminEmail) == null)
        {
            User admin = new User
            {
                Email = adminEmail,
                UserName = adminEmail,
                PhoneNumber = "380970601478",
                Year = 1990,
                City = "Днепр",
                Address = "Титова 12, кв 33."
            };
            IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            User alex = new User
            {
                Email = "alex@gmail.com",
                UserName = "alex@gmail.com",
                PhoneNumber = "38096546798",
                Year = 2001,
                City = "Днепр",
                Address = "Карла Маркса 121, кв 32."
            };
            result = await userManager.CreateAsync(alex, "qwerty");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(alex, "Editor");
            }

            User tom = new User
            {
                Email = "tom@gmail.com",
                UserName = "tom@gmail.com",
                PhoneNumber = "380665459874",
                Year = 1995,
                City = "Днепр",
                Address = "Тополь 3, дом 44 кв 7."
            };
            result = await userManager.CreateAsync(tom, "1234567AS");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(tom, "Client");
            }

            User marry = new User
            {
                Email = "marry@in.ua",
                UserName = "marry@in.ua",
                PhoneNumber = "380964578796",
                Year = 1981,
                City = "Киев",
                Address = "Шевеченко, дом 7 кв 14."
            };
            result = await userManager.CreateAsync(marry, "2S91lds");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(marry, "Client");
            }
        }
    }

    public static async Task InitializeContentAsync(ApplicationContext context)
    {
        if (!await context.Categories.AnyAsync())
        {
            await context.Categories.AddRangeAsync
            (
                new Category[]
                {
                    new Category
                    {
                        Name = "Пицца", Description = "Вкуснейшая пицца в городе для истинных гурманов.",
                        DateOfPublication = DateTime.Now
                    },
                    new Category
                    {
                        Name = "Салаты",
                        Description = "Салаты с рыбой, мясом, овощами - большой выбор меню на любой вкус.",
                        DateOfPublication = DateTime.Now
                    },
                    new Category
                    {
                        Name = "Напитки",
                        Description = "Напитки являются одним из наиболее важных элементов культуры питания.",
                        DateOfPublication = DateTime.Now
                    }
                }
            );
            await context.SaveChangesAsync();
        }
    }
}