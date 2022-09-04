﻿// <auto-generated />
using System;
using BookTrip.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookTrip.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220712105345_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookTrip.Models.Bed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bed Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfSleepingSpots")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Beds");

                    b.HasDiscriminator<string>("Bed Type").HasValue("Bed");
                });

            modelBuilder.Entity("BookTrip.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePerNight")
                        .HasColumnType("int");

                    b.Property<string>("PropertyTypeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StarRating")
                        .HasColumnType("int");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PropertyTypeType");

                    b.HasIndex("UserId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("BookTrip.Models.PropertyType", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Type");

                    b.ToTable("PropertyTypes");
                });

            modelBuilder.Entity("BookTrip.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BookTrip.Models.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"), 1L, 1);

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.Property<bool>("IsBooked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCheckedIn")
                        .HasColumnType("bit");

                    b.Property<int?>("NumberOfGuestsLivingIn")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomId");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BookTrip.Models.RoomBed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AmountOfBeds")
                        .HasColumnType("int");

                    b.Property<int>("BedId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BedId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomBed");
                });

            modelBuilder.Entity("BookTrip.Models.RoomReservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationEndsAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReservationStartsAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("RoomReservations");
                });

            modelBuilder.Entity("BookTrip.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserSettings")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserSettings")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookTrip.Models.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("BookTrip.Models.BedTypes.CustomBed", b =>
                {
                    b.HasBaseType("BookTrip.Models.Bed");

                    b.HasDiscriminator().HasValue("Custom Bed");
                });

            modelBuilder.Entity("BookTrip.Models.BedTypes.FullBed", b =>
                {
                    b.HasBaseType("BookTrip.Models.Bed");

                    b.HasDiscriminator().HasValue("Full Bed");
                });

            modelBuilder.Entity("BookTrip.Models.BedTypes.SingleBed", b =>
                {
                    b.HasBaseType("BookTrip.Models.Bed");

                    b.HasDiscriminator().HasValue("Single Bed");
                });

            modelBuilder.Entity("BookTrip.Models.BedTypes.SofaBed", b =>
                {
                    b.HasBaseType("BookTrip.Models.Bed");

                    b.HasDiscriminator().HasValue("Sofa Bed");
                });

            modelBuilder.Entity("BookTrip.Models.BedTypes.TwinBed", b =>
                {
                    b.HasBaseType("BookTrip.Models.Bed");

                    b.HasDiscriminator().HasValue("Twin Bed");
                });

            modelBuilder.Entity("BookTrip.Models.Hotel", b =>
                {
                    b.HasOne("BookTrip.Models.PropertyType", "PropertyType")
                        .WithMany("Hotels")
                        .HasForeignKey("PropertyTypeType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookTrip.Models.User", "User")
                        .WithMany("Hotels")
                        .HasForeignKey("UserId");

                    b.Navigation("PropertyType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookTrip.Models.Review", b =>
                {
                    b.HasOne("BookTrip.Models.Hotel", "Hotel")
                        .WithMany("Reviews")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookTrip.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookTrip.Models.Room", b =>
                {
                    b.HasOne("BookTrip.Models.Hotel", "Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("BookTrip.Models.RoomBed", b =>
                {
                    b.HasOne("BookTrip.Models.Bed", "Bed")
                        .WithMany("RoomBeds")
                        .HasForeignKey("BedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookTrip.Models.Room", "Room")
                        .WithMany("RoomBeds")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bed");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("BookTrip.Models.RoomReservation", b =>
                {
                    b.HasOne("BookTrip.Models.Room", "Room")
                        .WithMany("RoomReservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookTrip.Models.User", "User")
                        .WithMany("RoomReservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookTrip.Models.User", b =>
                {
                    b.HasOne("BookTrip.Models.UserSettings", "Settings")
                        .WithOne("User")
                        .HasForeignKey("BookTrip.Models.User", "UserSettings")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Settings");
                });

            modelBuilder.Entity("BookTrip.Models.Bed", b =>
                {
                    b.Navigation("RoomBeds");
                });

            modelBuilder.Entity("BookTrip.Models.Hotel", b =>
                {
                    b.Navigation("Reviews");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("BookTrip.Models.PropertyType", b =>
                {
                    b.Navigation("Hotels");
                });

            modelBuilder.Entity("BookTrip.Models.Room", b =>
                {
                    b.Navigation("RoomBeds");

                    b.Navigation("RoomReservations");
                });

            modelBuilder.Entity("BookTrip.Models.User", b =>
                {
                    b.Navigation("Hotels");

                    b.Navigation("Reviews");

                    b.Navigation("RoomReservations");
                });

            modelBuilder.Entity("BookTrip.Models.UserSettings", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}