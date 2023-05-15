﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportsCompetition.Persistance;

#nullable disable

namespace SportsCompetition.Migrations
{
    [DbContext(typeof(SportCompetitionDbContext))]
    [Migration("20230515184532_StatusAttempt")]
    partial class StatusAttempt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("SportsCompetition.Models.Attempt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AttemptResult")
                        .HasColumnType("int");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<Guid>("SportsmanCompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Weihgt")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SportsmanCompetitionId");

                    b.ToTable("Attempt");
                });

            modelBuilder.Entity("SportsCompetition.Models.Competition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Equpment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Competition");
                });

            modelBuilder.Entity("SportsCompetition.Models.CompetitionRecord", b =>
                {
                    b.Property<Guid>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RecordId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompetitionId", "RecordId");

                    b.HasIndex("RecordId");

                    b.ToTable("CompetitionRecord");
                });

            modelBuilder.Entity("SportsCompetition.Models.CompetitionStandart", b =>
                {
                    b.Property<Guid>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StandartId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompetitionId", "StandartId");

                    b.HasIndex("StandartId");

                    b.ToTable("CompetitionStandart");
                });

            modelBuilder.Entity("SportsCompetition.Models.Decision", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttemptId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("JudgeDecision")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AttemptId");

                    b.ToTable("Decisions");
                });

            modelBuilder.Entity("SportsCompetition.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("SportsCompetition.Models.EmployeeEvent", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeeId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EmployeeEvent");
                });

            modelBuilder.Entity("SportsCompetition.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CurrentStream")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateofStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("SportsCompetition.Models.EventCompetition", b =>
                {
                    b.Property<Guid>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompetitionId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventCompetition");
                });

            modelBuilder.Entity("SportsCompetition.Models.EventSportsman", b =>
                {
                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SportsmanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StreamNumber")
                        .HasColumnType("int");

                    b.HasKey("EventId", "SportsmanId");

                    b.HasIndex("SportsmanId");

                    b.ToTable("EventSportsman");
                });

            modelBuilder.Entity("SportsCompetition.Models.Record", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RecordResult")
                        .HasColumnType("int");

                    b.Property<string>("TypeOfRecord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WeightOfSportsman")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Record");
                });

            modelBuilder.Entity("SportsCompetition.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "Token");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("SportsCompetition.Models.Sportsman", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Sportsmans");
                });

            modelBuilder.Entity("SportsCompetition.Models.SportsmanCompetition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CurrentAttempt")
                        .HasColumnType("int");

                    b.Property<Guid>("SportsmanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("SportsmanId");

                    b.HasIndex("StreamId");

                    b.ToTable("SportsmanCompetition", (string)null);
                });

            modelBuilder.Entity("SportsCompetition.Models.Standart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StandartResult")
                        .HasColumnType("int");

                    b.Property<int>("WeightOfSportsman")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Standart");
                });

            modelBuilder.Entity("SportsCompetition.Models.Stream", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Streams");
                });

            modelBuilder.Entity("SportsCompetition.Models.StreamJudgeEmployee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StreamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("EmployeeId", "StreamId");

                    b.HasIndex("StreamId");

                    b.ToTable("StreamJudgeEmployee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsCompetition.Models.Attempt", b =>
                {
                    b.HasOne("SportsCompetition.Models.SportsmanCompetition", "SportsmanCompetition")
                        .WithMany("Attempts")
                        .HasForeignKey("SportsmanCompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SportsmanCompetition");
                });

            modelBuilder.Entity("SportsCompetition.Models.CompetitionRecord", b =>
                {
                    b.HasOne("SportsCompetition.Models.Competition", null)
                        .WithMany("CompetitionRecords")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsCompetition.Models.Record", null)
                        .WithMany("CompetitionRecords")
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsCompetition.Models.CompetitionStandart", b =>
                {
                    b.HasOne("SportsCompetition.Models.Competition", null)
                        .WithMany("CompetitionStandarts")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsCompetition.Models.Standart", null)
                        .WithMany("CompetitionStandarts")
                        .HasForeignKey("StandartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsCompetition.Models.Decision", b =>
                {
                    b.HasOne("SportsCompetition.Models.Attempt", "Attempt")
                        .WithMany("Decisions")
                        .HasForeignKey("AttemptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attempt");
                });

            modelBuilder.Entity("SportsCompetition.Models.Employee", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportsCompetition.Models.EmployeeEvent", b =>
                {
                    b.HasOne("SportsCompetition.Models.Employee", null)
                        .WithMany("EmployeeEvents")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsCompetition.Models.Event", null)
                        .WithMany("EmployeeEvents")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsCompetition.Models.EventCompetition", b =>
                {
                    b.HasOne("SportsCompetition.Models.Competition", null)
                        .WithMany("EventCompetitions")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsCompetition.Models.Event", null)
                        .WithMany("EventCompetitions")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsCompetition.Models.EventSportsman", b =>
                {
                    b.HasOne("SportsCompetition.Models.Event", null)
                        .WithMany("EventSportsmans")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsCompetition.Models.Sportsman", null)
                        .WithMany("EventSportsmans")
                        .HasForeignKey("SportsmanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsCompetition.Models.RefreshToken", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportsCompetition.Models.Sportsman", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser<System.Guid>", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SportsCompetition.Models.SportsmanCompetition", b =>
                {
                    b.HasOne("SportsCompetition.Models.Competition", null)
                        .WithMany("SportsmanCompetitions")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsCompetition.Models.Sportsman", null)
                        .WithMany("SportsmanCompetitions")
                        .HasForeignKey("SportsmanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsCompetition.Models.Stream", "Stream")
                        .WithMany("SportsmanCompetitions")
                        .HasForeignKey("StreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stream");
                });

            modelBuilder.Entity("SportsCompetition.Models.Stream", b =>
                {
                    b.HasOne("SportsCompetition.Models.Event", "Event")
                        .WithMany("Shedule")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("SportsCompetition.Models.StreamJudgeEmployee", b =>
                {
                    b.HasOne("SportsCompetition.Models.Stream", null)
                        .WithMany("StreamJudgeEmployees")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsCompetition.Models.Employee", null)
                        .WithMany("StreamJudgeEmployees")
                        .HasForeignKey("StreamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsCompetition.Models.Attempt", b =>
                {
                    b.Navigation("Decisions");
                });

            modelBuilder.Entity("SportsCompetition.Models.Competition", b =>
                {
                    b.Navigation("CompetitionRecords");

                    b.Navigation("CompetitionStandarts");

                    b.Navigation("EventCompetitions");

                    b.Navigation("SportsmanCompetitions");
                });

            modelBuilder.Entity("SportsCompetition.Models.Employee", b =>
                {
                    b.Navigation("EmployeeEvents");

                    b.Navigation("StreamJudgeEmployees");
                });

            modelBuilder.Entity("SportsCompetition.Models.Event", b =>
                {
                    b.Navigation("EmployeeEvents");

                    b.Navigation("EventCompetitions");

                    b.Navigation("EventSportsmans");

                    b.Navigation("Shedule");
                });

            modelBuilder.Entity("SportsCompetition.Models.Record", b =>
                {
                    b.Navigation("CompetitionRecords");
                });

            modelBuilder.Entity("SportsCompetition.Models.Sportsman", b =>
                {
                    b.Navigation("EventSportsmans");

                    b.Navigation("SportsmanCompetitions");
                });

            modelBuilder.Entity("SportsCompetition.Models.SportsmanCompetition", b =>
                {
                    b.Navigation("Attempts");
                });

            modelBuilder.Entity("SportsCompetition.Models.Standart", b =>
                {
                    b.Navigation("CompetitionStandarts");
                });

            modelBuilder.Entity("SportsCompetition.Models.Stream", b =>
                {
                    b.Navigation("SportsmanCompetitions");

                    b.Navigation("StreamJudgeEmployees");
                });
#pragma warning restore 612, 618
        }
    }
}
