using Microsoft.EntityFrameworkCore;

namespace Migartions.Helpers
{
    public class DbContextHelper
    {
        public static async Task ApplyMigrations(DbContext context)
        {
            await context.Database.MigrateAsync();
        }
    }
}
