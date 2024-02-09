using WooneasyManagement.Application.Amenities.Interfaces;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Domain.Entities;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Persistence.Amenities;

public class AmenityWriteService(IApplicationDbContext context) : IAmenityWriteService
{
    public async Task AddAmenity<T>(T amenity, CancellationToken cancellationToken)
        where T : Amenity
    {
        await context.Set<T>().AddAsync(amenity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}