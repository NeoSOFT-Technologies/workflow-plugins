using HireEmployee.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireEmployee.Repositories
{
    public class HireEmployeeDbContext : DbContext
    {
        public HireEmployeeDbContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>().HasData(new Job
            {
                Id = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"),
                Designation = "Software Engineer",
                Skills = "Dot Net, NodeJS, SQL",
                Salary = "30,000 - 40,0000",
                Experience = 3
            });
            modelBuilder.Entity<Job>().HasData(new Job
            {
                Id = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}"),
                Designation = "Automation Tester",
                Skills = "Dot Net, Django, NodeJS, SQL",
                Salary = "25,000 - 28,0000",
                Experience = 1
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = Guid.Parse("{5b1c5587-eddd-4fc0-a6ca-3f83983b14e5}"),
                JobId = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"),
                Designation = "Software Engineer",
                Name = "Shivam Gupta",
                Age = 21,
                Skill = "Dot Net, Python, Django, ReactJs",
                Experience = 3,
                Email = "shivam@gmail.com",
                ExpectedSalary = 380000,
                Resume = "ShivamResume.docx",
                Review = false,
                PhoneScreening = false,
                Interview = false,
                OfferAccepted = false
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = Guid.Parse("{5606e734-8d48-49af-a981-4ad9d862cc8d}"),
                JobId = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"),
                Designation = "Software Engineer",
                Name = "Alista Menezes",
                Age = 20,
                Skill = "Python, Angular, NodeJS ",
                Experience = 1,
                Email = "alista@gmail.com",
                ExpectedSalary = 300000,
                Resume = "AlistaResume.docx",
                Review = false,
                PhoneScreening = false,
                Interview = false,
                OfferAccepted = false
            });

            modelBuilder.Entity<Candidate>().HasData(new Candidate
            {
                Id = Guid.Parse("{fc5c071a-89a4-4622-a7ea-7afcb08ed22e}"),
                JobId = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}"),
                Designation = "Software Engineer",
                Name = "Rakesh Verma",
                Age = 35,
                Skill = "Java, Dot Net, MongoDb",
                Experience = 7,
                Email = "rakesh@gmail.com",
                ExpectedSalary = 410000,
                Resume = "RakeshResume.docx",
                Review = false,
                PhoneScreening = false,
                Interview = false,
                OfferAccepted = false
            });
        }

    }
}
