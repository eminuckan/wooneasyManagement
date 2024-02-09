using Carter;
using MediatR;
using WooneasyManagement.Application.Cities.Commands;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Interfaces.Storage;

namespace WooneasyManagement.API.Endpoints
{
    public class CityEndpoints(IStorageService storageService) : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/cities");

            group.MapPost("", CreateCity);
            group.MapPost("upload", UploadFile).DisableAntiforgery();
        }

        private async Task<IResult> CreateCity(
            ISender sender,
            CreateCityCommand request
        )
        {
            var response = await sender.Send(request);

            return response.IsSuccess ? Results.Created() : response.ToProblemDetails();
        }

        private async Task<IResult> UploadFile(
            IFormFileCollection files    
        )
        {  
            await storageService.UploadAsync("images", files);
            return Results.Ok();
        }
    }


}
