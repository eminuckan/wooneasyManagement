using WooneasyManagement.Domain.Entities.Common;

namespace WooneasyManagement.Domain.Entities
{

    public abstract class Amenity : BaseEntity
    {
        public required string Title { get; set; }
        public required string IconClass { get; set; }
    }

    public class PropertyAmenity : Amenity
    {
        public ICollection<Property>? Properties { get; set; }
    }

    public class RoomAmenity : Amenity
    {
        public ICollection<Room>? Rooms { get; set; }
    }

    public class UnitAmenity : Amenity
    {
        public ICollection<Unit>? Units { get; set; }
    }
}
