using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WooneasyManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WooneasyManagement.Persistence.Configurations
{
    public class UnitConfiguration: IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.Property(u => u.NoBath)
                .HasDefaultValue(2);
            builder.Property(u => u.NoKitchen)
                .HasDefaultValue(1);
            builder.Property(u => u.NoBed)
                .HasDefaultValue(1);
        }
    }
}
