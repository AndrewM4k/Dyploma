﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Migartions.Persistance;

#nullable disable

namespace Migartions.Migrations
{
    [DbContext(typeof(ComposeApiDbContext))]
    [Migration("20230401211145_Database_creation")]
    partial class Database_creation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Migartions.Models.Competition", b =>
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

            modelBuilder.Entity("Migartions.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Migartions.Models.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateofStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("Migartions.Models.EventCompetition", b =>
                {
                    b.Property<Guid>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompetitionId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("EventCompetition");
                });

            modelBuilder.Entity("Migartions.Models.Movement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.ToTable("Movement");
                });

            modelBuilder.Entity("Migartions.Models.Sportsman", b =>
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

                    b.HasKey("Id");

                    b.ToTable("Sportsmens");
                });

            modelBuilder.Entity("Migartions.Models.SportsmanCompetition", b =>
                {
                    b.Property<Guid>("CompetitionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SportsmanId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompetitionId", "SportsmanId");

                    b.HasIndex("SportsmanId");

                    b.ToTable("SportsmanCompetition");
                });

            modelBuilder.Entity("Migartions.Models.Employee", b =>
                {
                    b.HasOne("Migartions.Models.Event", null)
                        .WithMany("Employees")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("Migartions.Models.EventCompetition", b =>
                {
                    b.HasOne("Migartions.Models.Competition", null)
                        .WithMany("EventCompetitions")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Migartions.Models.Event", null)
                        .WithMany("EventCompetitions")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Migartions.Models.Movement", b =>
                {
                    b.HasOne("Migartions.Models.Competition", null)
                        .WithMany("Movements")
                        .HasForeignKey("CompetitionId");
                });

            modelBuilder.Entity("Migartions.Models.SportsmanCompetition", b =>
                {
                    b.HasOne("Migartions.Models.Competition", null)
                        .WithMany("SportsmanCompetitions")
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Migartions.Models.Sportsman", null)
                        .WithMany("SportsmanCompetitions")
                        .HasForeignKey("SportsmanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Migartions.Models.Competition", b =>
                {
                    b.Navigation("EventCompetitions");

                    b.Navigation("Movements");

                    b.Navigation("SportsmanCompetitions");
                });

            modelBuilder.Entity("Migartions.Models.Event", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("EventCompetitions");
                });

            modelBuilder.Entity("Migartions.Models.Sportsman", b =>
                {
                    b.Navigation("SportsmanCompetitions");
                });
#pragma warning restore 612, 618
        }
    }
}