using System.ComponentModel.DataAnnotations;
using WooneasyManagement.Domain.Entities.Common;

namespace WooneasyManagement.Domain.Entities
{
    public class Room : BaseEntity
    {

        public required int Size { get; set; }
        
        public required decimal Price { get; set; }

        public required string Slug { get; set; }

        [Range(0, 100)]
        public decimal? DiscountRate { get; set; }

        public required int Order { get; set; }

        public string? Description { get; set; }

        public required Unit Unit { get; set; }

        public required RoomType Type { get; set; }

        public ICollection<RoomAmenity>? RoomAmenities { get; set; }
    }
}
