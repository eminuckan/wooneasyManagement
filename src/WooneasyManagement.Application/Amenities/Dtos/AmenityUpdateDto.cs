namespace WooneasyManagement.Application.Amenities.Dtos
{
    public record AmenityUpdateDto
    {
        public string? Title { get; init; }
        public string? IconClass { get; init; }
    }
}
