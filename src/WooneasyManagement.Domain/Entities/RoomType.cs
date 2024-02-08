using WooneasyManagement.Domain.Entities.Common;

namespace WooneasyManagement.Domain.Entities
{
    public class RoomType : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
