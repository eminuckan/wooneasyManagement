using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Amenities.Dtos;
using WooneasyManagement.Application.Amenities.Interfaces;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Domain.Entities;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Application.Amenities.Commands;

public record CreateAmenityCommand : AmenityCreateDto, IRequest<Result>
{
}

public class CreateAmenityCommandHandler(IApplicationDbContext context, IMapper mapper, IAmenityWriteService amenityWriteService)
    : IRequestHandler<CreateAmenityCommand, Result>
{
    public async Task<Result> Handle(CreateAmenityCommand request, CancellationToken cancellationToken)
    {
        var existingAmenity =
            await context.Amenities.FirstOrDefaultAsync(a => a.Title == request.Title, cancellationToken);

        
        if (existingAmenity is not null) return Result.Fail(AmenityErrors.AmenityAlreadyExists);

        
        switch (request.AmenityType)
        {
            case AmenityType.Property:
                await amenityWriteService.AddAmenity<PropertyAmenity>(mapper.Map<PropertyAmenity>(request), cancellationToken);
                break;
            case AmenityType.Unit:
                await amenityWriteService.AddAmenity<UnitAmenity>(mapper.Map<UnitAmenity>(request), cancellationToken);
                break;
            case AmenityType.Room:
                await amenityWriteService.AddAmenity<RoomAmenity>(mapper.Map<RoomAmenity>(request), cancellationToken);
                break;
            default:
                return Result.Fail(AmenityErrors.AmenityTypeInvalid);
        }

        await context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}
