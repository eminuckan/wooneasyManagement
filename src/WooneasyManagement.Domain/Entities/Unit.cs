using WooneasyManagement.Domain.Entities.Common;

namespace WooneasyManagement.Domain.Entities
{
    public class Unit : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public int? Size { get; set; }
        
        public int NoBath { get; set; }

        public int NoKitchen { get; set; }

        public int NoBed { get; set; }

        public required Property Property { get; set; }

        public ICollection<Room>? Rooms { get; set; }

        public ICollection<UnitAmenity>? UnitAmenities { get; set; }
    }
}
