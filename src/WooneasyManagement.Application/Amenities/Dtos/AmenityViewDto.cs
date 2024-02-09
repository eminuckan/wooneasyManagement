using WooneasyManagement.Application.Common.Dtos;
using WooneasyManagement.Domain.Enums;

namespace WooneasyManagement.Application.Amenities.Dtos;

public class AmenityViewDto : ViewDto
{
    public required string Title { get; set; }
    public required string IconClass { get; set; }

    public AmenityType AmenityType { get; set; }
}