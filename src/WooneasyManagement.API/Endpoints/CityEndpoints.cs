using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            group.MapGet("images", GetCityImages);
            group.MapGet("has-file", HasFile);
            group.MapDelete("delete-image", DeleteCityImage);
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
            var list = await storageService.UploadAsync("wooneasy-management","city-images", files);
            return Results.Ok(list);
        }

        private async Task<IResult> GetCityImages(
            IStorageService storageService
        )
        {
            var files = await storageService.GetFiles("wooneasy-management", "city-images");
            return Results.Ok(files);
        }

        public async Task<IResult> DeleteCityImage(
            IStorageService storageService,
            [FromQuery] string fileName    
        )
        {
            await storageService.DeleteAsync("wooneasy-management","city-images", fileName);

            return Results.NoContent();
        }

        public async Task<IResult> HasFile(
                IStorageService storageService,
                [FromQuery] string fileName
            )
        {
            var hasFile = await storageService.HasFile("wooneasy-management", "city-images", fileName);

            return Results.Ok(hasFile);
        }

    }


}
