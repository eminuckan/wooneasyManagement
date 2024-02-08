using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Domain.Entities;
using WooneasyManagement.Domain.Entities.Common;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Persistence.Contexts
{
    public class WooneasyManagementDbContext : DbContext, IApplicationDbContext
    {
        public WooneasyManagementDbContext(DbContextOptions<WooneasyManagementDbContext> options) : base(options){}
        
        public DbSet<Property> Properties { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<UnitAmenity> UnitAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WooneasyManagementDbContext).Assembly);
            
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entityEntries = ChangeTracker.Entries<BaseEntity>();

            foreach (var item in entityEntries)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        item.Entity.CreatedDate = DateTime.UtcNow;
                        item.Entity.ModifiedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        item.Entity.ModifiedDate = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
