using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Application.Common.Interfaces.Storage;
using WooneasyManagement.Application.Files.Interfaces;
using WooneasyManagement.Domain.Entities;

namespace WooneasyManagement.Application.Cities.Commands;

public class UploadCityCoverImageCommand : IRequest<Result>
{
    public Guid CityId { get; set; }
    public required IFormFile File { get; set; }
}

public class UploadCityCoverImageCommandHandler(
    IApplicationDbContext context,
    IFileService<CityImageFile> fileService)
    : IRequestHandler<UploadCityCoverImageCommand, Result>
{
    public async Task<Result> Handle(UploadCityCoverImageCommand request, CancellationToken cancellationToken)
    {
        var city = await context.Cities.FirstOrDefaultAsync(c => c.Id == request.CityId, cancellationToken);

        if (city is null)
            return Result.Fail(CityErrors.CityNotFound);

        var coverImage = await fileService.UploadAsync("wooneasy-management", "city-images", request.File);
        city.CoverImage = coverImage;
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}