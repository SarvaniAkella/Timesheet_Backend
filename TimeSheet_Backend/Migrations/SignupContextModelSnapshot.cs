﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeSheet_Backend.Models;

#nullable disable

namespace TimeSheet_Backend.Migrations
{
    [DbContext(typeof(SignupContext))]
    partial class SignupContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TimeSheet_Backend.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActivityId"));

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActivityId");

                    b.ToTable("Activities");

                    b.HasData(
                        new
                        {
                            ActivityId = 1,
                            ActivityName = "Unit Testing"
                        },
                        new
                        {
                            ActivityId = 2,
                            ActivityName = "Acceptance Testing"
                        },
                        new
                        {
                            ActivityId = 3,
                            ActivityName = "Warranty/MC"
                        },
                        new
                        {
                            ActivityId = 4,
                            ActivityName = "System Testing"
                        },
                        new
                        {
                            ActivityId = 5,
                            ActivityName = "Coding/Implementation"
                        },
                        new
                        {
                            ActivityId = 6,
                            ActivityName = "Design"
                        },
                        new
                        {
                            ActivityId = 7,
                            ActivityName = "Support"
                        },
                        new
                        {
                            ActivityId = 8,
                            ActivityName = "Integration Testing"
                        },
                        new
                        {
                            ActivityId = 9,
                            ActivityName = "Requirements Development"
                        },
                        new
                        {
                            ActivityId = 10,
                            ActivityName = "Planning"
                        },
                        new
                        {
                            ActivityId = 11,
                            ActivityName = "PTO"
                        });
                });

            modelBuilder.Entity("TimeSheet_Backend.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            ProjectId = 1,
                            ProjectName = "Persona Nutrition"
                        },
                        new
                        {
                            ProjectId = 2,
                            ProjectName = "Puritains"
                        },
                        new
                        {
                            ProjectId = 3,
                            ProjectName = "Nestle Health Sciences"
                        },
                        new
                        {
                            ProjectId = 4,
                            ProjectName = "Market Central"
                        },
                        new
                        {
                            ProjectId = 5,
                            ProjectName = "Family Central"
                        },
                        new
                        {
                            ProjectId = 6,
                            ProjectName = "Internal POC"
                        },
                        new
                        {
                            ProjectId = 7,
                            ProjectName = "External POC"
                        },
                        new
                        {
                            ProjectId = 8,
                            ProjectName = "Marketing & Sales"
                        });
                });

            modelBuilder.Entity("TimeSheet_Backend.Models.TimeSheet", b =>
                {
                    b.Property<int>("TimeSheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TimeSheetId"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("hours")
                        .HasColumnType("int");

                    b.Property<string>("task")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TimeSheetId");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("TimeSheets");
                });

            modelBuilder.Entity("TimeSheet_Backend.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobileno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("roleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TimeSheet_Backend.Models.role", b =>
                {
                    b.Property<int>("roleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("roleId"));

                    b.Property<string>("roleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("roleId");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            roleId = 1,
                            roleName = "User"
                        },
                        new
                        {
                            roleId = 2,
                            roleName = "Hr"
                        },
                        new
                        {
                            roleId = 3,
                            roleName = "Admin"
                        });
                });

            modelBuilder.Entity("TimeSheet_Backend.Models.TimeSheet", b =>
                {
                    b.HasOne("TimeSheet_Backend.Models.Activity", "Activity")
                        .WithMany("TimeSheets")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeSheet_Backend.Models.Project", "Project")
                        .WithMany("TimeSheets")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeSheet_Backend.Models.User", "User")
                        .WithMany("TimeSheets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeSheet_Backend.Models.Activity", b =>
                {
                    b.Navigation("TimeSheets");
                });

            modelBuilder.Entity("TimeSheet_Backend.Models.Project", b =>
                {
                    b.Navigation("TimeSheets");
                });

            modelBuilder.Entity("TimeSheet_Backend.Models.User", b =>
                {
                    b.Navigation("TimeSheets");
                });
#pragma warning restore 612, 618
        }
    }
}
