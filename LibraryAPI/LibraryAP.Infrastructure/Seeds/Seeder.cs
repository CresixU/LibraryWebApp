using LibraryAPI.Infrastructure.Seeds.Seeds;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryAPI.Infrastructure.Seeds
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterSeeds(this IServiceCollection service)
        {
            service.AddScoped<ISeed, RoleSeeder>();
            service.AddScoped<ISeed, CategorySeeder>();
            service.AddScoped<ISeed, BookSeeder>();
            service.AddScoped<ISeed, UserSeeder>();
            service.AddScoped<ISeedService, SeedService>();

            return service;
        }
    }
}
