using DishFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DishFinder.Infrastructure.Persistence.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired();
        builder.Property(r => r.Address).IsRequired();
        builder.HasOne(r => r.Area).WithMany(a => a.Restaurants).HasForeignKey(r => r.AreaId);
    }
}
