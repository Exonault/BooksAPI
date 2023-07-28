using BooksAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Comic> Comics { get; set; }

    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        modelBuilder.Entity<Comic>().ToTable("Comics");
        modelBuilder.Entity<Order>().ToTable("Orders");
    }
}