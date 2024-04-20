using Microsoft.Extensions.DependencyInjection;
using WooneasyManagement.Application.Common.Interfaces.Storage;
using WooneasyManagement.Infrastructure.Services.Storage;

namespace WooneasyManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
