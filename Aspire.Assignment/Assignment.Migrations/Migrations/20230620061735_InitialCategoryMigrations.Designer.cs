﻿// <auto-generated />
using System;
using Assignment.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment.Migrations.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230620061735_InitialCategoryMigrations")]
    partial class InitialCategoryMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("Assignment.Contracts.Data.Entities.App", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("App");
                });

            modelBuilder.Entity("Assignment.Contracts.Data.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("5cce7a1e-34ae-4f2b-ab0b-415519a8b908"),
                            AddedOn = new DateTime(2023, 6, 20, 6, 17, 35, 51, DateTimeKind.Utc).AddTicks(7411),
                            Type = "CRM"
                        },
                        new
                        {
                            Id = new Guid("c6f9b020-bebf-4f5a-b880-201e12ab6b26"),
                            AddedOn = new DateTime(2023, 6, 20, 6, 17, 35, 51, DateTimeKind.Utc).AddTicks(7414),
                            Type = "Cloud"
                        },
                        new
                        {
                            Id = new Guid("2fb2c391-0bc6-40a9-b85c-d5196864762e"),
                            AddedOn = new DateTime(2023, 6, 20, 6, 17, 35, 51, DateTimeKind.Utc).AddTicks(7416),
                            Type = "Client Side Scripting"
                        },
                        new
                        {
                            Id = new Guid("5e7dcdbc-bdea-4805-b454-a8761823e1c9"),
                            AddedOn = new DateTime(2023, 6, 20, 6, 17, 35, 51, DateTimeKind.Utc).AddTicks(7417),
                            Type = "Programming Language"
                        });
                });

            modelBuilder.Entity("Assignment.Contracts.Data.Entities.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("OperationAccess")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Operation", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OperationAccess = "ADD,EDIT,VIEW,DELETE"
                        },
                        new
                        {
                            Id = 2,
                            AddedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            OperationAccess = "ADD,VIEW"
                        });
                });

            modelBuilder.Entity("Assignment.Contracts.Data.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Assignment.Contracts.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "ADMIN"
                        },
                        new
                        {
                            Id = 2,
                            AddedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "GUEST"
                        },
                        new
                        {
                            Id = 3,
                            AddedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "DEVELOPER"
                        },
                        new
                        {
                            Id = 4,
                            AddedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "MANAGER"
                        });
                });

            modelBuilder.Entity("Assignment.Contracts.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastPasswordChange")
                        .HasColumnType("TEXT");

                    b.Property<int>("OperationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.HasIndex("RoleId");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 5,
                            AddedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "Admin@ayi.com",
                            OperationId = 1,
                            Password = "AQAAAAEAACcQAAAAEK7fK9rklmNwB8J395U3LgJhevQEwGd/RazdOjguuNDbCnNIFoP9V8fq5hxckoPQew==",
                            RoleId = 1,
                            Username = "Admin"
                        });
                });

            modelBuilder.Entity("Assignment.Contracts.Data.Entities.User", b =>
                {
                    b.HasOne("Assignment.Contracts.Data.Entities.Operation", "Operation")
                        .WithMany()
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment.Contracts.Data.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operation");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}