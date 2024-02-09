using AutoMapper;
using WooneasyManagement.Application.Amenities.Dtos;
using WooneasyManagement.Domain.Entities;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Application.Amenities;

public class AmenityMapping : Profile
{
    public AmenityMapping()
    {
        CreateMap<PropertyAmenity, AmenityViewDto>()
            .ForMember(d => d.AmenityType, opt => opt.MapFrom(src => AmenityType.Property));

        CreateMap<UnitAmenity, AmenityViewDto>()
            .ForMember(d => d.AmenityType, opt => opt.MapFrom(src => AmenityType.Unit));

        CreateMap<RoomAmenity, AmenityViewDto>()
            .ForMember(d => d.AmenityType, opt => opt.MapFrom(src => AmenityType.Room));

        CreateMap<Amenity, AmenityViewDto>().ForMember(d => d.AmenityType, opt => opt.MapFrom(src =>
            src.GetType().Name == nameof(PropertyAmenity) ? AmenityType.Property :
            src.GetType().Name == nameof(UnitAmenity) ? AmenityType.Unit :
            src.GetType().Name == nameof(RoomAmenity) ? AmenityType.Room : AmenityType.Property
        ));

        CreateMap<AmenityCreateDto, PropertyAmenity>();
        CreateMap<AmenityCreateDto, UnitAmenity>();
        CreateMap<AmenityCreateDto, RoomAmenity>();
    }
}