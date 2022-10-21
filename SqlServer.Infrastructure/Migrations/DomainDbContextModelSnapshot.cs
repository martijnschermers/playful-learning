﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SqlServer.Infrastructure;

#nullable disable

namespace SqlServer.Infrastructure.Migrations
{
    [DbContext(typeof(DomainDbContext))]
    partial class DomainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Core.Domain.Allergy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("UserId");

                    b.ToTable("Allergies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Gluten",
                            Name = 4
                        },
                        new
                        {
                            Id = 2,
                            Description = "Lactose",
                            Name = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Noten",
                            Name = 0
                        },
                        new
                        {
                            Id = 4,
                            Description = "Soja",
                            Name = 2
                        },
                        new
                        {
                            Id = 5,
                            Description = "Tarwe",
                            Name = 3
                        },
                        new
                        {
                            Id = 6,
                            Description = "Vegetarisch",
                            Name = 5
                        });
                });

            modelBuilder.Entity("Core.Domain.Drink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("ContainsAlcohol")
                        .HasColumnType("bit");

                    b.Property<int?>("GameNightId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameNightId");

                    b.ToTable("Drinks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContainsAlcohol = true,
                            Name = "Bier"
                        },
                        new
                        {
                            Id = 2,
                            ContainsAlcohol = false,
                            Name = "Water"
                        },
                        new
                        {
                            Id = 3,
                            ContainsAlcohol = false,
                            Name = "Cola"
                        },
                        new
                        {
                            Id = 4,
                            ContainsAlcohol = true,
                            Name = "Wijn"
                        },
                        new
                        {
                            Id = 5,
                            ContainsAlcohol = false,
                            Name = "Fanta"
                        });
                });

            modelBuilder.Entity("Core.Domain.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GameNightId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameNightId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("Core.Domain.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameNightId")
                        .HasColumnType("int");

                    b.Property<int>("Genre")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOnlyForAdults")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameNightId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Versimpelde versie van pesten!",
                            Genre = 0,
                            Image = "",
                            IsOnlyForAdults = false,
                            Name = "Uno",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Description = "Het spel met treinen!",
                            Genre = 0,
                            Image = "",
                            IsOnlyForAdults = false,
                            Name = "Ticket to Ride",
                            Type = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Het spel met geld.",
                            Genre = 0,
                            Image = "",
                            IsOnlyForAdults = false,
                            Name = "Monopoly",
                            Type = 1
                        });
                });

            modelBuilder.Entity("Core.Domain.GameNight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOnlyForAdults")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPotluck")
                        .HasColumnType("bit");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OrganizerId");

                    b.ToTable("GameNights");
                });

            modelBuilder.Entity("Core.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameNightId")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("GameNightId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Core.Domain.Allergy", b =>
                {
                    b.HasOne("Core.Domain.Food", null)
                        .WithMany("Allergies")
                        .HasForeignKey("FoodId");

                    b.HasOne("Core.Domain.User", null)
                        .WithMany("Allergies")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Core.Domain.Drink", b =>
                {
                    b.HasOne("Core.Domain.GameNight", null)
                        .WithMany("Drinks")
                        .HasForeignKey("GameNightId");
                });

            modelBuilder.Entity("Core.Domain.Food", b =>
                {
                    b.HasOne("Core.Domain.GameNight", null)
                        .WithMany("Foods")
                        .HasForeignKey("GameNightId");
                });

            modelBuilder.Entity("Core.Domain.Game", b =>
                {
                    b.HasOne("Core.Domain.GameNight", null)
                        .WithMany("Games")
                        .HasForeignKey("GameNightId");
                });

            modelBuilder.Entity("Core.Domain.GameNight", b =>
                {
                    b.HasOne("Core.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.User", "Organizer")
                        .WithMany()
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Organizer");
                });

            modelBuilder.Entity("Core.Domain.User", b =>
                {
                    b.HasOne("Core.Domain.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.GameNight", null)
                        .WithMany("Players")
                        .HasForeignKey("GameNightId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Core.Domain.Food", b =>
                {
                    b.Navigation("Allergies");
                });

            modelBuilder.Entity("Core.Domain.GameNight", b =>
                {
                    b.Navigation("Drinks");

                    b.Navigation("Foods");

                    b.Navigation("Games");

                    b.Navigation("Players");
                });

            modelBuilder.Entity("Core.Domain.User", b =>
                {
                    b.Navigation("Allergies");
                });
#pragma warning restore 612, 618
        }
    }
}
