using DishFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DishFinder.Infrastructure.Persistence.Configurations;

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Currency).HasMaxLength(10);
        builder.HasOne(m => m.Restaurant).WithMany(r => r.MenuItems).HasForeignKey(m => m.RestaurantId);
        builder.HasOne(m => m.Dish).WithMany(d => d.MenuItems).HasForeignKey(m => m.DishId);
    }
}
