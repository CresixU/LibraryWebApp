using LibraryAPI.Data.Seeds;

namespace LibraryAPI.Data
{
    public static class DataDependencyInjection
    {
        public static IServiceCollection RegisterSeeds(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ISeed, RoleSeeder>();
            serviceCollection.AddScoped<ISeed, CategorySeeder>();
            serviceCollection.AddScoped<ISeed, BookSeeder>();
            serviceCollection.AddScoped<ISeed, UserSeeder>();
            serviceCollection.AddScoped<ISeedService, SeedService>();

            return serviceCollection;
        }
    }
}
