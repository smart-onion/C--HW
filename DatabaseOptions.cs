using HW2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public static class DbOptions<TContext> where TContext : DbContext
{
    public static DbContextOptions GetOptions(string jsonFile, string connectionString)
    {

        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("application.json");
        var config = builder.Build();
        string connString = config.GetConnectionString(connectionString);
        var optionBuilder = new DbContextOptionsBuilder<TContext>();
        return optionBuilder.UseSqlServer(connString).Options;
    }

}

