using Microsoft.EntityFrameworkCore;

namespace SampleApp.Data;

public class SampleDBContext : DbContext
{
    public SampleDBContext(DbContextOptions<SampleDBContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Database Seeding
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop",
            });

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 2,
                Name = "Keyboard",
            });
    }
}