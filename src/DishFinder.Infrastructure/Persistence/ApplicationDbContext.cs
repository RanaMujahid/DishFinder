using DishFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DishFinder.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Area> Areas => Set<Area>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<Bookmark> Bookmarks => Set<Bookmark>();
    public DbSet<OwnerRequest> OwnerRequests => Set<OwnerRequest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
