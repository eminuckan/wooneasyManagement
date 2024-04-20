using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common;

namespace WooneasyManagement.Application.Cities;

public static class CityErrors
{
    public static readonly Error CityAlreadyExists = new Error
    {
        Title = "City Already Exists",
        Detail = "A city with the specified name already exists in the database.",
        StatusCode = StatusCodes.Status409Conflict,
        Instance = "Details"
    };

    public static readonly Error CityNotFound = new Error
    {
        Title = "City Not Found",
        Detail = "City not found. The requested city could not be found.",
        StatusCode = StatusCodes.Status404NotFound
    };
}