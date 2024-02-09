using AutoMapper;
using MediatR;
using WooneasyManagement.Application.Amenities.Dtos;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;

namespace WooneasyManagement.Application.Amenities.Queries
{
    public record GetAmenityQuery : IRequest<Result>
    {
        public required Guid PropertyAmenityId { get; set; }
    }

    public class GetAmenityQueryHandler(IApplicationDbContext context, IMapper mapper)
        : IRequestHandler<GetAmenityQuery, Result>
    {
        public async Task<Result> Handle(GetAmenityQuery request, CancellationToken cancellationToken)
        {
            var queryResult = await context.Amenities.FindAsync(request.PropertyAmenityId);


            if (queryResult is null)
            {
                return Result.Failure(AmenityErrors.AmenityNotFound);
            }

            var amenity = mapper.Map<AmenityViewDto>(queryResult);

            return Result.Success().WithData(amenity);
        }
    }
}
