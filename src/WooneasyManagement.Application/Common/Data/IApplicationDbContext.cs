using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Domain.Entities;
using File = WooneasyManagement.Domain.Entities.File;

namespace WooneasyManagement.Application.Common.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<PropertyAmenity> PropertyAmenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<UnitAmenity> UnitAmenities { get; set; }

        public DbSet<File> Files { get; set; }
        public DbSet<CityImageFile> CityImageFiles { get; set; }
        public DbSet<PropertyImageFile> PropertyImageFiles { get; set; }
        public DbSet<UnitImageFile> UnitImageFiles { get; set; }
        public DbSet<RoomImageFile> RoomImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
