using BooksAPI.BE.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.BE.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<LibraryComic> LibraryComics { get; set; }
    public DbSet<UserComic> UserComics { get; set; }
    public DbSet<Order> Orders { get; set; }

    public DbSet<Author> Authors { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        SeedAspNetRolesTable(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        builder.Entity<LibraryComic>()
            .HasMany(x => x.UserComics)
            .WithOne(uc => uc.LibraryComic)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<User>()
            .HasMany(u => u.UserComics)
            .WithOne(uc => uc.User)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void SeedAspNetRolesTable(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>()
            .HasData(new IdentityRole("Admin") {Id = "3187bce0-f9a9-48fb-adb6-36cea86dfb16", NormalizedName = "ADMIN" },
                new IdentityRole("User") {Id = "284b3a5c-4235-4b01-ba23-09f2f6f9737c", NormalizedName = "USER" });
    }
}