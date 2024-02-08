using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Amenities;
using WooneasyManagement.Application.Amenities.Commands;
using WooneasyManagement.Domain.Entities;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Application.Common.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<PropertyAmenity, AmenityViewDto>();
            CreateMap<UnitAmenity, AmenityViewDto>();
            CreateMap<RoomAmenity, AmenityViewDto>();
            CreateMap<Amenity, AmenityViewDto>();

            CreateMap<AmenityCreateDto, PropertyAmenity>();
            CreateMap<AmenityCreateDto, UnitAmenity>();
            CreateMap<AmenityCreateDto, RoomAmenity>();
        }
    }
}
