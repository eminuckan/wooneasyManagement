using AutoMapper;
using WooneasyManagement.Domain.Entities;
using File = WooneasyManagement.Domain.Entities.File;

namespace WooneasyManagement.Application.Files;

public class FileMapping : Profile
{
    public FileMapping()
    {
        CreateMap<FileInfoDto, File>().ReverseMap();
        CreateMap<FileInfoDto, CityImageFile>().ReverseMap();
    }
}