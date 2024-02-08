using WooneasyManagement.Application.Common;
using WooneasyManagement.Domain.Entities;

namespace WooneasyManagement.Application.Amenities.Interfaces;

public interface IAmenityWriteService
{
    Task AddAmenity<T>(T amenity, CancellationToken cancellationToken) where T : Amenity;
    Task UpdateAmenity<T>(T amenity, CancellationToken cancellationToken) where T : Amenity;
    Task DeleteAmenity<T>(T amenity, CancellationToken cancellationToken) where T : Amenity;

}