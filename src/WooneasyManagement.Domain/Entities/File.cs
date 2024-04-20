using System.ComponentModel.DataAnnotations.Schema;
using WooneasyManagement.Domain.Entities.Common;

namespace WooneasyManagement.Domain.Entities;

public class File : BaseEntity
{
    public required string FileName { get; set; }
    public required string FilePath { get; set; }
    public required string BucketOrMainDirectory { get; set; }
    public required string Storage { get; set; }

    [NotMapped]
    public override DateTime ModifiedDate
    {
        get => base.ModifiedDate;
        set => base.ModifiedDate = value;
    }
}

public class CityImageFile : File { }

public class PropertyImageFile : File { }

public class UnitImageFile : File { }

public class RoomImageFile : File { }

public class InvoiceFile : File { }