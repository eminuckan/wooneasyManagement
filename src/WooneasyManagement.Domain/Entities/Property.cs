using System.ComponentModel.DataAnnotations;
using WooneasyManagement.Domain.Entities.Common;

namespace WooneasyManagement.Domain.Entities
{
    public class Property : BaseEntity
    {
        public required string StreetAddress { get; set; }

        public required City City { get; set; }

        [MaxLength(5)]
        public string? ZipCode { get; set; }

        public decimal? Lat { get; set; }

        public decimal? Lon { get; set; }

        public string? Description { get; set; }


        public ICollection<Unit>? Units { get; set; }
        public ICollection<PropertyAmenity>? PropertyAmenities { get; set; }


    }
}
