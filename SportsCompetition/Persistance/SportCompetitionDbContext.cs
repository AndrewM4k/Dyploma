using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsCompetition.Enums;
using SportsCompetition.Models;

namespace SportsCompetition.Persistance
{
    public class SportCompetitionDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Sportsman> Sportsmans { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<Standart> Standart { get; set; }
        public DbSet<Competition> Competition { get; set; }
        public DbSet<SportsmanCompetition> SportsmanCompetition { get; set; }
        public DbSet<Models.Stream> Streams { get; set; }
        public DbSet<Attempt> Attempt { get; set; }

        public SportCompetitionDbContext(DbContextOptions<SportCompetitionDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SportsmanCompetition>()
                .ToTable("SportsmanCompetition");

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Competitions)
                .WithMany(c => c.Events)
                .UsingEntity<EventCompetition>(
                    @event => @event
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

            modelBuilder.Entity<Sportsman>()
                .Property(s => s.Gender)
                .HasConversion(new EnumToStringConverter<Gender>());

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Employees)
                .WithMany(e => e.Events)
                .UsingEntity<EmployeeEvent>(
                    @event => @event
                        .HasOne<Models.Employee>()
                        .WithMany(e => e.EmployeeEvents)
                        .HasForeignKey(ee => ee.EmployeeId),
                    competition => competition
                        .HasOne<Event>()
                        .WithMany(e => e.EmployeeEvents)
                        .HasForeignKey(ee => ee.EventId)
                    );

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Sportsmans)
                .WithMany(e => e.Events)
                .UsingEntity<EventSportsman>(
            @event => @event
                        .HasOne<Sportsman>()
                        .WithMany(e => e.EventSportsmans)
                        .HasForeignKey(ee => ee.SportsmanId),
                    competition => competition
                        .HasOne<Event>()
                        .WithMany(e => e.EventSportsmans)
                        .HasForeignKey(ee => ee.EventId)
                    );

            modelBuilder.Entity<Competition>()
               .HasMany(e => e.Standarts)
               .WithMany(e => e.Competitions)
               .UsingEntity<CompetitionStandart>(
           competition => competition
                       .HasOne<Standart>()
                       .WithMany(c => c.CompetitionStandarts)
                       .HasForeignKey(c => c.StandartId),
                   standart => standart
                       .HasOne<Competition>()
                       .WithMany(s => s.CompetitionStandarts)
                       .HasForeignKey(s => s.CompetitionId)
                   );

            modelBuilder.Entity<Standart>()
                .Property(s => s.Gender)
                .HasConversion(new EnumToStringConverter<Gender>());

            modelBuilder.Entity<Competition>()
               .HasMany(e => e.Records)
               .WithMany(e => e.Competitions)
               .UsingEntity<CompetitionRecord>(
                   competition => competition
                       .HasOne<Record>()
                       .WithMany(c => c.CompetitionRecords)
                       .HasForeignKey(c => c.RecordId),
                   record => record
                       .HasOne<Competition>()
                       .WithMany(r => r.CompetitionRecords)
                       .HasForeignKey(s => s.CompetitionId)
                   );

            modelBuilder.Entity<Record>()
                .Property(s => s.Gender)
                .HasConversion(new EnumToStringConverter<Gender>());

            modelBuilder.Entity<SportsmanCompetition>()
                .HasMany(s => s.Attempts)
                .WithOne(a => a.SportsmanCompetition);

            modelBuilder.Entity<Models.Stream>()
                .HasMany(s => s.SportsmanCompetitions)
                .WithOne(o => o.Stream);

            modelBuilder.Entity<Models.Stream>()
               .HasMany(s => s.Employees)
               .WithMany(e => e.Streams)
               .UsingEntity<StreamJudgeEmployee>(
                   employee => employee
                       .HasOne<Employee>()
                       .WithMany(e => e.StreamJudgeEmployees)
                       .HasForeignKey(c => c.StreamId),
                   stream => stream
                       .HasOne<Models.Stream>()
                       .WithMany(s => s.StreamJudgeEmployees)
                       .HasForeignKey(s => s.EmployeeId)
                   );

            modelBuilder.Entity<Event>()
                .HasMany(s => s.Shedule)
                .WithOne(o => o.Event);

            modelBuilder.Entity<RefreshToken>()
                .HasKey(t => new { t.UserId, t.Token });

            modelBuilder.Entity<Attempt>()
                .HasMany(a => a.Decisions)
                .WithOne(d => d.Attempt);
        }
    }
}
