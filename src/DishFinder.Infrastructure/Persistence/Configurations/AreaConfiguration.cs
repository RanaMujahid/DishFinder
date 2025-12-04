using DishFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DishFinder.Infrastructure.Persistence.Configurations;

public class AreaConfiguration : IEntityTypeConfiguration<Area>
{
    public void Configure(EntityTypeBuilder<Area> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.City).IsRequired();
    }
}
