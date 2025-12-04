using DishFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DishFinder.Infrastructure.Persistence.Configurations;

public class OwnerRequestConfiguration : IEntityTypeConfiguration<OwnerRequest>
{
    public void Configure(EntityTypeBuilder<OwnerRequest> builder)
    {
        builder.HasKey(o => o.Id);
        builder.HasOne(o => o.User).WithMany(u => u.OwnerRequests).HasForeignKey(o => o.UserId);
        builder.HasOne(o => o.Restaurant).WithMany(r => r.OwnerRequests).HasForeignKey(o => o.RestaurantId);
    }
}
