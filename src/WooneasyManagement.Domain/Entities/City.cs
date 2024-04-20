using System.ComponentModel.DataAnnotations;
using WooneasyManagement.Domain.Entities.Common;

namespace WooneasyManagement.Domain.Entities
{
    public class City : BaseEntity
    {
        public required string Name { get; set; }

        public string? State { get; set; }

        public required string Country { get; set; }

        public CityImageFile? CoverImage { get; set; }
    }
}
