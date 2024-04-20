using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Interfaces.Storage;
using WooneasyManagement.Application.Files.Commands;
using WooneasyManagement.Application.Files.Queries;

namespace WooneasyManagement.API.Endpoints
{
    public class FileEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/files");

            group.MapPost("upload", UploadFiles).DisableAntiforgery();
            group.MapDelete("bulk-delete", BulkDelete);
            group.MapGet("", GetFiles);
            group.MapGet("{id}", HasFile);
        }

        private async Task<IResult> UploadFiles(
            ISender sender,
            [FromQuery] string b,
            [FromQuery] string p,
            [FromForm] IFormFileCollection files
        )
        {
            var response = await sender.Send(new UploadFilesCommand()
            {
                BucketOrMainDirectory = b,
                Prefix = p,
                Files = files
            });
            return response.ToMinimalAPIResult();
        }

        private async Task<IResult> BulkDelete(
            IStorageService storageService,
            ISender sender,
            [FromBody] DeleteFilesCommand request
        )
        {
            var response = await sender.Send(request);
           
            return response.IsSuccess ? Results.NoContent() : Results.BadRequest();
        }

        private async Task<IResult> GetFiles(
            ISender sender,
            [FromQuery] string? b,
            [FromQuery] string? p
        )
        {
            var response = await sender.Send(new GetAllFilesQuery()
            {
                BucketOrMainDirectory = b,
                Prefix = p
            });
            return response.ToMinimalAPIResult();
        }

        private async Task<IResult> HasFile(
            ISender sender,
            [FromQuery] string b,
            [FromQuery] string? p,
            [FromRoute] string id
        )
        {
            var response = await sender.Send(new HasFileQuery()
            {
                BucketOrMainDirectory = b,
                Prefix = p,
                FileName = id
            });
            return response.ToMinimalAPIResult();
        }
    }
}
