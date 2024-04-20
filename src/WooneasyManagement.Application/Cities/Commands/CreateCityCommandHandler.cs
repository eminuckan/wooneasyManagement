using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Cities.Dtos;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Domain.Entities;

namespace WooneasyManagement.Application.Cities.Commands;

public class CreateCityCommand : CityCreateDto, IRequest<Result>;

public class CreateCityCommandHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<CreateCityCommand, Result>
{
    public async Task<Result> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = mapper.Map<City>(request);

        var existingCity = await context.Cities.FirstOrDefaultAsync(c => c.Name == city.Name, cancellationToken);

        if (existingCity is not null) return Result.Fail(CityErrors.CityAlreadyExists);

        await context.Cities.AddAsync(city, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}