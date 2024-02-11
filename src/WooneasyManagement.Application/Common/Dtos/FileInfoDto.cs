namespace WooneasyManagement.Application.Common.Dtos;

public class FileInfoDto
{
    public string FileName { get; set; }
    public string? Path { get; set; }
    public string? BucketOrMainDirectory { get; set; }
}