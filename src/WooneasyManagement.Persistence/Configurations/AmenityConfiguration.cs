using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WooneasyManagement.Domain.Entities;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Persistence.Configurations;

public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
{
    public void Configure(EntityTypeBuilder<Amenity> builder)
    {
        
        builder.HasIndex(a => a.Title).IsUnique();
        builder
            .HasDiscriminator<AmenityType>("AmenityType")
            .HasValue<PropertyAmenity>(AmenityType.Property)
            .HasValue<UnitAmenity>(AmenityType.Unit)
            .HasValue<RoomAmenity>(AmenityType.Room)
            ;
    }
}