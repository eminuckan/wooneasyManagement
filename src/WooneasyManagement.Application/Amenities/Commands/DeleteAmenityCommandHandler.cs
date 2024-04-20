using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;

namespace WooneasyManagement.Application.Amenities.Commands
{
    public record DeleteAmenityCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    };
    public class DeleteAmenityCommandHandler(IApplicationDbContext context): IRequestHandler<DeleteAmenityCommand, Result> 
    {
        public async Task<Result> Handle(DeleteAmenityCommand request, CancellationToken cancellationToken)
        {
            var amenity = await context.Amenities.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken: cancellationToken);
            if (amenity is null)
            {
                return Result.Fail(AmenityErrors.AmenityNotFound);
            }

            context.Amenities.Remove(amenity);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
