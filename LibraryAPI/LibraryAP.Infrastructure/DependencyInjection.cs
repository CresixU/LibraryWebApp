using LibraryAPI.Infrastructure.Context;
using LibraryAPI.Infrastructure.Extensions;
using LibraryAPI.Infrastructure.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<LibraryContext>(
            option => option.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnectionString"))
            );
            /*service.AddControllers().AddJsonOptions(
                option => option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve
                );*/
            //await builder.Services.MigrateDatabase();
            builder.Services.RegisterSeeds();
            //await builder.Services.ExecuteSeed();
            return builder.Services;
        }
    }
}