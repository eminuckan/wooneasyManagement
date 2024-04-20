using MediatR;
using Microsoft.AspNetCore.Mvc;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Interfaces.Storage;

namespace WooneasyManagement.Application.Files.Queries;

public class HasFileQuery : IRequest<Result>
{
    [FromQuery] public required string BucketOrMainDirectory { get; set; }

    [FromQuery] public string? Prefix { get; set; }

    [FromRoute] public required string FileName { get; set; }
}

public class HasFileQueryHandler(IStorageService storageService) : IRequestHandler<HasFileQuery, Result>
{
    public async Task<Result> Handle(HasFileQuery request, CancellationToken cancellationToken)
    {
        var result = await storageService.HasFile(request.BucketOrMainDirectory, request.Prefix, request.FileName);

        return result
            ? Result.Ok()
            : Result.Fail(FileErrors.FileNotFoundError);
    }
}