﻿// <auto-generated />
using System;
using LinkoChat.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LinkoChat.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240711143511_EditQueueTime")]
    partial class EditQueueTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("LinkoChat.Domain.Models.Chat", b =>
                {
                    b.Property<Guid>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<long>("ConnectedTo")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("StateId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Location", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("char(36)");

                    b.Property<double?>("Latitude")
                        .HasColumnType("double");

                    b.Property<double?>("Longitude")
                        .HasColumnType("double");

                    b.Property<Guid?>("StateId")
                        .HasColumnType("char(36)");

                    b.HasKey("UserId");

                    b.HasIndex("CityId");

                    b.HasIndex("StateId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("char(36)");

                    b.Property<long>("From")
                        .HasColumnType("bigint");

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<long>("To")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Profile", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Age")
                        .HasColumnType("int");

                    b.Property<string>("CallerUsername")
                        .HasColumnType("longtext");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsCompletedProfile")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsFirstStart")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaritalStatus")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Picture")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("SameAgeSearch")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("SameStateSearch")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Queue", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsSearching")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RequestedGender")
                        .HasColumnType("int");

                    b.Property<DateTime>("SearchingFrom")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId");

                    b.ToTable("Queues");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Statements");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("UserId"));

                    b.Property<int>("Coin")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsInChat")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRegistered")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("StepStatus")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Chat", b =>
                {
                    b.HasOne("LinkoChat.Domain.Models.User", "User")
                        .WithMany("Chats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.City", b =>
                {
                    b.HasOne("LinkoChat.Domain.Models.State", "State")
                        .WithMany("Cities")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Location", b =>
                {
                    b.HasOne("LinkoChat.Domain.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("LinkoChat.Domain.Models.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId");

                    b.HasOne("LinkoChat.Domain.Models.User", "User")
                        .WithOne("Location")
                        .HasForeignKey("LinkoChat.Domain.Models.Location", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("State");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Message", b =>
                {
                    b.HasOne("LinkoChat.Domain.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Profile", b =>
                {
                    b.HasOne("LinkoChat.Domain.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("LinkoChat.Domain.Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Queue", b =>
                {
                    b.HasOne("LinkoChat.Domain.Models.User", "User")
                        .WithOne("Queue")
                        .HasForeignKey("LinkoChat.Domain.Models.Queue", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.Chat", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.State", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("LinkoChat.Domain.Models.User", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("Profile")
                        .IsRequired();

                    b.Navigation("Queue");
                });
#pragma warning restore 612, 618
        }
    }
}
