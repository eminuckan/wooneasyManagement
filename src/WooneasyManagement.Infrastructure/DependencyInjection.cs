using Microsoft.Extensions.DependencyInjection;
using WooneasyManagement.Application.Interfaces.Storage;
using WooneasyManagement.Infrastructure.Services.Storage;

namespace WooneasyManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddSingleton<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddSingleton<IStorage, T>();
        }
    }
}
