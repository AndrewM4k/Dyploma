using Microsoft.EntityFrameworkCore;

namespace Migartions.Helpers
{
    public class DbContexthelper
    {
        public static async Task ApplyMigrations(DbContext context)
        {
            await context.Database.MigrateAsync();
        }
    }
}
