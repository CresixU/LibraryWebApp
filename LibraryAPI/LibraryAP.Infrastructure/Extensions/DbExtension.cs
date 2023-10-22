using LibraryAPI.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryAPI.Infrastructure.Extensions
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
