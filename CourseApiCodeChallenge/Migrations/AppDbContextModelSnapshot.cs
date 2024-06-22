﻿// <auto-generated />
using System;
using CourseApiCodeChallenge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourseApiCodeChallenge.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<Guid>("SectionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VideoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SectionId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.Section", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.WatchLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PercentageWatched")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("LessonId");

                    b.HasIndex("UserId");

                    b.ToTable("WatchLogs");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.Lesson", b =>
                {
                    b.HasOne("CourseApiCodeChallenge.Entities.Section", "Section")
                        .WithMany("Lessons")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Section");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.Section", b =>
                {
                    b.HasOne("CourseApiCodeChallenge.Entities.Course", "Course")
                        .WithMany("Sections")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.WatchLog", b =>
                {
                    b.HasOne("CourseApiCodeChallenge.Entities.Course", "Course")
                        .WithMany("WatchLogs")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseApiCodeChallenge.Entities.Lesson", "Lesson")
                        .WithMany("WatchLogs")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseApiCodeChallenge.Entities.User", "User")
                        .WithMany("WatchLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Lesson");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.Course", b =>
                {
                    b.Navigation("Sections");

                    b.Navigation("WatchLogs");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.Lesson", b =>
                {
                    b.Navigation("WatchLogs");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.Section", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("CourseApiCodeChallenge.Entities.User", b =>
                {
                    b.Navigation("WatchLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
