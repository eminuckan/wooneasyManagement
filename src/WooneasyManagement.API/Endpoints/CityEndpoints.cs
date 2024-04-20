using Carter;
using MediatR;
using WooneasyManagement.Application.Cities.Commands;
using WooneasyManagement.Application.Common;

namespace WooneasyManagement.API.Endpoints;

public class CityEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/cities");

        group.MapPost("", CreateCity);
        group.MapPatch("upload/{id}", UploadFile).DisableAntiforgery();
        group.MapPatch("remove-image/{id}", RemoveCityImage);
    }

    private async Task<IResult> CreateCity(
        ISender sender,
        CreateCityCommand request
    )
    {
        var response = await sender.Send(request);

        return response.ToMinimalAPIResult();
    }

    private async Task<IResult> UploadFile(
        ISender sender,
        IFormFile file,
        Guid id
    )
    {
        var response = await sender.Send(new UploadCityCoverImageCommand { CityId = id, File = file });
        return response.ToMinimalAPIResult();
    }

    public async Task<IResult> RemoveCityImage(
        ISender sender,
        Guid id
    )
    {
        var response = await sender.Send(new RemoveCityCoverImageCommand { CityId = id });
        return response.ToMinimalAPIResult();
    }
}