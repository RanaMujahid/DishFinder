using DishFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DishFinder.Infrastructure.Persistence.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Url).IsRequired();
        //builder.HasOne(p => p.Review).WithMany(r => r.Photos).HasForeignKey(p => p.ReviewId).OnDelete(DeleteBehavior.SetNull);
        //builder.HasOne(p => p.Restaurant).WithMany(r => r.Photos).HasForeignKey(p => p.RestaurantId).OnDelete(DeleteBehavior.SetNull);
        //builder.HasOne(p => p.Dish).WithMany(d => d.Photos).HasForeignKey(p => p.DishId).OnDelete(DeleteBehavior.SetNull);
    }
}
