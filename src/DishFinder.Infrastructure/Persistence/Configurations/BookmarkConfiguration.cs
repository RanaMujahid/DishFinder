using DishFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DishFinder.Infrastructure.Persistence.Configurations;

public class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
{
    public void Configure(EntityTypeBuilder<Bookmark> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasOne(b => b.User).WithMany(u => u.Bookmarks).HasForeignKey(b => b.UserId);
        builder.HasOne(b => b.Restaurant).WithMany(r => r.Bookmarks).HasForeignKey(b => b.RestaurantId).OnDelete(DeleteBehavior.SetNull);
        builder.HasOne(b => b.Dish).WithMany(d => d.Bookmarks).HasForeignKey(b => b.DishId).OnDelete(DeleteBehavior.SetNull);
    }
}
