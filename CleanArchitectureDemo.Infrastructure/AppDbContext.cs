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

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the schema for the Expenses entity
            modelBuilder.Entity<Expense>(entity =>
            {
                entity.ToTable("Expenses", "finance"); // Schema "finance"
                entity.HasKey(e => e.ExpenseId); // Primary key
                entity.Property(e => e.ExpenseDate).IsRequired(); // Example property configuration
                // Add other configurations as needed
            });

            // Configure the schema for the Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories", "finance"); // Schema "finance"
                entity.HasKey(c => c.CategoryId); // Primary key
                // Add other configurations as needed
            });

            // Example for other entities if you have them:
            // modelBuilder.Entity<AnotherEntity>(entity => { /* configuration */ });
        }
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
