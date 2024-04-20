using MediatR;
using Microsoft.EntityFrameworkCore;
using WooneasyManagement.Application.Common;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Application.Common.Interfaces.Storage;

namespace WooneasyManagement.Application.Files.Commands;

public class DeleteFilesCommand : IRequest<Result>
{
    public required List<string> FileNames { get; set; }
    public required string BucketOrMainDirectory { get; set; }
    public required string Prefix { get; set; }
}
public class DeleteFilesCommandHandler(IStorageService storageService , IApplicationDbContext context) : IRequestHandler<DeleteFilesCommand, Result>
{
    public async Task<Result> Handle(DeleteFilesCommand request, CancellationToken cancellationToken)
    {
        await storageService.DeleteAsync(request.BucketOrMainDirectory, request.Prefix, request.FileNames);

        foreach (var fileName in request.FileNames)
        {
            try
            {
                var file = await context.Files.FirstAsync(f => f.FileName == fileName,
                    cancellationToken: cancellationToken);

                context.Files.Remove(file); 
                await context.SaveChangesAsync(cancellationToken);
                
            }
            catch (Exception e)
            {
                return Result.Fail(FileErrors.FileNotFoundError);
            }
        }

        return Result.Ok();
    }
}