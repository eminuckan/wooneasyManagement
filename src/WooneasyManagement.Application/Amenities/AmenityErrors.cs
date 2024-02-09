using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common;

namespace WooneasyManagement.Application.Amenities;

public static class AmenityErrors
{
    public static readonly Error AmenityNotFound =
        new("Amenities.NotFound", "Amenity not found", StatusCodes.Status404NotFound);

    public static readonly Error AmenityAlreadyExists =
        new("Amenities.AlreadyExists", "Amenity already exists", StatusCodes.Status400BadRequest);

    public static readonly Error AmenityCreationFailed = new("Amenities.CreationFailed", "Amenity creation failed",
        StatusCodes.Status400BadRequest);

    public static readonly Error AmenityUpdateFailed =
        new("Amenities.UpdateFailed", "Amenity update failed", StatusCodes.Status400BadRequest);

    public static readonly Error AmenityDeletionFailed = new("Amenities.DeletionFailed", "Amenity deletion failed",
        StatusCodes.Status400BadRequest);

    public static readonly Error AmenityTypeInvalid = new("Amenities.TypeInvalid", "Amenity type is invalid",
               StatusCodes.Status400BadRequest);
}