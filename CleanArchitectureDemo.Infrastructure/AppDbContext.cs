using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using CleanArchitectureDemo.Domain.Entities;

using System.IO;

namespace CleanArchitectureDemo.Infrastructure.Persistence
{
     public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }
    }
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CleanArchitectureDemo.API"))

            //.SetBasePath(Directory.GetCurrentDirectory()) // Needed to locate appsettings.json
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(config.GetConnectionString("PostgressConnection"));

        return new AppDbContext(optionsBuilder.Options);
    }
}

/*
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // for design-time context
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("PostgressConnection"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }*/
}
