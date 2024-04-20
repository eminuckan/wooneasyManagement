using AutoMapper;
using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Application.Common.Interfaces.Storage;
using WooneasyManagement.Application.Files.Interfaces;
using File = WooneasyManagement.Domain.Entities.File;

namespace WooneasyManagement.Persistence.Services;

public class FileService<TEntity>(IApplicationDbContext context, IStorageService storageService, IMapper mapper)
    : IFileService<TEntity> where TEntity : File
{
    public async Task<TEntity> UploadAsync(string bucketOrMainDirectory, string prefix, IFormFile file)
    {
        var fileInfo = await storageService.UploadAsync(bucketOrMainDirectory, prefix, file);

        var uploadedFile = mapper.Map<TEntity>(fileInfo);
        uploadedFile.Storage = storageService.StorageName;

        await context.Set<TEntity>().AddAsync(uploadedFile);
        await context.SaveChangesAsync();
        return uploadedFile;
    }

    public async Task<List<TEntity>> UploadAsync(string bucketOrMainDirectory, string prefix, IFormFileCollection files)
    {
        List<TEntity> uploadedFiles = new();
        foreach (var file in files)
        {
            var uploadedFile = await UploadAsync(bucketOrMainDirectory, prefix, file);
            uploadedFiles.Add(uploadedFile);
        }

        return uploadedFiles;
    }
}