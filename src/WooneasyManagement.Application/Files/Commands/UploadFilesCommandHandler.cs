using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Files.Interfaces;
using File = WooneasyManagement.Domain.Entities.File;

namespace WooneasyManagement.Application.Files.Commands;

public class UploadFilesCommand : IRequest<Result>
{
    public required string BucketOrMainDirectory { get; set; }
    public string? Prefix { get; set; }
    public required IFormFileCollection Files { get; set; }
}

public class UploadFilesCommandHandler(
    IMapper mapper,
    IFileService<File> fileService) : IRequestHandler<UploadFilesCommand, Result>
{
    public async Task<Result> Handle(UploadFilesCommand request, CancellationToken cancellationToken)
    {
        var uploadedFiles = await fileService.UploadAsync(request.BucketOrMainDirectory, request.Prefix, request.Files);

        List<FileInfoDto> infos = mapper.Map<List<FileInfoDto>>(uploadedFiles);

        return Result<List<FileInfoDto>>.Ok(infos);
    }
}