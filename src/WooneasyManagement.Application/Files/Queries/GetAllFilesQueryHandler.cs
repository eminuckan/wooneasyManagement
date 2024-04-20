using AutoMapper;
using MediatR;
using System.Collections.Generic;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;

namespace WooneasyManagement.Application.Files.Queries;

public class GetAllFilesQuery : IRequest<Result>
{
    public string? BucketOrMainDirectory { get; set; }
    public string? Prefix { get; set; }
}

public class GetAllFilesQueryHandler(IMapper mapper, IApplicationDbContext context)
    : IRequestHandler<GetAllFilesQuery, Result>
{
    public async Task<Result> Handle(GetAllFilesQuery request, CancellationToken cancellationToken)
    {
        var files = context.Files.Where(f =>
            (string.IsNullOrEmpty(request.BucketOrMainDirectory) ||
             f.BucketOrMainDirectory == request.BucketOrMainDirectory) &&
            (string.IsNullOrEmpty(request.Prefix) || f.FilePath == request.Prefix)).ToList();

        List<FileInfoDto> dtoData = mapper.Map<List<FileInfoDto>>(files);

        return Result<List<FileInfoDto>>.Ok(dtoData);
    }
}