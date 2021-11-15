﻿// <auto-generated />
using System;
using HireEmployee.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HireEmployee.Repositories.Migrations
{
    [DbContext(typeof(HireEmployeeDbContext))]
    [Migration("20211111134307_Added Designation coloumn to Candidates")]
    partial class AddedDesignationcoloumntoCandidates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("HireEmployee.Entities.Candidate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Designation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExpectedSalary")
                        .HasColumnType("int");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<bool>("Interview")
                        .HasColumnType("bit");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OfferAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("PhoneScreening")
                        .HasColumnType("bit");

                    b.Property<string>("Resume")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Review")
                        .HasColumnType("bit");

                    b.Property<string>("Skill")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Candidates");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5b1c5587-eddd-4fc0-a6ca-3f83983b14e5"),
                            Age = 21,
                            Designation = "Software Engineer",
                            Email = "shivam@gmail.com",
                            ExpectedSalary = 380000,
                            Experience = 3,
                            Interview = false,
                            JobId = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"),
                            Name = "Shivam Gupta",
                            OfferAccepted = false,
                            PhoneScreening = false,
                            Resume = "ShivamResume.docx",
                            Review = false,
                            Skill = "Dot Net, Python, Django, ReactJs"
                        },
                        new
                        {
                            Id = new Guid("5606e734-8d48-49af-a981-4ad9d862cc8d"),
                            Age = 20,
                            Designation = "Software Engineer",
                            Email = "alista@gmail.com",
                            ExpectedSalary = 300000,
                            Experience = 1,
                            Interview = false,
                            JobId = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"),
                            Name = "Alista Menezes",
                            OfferAccepted = false,
                            PhoneScreening = false,
                            Resume = "AlistaResume.docx",
                            Review = false,
                            Skill = "Python, Angular, NodeJS "
                        },
                        new
                        {
                            Id = new Guid("fc5c071a-89a4-4622-a7ea-7afcb08ed22e"),
                            Age = 35,
                            Designation = "Software Engineer",
                            Email = "rakesh@gmail.com",
                            ExpectedSalary = 410000,
                            Experience = 7,
                            Interview = false,
                            JobId = new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"),
                            Name = "Rakesh Verma",
                            OfferAccepted = false,
                            PhoneScreening = false,
                            Resume = "RakeshResume.docx",
                            Review = false,
                            Skill = "Java, Dot Net, MongoDb"
                        });
                });

            modelBuilder.Entity("HireEmployee.Entities.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Designation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Experience")
                        .HasColumnType("int");

                    b.Property<string>("Salary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jobs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"),
                            Designation = "Software Engineer",
                            Experience = 3,
                            Salary = "30,000 - 40,0000",
                            Skills = "Dot Net, NodeJS, SQL"
                        },
                        new
                        {
                            Id = new Guid("6313179f-7837-473a-a4d5-a5571b43e6a6"),
                            Designation = "Automation Tester",
                            Experience = 1,
                            Salary = "25,000 - 28,0000",
                            Skills = "Dot Net, Django, NodeJS, SQL"
                        });
                });

            modelBuilder.Entity("HireEmployee.Entities.Candidate", b =>
                {
                    b.HasOne("HireEmployee.Entities.Job", "Job")
                        .WithMany("Candidates")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Job");
                });

            modelBuilder.Entity("HireEmployee.Entities.Job", b =>
                {
                    b.Navigation("Candidates");
                });
#pragma warning restore 612, 618
        }
    }
}
