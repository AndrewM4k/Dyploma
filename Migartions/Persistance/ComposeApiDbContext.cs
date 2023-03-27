using Microsoft.EntityFrameworkCore;
using Migartions.Models;

namespace Migartions.Persistance
{
    public class ComposeApiDbContext : DbContext
    {
        public DbSet<Sportsmen> Sportsmens { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ComposeApiDbContext(DbContextOptions<ComposeApiDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sportsmen>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Employee>()
                .HasKey(e => e.Id);
        }
    }
}
