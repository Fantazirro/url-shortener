using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data;

namespace UrlShortener.Api.Helpers
{
    public static class DatabaseMigrator
    {
        public static void ApplyMigration(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}