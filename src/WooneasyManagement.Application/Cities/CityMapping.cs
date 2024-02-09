using AutoMapper;
using WooneasyManagement.Application.Cities.Dtos;
using WooneasyManagement.Domain.Entities;

namespace WooneasyManagement.Application.Cities;

public class CityMapping : Profile
{
    public CityMapping()
    {
        CreateMap<City, CityViewDto>();
        CreateMap<CityCreateDto, City>();
    }
}