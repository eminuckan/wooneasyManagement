using Carter;
using MediatR;
using WooneasyManagement.Application.Amenities.Commands;
using WooneasyManagement.Application.Amenities.Queries;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.API.Endpoints;

public class AmenityEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/amenities");

        group.MapPost("", CreateAmenity);

        group.MapGet("", GetAmenities);

        group.MapGet("{id}", GetAmenity).WithName(nameof(GetAmenity));

        group.MapPut("{id}", UpdateAmenity).WithName(nameof(UpdateAmenity));

        group.MapDelete("{id}", DeleteAmenity).WithName(nameof(DeleteAmenity));
    }

    public static async Task<IResult> CreateAmenity(
        CreateAmenityCommand request,
        ISender sender
    )
    {
        var response = await sender.Send(request);

        return response.IsSuccess ? Results.Created() : response.ToProblemDetails();
    }

    public static async Task<IResult> GetAmenities(
        ISender sender,
        AmenityType? amenityType
    )
    {
        var response = await sender.Send(new GetAllAmenitiesQuery { AmenityType = amenityType });

        return response.IsSuccess ? Results.Ok(response.Data) : response.ToProblemDetails();
    }

    public static async Task<IResult> GetAmenity(
        ISender sender,
        Guid id
    )
    {
        var response = await sender.Send(new GetAmenityQuery { PropertyAmenityId = id });

        return response.IsSuccess ? Results.Ok(response.Data) : response.ToProblemDetails();
    }

    public static async Task<IResult> DeleteAmenity(
        ISender sender,
        Guid id
    )
    {
        var response = await sender.Send(new DeleteAmenityCommand { Id = id });

        return response.IsSuccess ? Results.NoContent() : response.ToProblemDetails();
    }

    public static async Task<IResult> UpdateAmenity(
        ISender sender,
        Guid id,
        UpdateAmenityCommand request
    )
    {
        request.Id = id;
        var response = await sender.Send(request);

        return response.IsSuccess ? Results.Ok() : response.ToProblemDetails();
    }
}