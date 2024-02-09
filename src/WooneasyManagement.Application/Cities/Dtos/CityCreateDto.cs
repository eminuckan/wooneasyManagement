namespace WooneasyManagement.Application.Cities.Dtos
{
    public class CityCreateDto
    {
        public required string Name { get; set; }

        public string? State { get; set; }

        public required string Country { get; set; }
    }
}
