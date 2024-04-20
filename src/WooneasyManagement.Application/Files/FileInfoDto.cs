namespace WooneasyManagement.Application.Files;

public class FileInfoDto
{
    public required string FileName { get; set; }
    public required string FilePath { get; set; }
    public required string BucketOrMainDirectory { get; set; }
    public required string Storage { get; set; }
}