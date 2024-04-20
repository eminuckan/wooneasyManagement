using MediatR;
using Microsoft.AspNetCore.Mvc;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;

namespace WooneasyManagement.Application.Cities.Commands;

public class RemoveCityCoverImageCommand : IRequest<Result>
{
    [FromRoute] public required Guid CityId { get; set; }
}

public class RemoveCityCoverImageCommandHandler(IApplicationDbContext context)
    : IRequestHandler<RemoveCityCoverImageCommand, Result>
{
    public async Task<Result> Handle(RemoveCityCoverImageCommand request, CancellationToken cancellationToken)
    {
        var city = await context.Cities.FindAsync(request.CityId);

        if (city is null)
            return Result.Fail(CityErrors.CityNotFound);

        city.CoverImage = null;
        context.Cities.Update(city);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}