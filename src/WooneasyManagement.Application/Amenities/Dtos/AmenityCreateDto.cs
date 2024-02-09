using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Application.Amenities.Dtos;

public record AmenityCreateDto
{
    public required string Title { get; set; }
    public required string IconClass { get; set; }

    public required AmenityType AmenityType { get; set; }
}