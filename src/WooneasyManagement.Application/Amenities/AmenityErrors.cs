using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common;

namespace WooneasyManagement.Application.Amenities;

public static class AmenityErrors
{
    public static readonly Error AmenityNotFound = new()
    {
        Title = "Amenity Not Found",
        Detail = "The specified amenity could not be found. Double-check the amenity name for typos.",
        Instance = "Amenities",
        Severity = ErrorSeverity.Low,
        StatusCode = StatusCodes.Status404NotFound
    };

    public static readonly Error AmenityAlreadyExists = new()
    {
        Title = "Amenity Already Exists",
        Detail = "An amenity with this name already exists. Please choose a different name or consider updating the existing amenity.",
        StatusCode = StatusCodes.Status409Conflict
    };

    public static readonly Error AmenityCreationFailed = new()
    {
        Title = "Amenity Creation Failed",
        Detail = "There was an error creating the new amenity. Please try again later.",
        StatusCode = StatusCodes.Status500InternalServerError
    };

    public static readonly Error AmenityUpdateFailed = new()
    {
        Title = "Amenity Update Failed",
        Detail = "There was an error updating the amenity. Please try again later.",
        StatusCode = StatusCodes.Status500InternalServerError
    };

    public static readonly Error AmenityDeletionFailed = new()
    {
        Title = "Amenity Deletion Failed",
        Detail = "There was an error deleting the amenity. Please try again later.",
        StatusCode = StatusCodes.Status500InternalServerError
    };

    public static readonly Error AmenityTypeInvalid = new()
    {
        Title = "Invalid Amenity Type",
        Detail = "The provided amenity type is invalid. Please refer to the documentation for a list of valid amenity types.",
        StatusCode = StatusCodes.Status400BadRequest
    };
}
