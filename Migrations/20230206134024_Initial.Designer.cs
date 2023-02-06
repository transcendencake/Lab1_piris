﻿// <auto-generated />
using System;
using Lab1_piris.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab1piris.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230206134024_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lab1_piris.Data.Models.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Lab1_piris.Data.Models.Client", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("BirthPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("DisabilityId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FamilyStateId")
                        .HasColumnType("bigint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomePhone")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("IsMale")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LivingAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("LivingCityId")
                        .HasColumnType("bigint");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhone")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<decimal?>("MonthIncome")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PassportId")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<DateTime>("PassportIssuedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PassportIssuedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassportNumber")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("PassportSeries")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Pensioner")
                        .HasColumnType("bit");

                    b.Property<string>("PlaceOfWork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RegistrationCityId")
                        .HasColumnType("bigint");

                    b.Property<string>("WorkingPosition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("СitizenshipId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DisabilityId");

                    b.HasIndex("FamilyStateId");

                    b.HasIndex("LivingCityId");

                    b.HasIndex("RegistrationCityId");

                    b.HasIndex("СitizenshipId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Lab1_piris.Data.Models.Disability", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Disabilities");
                });

            modelBuilder.Entity("Lab1_piris.Data.Models.FamilyState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FamilyStates");
                });

            modelBuilder.Entity("Lab1_piris.Data.Models.Сitizenship", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Сitizenship");
                });

            modelBuilder.Entity("Lab1_piris.Data.Models.Client", b =>
                {
                    b.HasOne("Lab1_piris.Data.Models.Disability", "Disability")
                        .WithMany()
                        .HasForeignKey("DisabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab1_piris.Data.Models.FamilyState", "FamilyState")
                        .WithMany()
                        .HasForeignKey("FamilyStateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab1_piris.Data.Models.City", "LivingCity")
                        .WithMany()
                        .HasForeignKey("LivingCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab1_piris.Data.Models.City", "RegistrationCity")
                        .WithMany()
                        .HasForeignKey("RegistrationCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab1_piris.Data.Models.Сitizenship", "Сitizenship")
                        .WithMany()
                        .HasForeignKey("СitizenshipId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disability");

                    b.Navigation("FamilyState");

                    b.Navigation("LivingCity");

                    b.Navigation("RegistrationCity");

                    b.Navigation("Сitizenship");
                });
#pragma warning restore 612, 618
        }
    }
}
