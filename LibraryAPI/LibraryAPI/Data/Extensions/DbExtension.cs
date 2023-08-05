using LibraryAPI.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data.Extensions
{
    public static class DbExtension
    {
        public static async Task MigrateDatabase(this IServiceCollection serviceCollection)
        {
            using var scope = serviceCollection.BuildServiceProvider().CreateScope();

            var dbContext = scope.ServiceProvider
                .GetRequiredService<LibraryContext>();

            await dbContext.Database.MigrateAsync();
        }
    }
}
