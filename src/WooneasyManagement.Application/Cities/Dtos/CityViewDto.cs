using WooneasyManagement.Application.Common.Dtos;

namespace WooneasyManagement.Application.Cities.Dtos
{
    public class CityViewDto : ViewDto
    {
        public required string Name { get; set; }

        public string? State { get; set; }

        public required string Country { get; set; }
    }
}
