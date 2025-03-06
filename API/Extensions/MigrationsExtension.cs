using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class MigrationsExtension
    {
        public static void ApplyMigrations(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<VideoDBContext>();

            dbContext.Database.Migrate();
        }
    }
}
