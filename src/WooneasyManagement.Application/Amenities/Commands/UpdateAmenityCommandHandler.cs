using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Amenities.Dtos;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;

namespace WooneasyManagement.Application.Amenities.Commands;

public record UpdateAmenityCommand: AmenityUpdateDto, IRequest<Result>
{
    public Guid Id { get; set; }
}

public class UpdateAmenityCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateAmenityCommand, Result>
{
    public async Task<Result> Handle(UpdateAmenityCommand request, CancellationToken cancellationToken)
    {
        var amenity = await context.Amenities.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (amenity is null)
        {
            return Result.Fail(AmenityErrors.AmenityNotFound);
        }

        var existingAmenity = await context.Amenities.FirstOrDefaultAsync(a => a.Title == request.Title && a.Id != request.Id, cancellationToken);

        if (existingAmenity is not null)
        {
            return Result.Fail(AmenityErrors.AmenityAlreadyExists);
        }

        if (request.Title is not null || request.IconClass is not null)
        {
            if (request.Title != null) amenity.Title = request.Title;
            if (request.IconClass != null) amenity.IconClass = request.IconClass;
        
            context.Amenities.Update(amenity);
            await context.SaveChangesAsync(cancellationToken);
        }
        
        return Result.Ok();
    }
}