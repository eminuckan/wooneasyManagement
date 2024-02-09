using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Amenities.Dtos;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Application.Amenities.Queries;

public record GetAllAmenitiesQuery : IRequest<Result>
{
    public AmenityType? AmenityType { get; set; }
}

public class GetAllAmenitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetAllAmenitiesQuery, Result>
{
    public async Task<Result> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
    {
        var amenities = new List<AmenityViewDto>();
        switch (request.AmenityType)
        {
            case AmenityType.Property:
                var propertyAmenities = await context.PropertyAmenities.ToListAsync(cancellationToken);
                amenities = mapper.Map<List<AmenityViewDto>>(propertyAmenities);
                break;
            case AmenityType.Room:
                var roomAmenities = await context.RoomAmenities.ToListAsync(cancellationToken);
                amenities = mapper.Map<List<AmenityViewDto>>(roomAmenities);
                break;
            case AmenityType.Unit:
                var unitAmenities = await context.UnitAmenities.ToListAsync(cancellationToken);
                amenities = mapper.Map<List<AmenityViewDto>>(unitAmenities);
                break;
            default:
                var allAmenities = await context.Amenities.ToListAsync(cancellationToken);
                amenities = mapper.Map<List<AmenityViewDto>>(allAmenities);
                break;
        }

        return Result.Success().WithData(amenities);
    }
}