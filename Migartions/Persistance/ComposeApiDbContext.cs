using Microsoft.EntityFrameworkCore;
using Migartions.Models;

namespace Migartions.Persistance
{
    public class ComposeApiDbContext : DbContext
    {
        public DbSet<Sportsman> Sportsmens { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<Standart> Standart { get; set; }
        public DbSet<Movement> Movement { get; set; }
        public DbSet<Competition> Competition { get; set; }

        public ComposeApiDbContext(DbContextOptions<ComposeApiDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Competitions)
                .WithMany(c => c.Events)
                .UsingEntity<EventCompetition>(
                    eevent => eevent
                        .HasOne<Competition>()
                        .WithMany(c => c.EventCompetitions)
                        .HasForeignKey(ec => ec.CompetitionId),
                    competition => competition
                        .HasOne<Event>()
                        .WithMany(e => e.EventCompetitions)
                        .HasForeignKey(ec => ec.EventId)
                    );

            modelBuilder.Entity<Sportsman>()
                .HasMany(s => s.Competitions)
                .WithMany(c => c.Sportsmans)
                .UsingEntity<SportsmanCompetition>(
                    sportsmen => sportsmen
                        .HasOne<Competition>()
                        .WithMany(c => c.SportsmanCompetitions)
                        .HasForeignKey(sc => sc.CompetitionId),
                    competition => competition
                        .HasOne<Sportsman>()
                        .WithMany(s => s.SportsmanCompetitions)
                        .HasForeignKey(sc => sc.SportsmanId)
                    );

            modelBuilder.Entity<Competition>()
                .HasMany(c => c.Movements)
                .WithMany(m => m.Competitions)
                .UsingEntity<MovementCompetition>(
                    competition => competition
                        .HasOne<Movement>()
                        .WithMany(c => c.MovementCompetitions)
                        .HasForeignKey(mc => mc.MovementId),
                    movement => movement
                        .HasOne<Competition>()
                        .WithMany(p => p.MovementCompetitions)
                        .HasForeignKey(o => o.CompetitionId)
                    );

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.Events)
                .UsingEntity<EmployeeEvent>(
                    eevent => eevent
                        .HasOne<Employee>()
                        .WithMany(e => e.EmployeeEvents)
                        .HasForeignKey(ee => ee.EmployeeId),
                    competition => competition
                        .HasOne<Event>()
                        .WithMany(e => e.EmployeeEvents)
                        .HasForeignKey(ee => ee.EventId)
                    );

            modelBuilder.Entity<Competition>()
                .HasMany(s => s.Standarts)
                .WithOne(o => o.Competition);

            modelBuilder.Entity<Competition>()
                .HasMany(s => s.Records)
                .WithOne(o => o.Competition);
        }
    }
}
