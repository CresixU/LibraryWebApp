namespace LibraryAPI.Data.Extensions
{
    public static class SeedExtensions
    {
        public static async Task ExecuteSeed(this IServiceCollection serviceCollection)
        {
            using var scope = serviceCollection.BuildServiceProvider().CreateScope();

            var seedService = scope.ServiceProvider
                .GetRequiredService<ISeedService>();

            await seedService.ExecuteSeeds();
        }
    }
}
