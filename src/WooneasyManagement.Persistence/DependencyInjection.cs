using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WooneasyManagement.Application.Amenities.Interfaces;
using WooneasyManagement.Application.Common.Data;
using WooneasyManagement.Application.Files.Interfaces;
using WooneasyManagement.Persistence.Amenities;
using WooneasyManagement.Persistence.Contexts;
using WooneasyManagement.Persistence.Services;

namespace WooneasyManagement.Persistence;

public static class DependencyInjection
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<IApplicationDbContext, WooneasyManagementDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddScoped<IAmenityWriteService, AmenityWriteService>();
        services.AddScoped(typeof(IFileService<>), typeof(FileService<>));
    }
}